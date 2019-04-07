using System;
using System.Collections.Generic;
using System.Text;
using GestionBares.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestionBares.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Traslado>().HasOne(t => t.Origen).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Traslado>().HasOne(t => t.Destino).WithMany().OnDelete(DeleteBehavior.Restrict);
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
    }
}
