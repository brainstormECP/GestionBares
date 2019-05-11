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
            var colors = GetColor();
            var turnosEnPeriodo = _context.Set<Turno>().Where(t => t.FechaInicio >= parametros.FechaInicio && t.FechaInicio <= parametros.FechaFin).ToList();
            var datosCosto = new DatosGraficas()
            {
                Labels = turnosEnPeriodo.OrderBy(t => t.FechaInicio).Select(t => t.FechaInicio.ToShortDateString()).ToList(),
            };
            var index = 0;
            foreach (var barId in parametros.Bares)
            {
                var bar = _context.Set<Bar>().Find(barId);
                var costos = turnosEnPeriodo.Where(t => t.BarId == barId).Select(c => new
                {
                    Fecha = c.FechaInicio,
                    Costos = existenciaService.ExistenciaDeBarPorTurno(c.Id).Sum(e => (double)e.Costo * e.Consumo)
                }).ToList();
                datosCosto.Datasets.Add(new Dataset
                {
                    Label = bar.Nombre,
                    BackgroundColor = colors[index],
                    BorderColor = colors[index],
                    Fill = false,
                    Data = datosCosto.Labels.Select(c => costos.Any(d => d.Fecha.ToShortDateString() == c) ? costos.Where(d => d.Fecha.ToShortDateString() == c).Sum(s => s.Costos) : 0).ToList()
                });
                index++;
            }
            var datosVentas = new DatosGraficas()
            {
                Labels = turnosEnPeriodo.OrderBy(t => t.FechaInicio).Select(t => t.FechaInicio.ToShortDateString()).ToList(),
            };
            index = 0;
            foreach (var barId in parametros.Bares)
            {
                var bar = _context.Set<Bar>().Find(barId);
                var ventas = turnosEnPeriodo.Where(t => t.BarId == barId).Select(c => new
                {
                    Fecha = c.FechaInicio,
                    Ventas = existenciaService.ExistenciaVentaDeBarPorTurno(c.Id).Sum(e => e.Consumo * (double)e.Precio)
                }).ToList();
                datosVentas.Datasets.Add(new Dataset
                {
                    Label = bar.Nombre,
                    BackgroundColor = colors[index],
                    BorderColor = colors[index],
                    Fill = false,
                    Data = datosVentas.Labels.Select(c => ventas.Any(d => d.Fecha.ToShortDateString() == c) ? ventas.Where(d => d.Fecha.ToShortDateString() == c).Sum(s => s.Ventas) : 0).ToList()
                });
                index++;
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
            var colors = GetColor();
            var turnosEnPeriodo = _context.Set<Turno>().Where(t => t.FechaInicio >= parametros.FechaInicio && t.FechaInicio <= parametros.FechaFin).ToList();
            var datosCosto = new DatosGraficas()
            {
                Labels = turnosEnPeriodo.OrderBy(t => t.FechaInicio).Select(t => t.FechaInicio.ToShortDateString()).ToList(),
            };
            var index = 0;
            foreach (var dependienteId in parametros.Dependientes)
            {
                var dependiente = _context.Set<Dependiente>().Find(dependienteId);
                var costos = turnosEnPeriodo.Where(t => t.BarId == dependienteId).Select(c => new
                {
                    Fecha = c.FechaInicio,
                    Costo = existenciaService.ExistenciaDeBarPorTurno(c.Id).Sum(e => e.Consumo * (double)e.Costo)
                }).ToList();
                datosCosto.Datasets.Add(new Dataset
                {
                    Label = dependiente.NombreCompleto,
                    BackgroundColor = colors[index],
                    BorderColor = colors[index],
                    Fill = false,
                    Data = datosCosto.Labels.Select(c => costos.Any(d => d.Fecha.ToShortDateString() == c) ? costos.Where(d => d.Fecha.ToShortDateString() == c).Sum(s => s.Costo) : 0).ToList()
                });
                index++;
            }
            var datosVentas = new DatosGraficas()
            {
                Labels = turnosEnPeriodo.OrderBy(t => t.FechaInicio).Select(t => t.FechaInicio.ToShortDateString()).ToList(),
            };
            index = 0;
            foreach (var dependienteId in parametros.Dependientes)
            {
                var dependiente = _context.Set<Dependiente>().Find(dependienteId);
                var ventas = turnosEnPeriodo.Where(t => t.BarId == dependienteId).Select(c => new
                {
                    Fecha = c.FechaInicio,
                    Ventas = existenciaService.ExistenciaVentaDeBarPorTurno(c.Id).Sum(e => e.Consumo * (double)e.Precio)
                }).ToList();
                datosVentas.Datasets.Add(new Dataset
                {
                    Label = dependiente.NombreCompleto,
                    BackgroundColor = colors[index],
                    BorderColor = colors[index],
                    Fill = false,
                    Data = datosVentas.Labels.Select(c => ventas.Any(d => d.Fecha.ToShortDateString() == c) ? ventas.Where(d => d.Fecha.ToShortDateString() == c).Sum(s => s.Ventas) : 0).ToList()
                });
                index++;
            }
            ViewBag.Costos = datosCosto;
            ViewBag.Ventas = datosVentas;
            ViewBag.Dependientes = new MultiSelectList(_context.Set<Dependiente>().ToList(), "Id", "NombreCompleto", parametros.Dependientes);
            ViewBag.FormaDePeriodo = new SelectList(Enum.GetValues(typeof(FormaDePeriodo)), parametros.FormaDePeriodo);
            return View(parametros);
        }

        public IActionResult MovimientosDeProducto()
        {
            ViewBag.ProductoId = new SelectList(_context.Set<Producto>().ToList(), "Id", "Nombre");
            return View(new List<MovimientoDeProductoVM>());
        }

        [HttpPost]
        public IActionResult MovimientosDeProducto(ParametrosVM parametros)
        {
            var existenciaService = new ExistenciasService(_context);
            var turnosEnPeriodo = _context.Set<Turno>().Where(t => t.FechaInicio >= parametros.FechaInicio && t.FechaInicio <= parametros.FechaFin).ToList();
            var result = new List<MovimientoDeProductoVM>();
            //controles de existencia
            result.AddRange(_context.Set<DetalleControlExistencia>()
                .Include(c => c.Control.Turno.Bar)
                .Where(c => c.ProductoId == parametros.ProductoId && turnosEnPeriodo.Any(t => t.Id == c.Control.TurnoId))
                .Select(c => new MovimientoDeProductoVM
                {
                    Bar = c.Control.Turno.Bar.Nombre,
                    Fecha = c.Control.Fecha,
                    Cantidad = (decimal)c.Cantidad,
                    TipoDeMovimiento = "Control de existencia"
                }));
            //controles de existencias de ventas
            result.AddRange(_context.Set<DetalleControlExistenciaVenta>()
            .Include(c => c.Control.Turno.Bar)
            .Where(c => c.ProductoId == parametros.ProductoId && turnosEnPeriodo.Any(t => t.Id == c.Control.TurnoId))
            .Select(c => new MovimientoDeProductoVM
            {
                Bar = c.Control.Turno.Bar.Nombre,
                Fecha = c.Control.Fecha,
                Cantidad = (decimal)c.Cantidad,
                TipoDeMovimiento = "Control de existencia para venta"
            }));
            //entradas de almacen
            result.AddRange(_context.Set<EntregaDeAlmacen>()
                .Include(c => c.Turno.Bar)
                .Where(c => c.ProductoId == parametros.ProductoId && turnosEnPeriodo.Any(t => t.Id == c.TurnoId))
                .Select(c => new MovimientoDeProductoVM
                {
                    Bar = c.Turno.Bar.Nombre,
                    Fecha = c.Turno.FechaInicio,
                    Cantidad = (decimal)c.Cantidad,
                    TipoDeMovimiento = "Entrada de almacen"
                }));
            //entradas de almacen de venta
            result.AddRange(_context.Set<EntregaDeAlmacenVenta>()
                .Include(c => c.Turno.Bar)
                .Where(c => c.ProductoId == parametros.ProductoId && turnosEnPeriodo.Any(t => t.Id == c.TurnoId))
                .Select(c => new MovimientoDeProductoVM
                {
                    Bar = c.Turno.Bar.Nombre,
                    Fecha = c.Turno.FechaInicio,
                    Cantidad = (decimal)c.Cantidad,
                    TipoDeMovimiento = "Entrada de almacen para venta"
                }));
            // traslados
            result.AddRange(_context.Set<Traslado>()
            .Include(c => c.Turno.Bar)
            .Where(c => c.ProductoId == parametros.ProductoId && turnosEnPeriodo.Any(t => t.Id == c.TurnoId))
            .Select(c => new MovimientoDeProductoVM
            {
                Bar = c.Turno.Bar.Nombre,
                Fecha = c.Fecha,
                Cantidad = (decimal)c.Cantidad,
                TipoDeMovimiento = "Traslado"
            }));
            // traslados de venta
            result.AddRange(_context.Set<TrasladoVenta>()
            .Include(c => c.Turno.Bar)
            .Where(c => c.ProductoId == parametros.ProductoId && turnosEnPeriodo.Any(t => t.Id == c.TurnoId))
            .Select(c => new MovimientoDeProductoVM
            {
                Bar = c.Turno.Bar.Nombre,
                Fecha = c.Fecha,
                Cantidad = (decimal)c.Cantidad,
                TipoDeMovimiento = "Traslado para venta"
            }));
            ViewBag.ProductoId = new SelectList(_context.Set<Producto>().ToList(), "Id", "Nombre", parametros.ProductoId);
            return View(result);
        }

        #region Helpers
        private List<string> GetColor()
        {
            const int DELTA_PERCENT = 10;
            List<Color> alreadyChoosenColors = new List<Color>();

            // initialize the random generator
            Random r = new Random();

            for (int i = 0; i < 30; i++)
            {
                bool chooseAnotherColor = true;
                Color tmpColor = Color.FromArgb(0, 0, 0);
                while (chooseAnotherColor)
                {
                    // create a random color by generating three random channels
                    //
                    int redColor = r.Next(0, 255);
                    int greenColor = r.Next(0, 255);
                    int blueColor = r.Next(0, 255);
                    tmpColor = Color.FromArgb(redColor, greenColor, blueColor);

                    // check if a similar color has already been created
                    //
                    chooseAnotherColor = false;
                    foreach (Color c in alreadyChoosenColors)
                    {
                        int delta = c.R * DELTA_PERCENT / 100;
                        if (c.R - delta <= tmpColor.R && tmpColor.R <= c.R + delta)
                        {
                            chooseAnotherColor = true;
                            break;
                        }

                        delta = c.G * DELTA_PERCENT / 100;
                        if (c.G - delta <= tmpColor.G && tmpColor.G <= c.G + delta)
                        {
                            chooseAnotherColor = true;
                            break;
                        }

                        delta = c.B * DELTA_PERCENT / 100;
                        if (c.B - delta <= tmpColor.B && tmpColor.B <= c.B + delta)
                        {
                            chooseAnotherColor = true;
                            break;
                        }
                    }
                }

                alreadyChoosenColors.Add(tmpColor);
                // you can safely use the tmpColor here

            }
            var pallete = new List<Color>();
            return alreadyChoosenColors.Select(c => HexConverter(c)).ToList();
        }

        private String HexConverter(Color color)
        {
            return "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
        }
        #endregion
    }
}
