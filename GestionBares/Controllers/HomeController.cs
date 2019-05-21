using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GestionBares.Models;
using Microsoft.AspNetCore.Authorization;
using GestionBares.Data;
using Microsoft.EntityFrameworkCore;
using GestionBares.Utils;

namespace GestionBares.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext context)
        {
            _db = context;
        }
        public IActionResult Index()
        {
            if (User.IsInRole(DefinicionRoles.Dependiente))
            {
                var dependiente = _db.Set<Dependiente>().SingleOrDefault(u => u.Usuario.UserName == User.Identity.Name);
                if (!_db.Set<Turno>().Any(t => t.DependienteId == dependiente.Id && t.Activo))
                {
                    ViewBag.Turno = "No existe un turno iniciado, inicie uno para comenzar a trabajar.";
                }
                else
                {
                    var turno = _db.Set<Turno>()
                        .Include(t => t.Bar)
                        .SingleOrDefault(t => t.DependienteId == dependiente.Id && t.Activo);
                    ViewBag.Turno = $"Usted tiene un turno abierto en el bar {turno.Bar.Nombre} con fecha {turno.FechaInicio.ToShortDateString()}.";
                }
            }
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
