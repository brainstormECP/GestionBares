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
    public class TrasladosVentasController : Controller
    {
        private readonly DependienteDbContext _context;

        public TrasladosVentasController(DependienteDbContext context)
        {
            _context = context;
        }

        // GET: Traslados
        public IActionResult Index()
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
                traslados = traslados.Where(t => t.TurnoId == turno.Id).ToList();
            }
            return View(traslados);
        }

        // GET: Traslados/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var traslado = _context.TrasladosVenta
                        .Include(t => t.Destino)
                        .Include(t => t.Turno)
                        .Include(t => t.Producto)
            .FirstOrDefault(m => m.Id == id);
            if (traslado == null)
            {
                return NotFound();
            }

            return View(traslado);
        }

        // GET: Traslados/Create
        public IActionResult Create()
        {
            var dependiente = _context.Set<Dependiente>().SingleOrDefault(u => u.Usuario.UserName == User.Identity.Name);
            if (!_context.Set<Turno>().Any(t => t.DependienteId == dependiente.Id && t.Activo))
            {
                TempData["info"] = "Usted no tiene turno abierto.";
                return RedirectToAction("Nuevo", "Turno");
            }
            var turno = _context.Set<Turno>().SingleOrDefault(t => t.DependienteId == dependiente.Id && t.Activo);
            ViewBag.TurnoId = turno.Id;
            var baresDestino = _context.Set<Turno>().Include(t => t.Bar).Where(b => b.Activo && b.BarId != turno.BarId).Select(c => c.Bar);
            ViewData["DestinoId"] = new SelectList(baresDestino, "Id", "Nombre");
            var productos = _context.Set<Producto>()
                .Include(p => p.Unidad)
                .Where(p => _context.Set<StandardVenta>().Any(s => s.ProductoId == p.Id && s.BarId == turno.BarId))
                .Select(p => new { Id = p.Id, Nombre = p.Nombre + " (" + p.Unidad.Nombre + ")" });
            ViewData["ProductoId"] = new SelectList(productos, "Id", "Nombre");
            return View();
        }

        // POST: Traslados/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,ProductoId,Cantidad,DestinoId,TurnoId")] TrasladoVenta traslado)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Set<IdentityUser>().SingleOrDefault(u => u.UserName == User.Identity.Name);
                if (user == null)
                {
                    TempData["error"] = "No estas autenticado";
                    return RedirectToAction(nameof(Index));
                }
                traslado.UsuarioId = user.Id;
                traslado.Fecha = DateTime.Now;
                _context.Add(traslado);
                _context.SaveChanges();
                TempData["exito"] = "La acción se ha realizado correctamente";
                return RedirectToAction(nameof(Index));
            }
            ViewData["DestinoId"] = new SelectList(_context.Bares, "Id", "Nombre", traslado.DestinoId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre", traslado.ProductoId);
            ViewData["TurnoId"] = traslado.TurnoId;
            TempData["error"] = "Error en ralizar esta acción";
            return View(traslado);
        }

        // GET: Traslados/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var traslado = _context.TrasladosVenta
                .Include(t => t.Turno.Bar)
                .Include(t => t.Turno.Dependiente)
                .FirstOrDefault(t => t.Id == id);
            if (traslado == null)
            {
                return NotFound();
            }
            ViewData["DestinoId"] = new SelectList(_context.Bares, "Id", "Nombre", traslado.DestinoId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre", traslado.ProductoId);
            ViewData["TurnoId"] = traslado.TurnoId;
            return View(traslado);
        }

        // POST: Traslados/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,ProductoId,Cantidad,DestinoId,TurnoId,UsuarioId,Fecha")] TrasladoVenta traslado)
        {
            if (id != traslado.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(traslado);
                    TempData["exito"] = "La acción se ha realizado correctamente";
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrasladoExists(traslado.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DestinoId"] = new SelectList(_context.Bares, "Id", "Nombre", traslado.DestinoId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre", traslado.ProductoId);
            ViewData["TurnoId"] = traslado.TurnoId;
            TempData["error"] = "Error en ralizar esta acción";
            return View(traslado);
        }

        public IActionResult Eliminar(int id)
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
            return RedirectToAction(nameof(Index));
        }

        private bool TrasladoExists(int id)
        {
            return _context.TrasladosVenta.Any(e => e.Id == id);
        }
    }
}
