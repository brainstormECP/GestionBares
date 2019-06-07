using System;
using System.Collections.Generic;
using System.Text;
using GestionBares.Models;
using GestionBares.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestionBares.Data
{
    public class DependienteDbContext : IdentityDbContext
    {
        public DependienteDbContext(DbContextOptions<DependienteDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Traslado>().HasOne(t => t.Turno).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<TrasladoVenta>().HasOne(t => t.Turno).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Traslado>().HasOne(t => t.Destino).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<TrasladoVenta>().HasOne(t => t.Destino).WithMany().OnDelete(DeleteBehavior.Restrict);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Bar> Bares { get; set; }
        public DbSet<Dependiente> Dependientes { get; set; }
        public DbSet<FamiliaDeProducto> FamiliasDeProductos { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Turno> Turnos { get; set; }
        public DbSet<ControlExistencia> ControlesDeExistencias { get; set; }
        public DbSet<DetalleControlExistencia> DetallesControlesDeExistencias { get; set; }
        public DbSet<UnidadDeMedida> UnidadesDeMedidas { get; set; }
        public DbSet<EntregaDeAlmacen> EntregasDeAlmacen { get; set; }
        public DbSet<PedidoAlmacen> PedidosDeAlmacen { get; set; }
        public DbSet<DetallePedidoAlmacen> DetallesPedidosDeAlmacen { get; set; }
        public DbSet<Traslado> Traslados { get; set; }
        public DbSet<Standard> Standards { get; set; }
        public DbSet<StandardVenta> StandardVentas { get; set; }
        public DbSet<DependienteBar> DependientesBares { get; set; }
        public DbSet<EntregaDeAlmacenVenta> EntregasDeAlmacenVenta { get; set; }
        public DbSet<TrasladoVenta> TrasladosVenta { get; set; }
        public DbSet<ControlExistenciaVenta> ControlesDeExistenciasVenta { get; set; }
        public DbSet<DetalleControlExistenciaVenta> DetallesControlesDeExistenciasVenta { get; set; }
    }
}
