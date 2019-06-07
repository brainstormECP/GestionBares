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
using GestionBares.ViewModels;
using GestionBares.Models.AlmacenModels;

namespace GestionBares.Controllers
{
    [Authorize(Roles = DefinicionRoles.Economia)]
    public class AprobacionEconomiaController : Controller
    {
        private readonly EconomiaDbContext _context;
        private readonly AlmacenDbContext _almacen;

        public AprobacionEconomiaController(EconomiaDbContext context)
        {
            _context = context;
            _almacen = new AlmacenDbContext();
        }

        public IActionResult Aprobar(int id, string url)
        {
            var control = _context.Set<ControlExistencia>()
                .Include(d => d.Detalles)
                .Include(d => d.Turno.Bar)
                .SingleOrDefault(d => !d.Aprobado && d.Id == id);
            if (control != null)
            {
                control.Aprobado = true;
                foreach (var item in control.Detalles)
                {
                    _almacen.Set<Control>().Add(new Control
                    {
                        Fecha = control.Fecha,
                        BarId = control.Turno.BarId,
                        ProductoId = item.ProductoId,
                        Cantidad = item.Cantidad
                    });
                }
                _context.SaveChanges();
            }
            return RedirectToAction("ResumenControles", "Reportes");
        }

        public IActionResult AprobarVentas(int id, string url)
        {
            var control = _context.Set<ControlExistenciaVenta>()
                .Include(d => d.Detalles)
                .Include(d => d.Turno.Bar)
                .SingleOrDefault(d => !d.Aprobado && d.Id == id);
            if (control != null)
            {
                control.Aprobado = true;
                foreach (var item in control.Detalles)
                {
                    _almacen.Set<Control>().Add(new Control
                    {
                        Fecha = control.Fecha,
                        BarId = control.Turno.BarId,
                        ProductoId = item.ProductoId,
                        Cantidad = item.Cantidad
                    });
                }
                _context.SaveChanges();
            }
            return RedirectToAction("ResumenVentas", "Reportes");
        }
    }
}
