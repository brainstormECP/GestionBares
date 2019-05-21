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
    [Authorize]
    public class TurnosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ExistenciasService _existenciaService;

        public TurnosController(ApplicationDbContext context)
        {
            _context = context;
            _existenciaService = new ExistenciasService(context);
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
            ViewData["DependienteId"] = new SelectList(_context.Dependientes, "Id", "NombreCompleto");
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
            ViewData["DependienteId"] = new SelectList(_context.Dependientes, "Id", "NombreCompleto", turno.DependienteId);
            TempData["error"] = "Error en ralizar esta acción";
            return View(turno);
        }

        [Authorize(Roles = DefinicionRoles.Dependiente)]
        public IActionResult Nuevo()
        {
            var dependiente = _context.Set<Dependiente>().SingleOrDefault(u => u.Usuario.UserName == User.Identity.Name);
            if (_context.Set<Turno>().Any(t => t.DependienteId == dependiente.Id && t.Activo))
            {
                TempData["info"] = "Ya usted tiene un turno abierto.";
                return RedirectToAction(nameof(Cerrar));
            }
            var bares = _context.Set<DependienteBar>()
                .Include(d => d.Bar)
                .Where(d => d.DependienteId == dependiente.Id && !_context.Set<Turno>().Any(t => t.BarId == d.BarId && t.Activo)).Select(b => b.Bar);
            ViewData["BarId"] = new SelectList(bares, "Id", "Nombre");
            return View(new Turno { DependienteId = dependiente.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Nuevo([Bind("DependienteId,BarId")] Turno turno)
        {
            if (turno.BarId > 0 && turno.DependienteId > 0)
            {
                turno.Activo = true;
                turno.FechaInicio = DateTime.Now;
                _context.Add(turno);
                var control = new ControlExistencia
                {
                    Turno = turno,
                    Fecha = DateTime.Now,
                    Activo = true,
                };
                var existenciasAnteriores = _existenciaService.ExistenciaAnterior(turno.BarId, 0, DateTime.Now);
                control.Detalles = existenciasAnteriores.Select(e => new DetalleControlExistencia { ProductoId = e.ProductoId, Cantidad = e.Cantidad, Costo = _context.Set<Producto>().Find(e.ProductoId).Costo }).ToList();
                _context.Add(control);
                var controlVenta = new ControlExistenciaVenta
                {
                    Turno = turno,
                    Fecha = DateTime.Now,
                    Activo = true,
                };
                var existenciasVentaAnteriores = _existenciaService.ExistenciaVentaAnterior(turno.BarId, 0, DateTime.Now);
                controlVenta.Detalles = existenciasVentaAnteriores.Select(e => new DetalleControlExistenciaVenta { ProductoId = e.ProductoId, Cantidad = e.Cantidad, Costo = _context.Set<Producto>().Find(e.ProductoId).Costo, Precio = _context.Set<Producto>().Find(e.ProductoId).Precio }).ToList();
                _context.Add(controlVenta);
                _context.SaveChanges();
                TempData["exito"] = "La acción se ha realizado correctamente";
                return RedirectToAction("Index", "Home");
            }
            ViewData["BarId"] = new SelectList(_context.Bares, "Id", "Nombre", turno.BarId);
            TempData["error"] = "Error en realizar esta acción";
            return View(turno);
        }

        [Authorize(Roles = DefinicionRoles.Dependiente)]
        public IActionResult Cerrar()
        {
            var dependiente = _context.Set<Dependiente>().SingleOrDefault(u => u.Usuario.UserName == User.Identity.Name);
            if (!_context.Set<Turno>().Any(t => t.DependienteId == dependiente.Id && t.Activo))
            {
                TempData["info"] = "Usted no tiene turno por cerrar, si desea abra uno nuevo.";
                return RedirectToAction(nameof(Nuevo));
            }
            var turno = _context.Set<Turno>().SingleOrDefault(t => t.DependienteId == dependiente.Id && t.Activo);
            ViewBag.TurnoId = turno.Id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cerrar(int id)
        {
            var turno = _context.Set<Turno>().SingleOrDefault(t => t.Id == id);
            turno.FechaFin = DateTime.Now;
            turno.Activo = false;
            foreach (var control in _context.Set<ControlExistencia>().Where(c => c.TurnoId == id))
            {
                control.Activo = false;
            }
            foreach (var control in _context.Set<ControlExistenciaVenta>().Where(c => c.TurnoId == id))
            {
                control.Activo = false;
            }
            _context.SaveChanges();
            TempData["exito"] = "La acción se ha realizado correctamente";
            return RedirectToAction("Index", "Home");
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
            ViewData["DependienteId"] = new SelectList(_context.Dependientes, "Id", "NombreCompleto", turno.DependienteId);
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
            ViewData["DependienteId"] = new SelectList(_context.Dependientes, "Id", "NombreCompleto", turno.DependienteId);
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
