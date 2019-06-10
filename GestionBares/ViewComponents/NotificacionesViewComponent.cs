using GestionBares.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionBares.Models;
using GestionBares.ViewModels;
using GestionBares.Utils;

namespace GestionBares.ViewComponents
{

    public class NotificacionesViewComponent : ViewComponent
    {
        private readonly DbContext _db;

        public NotificacionesViewComponent(DbContext context)
        {
            _db = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var notificaciones = new List<NotificacionVM>();

            var traslados = _db.Set<Traslado>()
                .Include(t => t.Destino)
                .Include(t => t.Turno.Bar)
                .Include(t => t.Turno.Dependiente)
                .Include(t => t.Producto)
                .ToList();
            var trasladosVentas = _db.Set<TrasladoVenta>()
                .Include(t => t.Destino)
                .Include(t => t.Turno.Bar)
                .Include(t => t.Turno.Dependiente)
                .Include(t => t.Producto)
                .ToList();
            if (User.IsInRole(DefinicionRoles.Dependiente))
            {
                var dependiente = _db.Set<Dependiente>().SingleOrDefault(u => u.Usuario.UserName == User.Identity.Name);
                if (_db.Set<Turno>().Any(t => t.DependienteId == dependiente.Id && t.Activo))
                {
                    var turno = _db.Set<Turno>().SingleOrDefault(t => t.DependienteId == dependiente.Id && t.Activo);
                    traslados = traslados.Where(t => t.DestinoId == turno.BarId && t.Fecha >= turno.FechaInicio).ToList();
                    trasladosVentas = trasladosVentas.Where(t => t.DestinoId == turno.BarId && t.Fecha >= turno.FechaInicio).ToList();
                    if (traslados.Any())
                    {
                        notificaciones.Add(new NotificacionVM { Descripcion = $"Existen {traslados.Count } traslados pendientes por recibir.", Url = "/trasladosrecibidos" });
                    }
                    if (trasladosVentas.Any())
                    {
                        notificaciones.Add(new NotificacionVM { Descripcion = $"Existen {trasladosVentas.Count } traslados de venta pendientes por recibir.", Url = "/trasladosrecibidos/deventas" });
                    }
                }
            }
            return View(notificaciones);
        }
    }
}
