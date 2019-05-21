using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionBares.Data;
using GestionBares.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using GestionBares.Utils;

namespace GestionBares.Controllers
{
    [Authorize]
    public class SugerenciasPedidosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SugerenciasPedidos _sugerencias;

        public SugerenciasPedidosController(ApplicationDbContext context)
        {
            _context = context;
            _sugerencias = new SugerenciasPedidos(context);
        }

        // GET: Traslados
        public IActionResult Index()
        {
            if (!User.IsInRole(DefinicionRoles.Dependiente))
            {
                return View();
            }
            var dependiente = _context.Set<Dependiente>().SingleOrDefault(u => u.Usuario.UserName == User.Identity.Name);
            if (!_context.Set<Turno>().Any(t => t.DependienteId == dependiente.Id && t.Activo))
            {
                return RedirectToAction("Nuevo", "Turnos");
            }
            var turno = _context.Set<Turno>()
                .Include(t => t.Bar)
                .SingleOrDefault(t => t.DependienteId == dependiente.Id && t.Activo);
            var sugerencias = _sugerencias.Sugerencias(turno.Id);
            return View(sugerencias);
        }

        public IActionResult Ventas()
        {
            if (!User.IsInRole(DefinicionRoles.Dependiente))
            {
                return View();
            }
            var dependiente = _context.Set<Dependiente>().SingleOrDefault(u => u.Usuario.UserName == User.Identity.Name);
            if (!_context.Set<Turno>().Any(t => t.DependienteId == dependiente.Id && t.Activo))
            {
                return RedirectToAction("Nuevo", "Turnos");
            }
            var turno = _context.Set<Turno>()
                .Include(t => t.Bar)
                .SingleOrDefault(t => t.DependienteId == dependiente.Id && t.Activo);
            var sugerencias = _sugerencias.SugerenciasVentas(turno.Id);
            return View(sugerencias);
        }
    }
}
