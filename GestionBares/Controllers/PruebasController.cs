using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GestionBares.Utils;
using GestionBares.Data;
using GestionBares.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using GestionBares.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Drawing;

namespace GestionBares.Controllers
{
    public class PruebasController : Controller
    {


        private readonly ApplicationDbContext _context;

        public PruebasController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult GenerarTurnosAleatorio()
        {
            var bares = _context.Bares.ToList();

            ViewBag.Bares = new MultiSelectList(_context.Set<Bar>().ToList(), "Id", "Nombre");
            ViewBag.Dependientes = new MultiSelectList(_context.Set<Dependiente>().ToList(), "Id", "Nombres");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GenerarTurnosAleatorio([Bind("Dependientes, CantidadDeTurnos, Bares, FechaInicio, FechaFin, ConsumoMinimo, ConsumoMaximo, VentasMinimo, VentasMaximo")] Pruebas pruebas)
        {
            int numeroEntradas = pruebas.CantidadDeTurnos;
            List<Bar> ListaBares = new List<Bar>();
            foreach (var item in pruebas.Bares)
                ListaBares.Add(_context.Bares.SingleOrDefault(c => c.Id == item));

            List<Dependiente> ListaDependientes = new List<Dependiente>();
            foreach (var item in pruebas.Dependientes)
                ListaDependientes.Add(_context.Dependientes.SingleOrDefault(c => c.Id == item));

            var ListaProductos = _context.Productos.ToList();

            Random random = new Random();

            for (int x = 1; x <= numeroEntradas; x++)
            {
                DateTime FechaInicio = new DateTime(
                    random.Next(pruebas.FechaInicio.Year, pruebas.FechaFin.Year),
                    random.Next(pruebas.FechaInicio.Month, pruebas.FechaFin.Month),
                    random.Next(pruebas.FechaInicio.Day, pruebas.FechaFin.Day),
                    random.Next(6, 10),
                    random.Next(59),
                    random.Next(59));

                DateTime FechaFin = new DateTime(
                    FechaInicio.Year,
                    FechaInicio.Month,
                    FechaInicio.Day,
                    random.Next(19, 23),
                    random.Next(59),
                    random.Next(59));

                Turno t = new Turno() { BarId = ListaBares[random.Next(ListaBares.Count)].Id, DependienteId = ListaDependientes[random.Next(ListaDependientes.Count)].Id, FechaInicio = FechaInicio, FechaFin = FechaFin, Activo = false };
                _context.Turnos.Add(t);

                ControlExistencia c = new ControlExistencia() { TurnoId = t.Id };
                ControlExistenciaVenta cV = new ControlExistenciaVenta() { TurnoId = t.Id };
                _context.ControlesDeExistencias.Add(c);
                _context.ControlesDeExistenciasVenta.Add(cV);

                foreach (var item in ListaProductos)
                {
                    DetalleControlExistencia dc = new DetalleControlExistencia() { ControlId = c.Id, ProductoId = item.Id, Cantidad = pruebas.CantidadMinima + (pruebas.CantidadMaxima - pruebas.CantidadMinima) * random.NextDouble() };
                    _context.DetallesControlesDeExistencias.Add(dc);

                    DetalleControlExistenciaVenta dcV = new DetalleControlExistenciaVenta() { ControlId = cV.Id, ProductoId = item.Id, Cantidad = pruebas.VentaMinima + (pruebas.VentaMaxima - pruebas.VentaMinima) * random.NextDouble() };
                    _context.DetallesControlesDeExistenciasVenta.Add(dcV);
                }

            }

            TempData["exito"] = "La acción se ha realizado correctamente";
            _context.SaveChanges();
            return Redirect(nameof(GenerarTurnosAleatorio));

        }
    }
}