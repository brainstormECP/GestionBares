using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionBares.Data;
using GestionBares.Models;

namespace GestionBares.Controllers
{
    public class ControlesExistenciasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ControlesExistenciasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ControlesExistencias
        public IActionResult Index()
        {
            var applicationDbContext = _context.ControlesDeExistencias
                .Include(c => c.Turno.Dependiente)
                .Include(c => c.Turno.Bar);
            return View(applicationDbContext.ToList());
        }

        // GET: ControlesExistencias/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var controlExistencia = _context.ControlesDeExistencias
                        .Include(c => c.Turno)
            .FirstOrDefault(m => m.Id == id);
            if (controlExistencia == null)
            {
                return NotFound();
            }

            return View(controlExistencia);
        }

        // GET: ControlesExistencias/Create
        public IActionResult Create()
        {
            ViewData["TurnoId"] = new SelectList(_context.Turnos, "Id", "Id");
            return View();
        }

        // POST: ControlesExistencias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,TurnoId")] ControlExistencia controlExistencia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(controlExistencia);
                _context.SaveChanges();
                TempData["exito"] = "La acción se ha realizado correctamente";
                return RedirectToAction(nameof(Index));
            }
            ViewData["TurnoId"] = new SelectList(_context.Turnos, "Id", "Id", controlExistencia.TurnoId);
            TempData["error"] = "Error en ralizar esta acción";
            return View(controlExistencia);
        }

        // GET: ControlesExistencias/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var controlExistencia = _context.ControlesDeExistencias
                .Include(c => c.Turno.Dependiente)
                .Include(c => c.Turno.Bar)
                .Include(c => c.Detalles).ThenInclude(c => c.Producto)
                .SingleOrDefault(c => c.Id == id);
            if (controlExistencia == null)
            {
                return NotFound();
            }
            ViewData["Productos"] = new SelectList(_context.Set<Producto>(), "Id", "Nombre");
            return View(controlExistencia);
        }

        [HttpPost]
        public IActionResult AgregarProducto(DetalleControlExistencia detalle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detalle);
                var control = _context.Set<ControlExistencia>()
                    .Include(c => c.Detalles).ThenInclude(c => c.Producto)
                    .FirstOrDefault(c => c.Id == detalle.ControlId);
                _context.SaveChanges();

            }
            return RedirectToAction(nameof(Edit), new { Id = detalle.ControlId });
        }

        public IActionResult QuitarDetalle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalle = _context.Set<DetalleControlExistencia>().Find(id);
            if (detalle == null)
            {
                return NotFound();
            }
            _context.Remove(detalle);
            _context.SaveChanges();
            return RedirectToAction(nameof(Edit), new { Id = detalle.ControlId });
        }

        // POST: ControlesExistencias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,TurnoId")] ControlExistencia controlExistencia)
        {
            if (id != controlExistencia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(controlExistencia);
                    TempData["exito"] = "La acción se ha realizado correctamente";
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ControlExistenciaExists(controlExistencia.Id))
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
            ViewData["TurnoId"] = new SelectList(_context.Turnos, "Id", "Id", controlExistencia.TurnoId);
            TempData["error"] = "Error en ralizar esta acción";
            return View(controlExistencia);
        }

        public IActionResult Eliminar(int id)
        {
            var controlExistencia = _context.ControlesDeExistencias.Find(id);
            _context.ControlesDeExistencias.Remove(controlExistencia);
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

        private bool ControlExistenciaExists(int id)
        {
            return _context.ControlesDeExistencias.Any(e => e.Id == id);
        }
    }
}
