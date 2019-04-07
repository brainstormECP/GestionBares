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
    public class TrasladosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrasladosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Traslados
        public IActionResult Index()
        {
            var applicationDbContext = _context.Traslados.Include(t => t.Destino).Include(t => t.Origen).Include(t => t.Producto);
            return View(applicationDbContext.ToList());
        }

        // GET: Traslados/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var traslado = _context.Traslados
                        .Include(t => t.Destino)
                        .Include(t => t.Origen)
                        .Include(t => t.Producto)
            .FirstOrDefault(m => m.Id == id);
            if (traslado == null)
            {
                return NotFound();
            }

            return View(traslado);
        }

        // GET: Traslados/Create
        public IActionResult Create()
        {
            ViewData["DestinoId"] = new SelectList(_context.Bares, "Id", "Nombre");
            ViewData["OrigenId"] = new SelectList(_context.Bares, "Id", "Nombre");
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nomrbre");
            return View();
        }

        // POST: Traslados/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,ProductoId,Cantidad,OrigenId,DestinoId")] Traslado traslado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(traslado);
                _context.SaveChanges();
                TempData["exito"] = "La acción se ha realizado correctamente";
                return RedirectToAction(nameof(Index));
            }
            ViewData["DestinoId"] = new SelectList(_context.Bares, "Id", "Nombre", traslado.DestinoId);
            ViewData["OrigenId"] = new SelectList(_context.Bares, "Id", "Nombre", traslado.OrigenId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nomrbre", traslado.ProductoId);
            TempData["error"] = "Error en ralizar esta acción";
            return View(traslado);
        }

        // GET: Traslados/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var traslado = _context.Traslados.Find(id);
            if (traslado == null)
            {
                return NotFound();
            }
            ViewData["DestinoId"] = new SelectList(_context.Bares, "Id", "Nombre", traslado.DestinoId);
            ViewData["OrigenId"] = new SelectList(_context.Bares, "Id", "Nombre", traslado.OrigenId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nomrbre", traslado.ProductoId);
            return View(traslado);
        }

        // POST: Traslados/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,ProductoId,Cantidad,OrigenId,DestinoId")] Traslado traslado)
        {
            if (id != traslado.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(traslado);
                    TempData["exito"] = "La acción se ha realizado correctamente";
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrasladoExists(traslado.Id))
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
            ViewData["DestinoId"] = new SelectList(_context.Bares, "Id", "Nombre", traslado.DestinoId);
            ViewData["OrigenId"] = new SelectList(_context.Bares, "Id", "Nombre", traslado.OrigenId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nomrbre", traslado.ProductoId);
            TempData["error"] = "Error en ralizar esta acción";
            return View(traslado);
        }

        public IActionResult Eliminar(int id)
        {
            var traslado = _context.Traslados.Find(id);
            _context.Traslados.Remove(traslado);
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

        private bool TrasladoExists(int id)
        {
            return _context.Traslados.Any(e => e.Id == id);
        }
    }
}
