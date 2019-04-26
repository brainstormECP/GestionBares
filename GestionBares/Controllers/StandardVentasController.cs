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
    public class StandardVentasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StandardVentasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StandardVentas
        public IActionResult Index()
        {
            var applicationDbContext = _context.StandardVentas.Include(s => s.Bar).Include(s => s.Producto);
            return View(applicationDbContext.ToList());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(StandardVenta standardVenta)
        {
            if (ModelState.IsValid)
            {

                foreach (var item in _context.StandardVentas.ToList())
                {
                    _context.StandardVentas.Remove(item);
                }

                _context.SaveChanges();
                TempData["exito"] = "La acción se ha realizado correctamente";
                return RedirectToAction(nameof(Index));
            }
            
            TempData["error"] = "Error en ralizar esta acción";
            return View(standardVenta);
        }

        // GET: StandardVentas/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var standardVenta = _context.StandardVentas
                        .Include(s => s.Bar)
            .FirstOrDefault(m => m.Id == id);
            if (standardVenta == null)
            {
                return NotFound();
            }

            

            return View(standardVenta);
        }

        // GET: StandardVentas/Create
        public IActionResult Create()
        {
            StandardVenta standardVenta = new StandardVenta();
            foreach (var item in _context.Productos.OrderBy(c => c.Codigo).ToList())
            {
                standardVenta.CheckedProductos.Add(new CheckedProductos() { Producto = item, ProductoId = item.Id, Checked = true });
            }

            ViewData["BarId"] = new SelectList(_context.Bares, "Id", "Nombre");            
            return View(standardVenta);
        }

        // POST: StandardVentas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,BarId,CheckedProductos")] StandardVenta standardVenta)
        {

            foreach (var item in standardVenta.CheckedProductos)
            {
                if(_context.StandardVentas.Any(c => c.ProductoId == item.ProductoId && c.BarId == standardVenta.BarId))
                {
                    TempData["error"] = "Ya existe el standard de al menos uno de los Productos selecionados para este Bar";
                    return RedirectToAction(nameof(Index));
                }

                if(item.Checked == true)
                {
                    _context.Add(new StandardVenta() { BarId = standardVenta.BarId, ProductoId = item.ProductoId });
                }
            }

            _context.SaveChanges();          
            return RedirectToAction(nameof(Index));


        }

        // GET: StandardVentas/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var standardVenta = _context.StandardVentas.Include(c => c.Bar).Where(c => c.Id == id).Single();
            
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
        public IActionResult Edit(int id, [Bind("Id,BarId,CheckedProductos")] StandardVenta standardVenta)
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
                        if(item.Checked == true)
                        {
                            if(_context.StandardVentas.Any(c => c.ProductoId == item.ProductoId && c.BarId == standardVenta.BarId))
                            {
                                //No hacer nada!!!
                            }
                            else
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

        public IActionResult Eliminar(int id)
        {
            var standardVenta = _context.StandardVentas.Find(id);
            _context.StandardVentas.Remove(standardVenta);
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

        private bool StandardVentaExists(int id)
        {
            return _context.StandardVentas.Any(e => e.Id == id);
        }
    }
}
