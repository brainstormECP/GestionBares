using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionBares.Data;
using GestionBares.Models;
using Microsoft.AspNetCore.Authorization;
using GestionBares.Utils;
using GestionBares.ViewModels;
using System.Drawing;

namespace GestionBares.Controllers
{
    [Authorize]
    public class AsignacionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AsignacionController(ApplicationDbContext context)
        {
            _context = context;
        }

        //asigancion
        public IActionResult Asignacion()
        {
            ViewData["Dependientes"] = _context.Dependientes.OrderBy(c => c.Id).ToList();
            var checkedDependientes = _context.Dependientes.ToList().Select(d => new CheckedDependientes() { Checked = true, Dependiente = d, DependienteId = d.Id }).ToList();
            ViewData["CheckedDependiente"] = checkedDependientes;
            var checkedTurnos = _context.Bares.ToList().Select(b => new CheckedTurno() { Checked = true, Bar = b, BarId = b.Id }).ToList();
            ViewData["CheckedBares"] = checkedTurnos;
            ViewData["Bares"] = _context.Bares.OrderBy(c => c.Id).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Asignacion([Bind("CheckedDependientes, CheckedTurnos, FechaInicio, FechaFinal")] SeleccionarAsignacionVM seleccionarAsignacionVM)
        {

            var Dependientes = _context.Dependientes.Where(d => seleccionarAsignacionVM.CheckedDependientes.Any(c => c.DependienteId == d.Id && c.Checked == true)).OrderBy(d => d.Id).ToList();

            var Bares = _context.Bares.Where(b => seleccionarAsignacionVM.CheckedTurnos.Any(c => c.BarId == b.Id && c.Checked == true)).OrderBy(b => b.Id).ToList();

            var ventasDependientesPorBar = BuscarVentas(seleccionarAsignacionVM.CheckedDependientes.Where(d => d.Checked).Select(d => d.DependienteId).ToList(), seleccionarAsignacionVM.CheckedTurnos.Where(d => d.Checked).Select(d => d.BarId).ToList(), seleccionarAsignacionVM.FechaInicio, seleccionarAsignacionVM.FechaFinal);

            if (ventasDependientesPorBar.Any())
            {
                //Convertinedo en un array de 2 dimensiones
                double[,] Tabla = new double[Dependientes.Count, Bares.Count];

                for (int f = 0; f < Dependientes.Count; f++)
                {
                    var dependienteId = Dependientes[f].Id;
                    var ventas = ventasDependientesPorBar
                        .Any(v => v.DependienteId == dependienteId) ?
                        ventasDependientesPorBar.SingleOrDefault(v => v.DependienteId == dependienteId).VentasPorBares : new List<VentasPorBar>();
                    for (int c = 0; c < Bares.Count; c++)
                    {
                        var barId = Bares[c].Id;
                        Tabla[f, c] = ventas.Any(v => v.BarId == barId) ? ventas.SingleOrDefault(v => v.BarId == barId).PromedioVentasPorTurno : 0;
                    }
                }
                //Balancenado el Problema
                if (Tabla.GetLength(0) > Tabla.GetLength(1))
                {
                    int Diferencia = Tabla.GetLength(0) - Tabla.GetLength(1);
                    double[,] NuevaTabla = new double[Tabla.GetLength(0), Tabla.GetLength(0)];

                    for (int f = 0; f < Tabla.GetLength(0); f++)
                    {
                        for (int c = 0; c < Tabla.GetLength(1); c++)
                        {
                            NuevaTabla[f, c] = Tabla[f, c];
                        }
                    }
                    Tabla = NuevaTabla;
                    //Agregando Bares ficticios
                    for (int x = 0; x < Diferencia; x++)
                    {
                        Bares.Add(new Bar() { Nombre = "Ficticio010101" });
                    }
                }

                int[] AsginacionOptima = HungarianAlgorithm.FindAssignments(Tabla, true);
                List<string> Asignaciones = new List<string>();
                for (int x = 0; x < AsginacionOptima.Count(); x++)
                {
                    if (!(Bares[AsginacionOptima[x]].Nombre == "Ficticio010101"))
                        Asignaciones.Add(Dependientes[x].NombreCompleto + " --> " + Bares[AsginacionOptima[x]].Nombre);
                }
                ViewData["AsignacionOptima"] = Asignaciones;
            }
            else
                ViewData["AsignacionOptima"] = new List<string>() { "No existe Turnos para esta fecha" };

            ViewData["CrearTablaRazon"] = ventasDependientesPorBar;

            ViewData["Dependientes"] = _context.Dependientes.OrderBy(c => c.Id).ToList();
            var checkedDependientes = _context.Dependientes.ToList().Select(d => new CheckedDependientes() { Checked = true, Dependiente = d, DependienteId = d.Id }).ToList();
            ViewData["CheckedDependiente"] = checkedDependientes;
            var checkedTurnos = _context.Bares.ToList().Select(b => new CheckedTurno() { Checked = true, Bar = b, BarId = b.Id }).ToList();
            ViewData["CheckedBares"] = checkedTurnos;
            ViewData["Bares"] = _context.Bares.OrderBy(c => c.Id).ToList();
            return View("Asignacion", ventasDependientesPorBar);

        }

        #region Helpers
        private List<VentasDependientesPorBar> BuscarVentas(List<int> dependientesId, List<int> baresId, DateTime fechaInicio, DateTime fechaFinal)
        {
            var existenciaService = new ExistenciasService(_context);
            var turnosEnPeriodo = _context.Turnos
                .Include(c => c.Bar)
                .Where(c => dependientesId.Any(d => d == c.DependienteId) &&
                    baresId.Any(b => b == c.BarId) &&
                    c.FechaInicio >= fechaInicio &&
                    c.FechaFin <= fechaFinal)
                .ToList();
            if (turnosEnPeriodo == null) turnosEnPeriodo = new List<Turno>();
            var ventas = turnosEnPeriodo.Select(c => new
            {
                Fecha = c.FechaInicio,
                TurnoId = c.Id,
                BarId = c.BarId,
                DependienteId = c.DependienteId,
                Ventas = existenciaService.ExistenciaVentaDeBarPorTurno(c.Id).Sum(e => e.Consumo * (double)e.Precio),
            }).ToList();
            var bares = _context.Set<Bar>().Select(b => new { Id = b.Id, Nombre = b.Nombre }).ToList();
            var dependientes = _context.Set<Dependiente>().Select(b => new { Id = b.Id, Nombre = b.NombreCompleto }).ToList();
            var ventasDependientesPorBar = ventas.GroupBy(v => v.DependienteId).Select(v => new VentasDependientesPorBar
            {
                DependienteId = v.Key,
                DependienteNombre = dependientes.FirstOrDefault(d => d.Id == v.Key).Nombre,
                VentasPorBares = v.GroupBy(b => b.BarId).Select(b => new VentasPorBar
                {
                    BarId = b.Key,
                    BarNombre = bares.FirstOrDefault(s => s.Id == b.Key).Nombre,
                    PromedioVentasPorTurno = b.Sum(p => p.Ventas) / b.Count(),
                }).ToList(),
            }).ToList();
            return ventasDependientesPorBar;
        }
        #endregion
    }
}
