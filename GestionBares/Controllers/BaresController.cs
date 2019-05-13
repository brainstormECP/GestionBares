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
using GestionBares.ViewModels;

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

        public IActionResult Standard(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var bar = _context.Set<Bar>().Where(c => c.Id == id).Single();

            if (bar == null)
            {
                return NotFound();
            }
            var standard = new Standard { BarId = bar.Id, Bar = bar };
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
        public IActionResult Standard(int id, [Bind("Id,BarId,CheckedProductos")] Standard standard)
        {
            foreach (var item in standard.CheckedProductos)
            {
                if (item.Checked == true)
                {
                    if (!_context.Standards.Any(c => c.ProductoId == item.ProductoId && c.BarId == standard.BarId))
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

        // GET: StandardVentas/Edit/5
        public IActionResult StandardDeVentas(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var bar = _context.Set<Bar>().Where(c => c.Id == id).Single();

            if (bar == null)
            {
                return NotFound();
            }
            var standardVenta = new StandardVenta { BarId = bar.Id, Bar = bar };

            if (standardVenta == null)
            {
                return NotFound();
            }

            //CODIGO MIO ++++++++
            foreach (var item in _context.Productos.ToList())
            {
                if (_context.StandardVentas.Any(c => c.ProductoId == item.Id && c.BarId == standardVenta.BarId))
                {
                    standardVenta.CheckedProductos.Add(new CheckedProductos() { Checked = true, ProductoId = item.Id, Producto = item });
                }
                else
                {
                    standardVenta.CheckedProductos.Add(new CheckedProductos() { Checked = false, ProductoId = item.Id, Producto = item });
                }

            }
            //CODIGO MIO -------

            return View(standardVenta);
        }

        // POST: StandardVentas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult StandardDeVentas(int id, [Bind("Id,BarId,CheckedProductos")] StandardVenta standardVenta)
        {
            if (id != standardVenta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    foreach (var item in standardVenta.CheckedProductos)
                    {
                        if (item.Checked == true)
                        {
                            if (!_context.StandardVentas.Any(c => c.ProductoId == item.ProductoId && c.BarId == standardVenta.BarId))
                            {
                                _context.Add(new StandardVenta() { BarId = standardVenta.BarId, ProductoId = _context.Productos.Where(c => c.Id == item.ProductoId).Select(c => c.Id).Single() });
                            }
                        }
                        else
                        {
                            if (_context.StandardVentas.Any(c => c.ProductoId == item.ProductoId && c.BarId == standardVenta.BarId))
                            {
                                _context.Remove(_context.StandardVentas.Where(c => c.ProductoId == item.ProductoId && c.BarId == standardVenta.BarId).Single());
                            }
                        }
                    }
                    _context.SaveChanges();
                    TempData["exito"] = "La acción se ha realizado correctamente";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StandardVentaExists(standardVenta.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            TempData["error"] = "Error en ralizar esta acción";
            return RedirectToAction(nameof(Index));
        }

        private bool StandardVentaExists(int id)
        {
            return _context.StandardVentas.Any(e => e.Id == id);
        }

        private bool StandardExists(int id)
        {
            return _context.Standards.Any(e => e.Id == id);
        }

        private bool BarExists(int id)
        {
            return _context.Bares.Any(e => e.Id == id);
        }
    }
}
