﻿using System;
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
    public class TurnosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TurnosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Turnos
        public IActionResult Index()
        {
            var applicationDbContext = _context.Turnos.Include(t => t.Bar).Include(t => t.Dependiente);
            return View(applicationDbContext.ToList());
        }

        // GET: Turnos/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turno = _context.Turnos
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
            ViewData["BarId"] = new SelectList(_context.Bares, "Id", "Nombre");
            ViewData["DependienteId"] = new SelectList(_context.Dependientes, "Id", "Nombre");
            return View();
        }

        // POST: Turnos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,FechaInicio,FechaFin,Activo,DependienteId,BarId")] Turno turno)
        {
            turno.Activo = true;
            if (ModelState.IsValid)
            {
                _context.Add(turno);
                _context.SaveChanges();
                TempData["exito"] = "La acción se ha realizado correctamente";
                return RedirectToAction(nameof(Index));
            }
            ViewData["BarId"] = new SelectList(_context.Bares, "Id", "Nombre", turno.BarId);
            ViewData["DependienteId"] = new SelectList(_context.Dependientes, "Id", "Nombre", turno.DependienteId);
            TempData["error"] = "Error en ralizar esta acción";
            return View(turno);
        }

        // GET: Turnos/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turno = _context.Turnos.Find(id);
            if (turno == null)
            {
                return NotFound();
            }
            ViewData["BarId"] = new SelectList(_context.Bares, "Id", "Nombre", turno.BarId);
            ViewData["DependienteId"] = new SelectList(_context.Dependientes, "Id", "Nombre", turno.DependienteId);
            return View(turno);
        }

        // POST: Turnos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,FechaInicio,FechaFin,Activo,DependienteId,BarId")] Turno turno)
        {
            if (id != turno.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(turno);
                    TempData["exito"] = "La acción se ha realizado correctamente";
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TurnoExists(turno.Id))
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
            var turno = _context.Turnos.Find(id);
            _context.Turnos.Remove(turno);
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

        private bool TurnoExists(int id)
        {
            return _context.Turnos.Any(e => e.Id == id);
        }
    }
}