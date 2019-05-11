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

namespace GestionBares.Controllers
{
    [Authorize]
    public class ReportesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Consumo()
        {
            if (User.IsInRole(DefinicionRoles.Dependiente))
            {
                var dependiente = _context.Set<Dependiente>().SingleOrDefault(d => d.Usuario.UserName == User.Identity.Name);
                var turno = _context.Set<Turno>().SingleOrDefault(t => t.Activo && t.DependienteId == dependiente.Id);
                if (turno == null)
                {
                    return RedirectToAction("Nuevo", "Turnos");
                }
                return RedirectToAction(nameof(ConsumoEnTurno), new { Id = turno.Id });
            }
            return View();
        }
        // GET: ControlesExistencias
        public IActionResult ConsumoEnTurno(int id)
        {
            var turno = _context.Set<Turno>().Include(t => t.Bar).SingleOrDefault(t => t.Id == id);
            var es = new ExistenciasService(_context);
            var data = es.ExistenciaDeBarPorTurno(id);
            ViewBag.Bar = turno.Bar.Nombre;
            ViewBag.Fecha = turno.FechaInicio;
            return View(data);
        }

        public IActionResult Ventas()
        {
            if (User.IsInRole(DefinicionRoles.Dependiente))
            {
                var dependiente = _context.Set<Dependiente>().SingleOrDefault(d => d.Usuario.UserName == User.Identity.Name);
                var turno = _context.Set<Turno>().SingleOrDefault(t => t.Activo && t.DependienteId == dependiente.Id);
                if (turno == null)
                {
                    return RedirectToAction("Nuevo", "Turnos");
                }
                return RedirectToAction(nameof(VentasEnTurno), new { Id = turno.Id });
            }
            return View();
        }
        // GET: ControlesExistencias
        public IActionResult VentasEnTurno(int id)
        {
            var turno = _context.Set<Turno>().Include(t => t.Bar).SingleOrDefault(t => t.Id == id);
            var es = new ExistenciasService(_context);
            var data = es.ExistenciaVentaDeBarPorTurno(id);
            ViewBag.Bar = turno.Bar.Nombre;
            ViewBag.Fecha = turno.FechaInicio;
            return View(data);
        }

        public IActionResult CostosVentasBares()
        {
            ViewBag.Bares = new MultiSelectList(_context.Set<Bar>().ToList(), "Id", "Nombre");
            ViewBag.FormaDePeriodo = new SelectList(Enum.GetValues(typeof(FormaDePeriodo)));
            return View();
        }

        [HttpPost]
        public IActionResult CostosVentasBares(ParametrosVM parametros)
        {
            var existenciaService = new ExistenciasService(_context);
            var turnosEnPeriodo = _context.Set<Turno>().Where(t => t.FechaInicio >= parametros.FechaInicio && t.FechaInicio <= parametros.FechaFin).ToList();
            var datosCosto = new DatosGraficas()
            {
                Labels = turnosEnPeriodo.OrderBy(t => t.FechaInicio).Select(t => t.FechaInicio.ToShortDateString()).ToList(),
            };
            foreach (var barId in parametros.Bares)
            {
                var bar = _context.Set<Bar>().Find(barId);
                var costos = turnosEnPeriodo.Where(t => t.BarId == barId).Select(c => new
                {
                    Fecha = c.FechaInicio,
                    Costos = existenciaService.ExistenciaDeBarPorTurno(c.Id).Sum(e => e.Costo * e.Consumo)
                }).ToList();
                datosCosto.Datasets.Add(new Dataset
                {
                    Label = bar.Nombre,
                    BackgroundColor = "a1b1c1",
                    BorderColor = "a1b1c1",
                    Fill = false,
                    Data = datosCosto.Labels.Select(c => costos.Any(d => d.Fecha.ToShortDateString() == c) ? costos.Where(d => d.Fecha.ToShortDateString() == c).Sum(s => s.Costos) : 0).ToList()
                });
            }
            var datosVentas = new DatosGraficas()
            {
                Labels = turnosEnPeriodo.OrderBy(t => t.FechaInicio).Select(t => t.FechaInicio.ToShortDateString()).ToList(),
            };
            foreach (var barId in parametros.Bares)
            {
                var bar = _context.Set<Bar>().Find(barId);
                var ventas = turnosEnPeriodo.Where(t => t.BarId == barId).Select(c => new
                {
                    Fecha = c.FechaInicio,
                    Ventas = existenciaService.ExistenciaVentaDeBarPorTurno(c.Id).Sum(e => e.Consumo * e.Precio)
                }).ToList();
                datosVentas.Datasets.Add(new Dataset
                {
                    Label = bar.Nombre,
                    BackgroundColor = "#d3b577",
                    BorderColor = "#d3b577",
                    Fill = false,
                    Data = datosVentas.Labels.Select(c => ventas.Any(d => d.Fecha.ToShortDateString() == c) ? ventas.Where(d => d.Fecha.ToShortDateString() == c).Sum(s => s.Ventas) : 0).ToList()
                });
            }
            ViewBag.Costos = datosCosto;
            ViewBag.Ventas = datosVentas;
            ViewBag.Bares = new MultiSelectList(_context.Set<Bar>().ToList(), "Id", "Nombre", parametros.Bares);
            ViewBag.FormaDePeriodo = new SelectList(Enum.GetValues(typeof(FormaDePeriodo)), parametros.FormaDePeriodo);
            return View(parametros);
        }

        public IActionResult CostosVentasDependientes()
        {
            ViewBag.Dependientes = new MultiSelectList(_context.Set<Dependiente>().ToList(), "Id", "NombreCompleto");
            ViewBag.FormaDePeriodo = new SelectList(Enum.GetValues(typeof(FormaDePeriodo)));
            return View();
        }

        [HttpPost]
        public IActionResult CostosVentasDependientes(ParametrosVM parametros)
        {
            var existenciaService = new ExistenciasService(_context);
            var turnosEnPeriodo = _context.Set<Turno>().Where(t => t.FechaInicio >= parametros.FechaInicio && t.FechaInicio <= parametros.FechaFin).ToList();
            var datosCosto = new DatosGraficas()
            {
                Labels = turnosEnPeriodo.OrderBy(t => t.FechaInicio).Select(t => t.FechaInicio.ToShortDateString()).ToList(),
            };
            foreach (var dependienteId in parametros.Dependientes)
            {
                var dependiente = _context.Set<Dependiente>().Find(dependienteId);
                var costos = turnosEnPeriodo.Where(t => t.BarId == dependienteId).Select(c => new
                {
                    Fecha = c.FechaInicio,
                    Costo = existenciaService.ExistenciaDeBarPorTurno(c.Id).Sum(e => e.Consumo * e.Costo)
                }).ToList();
                datosCosto.Datasets.Add(new Dataset
                {
                    Label = dependiente.NombreCompleto,
                    BackgroundColor = "#a1b1c1",
                    BorderColor = "#a1b1c1",
                    Fill = false,
                    Data = datosCosto.Labels.Select(c => costos.Any(d => d.Fecha.ToShortDateString() == c) ? costos.Where(d => d.Fecha.ToShortDateString() == c).Sum(s => s.Costo) : 0).ToList()
                });
            }
            var datosVentas = new DatosGraficas()
            {
                Labels = turnosEnPeriodo.OrderBy(t => t.FechaInicio).Select(t => t.FechaInicio.ToShortDateString()).ToList(),
            };
            foreach (var dependienteId in parametros.Dependientes)
            {
                var dependiente = _context.Set<Dependiente>().Find(dependienteId);
                var ventas = turnosEnPeriodo.Where(t => t.BarId == dependienteId).Select(c => new
                {
                    Fecha = c.FechaInicio,
                    Ventas = existenciaService.ExistenciaVentaDeBarPorTurno(c.Id).Sum(e => e.Consumo * e.Precio)
                }).ToList();
                datosVentas.Datasets.Add(new Dataset
                {
                    Label = dependiente.NombreCompleto,
                    BackgroundColor = "#d3b577",
                    BorderColor = "#d3b577",
                    Fill = false,
                    Data = datosVentas.Labels.Select(c => ventas.Any(d => d.Fecha.ToShortDateString() == c) ? ventas.Where(d => d.Fecha.ToShortDateString() == c).Sum(s => s.Ventas) : 0).ToList()
                });
            }
            ViewBag.Costos = datosCosto;
            ViewBag.Ventas = datosVentas;
            ViewBag.Dependientes = new MultiSelectList(_context.Set<Dependiente>().ToList(), "Id", "NombreCompleto", parametros.Dependientes);
            ViewBag.FormaDePeriodo = new SelectList(Enum.GetValues(typeof(FormaDePeriodo)), parametros.FormaDePeriodo);
            return View(parametros);
        }
    }
}
