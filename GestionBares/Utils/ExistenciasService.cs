using System;
using System.Collections.Generic;
using System.Linq;
using GestionBares.Data;
using GestionBares.Models;
using GestionBares.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace GestionBares.Utils
{
    public class ExistenciasService
    {
        private readonly ApplicationDbContext _db;

        public ExistenciasService(ApplicationDbContext context)
        {
            _db = context;
        }

        public List<MovimientoVM> ExistenciaDeBar(int barId)
        {
            var turno = _db.Set<Turno>().Where(t => t.BarId == barId).OrderBy(t => t.FechaInicio).Last();
            return ExistenciaDeBarPorTurno(turno.Id);
        }

        public List<MovimientoVM> ExistenciaDeBarPorTurno(int turnoId)
        {
            var turno = _db.Set<Turno>().FirstOrDefault(t => t.Id == turnoId);
            var turnoAnterior = _db.Set<Turno>().Where(t => t.Id != turnoId && t.BarId == turno.BarId).OrderBy(t => t.FechaFin).Last();
            var result = new List<MovimientoVM>();
            var ultimoControl = _db.Set<ControlExistencia>()
                .Any(c => c.TurnoId == turnoAnterior.Id) ? _db.Set<ControlExistencia>()
                .Include(c => c.Detalles).ThenInclude(c => c.Producto)
                .Where(c => c.TurnoId == turnoAnterior.Id).OrderBy(c => c.Fecha).Last() : null;
            if (ultimoControl == null) return new List<MovimientoVM>();
            foreach (var detalle in ultimoControl.Detalles)
            {
                result.Add(new MovimientoVM { ProductoId = detalle.ProductoId, Producto = detalle.Producto.Nombre, Inicio = detalle.Cantidad, Costo = detalle.Costo });
            }
            var entradas = _db.Set<EntregaDeAlmacen>()
                .Include(e => e.Producto)
                .Where(e => e.TurnoId == turnoId);
            foreach (var detalle in entradas)
            {
                if (result.Any(r => r.ProductoId == detalle.ProductoId))
                {
                    var data = result.FirstOrDefault(r => r.ProductoId == detalle.ProductoId);
                    data.Entradas = detalle.Cantidad;
                }
                else
                {
                    result.Add(new MovimientoVM { ProductoId = detalle.ProductoId, Producto = detalle.Producto.Nombre, Entradas = detalle.Cantidad, Costo = detalle.Producto.Costo });
                }
            }
            var trasladosSalidas = _db.Set<Traslado>()
                .Include(t => t.Producto)
                .Where(t => t.TurnoId == turnoId && t.Aprobado);
            foreach (var detalle in trasladosSalidas)
            {
                if (result.Any(r => r.ProductoId == detalle.ProductoId))
                {
                    var data = result.FirstOrDefault(r => r.ProductoId == detalle.ProductoId);
                    data.Enviados = detalle.Cantidad;
                }
                else
                {
                    result.Add(new MovimientoVM { ProductoId = detalle.ProductoId, Producto = detalle.Producto.Nombre, Enviados = detalle.Cantidad, Costo = detalle.Producto.Costo });
                }
            }
            var trasladosEntradas = _db.Set<Traslado>()
                .Include(t => t.Producto)
                .Where(t => t.Fecha >= turno.FechaInicio && t.Fecha <= (turno.FechaFin ?? DateTime.Now) && t.DestinoId == turno.BarId && t.Aprobado);
            foreach (var detalle in trasladosEntradas)
            {
                if (result.Any(r => r.ProductoId == detalle.ProductoId))
                {
                    var data = result.FirstOrDefault(r => r.ProductoId == detalle.ProductoId);
                    data.Recibidos = detalle.Cantidad;
                }
                else
                {
                    result.Add(new MovimientoVM { ProductoId = detalle.ProductoId, Producto = detalle.Producto.Nombre, Recibidos = detalle.Cantidad, Costo = detalle.Producto.Costo });
                }
            }
            var control = _db.Set<ControlExistencia>()
                .Any(c => c.TurnoId == turno.Id) ? _db.Set<ControlExistencia>()
                .Include(c => c.Detalles).ThenInclude(c => c.Producto)
                .Where(c => c.TurnoId == turno.Id).OrderBy(c => c.Fecha).Last() : null;
            if (control != null)
            {
                foreach (var detalle in control.Detalles)
                {
                    if (result.Any(r => r.ProductoId == detalle.ProductoId))
                    {
                        var data = result.FirstOrDefault(r => r.ProductoId == detalle.ProductoId);
                        data.Fin = detalle.Cantidad;
                    }
                    else
                    {
                        result.Add(new MovimientoVM { ProductoId = detalle.ProductoId, Producto = detalle.Producto.Nombre, Fin = detalle.Cantidad, Costo = detalle.Costo });
                    }
                }
            }
            return result;
        }

        public List<MovimientoVM> ExistenciaVentaDeBar(int barId)
        {
            var turno = _db.Set<Turno>().Where(t => t.BarId == barId).OrderBy(t => t.FechaInicio).Last();
            return ExistenciaVentaDeBarPorTurno(turno.Id);
        }

        public List<MovimientoVM> ExistenciaVentaDeBarPorTurno(int turnoId)
        {
            var turno = _db.Set<Turno>().FirstOrDefault(t => t.Id == turnoId);
            var turnoAnterior = _db.Set<Turno>().Where(t => t.Id != turnoId && t.BarId == turno.BarId).OrderBy(t => t.FechaFin).Last();
            var result = new List<MovimientoVM>();
            var ultimoControl = _db.Set<ControlExistenciaVenta>()
                .Any(c => c.TurnoId == turnoAnterior.Id) ? _db.Set<ControlExistenciaVenta>()
                .Include(c => c.Detalles).ThenInclude(c => c.Producto)
                .Where(c => c.TurnoId == turnoAnterior.Id).OrderBy(c => c.Fecha).Last() : null;
            if (ultimoControl == null)
            {
                return new List<MovimientoVM>();
            }
            foreach (var detalle in ultimoControl.Detalles)
            {
                result.Add(new MovimientoVM { ProductoId = detalle.ProductoId, Producto = detalle.Producto.Nombre, Inicio = detalle.Cantidad, Costo = detalle.Costo, Precio = detalle.Precio });
            }
            var entradas = _db.Set<EntregaDeAlmacenVenta>()
                .Include(e => e.Producto)
                .Where(e => e.TurnoId == turnoId);
            foreach (var detalle in entradas)
            {
                if (result.Any(r => r.ProductoId == detalle.ProductoId))
                {
                    var data = result.FirstOrDefault(r => r.ProductoId == detalle.ProductoId);
                    data.Entradas = detalle.Cantidad;
                }
                else
                {
                    result.Add(new MovimientoVM { ProductoId = detalle.ProductoId, Producto = detalle.Producto.Nombre, Entradas = detalle.Cantidad, Costo = detalle.Producto.Costo, Precio = detalle.Producto.Precio });
                }
            }
            var trasladosSalidas = _db.Set<TrasladoVenta>().Where(t => t.TurnoId == turnoId && t.Aprobado);
            foreach (var detalle in trasladosSalidas)
            {
                if (result.Any(r => r.ProductoId == detalle.ProductoId))
                {
                    var data = result.FirstOrDefault(r => r.ProductoId == detalle.ProductoId);
                    data.Enviados = detalle.Cantidad;
                }
                else
                {
                    result.Add(new MovimientoVM { ProductoId = detalle.ProductoId, Producto = detalle.Producto.Nombre, Enviados = detalle.Cantidad, Costo = detalle.Producto.Costo, Precio = detalle.Producto.Precio });
                }
            }
            var trasladosEntradas = _db.Set<TrasladoVenta>()
                .Where(t => t.Fecha >= turno.FechaInicio && t.Fecha <= (turno.FechaFin ?? DateTime.Now) && t.DestinoId == turno.BarId && t.Aprobado);
            foreach (var detalle in trasladosEntradas)
            {
                if (result.Any(r => r.ProductoId == detalle.ProductoId))
                {
                    var data = result.FirstOrDefault(r => r.ProductoId == detalle.ProductoId);
                    data.Recibidos = detalle.Cantidad;
                }
                else
                {
                    result.Add(new MovimientoVM { ProductoId = detalle.ProductoId, Producto = detalle.Producto.Nombre, Recibidos = detalle.Cantidad, Costo = detalle.Producto.Costo, Precio = detalle.Producto.Precio });
                }
            }
            var control = _db.Set<ControlExistenciaVenta>()
                .Any(c => c.TurnoId == turno.Id) ? _db.Set<ControlExistenciaVenta>()
                .Include(c => c.Detalles).ThenInclude(c => c.Producto)
                .Where(c => c.TurnoId == turno.Id).OrderBy(c => c.Fecha).Last() : null;
            if (control != null)
            {
                foreach (var detalle in control.Detalles)
                {
                    if (result.Any(r => r.ProductoId == detalle.ProductoId))
                    {
                        var data = result.FirstOrDefault(r => r.ProductoId == detalle.ProductoId);
                        data.Fin = detalle.Cantidad;
                    }
                    else
                    {
                        result.Add(new MovimientoVM { ProductoId = detalle.ProductoId, Producto = detalle.Producto.Nombre, Fin = detalle.Cantidad, Costo = detalle.Costo, Precio = detalle.Precio });
                    }
                }
            }
            return result;
        }

    }
}