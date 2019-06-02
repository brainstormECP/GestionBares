using System;
using GestionBares.Data;
using GestionBares.Models;
using GestionBares.Models.AlmacenModels;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace GestionBaresTest
{
    public class BaseTest : IDisposable
    {
        public ApplicationDbContext _db { get; set; }
        public AlmacenDbContext _dbAlmacen { get; set; }
        public BaseTest()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;
            _db = new ApplicationDbContext(options);
            _db.Database.EnsureCreated();
            AddData();

            DbContextOptions<AlmacenDbContext> optionsAlmacen = new DbContextOptionsBuilder<AlmacenDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;
            _dbAlmacen = new AlmacenDbContext(optionsAlmacen);

            _dbAlmacen.Database.EnsureCreated();
        }

        public void AddData()
        {
            var unidad = new UnidadDeMedida { Id = 1, Nombre = "Unidad" };
            _db.Add(unidad);
            var familia = new FamiliaDeProducto { Id = 1, Nombre = "Refrescos" };
            _db.Add(familia);
            var producto1 = new Producto { Id = 1, Nombre = "TuKola", Codigo = "1", Precio = 1, Costo = 0.5m, FamiliaId = 1, UnidadId = 1 };
            var producto2 = new Producto { Id = 2, Nombre = "Coca Cola", Codigo = "2", Precio = 1.5m, Costo = 0.75m, FamiliaId = 1, UnidadId = 1 };
            var producto3 = new Producto { Id = 3, Nombre = "Spark", Codigo = "3", Precio = 1.75m, Costo = 0.9m, FamiliaId = 1, UnidadId = 1 };
            var producto4 = new Producto { Id = 4, Nombre = "7 Up", Codigo = "4", Precio = 3, Costo = 1, FamiliaId = 1, UnidadId = 1 };
            _db.Add(producto1);
            _db.Add(producto2);
            _db.Add(producto3);
            _db.Add(producto4);
            var dependiente1 = new Dependiente { Id = 1, Nombres = "Pepe", Apellidos = "Perez", Activo = true, UsuarioId = "f42559a2-2776-4e9b-9ba1-268597eff72b" };
            var dependiente2 = new Dependiente { Id = 2, Nombres = "Pedro", Apellidos = "Garcia", Activo = true, UsuarioId = "f42559a2-2776-4e9b-9ba1-268597eff72b" };
            _db.Add(dependiente1);
            _db.Add(dependiente2);
            var bar1 = new Bar { Id = 1, Nombre = "Piscina" };
            var bar2 = new Bar { Id = 2, Nombre = "Lobby" };
            _db.Add(bar1);
            _db.Add(bar2);
            _db.Add(new DependienteBar { Id = 1, BarId = 1, DependienteId = 1 });
            _db.Add(new DependienteBar { Id = 2, BarId = 1, DependienteId = 2 });
            _db.Add(new DependienteBar { Id = 3, BarId = 2, DependienteId = 1 });
            _db.Add(new DependienteBar { Id = 4, BarId = 2, DependienteId = 2 });
            //standard
            _db.Add(new Standard { Id = 1, BarId = 1, ProductoId = 1 });
            _db.Add(new Standard { Id = 2, BarId = 2, ProductoId = 1 });
            _db.Add(new Standard { Id = 3, BarId = 1, ProductoId = 2 });
            _db.Add(new Standard { Id = 4, BarId = 2, ProductoId = 2 });
            _db.Add(new Standard { Id = 5, BarId = 1, ProductoId = 3 });
            _db.Add(new Standard { Id = 6, BarId = 2, ProductoId = 3 });
            _db.Add(new Standard { Id = 7, BarId = 1, ProductoId = 4 });
            _db.Add(new Standard { Id = 8, BarId = 2, ProductoId = 4 });
            //standard de venta
            _db.Add(new StandardVenta { Id = 1, BarId = 1, ProductoId = 1 });
            _db.Add(new StandardVenta { Id = 2, BarId = 2, ProductoId = 1 });
            _db.Add(new StandardVenta { Id = 3, BarId = 1, ProductoId = 2 });
            _db.Add(new StandardVenta { Id = 4, BarId = 2, ProductoId = 2 });
            _db.Add(new StandardVenta { Id = 5, BarId = 1, ProductoId = 3 });
            _db.Add(new StandardVenta { Id = 6, BarId = 2, ProductoId = 3 });
            _db.Add(new StandardVenta { Id = 7, BarId = 1, ProductoId = 4 });
            _db.Add(new StandardVenta { Id = 8, BarId = 2, ProductoId = 4 });

            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Database.EnsureDeleted();
            _dbAlmacen.Database.EnsureDeleted();
            _db.Dispose();
            _dbAlmacen.Dispose();
        }
    }
}
