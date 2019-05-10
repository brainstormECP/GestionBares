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
using GestionBares.ViewModels;

namespace GestionBares.Controllers
{
    [Authorize]
    public class ReportesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Consumo()
        {
            if (User.IsInRole(DefinicionRoles.Dependiente))
            {
                var dependiente = _context.Set<Dependiente>().SingleOrDefault(d => d.Usuario.UserName == User.Identity.Name);
                var turno = _context.Set<Turno>().SingleOrDefault(t => t.Activo && t.DependienteId == dependiente.Id);
                if (turno == null)
                {
                    return RedirectToAction("Nuevo", "Turnos");
                }
                return RedirectToAction(nameof(ConsumoEnTurno), new { Id = turno.Id });
            }
            return View();
        }
        // GET: ControlesExistencias
        public IActionResult ConsumoEnTurno(int id)
        {
            var turno = _context.Set<Turno>().Include(t => t.Bar).SingleOrDefault(t => t.Id == id);
            var es = new ExistenciasService(_context);
            var data = es.ExistenciaDeBarPorTurno(id);
            ViewBag.Bar = turno.Bar.Nombre;
            ViewBag.Fecha = turno.FechaInicio;
            return View(data);
        }
    }
}
