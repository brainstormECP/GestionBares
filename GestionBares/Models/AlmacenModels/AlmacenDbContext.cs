using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace GestionBares.Models.AlmacenModels
{
    public class AlmacenDbContext : DbContext
    {
        public AlmacenDbContext() : base((new DbContextOptionsBuilder<AlmacenDbContext>() { }).UseSqlServer("Server=localhost;Database=gestion_bares_almacen;User Id=sa;Password=admin123*;").Options)
        {

        }
        public AlmacenDbContext(DbContextOptions<AlmacenDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Existencia>().HasData(new Existencia[] {
                new Existencia {Id = 1, CodigoProducto="1",Cantidad = 10},
                new Existencia {Id = 2, CodigoProducto="2",Cantidad = 20},
                new Existencia {Id = 3, CodigoProducto="3",Cantidad = 30},
                new Existencia {Id = 4, CodigoProducto="4",Cantidad = 10},
                new Existencia {Id = 5, CodigoProducto="5",Cantidad = 20},
                new Existencia {Id = 6, CodigoProducto="6",Cantidad = 30},
                new Existencia {Id = 7, CodigoProducto="7",Cantidad = 10},
                new Existencia {Id = 8, CodigoProducto="8",Cantidad = 10},
                new Existencia {Id = 9, CodigoProducto="9",Cantidad = 10},
                new Existencia {Id = 10, CodigoProducto="10",Cantidad = 10},
                new Existencia {Id = 11, CodigoProducto="11",Cantidad = 10},
                new Existencia {Id = 12, CodigoProducto="12",Cantidad = 10},
                new Existencia {Id = 13, CodigoProducto="13",Cantidad = 10},
                new Existencia {Id = 14, CodigoProducto="14",Cantidad = 10},
                new Existencia {Id = 15, CodigoProducto="15",Cantidad = 10},
                new Existencia {Id = 16, CodigoProducto="16",Cantidad = 10},
                new Existencia {Id = 17, CodigoProducto="17",Cantidad = 10},
                new Existencia {Id = 18, CodigoProducto="18",Cantidad = 10},
                new Existencia {Id = 19, CodigoProducto="19",Cantidad = 10},
                new Existencia {Id = 20, CodigoProducto="20",Cantidad = 10},
                new Existencia {Id = 21, CodigoProducto="21",Cantidad = 10},
                new Existencia {Id = 22, CodigoProducto="22",Cantidad = 10},
             });
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Existencia> Existencias { get; set; }
        public DbSet<Control> Controles { get; set; }
    }
}
