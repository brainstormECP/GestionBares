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
    public class UnidadesDeMedidasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UnidadesDeMedidasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UnidadesDeMedidas
        public IActionResult Index()
        {
            return View(_context.UnidadesDeMedidas.ToList());
        }

        // GET: UnidadesDeMedidas/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unidadDeMedida = _context.UnidadesDeMedidas
            .FirstOrDefault(m => m.Id == id);
            if (unidadDeMedida == null)
            {
                return NotFound();
            }

            return View(unidadDeMedida);
        }

        // GET: UnidadesDeMedidas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UnidadesDeMedidas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Nombre")] UnidadDeMedida unidadDeMedida)
        {
            if (ModelState.IsValid)
            {
                _context.Add(unidadDeMedida);
                _context.SaveChanges();
                TempData["exito"] = "La acción se ha realizado correctamente";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Error en ralizar esta acción";
            return View(unidadDeMedida);
        }

        // GET: UnidadesDeMedidas/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unidadDeMedida = _context.UnidadesDeMedidas.Find(id);
            if (unidadDeMedida == null)
            {
                return NotFound();
            }
            return View(unidadDeMedida);
        }

        // POST: UnidadesDeMedidas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Nombre")] UnidadDeMedida unidadDeMedida)
        {
            if (id != unidadDeMedida.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(unidadDeMedida);
                    TempData["exito"] = "La acción se ha realizado correctamente";
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnidadDeMedidaExists(unidadDeMedida.Id))
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
            return View(unidadDeMedida);
        }

        public IActionResult Eliminar(int id)
        {
            var unidadDeMedida = _context.UnidadesDeMedidas.Find(id);
            _context.UnidadesDeMedidas.Remove(unidadDeMedida);
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

        private bool UnidadDeMedidaExists(int id)
        {
            return _context.UnidadesDeMedidas.Any(e => e.Id == id);
        }
    }
}
