using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionBares.Data;
using GestionBares.Models;
using GestionBares.ViewModel;

namespace GestionBares.Controllers
{
    public class DependientesBaresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DependientesBaresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Turnos
        public IActionResult Index()
        {
            var applicationDbContext = _context.Set<DependienteBar>().Include(t => t.Bar).Include(t => t.Dependiente);
            return View(applicationDbContext.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(DependienteBar turno)
        {
            if (ModelState.IsValid)
            {

                foreach (var item in _context.Set<DependienteBar>().ToList())
                {
                    _context.Set<DependienteBar>().Remove(item);
                }

                _context.SaveChanges();
                TempData["exito"] = "La acción se ha realizado correctamente";
                return RedirectToAction(nameof(Index));
            }

            TempData["error"] = "Error en ralizar esta acción";
            return View(turno);
        }

        // GET: Turnos/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turno = _context.Set<DependienteBar>()
                        .Include(t => t.Bar)
                        .Include(t => t.Dependiente)
            .FirstOrDefault(m => m.Id == id);
            if (turno == null)
            {
                return NotFound();
            }

            return View(turno);
        }

        // GET: Turnos/Create
        public IActionResult Create()
        {
            var turno = new DependienteBar();
            foreach (var item in _context.Bares.OrderBy(c => c.Id).ToList())
            {
                turno.CheckedTurnos.Add(new CheckedTurno() { Bar = item, BarId = item.Id, Checked = false });
            }

            ViewData["DependienteId"] = new SelectList(_context.Dependientes, "Id", "Nombre");
            return View(turno);
        }

        // POST: Turnos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,DependienteId,CheckedTurnos")] DependienteBar turno)
        {

            foreach (var item in turno.CheckedTurnos)
            {
                if (_context.Set<DependienteBar>().Any(c => c.BarId == item.BarId && c.DependienteId == turno.DependienteId))
                {
                    TempData["error"] = "Ya existe el standard de al menos uno de los Productos selecionados para este Bar";
                    return RedirectToAction(nameof(Index));
                }

                if (item.Checked == true)
                {
                    _context.Add(new Turno() { DependienteId = turno.DependienteId, BarId = item.BarId });
                }
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: Turnos/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turno = _context.Set<DependienteBar>().Include(c => c.Dependiente).Where(c => c.Id == id).Single();


            //CODIGO MIO ++++++++
            foreach (var item in _context.Bares.ToList())
            {
                if (_context.Set<DependienteBar>().Any(c => c.BarId == item.Id && c.DependienteId == turno.DependienteId))
                {
                    turno.CheckedTurnos.Add(new CheckedTurno() { Checked = true, BarId = item.Id, Bar = item });
                }
                else
                {
                    turno.CheckedTurnos.Add(new CheckedTurno() { Checked = false, BarId = item.Id, Bar = item });
                }

            }
            //CODIGO MIO -------

            return View(turno);
        }

        // POST: Turnos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,DependienteId,CheckedTurnos")] DependienteBar turno)
        {
            if (id != turno.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (_context.Set<DependienteBar>().Any(x => x.BarId == turno.BarId))
                {
                    ViewData["BarId"] = new SelectList(_context.Bares, "Id", "Nombre", turno.BarId);
                    ViewData["DependienteId"] = new SelectList(_context.Dependientes, "Id", "Nombre", turno.DependienteId);
                    TempData["error"] = "El Bar ya estaba asignado al Dependiente";
                    return View(turno);
                }
                try
                {
                    foreach (var item in turno.CheckedTurnos)
                    {
                        if (item.Checked == true)
                        {
                            if (_context.Set<DependienteBar>().Any(c => c.BarId == item.BarId && c.DependienteId == turno.DependienteId))
                            {
                                //No hacer nada!!!
                            }
                            else
                            {
                                _context.Add(new Turno() { DependienteId = turno.DependienteId, BarId = _context.Bares.Where(c => c.Id == item.BarId).Select(c => c.Id).Single() });
                            }
                        }
                        else
                        {
                            if (_context.Set<DependienteBar>().Any(c => c.BarId == item.BarId && c.DependienteId == turno.DependienteId))
                            {
                                _context.Remove(_context.Set<DependienteBar>().Where(c => c.BarId == item.BarId && c.DependienteId == turno.DependienteId).Single());
                            }
                        }
                    }


                    _context.SaveChanges();
                    TempData["exito"] = "La acción se ha realizado correctamente";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DependienteBarExists(turno.Id))
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
            ViewData["BarId"] = new SelectList(_context.Bares, "Id", "Nombre", turno.BarId);
            ViewData["DependienteId"] = new SelectList(_context.Dependientes, "Id", "Nombre", turno.DependienteId);
            TempData["error"] = "Error en ralizar esta acción";
            return View(turno);
        }

        public IActionResult Eliminar(int id)
        {
            var turno = _context.Set<DependienteBar>().Find(id);
            _context.Set<DependienteBar>().Remove(turno);
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

        private bool DependienteBarExists(int id)
        {
            return _context.Set<DependienteBar>().Any(e => e.Id == id);
        }
    }
}
