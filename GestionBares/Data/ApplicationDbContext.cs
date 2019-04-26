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
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Traslado>().HasOne(t => t.Turno).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Traslado>().HasOne(t => t.Destino).WithMany().OnDelete(DeleteBehavior.Restrict);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole[] {
                new IdentityRole {Id = "1", Name = DefinicionRoles.Administrador, NormalizedName = DefinicionRoles.Administrador },
                new IdentityRole {Id = "2", Name = DefinicionRoles.Dependiente, NormalizedName = DefinicionRoles.Dependiente },
                new IdentityRole {Id = "3", Name = DefinicionRoles.Almacen, NormalizedName = DefinicionRoles.Almacen },
                new IdentityRole {Id = "4", Name = DefinicionRoles.Economia, NormalizedName = DefinicionRoles.Economia },
                new IdentityRole {Id = "5", Name = DefinicionRoles.AplusB, NormalizedName = DefinicionRoles.AplusB },
             });
            modelBuilder.Entity<Usuario>().HasData(new Usuario
            {
                Id = "f42559a2-2776-4e9b-9ba1-268597eff72b",
                Nombre = "admin",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@patriarca.cu",
                NormalizedEmail = "ADMIN@PATRIARCA.CU",
                Activo = true,
                PasswordHash = "AQAAAAEAACcQAAAAEP4OedI6m26WUn/2C4AcBkzdT6SnL/6E+xakQ/9mGAkqqp3t9PwyIR6l9obLouKIVg==",
                SecurityStamp = "43VMKYQKNTENYZVJNU2TII26X23H5PGV",
                ConcurrencyStamp = "36fd2616-8e8a-4cc6-8a5a-52d963207836",
            });
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { UserId = "f42559a2-2776-4e9b-9ba1-268597eff72b", RoleId = "1" });
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

    }
}
