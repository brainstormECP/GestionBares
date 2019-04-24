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

namespace GestionBares.Controllers
{
    [Authorize]
    public class BaresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BaresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bares
        public IActionResult Index()
        {
            return View(_context.Bares.ToList());
        }

        // GET: Bares/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bar = _context.Bares
            .FirstOrDefault(m => m.Id == id);
            if (bar == null)
            {
                return NotFound();
            }

            return View(bar);
        }

        // GET: Bares/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bares/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Nombre")] Bar bar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bar);
                _context.SaveChanges();
                TempData["exito"] = "La acción se ha realizado correctamente";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Error en ralizar esta acción";
            return View(bar);
        }

        // GET: Bares/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bar = _context.Bares.Find(id);
            if (bar == null)
            {
                return NotFound();
            }
            return View(bar);
        }

        // POST: Bares/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Nombre")] Bar bar)
        {
            if (id != bar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bar);
                    TempData["exito"] = "La acción se ha realizado correctamente";
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BarExists(bar.Id))
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
            TempData["error"] = "Error en ralizar esta acción";
            return View(bar);
        }

        public IActionResult Eliminar(int id)
        {
            var bar = _context.Bares.Find(id);
            _context.Bares.Remove(bar);
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

        private bool BarExists(int id)
        {
            return _context.Bares.Any(e => e.Id == id);
        }
    }
}
