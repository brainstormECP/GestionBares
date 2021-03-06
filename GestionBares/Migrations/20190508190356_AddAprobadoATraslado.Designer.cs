﻿// <auto-generated />
using System;
using GestionBares.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GestionBares.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190508190356_AddAprobadoATraslado")]
    partial class AddAprobadoATraslado
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GestionBares.Models.Bar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nombre");

                    b.HasKey("Id");

                    b.ToTable("Bares");
                });

            modelBuilder.Entity("GestionBares.Models.ControlExistencia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activo");

                    b.Property<DateTime>("Fecha");

                    b.Property<int>("TurnoId");

                    b.HasKey("Id");

                    b.HasIndex("TurnoId");

                    b.ToTable("ControlesDeExistencias");
                });

            modelBuilder.Entity("GestionBares.Models.ControlExistenciaVenta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activo");

                    b.Property<DateTime>("Fecha");

                    b.Property<int>("TurnoId");

                    b.HasKey("Id");

                    b.HasIndex("TurnoId");

                    b.ToTable("ControlesDeExistenciasVenta");
                });

            modelBuilder.Entity("GestionBares.Models.Dependiente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activo");

                    b.Property<string>("Apellidos")
                        .IsRequired();

                    b.Property<string>("Nombres")
                        .IsRequired();

                    b.Property<string>("UsuarioId");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Dependientes");
                });

            modelBuilder.Entity("GestionBares.Models.DependienteBar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BarId");

                    b.Property<int>("DependienteId");

                    b.HasKey("Id");

                    b.HasIndex("BarId");

                    b.HasIndex("DependienteId");

                    b.ToTable("DependientesBares");
                });

            modelBuilder.Entity("GestionBares.Models.DetalleControlExistencia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Cantidad");

                    b.Property<int>("ControlId");

                    b.Property<int>("ProductoId");

                    b.HasKey("Id");

                    b.HasIndex("ControlId");

                    b.HasIndex("ProductoId");

                    b.ToTable("DetallesControlesDeExistencias");
                });

            modelBuilder.Entity("GestionBares.Models.DetalleControlExistenciaVenta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Cantidad");

                    b.Property<int>("ControlId");

                    b.Property<int>("ProductoId");

                    b.HasKey("Id");

                    b.HasIndex("ControlId");

                    b.HasIndex("ProductoId");

                    b.ToTable("DetallesControlesDeExistenciasVenta");
                });

            modelBuilder.Entity("GestionBares.Models.DetallePedidoAlmacen", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Atendido");

                    b.Property<double>("Cantidad");

                    b.Property<int>("PedidoId");

                    b.Property<int>("ProductoId");

                    b.HasKey("Id");

                    b.HasIndex("PedidoId");

                    b.HasIndex("ProductoId");

                    b.ToTable("DetallesPedidosDeAlmacen");
                });

            modelBuilder.Entity("GestionBares.Models.DetallePedidoAlmacenVenta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Atendido");

                    b.Property<double>("Cantidad");

                    b.Property<int>("PedidoId");

                    b.Property<int>("ProductoId");

                    b.HasKey("Id");

                    b.HasIndex("PedidoId");

                    b.HasIndex("ProductoId");

                    b.ToTable("DetallesPedidosDeAlmacenVenta");
                });

            modelBuilder.Entity("GestionBares.Models.EntregaDeAlmacen", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Cantidad");

                    b.Property<int>("ProductoId");

                    b.Property<int>("TurnoId");

                    b.HasKey("Id");

                    b.HasIndex("ProductoId");

                    b.HasIndex("TurnoId");

                    b.ToTable("EntregasDeAlmacen");
                });

            modelBuilder.Entity("GestionBares.Models.EntregaDeAlmacenVenta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Cantidad");

                    b.Property<int>("ProductoId");

                    b.Property<int>("TurnoId");

                    b.HasKey("Id");

                    b.HasIndex("ProductoId");

                    b.HasIndex("TurnoId");

                    b.ToTable("EntregasDeAlmacenVenta");
                });

            modelBuilder.Entity("GestionBares.Models.FamiliaDeProducto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nombre")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("FamiliasDeProductos");
                });

            modelBuilder.Entity("GestionBares.Models.PedidoAlmacen", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("TurnoId");

                    b.HasKey("Id");

                    b.HasIndex("TurnoId");

                    b.ToTable("PedidosDeAlmacen");
                });

            modelBuilder.Entity("GestionBares.Models.PedidoAlmacenVenta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("TurnoId");

                    b.HasKey("Id");

                    b.HasIndex("TurnoId");

                    b.ToTable("PedidosDeAlmacenVenta");
                });

            modelBuilder.Entity("GestionBares.Models.Producto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Codigo")
                        .IsRequired();

                    b.Property<decimal>("Costo");

                    b.Property<int>("FamiliaId");

                    b.Property<string>("Nombre")
                        .IsRequired();

                    b.Property<decimal>("Precio");

                    b.Property<int>("UnidadId");

                    b.HasKey("Id");

                    b.HasIndex("FamiliaId");

                    b.HasIndex("UnidadId");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("GestionBares.Models.Standard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BarId");

                    b.Property<int>("ProductoId");

                    b.HasKey("Id");

                    b.HasIndex("BarId");

                    b.HasIndex("ProductoId");

                    b.ToTable("Standards");
                });

            modelBuilder.Entity("GestionBares.Models.StandardVenta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BarId");

                    b.Property<int>("ProductoId");

                    b.HasKey("Id");

                    b.HasIndex("BarId");

                    b.HasIndex("ProductoId");

                    b.ToTable("StandardVentas");
                });

            modelBuilder.Entity("GestionBares.Models.Traslado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Aprobado");

                    b.Property<double>("Cantidad");

                    b.Property<int>("DestinoId");

                    b.Property<DateTime>("Fecha");

                    b.Property<int>("ProductoId");

                    b.Property<int>("TurnoId");

                    b.Property<string>("UsuarioId");

                    b.HasKey("Id");

                    b.HasIndex("DestinoId");

                    b.HasIndex("ProductoId");

                    b.HasIndex("TurnoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Traslados");
                });

            modelBuilder.Entity("GestionBares.Models.TrasladoVenta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Cantidad");

                    b.Property<int>("DestinoId");

                    b.Property<DateTime>("Fecha");

                    b.Property<int>("ProductoId");

                    b.Property<int>("TurnoId");

                    b.Property<string>("UsuarioId");

                    b.HasKey("Id");

                    b.HasIndex("DestinoId");

                    b.HasIndex("ProductoId");

                    b.HasIndex("TurnoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("TrasladosVenta");
                });

            modelBuilder.Entity("GestionBares.Models.Turno", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activo");

                    b.Property<int>("BarId");

                    b.Property<int>("DependienteId");

                    b.Property<DateTime?>("FechaFin");

                    b.Property<DateTime>("FechaInicio");

                    b.HasKey("Id");

                    b.HasIndex("BarId");

                    b.HasIndex("DependienteId");

                    b.ToTable("Turnos");
                });

            modelBuilder.Entity("GestionBares.Models.UnidadDeMedida", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nombre")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("UnidadesDeMedidas");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new { Id = "1", ConcurrencyStamp = "7e05f4be-aed9-4e38-aeb3-a3f720c093e1", Name = "ADMINISTRADOR", NormalizedName = "ADMINISTRADOR" },
                        new { Id = "2", ConcurrencyStamp = "ec9c4da8-31a5-48cd-a683-6ecd547a4edc", Name = "DEPENDIENTE", NormalizedName = "DEPENDIENTE" },
                        new { Id = "3", ConcurrencyStamp = "1e624af6-988e-4f66-ad00-e841d22156df", Name = "AUDITOR", NormalizedName = "AUDITOR" },
                        new { Id = "4", ConcurrencyStamp = "a8962e98-4b6e-4bb4-8551-e713f8b7a888", Name = "A+B", NormalizedName = "A+B" }
                    );
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");

                    b.HasData(
                        new { UserId = "f42559a2-2776-4e9b-9ba1-268597eff72b", RoleId = "1" }
                    );
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("GestionBares.Models.Usuario", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<bool>("Activo");

                    b.Property<string>("Nombre");

                    b.ToTable("Usuario");

                    b.HasDiscriminator().HasValue("Usuario");

                    b.HasData(
                        new { Id = "f42559a2-2776-4e9b-9ba1-268597eff72b", AccessFailedCount = 0, ConcurrencyStamp = "36fd2616-8e8a-4cc6-8a5a-52d963207836", Email = "admin@patriarca.cu", EmailConfirmed = false, LockoutEnabled = false, NormalizedEmail = "ADMIN@PATRIARCA.CU", NormalizedUserName = "ADMIN", PasswordHash = "AQAAAAEAACcQAAAAEP4OedI6m26WUn/2C4AcBkzdT6SnL/6E+xakQ/9mGAkqqp3t9PwyIR6l9obLouKIVg==", PhoneNumberConfirmed = false, SecurityStamp = "43VMKYQKNTENYZVJNU2TII26X23H5PGV", TwoFactorEnabled = false, UserName = "admin", Activo = true, Nombre = "admin" }
                    );
                });

            modelBuilder.Entity("GestionBares.Models.ControlExistencia", b =>
                {
                    b.HasOne("GestionBares.Models.Turno", "Turno")
                        .WithMany()
                        .HasForeignKey("TurnoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GestionBares.Models.ControlExistenciaVenta", b =>
                {
                    b.HasOne("GestionBares.Models.Turno", "Turno")
                        .WithMany()
                        .HasForeignKey("TurnoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GestionBares.Models.Dependiente", b =>
                {
                    b.HasOne("GestionBares.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId");
                });

            modelBuilder.Entity("GestionBares.Models.DependienteBar", b =>
                {
                    b.HasOne("GestionBares.Models.Bar", "Bar")
                        .WithMany()
                        .HasForeignKey("BarId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GestionBares.Models.Dependiente", "Dependiente")
                        .WithMany()
                        .HasForeignKey("DependienteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GestionBares.Models.DetalleControlExistencia", b =>
                {
                    b.HasOne("GestionBares.Models.ControlExistencia", "Control")
                        .WithMany("Detalles")
                        .HasForeignKey("ControlId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GestionBares.Models.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GestionBares.Models.DetalleControlExistenciaVenta", b =>
                {
                    b.HasOne("GestionBares.Models.ControlExistenciaVenta", "Control")
                        .WithMany("Detalles")
                        .HasForeignKey("ControlId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GestionBares.Models.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GestionBares.Models.DetallePedidoAlmacen", b =>
                {
                    b.HasOne("GestionBares.Models.PedidoAlmacen", "Pedido")
                        .WithMany("Detalles")
                        .HasForeignKey("PedidoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GestionBares.Models.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GestionBares.Models.DetallePedidoAlmacenVenta", b =>
                {
                    b.HasOne("GestionBares.Models.PedidoAlmacenVenta", "Pedido")
                        .WithMany("Detalles")
                        .HasForeignKey("PedidoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GestionBares.Models.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GestionBares.Models.EntregaDeAlmacen", b =>
                {
                    b.HasOne("GestionBares.Models.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GestionBares.Models.Turno", "Turno")
                        .WithMany()
                        .HasForeignKey("TurnoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GestionBares.Models.EntregaDeAlmacenVenta", b =>
                {
                    b.HasOne("GestionBares.Models.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GestionBares.Models.Turno", "Turno")
                        .WithMany()
                        .HasForeignKey("TurnoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GestionBares.Models.PedidoAlmacen", b =>
                {
                    b.HasOne("GestionBares.Models.Turno", "Turno")
                        .WithMany()
                        .HasForeignKey("TurnoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GestionBares.Models.PedidoAlmacenVenta", b =>
                {
                    b.HasOne("GestionBares.Models.Turno", "Turno")
                        .WithMany()
                        .HasForeignKey("TurnoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GestionBares.Models.Producto", b =>
                {
                    b.HasOne("GestionBares.Models.FamiliaDeProducto", "Familia")
                        .WithMany()
                        .HasForeignKey("FamiliaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GestionBares.Models.UnidadDeMedida", "Unidad")
                        .WithMany()
                        .HasForeignKey("UnidadId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GestionBares.Models.Standard", b =>
                {
                    b.HasOne("GestionBares.Models.Bar", "Bar")
                        .WithMany()
                        .HasForeignKey("BarId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GestionBares.Models.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GestionBares.Models.StandardVenta", b =>
                {
                    b.HasOne("GestionBares.Models.Bar", "Bar")
                        .WithMany()
                        .HasForeignKey("BarId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GestionBares.Models.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GestionBares.Models.Traslado", b =>
                {
                    b.HasOne("GestionBares.Models.Bar", "Destino")
                        .WithMany()
                        .HasForeignKey("DestinoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("GestionBares.Models.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GestionBares.Models.Turno", "Turno")
                        .WithMany()
                        .HasForeignKey("TurnoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("GestionBares.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId");
                });

            modelBuilder.Entity("GestionBares.Models.TrasladoVenta", b =>
                {
                    b.HasOne("GestionBares.Models.Bar", "Destino")
                        .WithMany()
                        .HasForeignKey("DestinoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("GestionBares.Models.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GestionBares.Models.Turno", "Turno")
                        .WithMany()
                        .HasForeignKey("TurnoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("GestionBares.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId");
                });

            modelBuilder.Entity("GestionBares.Models.Turno", b =>
                {
                    b.HasOne("GestionBares.Models.Bar", "Bar")
                        .WithMany()
                        .HasForeignKey("BarId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GestionBares.Models.Dependiente", "Dependiente")
                        .WithMany()
                        .HasForeignKey("DependienteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
