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
    public class ProductosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Productos
        public IActionResult Index()
        {
            var applicationDbContext = _context.Productos.Include(p => p.Familia).Include(p => p.Unidad);
            return View(applicationDbContext.ToList());
        }

        // GET: Productos/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = _context.Productos
                        .Include(p => p.Familia)
                        .Include(p => p.Unidad)
            .FirstOrDefault(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Productos/Create
        public IActionResult Create()
        {
            ViewData["FamiliaId"] = new SelectList(_context.FamiliasDeProductos, "Id", "Nombre");
            ViewData["UnidadId"] = new SelectList(_context.UnidadesDeMedidas, "Id", "Nombre");
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Codigo,Nombre,UnidadId,FamiliaId,LimiteParaSolicitar")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producto);
                _context.SaveChanges();
                TempData["exito"] = "La acción se ha realizado correctamente";
                return RedirectToAction(nameof(Index));
            }
            ViewData["FamiliaId"] = new SelectList(_context.FamiliasDeProductos, "Id", "Nombre", producto.FamiliaId);
            ViewData["UnidadId"] = new SelectList(_context.UnidadesDeMedidas, "Id", "Nombre", producto.UnidadId);
            TempData["error"] = "Error en ralizar esta acción";
            return View(producto);
        }

        // GET: Productos/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = _context.Productos.Find(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["FamiliaId"] = new SelectList(_context.FamiliasDeProductos, "Id", "Nombre", producto.FamiliaId);
            ViewData["UnidadId"] = new SelectList(_context.UnidadesDeMedidas, "Id", "Nombre", producto.UnidadId);
            return View(producto);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Codigo,Nombre,UnidadId,FamiliaId,LimiteParaSolicitar")] Producto producto)
        {
            if (id != producto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    TempData["exito"] = "La acción se ha realizado correctamente";
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.Id))
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
            ViewData["FamiliaId"] = new SelectList(_context.FamiliasDeProductos, "Id", "Nombre", producto.FamiliaId);
            ViewData["UnidadId"] = new SelectList(_context.UnidadesDeMedidas, "Id", "Nombre", producto.UnidadId);
            TempData["error"] = "Error en ralizar esta acción";
            return View(producto);
        }

        public IActionResult Eliminar(int id)
        {
            var producto = _context.Productos.Find(id);
            _context.Productos.Remove(producto);
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

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.Id == id);
        }
    }
}
