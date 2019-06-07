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
    public class EntradasVentasController : Controller
    {
        private readonly DependienteDbContext _context;

        public EntradasVentasController(DependienteDbContext context)
        {
            _context = context;
        }

        // GET: Traslados
        public IActionResult Index()
        {
            var entregas = _context.Set<EntregaDeAlmacenVenta>()
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
                entregas = entregas.Where(t => t.TurnoId == turno.Id).ToList();
            }
            return View(entregas);
        }

        // GET: Traslados/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var traslado = _context.EntregasDeAlmacenVenta
                        .Include(t => t.Turno.Bar)
                        .Include(t => t.Turno.Dependiente)
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
            ViewData["ProductoId"] = new SelectList(_context.Productos.Include(p => p.Unidad).Select(p => new { Id = p.Id, Nombre = p.Nombre + " (" + p.Unidad.Nombre + ")" }), "Id", "Nombre");
            return View();
        }

        // POST: Traslados/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,ProductoId,Cantidad,TurnoId")] EntregaDeAlmacenVenta entrada)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Set<IdentityUser>().SingleOrDefault(u => u.UserName == User.Identity.Name);
                if (user == null)
                {
                    TempData["error"] = "No estas autenticado";
                    return RedirectToAction(nameof(Index));
                }
                _context.Add(entrada);
                _context.SaveChanges();
                TempData["exito"] = "La acción se ha realizado correctamente";
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductoId"] = new SelectList(_context.Productos.Include(p => p.Unidad).Select(p => new { Id = p.Id, Nombre = p.Nombre + " (" + p.Unidad.Nombre + ")" }), "Id", "Nombre", entrada.ProductoId);
            ViewData["TurnoId"] = entrada.TurnoId;
            TempData["error"] = "Error en ralizar esta acción";
            return View(entrada);
        }

        // GET: Traslados/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entrada = _context.Set<EntregaDeAlmacenVenta>()
                .Include(t => t.Turno.Bar)
                .Include(t => t.Turno.Dependiente)
                .FirstOrDefault(t => t.Id == id);
            if (entrada == null)
            {
                return NotFound();
            }
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre", entrada.ProductoId);
            ViewData["TurnoId"] = entrada.TurnoId;
            return View(entrada);
        }

        // POST: Traslados/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,ProductoId,Cantidad,TurnoId")] EntregaDeAlmacenVenta entrada)
        {
            if (id != entrada.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(entrada);
                    TempData["exito"] = "La acción se ha realizado correctamente";
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrasladoExists(entrada.Id))
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
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre", entrada.ProductoId);
            ViewData["TurnoId"] = entrada.TurnoId;
            TempData["error"] = "Error en ralizar esta acción";
            return View(entrada);
        }

        public IActionResult Eliminar(int id)
        {
            var entrada = _context.EntregasDeAlmacenVenta.Find(id);
            _context.Remove(entrada);
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
            return _context.EntregasDeAlmacenVenta.Any(e => e.Id == id);
        }
    }
}
