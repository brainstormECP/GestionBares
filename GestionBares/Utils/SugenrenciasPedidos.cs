using System;
using System.Collections.Generic;
using System.Linq;
using GestionBares.Data;
using GestionBares.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionBares.Utils
{
    class Promedio
    {
        public int ProductoId { get; set; }
        public double Cantidad { get; set; }
        public int Cuenta { get; set; }

    }
    public class SugerenciasPedidos
    {
        private DbContext _db;
        private ExistenciasService _existenciaService;

        public SugerenciasPedidos(DbContext context)
        {
            _db = context;
            _existenciaService = new ExistenciasService(context);
        }

        public List<DetalleControlExistencia> Sugerencias(int turnoId)
        {
            var result = new List<DetalleControlExistencia>();
            var turno = _db.Set<Turno>().FirstOrDefault(t => t.Id == turnoId);
            var turnosAnteriores = _db.Set<Turno>()
                .Where(t => t.FechaInicio < turno.FechaInicio && t.FechaInicio > turno.FechaInicio.AddMonths(-6) && t.BarId == turno.BarId)
                .ToList();
            var data = new List<Promedio>();
            foreach (var item in turnosAnteriores)
            {
                var existencias = _existenciaService.ExistenciaDeBarPorTurno(item.Id);
                foreach (var e in existencias)
                {
                    if (data.Any(d => d.ProductoId == e.ProductoId))
                    {
                        var temp = data.SingleOrDefault(d => d.ProductoId == e.ProductoId);
                        temp.Cantidad += e.Consumo;
                        temp.Cuenta++;
                    }
                    else
                    {
                        data.Add(new Promedio
                        {
                            ProductoId = e.ProductoId,
                            Cantidad = e.Consumo,
                            Cuenta = 1,
                        });
                    }
                }
            }
            var existenciasAnteriores = _existenciaService.ExistenciaAnterior(turno.BarId, 0, DateTime.Now);
            foreach (var item in existenciasAnteriores)
            {
                var temp = data.SingleOrDefault(d => d.ProductoId == item.ProductoId);
                if (temp != null)
                {
                    var diferencia = item.Cantidad - temp.Cantidad / temp.Cuenta;
                    if (diferencia < 0)
                    {
                        result.Add(new DetalleControlExistencia() { ProductoId = item.ProductoId, Producto = _db.Set<Producto>().Find(item.ProductoId), Cantidad = Math.Abs(diferencia) });
                    }
                }

            }
            return result;
        }
        public List<DetalleControlExistencia> SugerenciasVentas(int turnoId)
        {
            var result = new List<DetalleControlExistencia>();
            var turno = _db.Set<Turno>().FirstOrDefault(t => t.Id == turnoId);
            var turnosAnteriores = _db.Set<Turno>()
                .Where(t => t.FechaInicio < turno.FechaInicio && t.FechaInicio > turno.FechaInicio.AddMonths(-6) && t.BarId == turno.BarId).ToList();
            var data = new List<Promedio>();
            foreach (var item in turnosAnteriores)
            {
                var existencias = _existenciaService.ExistenciaVentaDeBarPorTurno(item.Id);
                foreach (var e in existencias)
                {
                    if (data.Any(d => d.ProductoId == e.ProductoId))
                    {
                        var temp = data.SingleOrDefault(d => d.ProductoId == e.ProductoId);
                        temp.Cantidad += e.Consumo;
                        temp.Cuenta++;
                    }
                    else
                    {
                        data.Add(new Promedio
                        {
                            ProductoId = e.ProductoId,
                            Cantidad = e.Consumo,
                            Cuenta = 1,
                        });
                    }
                }
            }
            var existenciasAnteriores = _existenciaService.ExistenciaVentaAnterior(turno.BarId, 0, DateTime.Now);
            foreach (var item in existenciasAnteriores)
            {
                var temp = data.SingleOrDefault(d => d.ProductoId == item.ProductoId);
                if (temp != null)
                {
                    var diferencia = item.Cantidad - temp.Cantidad / temp.Cuenta;
                    if (diferencia < 0)
                    {
                        result.Add(new DetalleControlExistencia() { ProductoId = item.ProductoId, Producto = _db.Set<Producto>().Find(item.ProductoId), Cantidad = Math.Abs(diferencia) });
                    }
                }

            }
            return result;
        }
    }
}