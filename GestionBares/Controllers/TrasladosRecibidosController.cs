using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionBares.Data;
using GestionBares.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using GestionBares.Utils;

namespace GestionBares.Controllers
{
    [Authorize(Roles = DefinicionRoles.Dependiente)]
    public class TrasladosRecibidosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrasladosRecibidosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Traslados
        public IActionResult Index()
        {
            var traslados = _context.Traslados
                .Include(t => t.Destino)
                .Include(t => t.Turno.Bar)
                .Include(t => t.Turno.Dependiente)
                .Include(t => t.Producto)
                .ToList();
            if (User.IsInRole(DefinicionRoles.Dependiente))
            {
                var dependiente = _context.Set<Dependiente>().SingleOrDefault(u => u.Usuario.UserName == User.Identity.Name);
                if (!_context.Set<Turno>().Any(t => t.DependienteId == dependiente.Id && t.Activo))
                {
                    TempData["info"] = "Usted no tiene turno abierto.";
                    return RedirectToAction("Nuevo", "Turno");
                }
                var turno = _context.Set<Turno>().SingleOrDefault(t => t.DependienteId == dependiente.Id && t.Activo);
                traslados = traslados.Where(t => t.DestinoId == turno.BarId && t.Fecha >= turno.FechaInicio).ToList();
            }
            return View(traslados);
        }

        public IActionResult DeVentas()
        {
            var traslados = _context.TrasladosVenta
                .Include(t => t.Destino)
                .Include(t => t.Turno.Bar)
                .Include(t => t.Turno.Dependiente)
                .Include(t => t.Producto)
                .ToList();
            if (User.IsInRole(DefinicionRoles.Dependiente))
            {
                var dependiente = _context.Set<Dependiente>().SingleOrDefault(u => u.Usuario.UserName == User.Identity.Name);
                if (!_context.Set<Turno>().Any(t => t.DependienteId == dependiente.Id && t.Activo))
                {
                    TempData["info"] = "Usted no tiene turno abierto.";
                    return RedirectToAction("Nuevo", "Turno");
                }
                var turno = _context.Set<Turno>().SingleOrDefault(t => t.DependienteId == dependiente.Id && t.Activo);
                traslados = traslados.Where(t => t.DestinoId == turno.BarId && t.Fecha >= turno.FechaInicio).ToList();
            }
            return View(traslados);
        }

        public IActionResult Recibir(int id)
        {
            var traslado = _context.Traslados.Find(id);
            traslado.Aprobado = true;
            try
            {
                _context.SaveChanges();
                TempData["exito"] = "La acción se ha realizado correctamente";
            }
            catch (Exception)
            {
                TempData["error"] = "Error en ralizar esta acción";
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult RecibirDeVenta(int id)
        {
            var traslado = _context.TrasladosVenta.Find(id);
            traslado.Aprobado = true;
            try
            {
                _context.SaveChanges();
                TempData["exito"] = "La acción se ha realizado correctamente";
            }
            catch (Exception)
            {
                TempData["error"] = "Error en ralizar esta acción";
                throw;
            }
            return RedirectToAction(nameof(DeVentas));
        }

        public IActionResult Eliminar(int id)
        {
            var traslado = _context.Traslados.Find(id);
            _context.Traslados.Remove(traslado);
            try
            {
                _context.SaveChanges();
                TempData["exito"] = "La acción se ha realizado correctamente";
            }
            catch (Exception)
            {
                TempData["error"] = "Error en ralizar esta acción";
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult EliminarDeVenta(int id)
        {
            var traslado = _context.TrasladosVenta.Find(id);
            _context.TrasladosVenta.Remove(traslado);
            try
            {
                _context.SaveChanges();
                TempData["exito"] = "La acción se ha realizado correctamente";
            }
            catch (Exception)
            {
                TempData["error"] = "Error en ralizar esta acción";
                throw;
            }
            return RedirectToAction(nameof(DeVentas));
        }
    }
}
