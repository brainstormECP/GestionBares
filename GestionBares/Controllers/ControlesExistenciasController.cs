﻿using System;
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
using GestionBares.ViewModels;

namespace GestionBares.Controllers
{
    [Authorize(Roles = DefinicionRoles.Dependiente)]
    public class ControlesExistenciasController : Controller
    {
        private readonly DependienteDbContext _context;
        private readonly ExistenciasService _existenciaService;

        public ControlesExistenciasController(DependienteDbContext context)
        {
            _context = context;
            _existenciaService = new ExistenciasService(context);
        }

        // GET: ControlesExistencias
        public IActionResult Index()
        {
            var controlExistencia = _context.ControlesDeExistencias
                .Include(c => c.Turno.Dependiente)
                .Include(c => c.Turno.Bar)
                .ToList();
            if (User.IsInRole(DefinicionRoles.Dependiente))
            {
                var dependiente = _context.Set<Dependiente>().SingleOrDefault(d => d.Usuario.UserName == User.Identity.Name);
                var turno = _context.Set<Turno>().SingleOrDefault(t => t.Activo && t.DependienteId == dependiente.Id);
                if (turno == null)
                {
                    return RedirectToAction("Nuevo", "Turnos");
                }
                controlExistencia = controlExistencia.Where(c => c.TurnoId == turno.Id).ToList();
            }
            return View(controlExistencia);
        }

        // GET: ControlesExistencias/Details/5
        public IActionResult Detalles(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var controlExistencia = _context.ControlesDeExistencias
                .Include(c => c.Turno.Bar)
                .Include(c => c.Turno.Dependiente)
                .Include(c => c.Detalles).ThenInclude(d => d.Producto.Unidad)
                .FirstOrDefault(m => m.Id == id);
            if (controlExistencia == null)
            {
                return NotFound();
            }

            return View(controlExistencia);
        }

        // GET: ControlesExistencias/Create
        public IActionResult Create()
        {
            if (User.IsInRole(DefinicionRoles.Dependiente))
            {
                return RedirectToAction(nameof(PorTurno));
            }
            ViewData["TurnoId"] = new SelectList(_context.Turnos.Include(t => t.Dependiente).Include(t => t.Bar), "Id", "Descripcion");
            return View();
        }

        // POST: ControlesExistencias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,TurnoId")] ControlExistencia controlExistencia)
        {
            if (ModelState.IsValid)
            {
                controlExistencia.Fecha = DateTime.Now;
                controlExistencia.Activo = true;
                _context.Add(controlExistencia);
                _context.SaveChanges();
                TempData["exito"] = "La acción se ha realizado correctamente";
                return RedirectToAction(nameof(Index));
            }
            ViewData["TurnoId"] = new SelectList(_context.Turnos.Include(t => t.Dependiente).Include(t => t.Bar), "Id", "Descripcion", controlExistencia.TurnoId);
            TempData["error"] = "Error en ralizar esta acción";
            return View(controlExistencia);
        }

        // GET: ControlesExistencias/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var control = _context.ControlesDeExistencias
                .Include(c => c.Turno.Dependiente)
                .Include(c => c.Turno.Bar)
                .Include(c => c.Detalles).ThenInclude(c => c.Producto)
                .SingleOrDefault(c => c.Id == id);
            if (control == null)
            {
                return NotFound();
            }
            var existenciaAnterior = _existenciaService.ExistenciaAnterior(control.Turno.BarId, control.Id, control.Fecha);
            var productos = _context.Set<Producto>()
                .Where(p => _context.Set<Standard>()
                    .Any(s => s.ProductoId == p.Id && s.BarId == control.Turno.BarId)
                    || (existenciaAnterior.Any(e => e.ProductoId == p.Id) ? existenciaAnterior.SingleOrDefault(e => e.ProductoId == p.Id).Cantidad : 0) > 0)
                .Select(p => new DetalleExistenciaVM
                {
                    ProductoId = p.Id,
                    Producto = p.Nombre,
                    Unidad = p.Unidad.Nombre,
                    Cantidad = _context.Set<DetalleControlExistencia>().Any(d => d.ControlId == control.Id && d.ProductoId == p.Id) ? _context.Set<DetalleControlExistencia>().SingleOrDefault(d => d.ControlId == control.Id && d.ProductoId == p.Id).Cantidad : 0,
                    CantidadAnterior = existenciaAnterior.Any(e => e.ProductoId == p.Id) ? existenciaAnterior.SingleOrDefault(e => e.ProductoId == p.Id).Cantidad : 0,
                });
            var data = new ControlExistenciaVM
            {
                Id = control.Id,
                TurnoId = control.TurnoId,
                Bar = control.Turno.Bar.Nombre,
                Dependiente = control.Turno.Dependiente.NombreCompleto,
                Fecha = control.Fecha,
                Detalles = productos.ToList()
            };
            return View("PorTurno", data);
        }

        public IActionResult Eliminar(int id)
        {
            var controlExistencia = _context.ControlesDeExistencias.Find(id);
            _context.ControlesDeExistencias.Remove(controlExistencia);
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

        public IActionResult PorTurno()
        {
            var dependiente = _context.Set<Dependiente>().SingleOrDefault(u => u.Usuario.UserName == User.Identity.Name);
            if (!_context.Set<Turno>().Any(t => t.DependienteId == dependiente.Id && t.Activo))
            {
                return RedirectToAction("Nuevo", "Turnos");
            }
            var turno = _context.Set<Turno>()
                .Include(t => t.Bar)
                .SingleOrDefault(t => t.DependienteId == dependiente.Id && t.Activo);
            ControlExistencia control;
            if (_context.Set<ControlExistencia>().Any(c => c.TurnoId == turno.Id && c.Activo))
            {
                control = _context.Set<ControlExistencia>()
                    .SingleOrDefault(c => c.TurnoId == turno.Id && c.Activo);
            }
            else
            {
                control = new ControlExistencia
                {
                    Turno = turno,
                    Fecha = DateTime.Now,
                    Activo = true,
                };
                _context.Add(control);
                _context.SaveChanges();
            }

            //agregar solo productos del bar del turno            
            return RedirectToAction(nameof(Edit), new { Id = control.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PorTurno(int id, ControlExistenciaVM controlExistencia)
        {
            var control = _context.Set<ControlExistencia>()
                .SingleOrDefault(c => c.Id == controlExistencia.Id);
            if (control == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    foreach (var item in controlExistencia.Detalles)
                    {
                        var producto = _context.Set<Producto>().Find(item.ProductoId);
                        if (_context.Set<DetalleControlExistencia>().Any(d => d.ControlId == control.Id && d.ProductoId == item.ProductoId))
                        {
                            var detalle = _context.Set<DetalleControlExistencia>().SingleOrDefault(d => d.ControlId == control.Id && d.ProductoId == item.ProductoId);
                            detalle.Cantidad = item.Cantidad;
                            detalle.Costo = producto.Costo;
                            _context.Update(detalle);
                        }
                        else
                        {
                            _context.Add(new DetalleControlExistencia { ControlId = control.Id, ProductoId = item.ProductoId, Cantidad = item.Cantidad, Costo = producto.Costo });
                        }
                    }
                    _context.SaveChanges();
                    TempData["exito"] = "La acción se ha realizado correctamente";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ControlExistenciaExists(controlExistencia.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Detalles), new { Id = control.Id });
            }
            TempData["error"] = "Error en realizar esta acción";
            return View(controlExistencia);
        }

        public IActionResult Cerrar(int id)
        {
            var controlExistencia = _context.ControlesDeExistencias.Find(id);
            controlExistencia.Activo = false;
            _context.Update(controlExistencia);
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

        private bool ControlExistenciaExists(int id)
        {
            return _context.ControlesDeExistencias.Any(e => e.Id == id);
        }
    }
}
