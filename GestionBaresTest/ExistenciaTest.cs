using System;
using System.Linq;
using GestionBares.Models;
using GestionBares.Utils;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace GestionBaresTest
{
    public class ExistenciaTest : BaseTest
    {
        public void AddExistencias()
        {
            for (int i = 1; i <= 4; i++)
            {
                //turno de un Bar
                var turno = new Turno { Id = i, Activo = false, BarId = 1, DependienteId = 1, FechaInicio = DateTime.Now.AddHours(-(8 - i)), FechaFin = DateTime.Now.AddHours(-(4 - i)) };
                _db.Add(turno);
                var turno2 = new Turno { Id = i * 10, Activo = false, BarId = 2, DependienteId = 2, FechaInicio = DateTime.Now.AddHours(-(8 - i)), FechaFin = DateTime.Now.AddHours(-(4 - i)) };
                _db.Add(turno2);
                var controlExistencia = new ControlExistencia { Id = i, Activo = false, Aprobado = true, TurnoId = i };
                _db.Add(controlExistencia);
                var controlExistenciaVenta = new ControlExistenciaVenta { Id = i, Activo = false, Aprobado = true, TurnoId = i };
                _db.Add(controlExistenciaVenta);
                var controlExistencia2 = new ControlExistencia { Id = i * 10, Activo = false, Aprobado = true, TurnoId = i * 10 };
                _db.Add(controlExistencia2);
                var controlExistenciaVenta2 = new ControlExistenciaVenta { Id = i * 10, Activo = false, Aprobado = true, TurnoId = i * 10 };
                _db.Add(controlExistenciaVenta2);
                for (int j = 1; j <= 4; j++)
                {
                    _db.Add(new DetalleControlExistencia { Id = i * 10 + j, ControlId = i, ProductoId = j, Costo = 0.5m, Cantidad = 10 - i });
                    _db.Add(new DetalleControlExistenciaVenta { Id = i * 10 + j, ControlId = i, ProductoId = j, Costo = 0.5m, Cantidad = 10 - i });
                    _db.Add(new DetalleControlExistencia { Id = i * 100 + j, ControlId = i * 10, ProductoId = j, Costo = 0.5m, Cantidad = 10 - i });
                    _db.Add(new DetalleControlExistenciaVenta { Id = i * 100 + j, ControlId = i * 10, ProductoId = j, Costo = 0.5m, Cantidad = 10 - i });
                }
            }
            _db.SaveChanges();
        }
        [Fact]
        public void ControlExistenciaTest()
        {
            AddExistencias();
            var existenciaService = new ExistenciasService(_db);
            Assert.Equal(1, existenciaService.ExistenciaDeBarPorTurno(2).SingleOrDefault(e => e.ProductoId == 1).Consumo);
        }
    }
}
