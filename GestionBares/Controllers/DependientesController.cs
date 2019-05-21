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
using Microsoft.AspNetCore.Identity;
using GestionBares.Utils;
using GestionBares.ViewModels;
using Microsoft.Extensions.Logging;

namespace GestionBares.Controllers
{
    [Authorize]
    public class DependientesController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly ApplicationDbContext _context;
        public ILogger<Usuario> _logger { get; set; }
        public DependientesController(ApplicationDbContext context, UserManager<Usuario> userManager, ILogger<Usuario> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        // GET: Dependientes
        public IActionResult Index()
        {
            var dependientes = _context.Dependientes
                .Include(d => d.Usuario)
                .Select(d =>
                    new DependienteVM
                    {
                        Id = d.Id,
                        Nombre = d.Nombres,
                        Activo = d.Activo,
                        UsuarioId = d.UsuarioId,
                        NombreDeUsuario = d.Usuario.UserName,
                        Bares = _context.Set<DependienteBar>().Where(b => b.DependienteId == d.Id).Select(b => b.Bar.Nombre).ToList()
                    })
                .ToList();

            return View(dependientes);
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
        public IActionResult Create(NuevoDependienteVM dependiente)
        {
            if (ModelState.IsValid)
            {
                var user = new Usuario
                {
                    UserName = dependiente.NombreUsuario,
                    Email = dependiente.NombreUsuario + "@patriarca.cu",
                    Activo = true
                };
                var result = _userManager.CreateAsync(user, dependiente.Password);
                if (result.Result.Succeeded)
                {
                    _logger.LogInformation("Usuario creado correctamente.");
                    var resultRole = _userManager.AddToRoleAsync(user, DefinicionRoles.Dependiente);
                    if (resultRole.Result.Succeeded)
                    {
                        _context.Add(new Dependiente
                        {
                            Nombres = dependiente.Nombres,
                            Apellidos = dependiente.Apellidos,
                            Usuario = user,
                            Activo = true,
                        });
                        _context.SaveChanges();
                        TempData["exito"] = "La acción se ha realizado correctamente";
                    }
                    TempData["error"] = "Error agregando rol";
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            TempData["error"] = "Error en realizar esta acción";
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
        public IActionResult Edit(int id, [Bind("Id,Nombres,Apellidos,UsuarioId,Activo")] Dependiente dependiente)
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
            var dependiente = _context.Dependientes
                .Include(d => d.Usuario)
                .FirstOrDefault(d => d.Id == id);
            dependiente.Activo = !dependiente.Activo;
            dependiente.Usuario.Activo = dependiente.Activo;
            _context.Update(dependiente);
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

        public IActionResult SeleccionBares(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var dependiente = _context.Set<Dependiente>().Find(id);
            var turno = new DependienteBar() { DependienteId = id.Value, Dependiente = dependiente };

            //CODIGO MIO ++++++++
            foreach (var item in _context.Set<Bar>().ToList())
            {
                if (_context.Set<DependienteBar>().Any(c => c.BarId == item.Id && c.DependienteId == turno.DependienteId))
                {
                    turno.CheckedTurnos.Add(new CheckedTurno() { Checked = true, BarId = item.Id, Bar = item });
                }
                else
                {
                    turno.CheckedTurnos.Add(new CheckedTurno() { Checked = false, BarId = item.Id, Bar = item });
                }
            }
            //CODIGO MIO -------

            return View(turno);
        }

        // POST: Turnos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SeleccionBares(int id, [Bind("Id,DependienteId,CheckedTurnos")] DependienteBar turno)
        {
            if (id != turno.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (_context.Set<DependienteBar>().Any(x => x.BarId == turno.BarId))
                {
                    ViewData["BarId"] = new SelectList(_context.Bares, "Id", "Nombre", turno.BarId);
                    ViewData["DependienteId"] = new SelectList(_context.Dependientes, "Id", "Nombre", turno.DependienteId);
                    TempData["error"] = "El Bar ya estaba asignado al Dependiente";
                    return View(turno);
                }
                try
                {
                    foreach (var item in turno.CheckedTurnos)
                    {
                        if (item.Checked == true)
                        {
                            if (_context.Set<DependienteBar>().Any(c => c.BarId == item.BarId && c.DependienteId == turno.DependienteId))
                            {
                                //No hacer nada!!!
                            }
                            else
                            {
                                _context.Add(new DependienteBar() { DependienteId = turno.DependienteId, BarId = _context.Bares.Where(c => c.Id == item.BarId).Select(c => c.Id).Single() });
                            }
                        }
                        else
                        {
                            if (_context.Set<DependienteBar>().Any(c => c.BarId == item.BarId && c.DependienteId == turno.DependienteId))
                            {
                                _context.Remove(_context.Set<DependienteBar>().Where(c => c.BarId == item.BarId && c.DependienteId == turno.DependienteId).Single());
                            }
                        }
                    }

                    _context.SaveChanges();
                    TempData["exito"] = "La acción se ha realizado correctamente";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DependienteBarExists(turno.Id))
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
            TempData["error"] = "Error en ralizar esta acción";
            return View(turno);
        }

        private bool DependienteExists(int id)
        {
            return _context.Dependientes.Any(e => e.Id == id);
        }

        private bool DependienteBarExists(int id)
        {
            return _context.Set<DependienteBar>().Any(e => e.Id == id);
        }
    }
}
