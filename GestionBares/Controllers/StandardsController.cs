using GestionBares.Data;
using GestionBares.Models;
using GestionBares.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GestionBares.Controllers
{
    public class StandardsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StandardsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Standards
        public IActionResult Index()
        {
            var applicationDbContext = _context.Standards.Include(s => s.Bar).Include(s => s.Producto);
            return View(applicationDbContext.ToList());
        }
        // POST: Standards/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Standard standard)
        {
            if (ModelState.IsValid)
            {

                foreach (var item in _context.Standards.ToList())
                {
                    _context.Standards.Remove(item);
                }

                _context.SaveChanges();
                TempData["exito"] = "La acción se ha realizado correctamente";
                return RedirectToAction(nameof(Index));
            }            
            TempData["error"] = "Error en ralizar esta acción";
            return View(standard);
        }

        // GET: Standards/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var standard = _context.Standards
                        .Include(s => s.Bar)
                        .Include(s => s.Producto)
            .FirstOrDefault(m => m.Id == id);
            if (standard == null)
            {
                return NotFound();
            }

            return View(standard);
        }

        // GET: Standards/Create
        public IActionResult Create()
        {
            Standard standard = new Standard();
            foreach (var item in _context.Productos.OrderBy(c => c.Codigo).ToList())
            {
                standard.CheckedProductos.Add(new CheckedProductos() { Producto = item, ProductoId = item.Id, Checked = true });
            }

            ViewData["BarId"] = new SelectList(_context.Bares, "Id", "Nombre");
            return View(standard);


        }

        // POST: Standards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,BarId,CheckedProductos")] Standard standard)
        {
            foreach (var item in standard.CheckedProductos)
            {
                if (_context.Standards.Any(c => c.ProductoId == item.ProductoId && c.BarId == standard.BarId))
                {
                    TempData["error"] = "Ya existe el standard de al menos uno de los Productos selecionados para este Bar";
                    return RedirectToAction(nameof(Index));
                }

                if (item.Checked == true)
                {
                    _context.Add(new Standard() { BarId = standard.BarId, ProductoId = item.ProductoId });
                }
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: Standards/Edit/5
        public IActionResult Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var standard = _context.Standards.Include(c => c.Bar).Where(c => c.Id == id).Single();

            if (standard == null)
            {
                return NotFound();
            }

            //CODIGO MIO ++++++++
            foreach (var item in _context.Productos.ToList())
            {
                if (_context.Standards.Any(c => c.ProductoId == item.Id && c.BarId == standard.BarId))
                {
                    standard.CheckedProductos.Add(new CheckedProductos() { Checked = true, ProductoId = item.Id, Producto = item });
                }
                else
                {
                    standard.CheckedProductos.Add(new CheckedProductos() { Checked = false, ProductoId = item.Id, Producto = item });
                }

            }
            //CODIGO MIO -------

            return View(standard);
        }

        // POST: Standards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,BarId,CheckedProductos")] Standard standard)
        {
            foreach (var item in standard.CheckedProductos)
            {
                if (item.Checked == true)
                {
                    if (_context.Standards.Any(c => c.ProductoId == item.ProductoId && c.BarId == standard.BarId))
                    {
                        //No hacer nada!!!
                    }
                    else
                    {
                        _context.Add(new Standard() { BarId = standard.BarId, ProductoId = _context.Productos.Where(c => c.Id == item.ProductoId).Select(c => c.Id).Single() });
                    }
                }
                else
                {
                    if (_context.Standards.Any(c => c.ProductoId == item.ProductoId && c.BarId == standard.BarId))
                    {
                        _context.Remove(_context.Standards.Where(c => c.ProductoId == item.ProductoId && c.BarId == standard.BarId).Single());
                    }
                }
            }            

            _context.SaveChanges();
            TempData["exito"] = "La acción se ha realizado correctamente";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Eliminar(int id)
        {
            var standard = _context.Standards.Find(id);
            _context.Standards.Remove(standard);
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

        private bool StandardExists(int id)
        {
            return _context.Standards.Any(e => e.Id == id);
        }
    }
}
