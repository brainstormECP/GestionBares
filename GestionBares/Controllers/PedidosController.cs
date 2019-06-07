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
    public class PedidosController : Controller
    {
        private readonly DependienteDbContext _context;

        public PedidosController(DependienteDbContext context)
        {
            _context = context;
        }

        // GET: Traslados
        public IActionResult Index()
        {
            var pedidos = _context.Set<PedidoAlmacen>()
                .Include(t => t.Turno.Bar)
                .Include(t => t.Turno.Dependiente)
                .Include(t => t.Detalles).ThenInclude(d => d.Producto)
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
                pedidos = pedidos.Where(t => t.TurnoId == turno.Id).ToList();
            }
            return View(pedidos);
        }

        // GET: Traslados/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var traslado = _context.PedidosDeAlmacen
                        .Include(t => t.Turno.Bar)
                        .Include(t => t.Turno.Dependiente)
                        .Include(t => t.Detalles).ThenInclude(d => d.Producto)
            .FirstOrDefault(m => m.Id == id);
            if (traslado == null)
            {
                return NotFound();
            }

            return View(traslado);
        }

        // GET: Traslados/Create
        public IActionResult Nuevo()
        {
            var dependiente = _context.Set<Dependiente>().SingleOrDefault(u => u.Usuario.UserName == User.Identity.Name);
            if (!_context.Set<Turno>().Any(t => t.DependienteId == dependiente.Id && t.Activo))
            {
                TempData["info"] = "Usted no tiene turno abierto.";
                return RedirectToAction("Nuevo", "Turno");
            }
            var turno = _context.Set<Turno>().SingleOrDefault(t => t.DependienteId == dependiente.Id && t.Activo);
            ViewBag.TurnoId = turno.Id;
            var pedido = new PedidoAlmacen { TurnoId = turno.Id };
            _context.Add(pedido);
            _context.SaveChanges();
            return RedirectToAction(nameof(Edit), new { Id = pedido.Id });
        }

        // GET: Traslados/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = _context.PedidosDeAlmacen
                .Include(t => t.Turno.Bar)
                .Include(t => t.Turno.Dependiente)
                .Include(t => t.Detalles).ThenInclude(t => t.Producto)
                .FirstOrDefault(t => t.Id == id);
            if (venta == null)
            {
                return NotFound();
            }
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre");
            return View(venta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AgregarDetalle([Bind("PedidoId,ProductoId,Cantidad")] DetallePedidoAlmacen detalle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detalle);
                _context.SaveChanges();
                TempData["exito"] = "La acción se ha realizado correctamente";
            }
            return RedirectToAction(nameof(Edit), new { Id = detalle.PedidoId });
        }

        public IActionResult EliminarDetalle(int id)
        {
            var detalle = _context.DetallesPedidosDeAlmacen.Find(id);
            var pedidoId = detalle.PedidoId;
            _context.Remove(detalle);
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
            return RedirectToAction(nameof(Edit), new { Id = pedidoId });
        }

        public IActionResult Eliminar(int id)
        {
            var pedido = _context.PedidosDeAlmacen.Find(id);
            _context.PedidosDeAlmacen.Remove(pedido);
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

        public IActionResult Sugerencias()
        {
            var dependiente = _context.Set<Dependiente>().SingleOrDefault(u => u.Usuario.UserName == User.Identity.Name);
            if (!_context.Set<Turno>().Any(t => t.DependienteId == dependiente.Id && t.Activo))
            {
                TempData["info"] = "Usted no tiene turno abierto.";
                return RedirectToAction("Nuevo", "Turno");
            }
            var turno = _context.Set<Turno>().SingleOrDefault(t => t.DependienteId == dependiente.Id && t.Activo);
            ViewBag.TurnoId = turno.Id;

            return View();
        }
    }
}
