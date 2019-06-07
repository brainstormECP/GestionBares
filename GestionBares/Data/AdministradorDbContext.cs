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
    public class AdministradorDbContext : IdentityDbContext
    {
        public AdministradorDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Dependiente> Dependientes { get; set; }
        public DbSet<Turno> Turnos { get; set; }
    }
}
