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
    public class DependientesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DependientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Dependientes
        public IActionResult Index()
        {
            return View(_context.Dependientes.ToList());
        }

        // GET: Dependientes/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dependiente = _context.Dependientes
            .FirstOrDefault(m => m.Id == id);
            if (dependiente == null)
            {
                return NotFound();
            }

            return View(dependiente);
        }

        // GET: Dependientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dependientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Nombre,UsuarioId")] Dependiente dependiente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dependiente);
                _context.SaveChanges();
                TempData["exito"] = "La acción se ha realizado correctamente";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Error en ralizar esta acción";
            return View(dependiente);
        }

        // GET: Dependientes/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dependiente = _context.Dependientes.Find(id);
            if (dependiente == null)
            {
                return NotFound();
            }
            return View(dependiente);
        }

        // POST: Dependientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Nombre,UsuarioId")] Dependiente dependiente)
        {
            if (id != dependiente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dependiente);
                    TempData["exito"] = "La acción se ha realizado correctamente";
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DependienteExists(dependiente.Id))
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
            return View(dependiente);
        }

        public IActionResult Eliminar(int id)
        {
            var dependiente = _context.Dependientes.Find(id);
            _context.Dependientes.Remove(dependiente);
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

        private bool DependienteExists(int id)
        {
            return _context.Dependientes.Any(e => e.Id == id);
        }
    }
}
