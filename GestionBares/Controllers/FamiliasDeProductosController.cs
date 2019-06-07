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
using GestionBares.Utils;

namespace GestionBares.Controllers
{
    [Authorize(Roles = DefinicionRoles.AmasB)]
    public class FamiliasDeProductosController : Controller
    {
        private readonly AmasBDbContext _context;

        public FamiliasDeProductosController(AmasBDbContext context)
        {
            _context = context;
        }

        // GET: FamiliasDeProductos
        public IActionResult Index()
        {
            return View(_context.FamiliasDeProductos.ToList());
        }

        // GET: FamiliasDeProductos/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var familiaDeProducto = _context.FamiliasDeProductos
            .FirstOrDefault(m => m.Id == id);
            if (familiaDeProducto == null)
            {
                return NotFound();
            }

            return View(familiaDeProducto);
        }

        // GET: FamiliasDeProductos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FamiliasDeProductos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Nombre")] FamiliaDeProducto familiaDeProducto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(familiaDeProducto);
                _context.SaveChanges();
                TempData["exito"] = "La acción se ha realizado correctamente";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Error en ralizar esta acción";
            return View(familiaDeProducto);
        }

        // GET: FamiliasDeProductos/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var familiaDeProducto = _context.FamiliasDeProductos.Find(id);
            if (familiaDeProducto == null)
            {
                return NotFound();
            }
            return View(familiaDeProducto);
        }

        // POST: FamiliasDeProductos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Nombre")] FamiliaDeProducto familiaDeProducto)
        {
            if (id != familiaDeProducto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(familiaDeProducto);
                    TempData["exito"] = "La acción se ha realizado correctamente";
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FamiliaDeProductoExists(familiaDeProducto.Id))
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
            return View(familiaDeProducto);
        }

        public IActionResult Eliminar(int id)
        {
            var familiaDeProducto = _context.FamiliasDeProductos.Find(id);
            _context.FamiliasDeProductos.Remove(familiaDeProducto);
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

        private bool FamiliaDeProductoExists(int id)
        {
            return _context.FamiliasDeProductos.Any(e => e.Id == id);
        }
    }
}
