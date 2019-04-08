using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GestionBares.Data.Migrations
{
    public partial class migOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Bares",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bares", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dependientes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: false),
                    UsuarioId = table.Column<int>(nullable: false),
                    UsuarioId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dependientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dependientes_AspNetUsers_UsuarioId1",
                        column: x => x.UsuarioId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FamiliasDeProductos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamiliasDeProductos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnidadesDeMedidas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadesDeMedidas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Turnos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FechaInicio = table.Column<DateTime>(nullable: false),
                    FechaFin = table.Column<DateTime>(nullable: true),
                    Activo = table.Column<bool>(nullable: false),
                    DependienteId = table.Column<int>(nullable: false),
                    BarId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turnos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Turnos_Bares_BarId",
                        column: x => x.BarId,
                        principalTable: "Bares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Turnos_Dependientes_DependienteId",
                        column: x => x.DependienteId,
                        principalTable: "Dependientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: false),
                    UnidadId = table.Column<int>(nullable: false),
                    FamiliaId = table.Column<int>(nullable: false),
                    LimiteParaSolicitar = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Productos_FamiliasDeProductos_FamiliaId",
                        column: x => x.FamiliaId,
                        principalTable: "FamiliasDeProductos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Productos_UnidadesDeMedidas_UnidadId",
                        column: x => x.UnidadId,
                        principalTable: "UnidadesDeMedidas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ControlesDeExistencias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TurnoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlesDeExistencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ControlesDeExistencias_Turnos_TurnoId",
                        column: x => x.TurnoId,
                        principalTable: "Turnos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PedidosDeAlmacen",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TurnoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidosDeAlmacen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PedidosDeAlmacen_Turnos_TurnoId",
                        column: x => x.TurnoId,
                        principalTable: "Turnos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntregasDeAlmacen",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductoId = table.Column<int>(nullable: false),
                    TurnoId = table.Column<int>(nullable: false),
                    Cantidad = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntregasDeAlmacen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntregasDeAlmacen_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntregasDeAlmacen_Turnos_TurnoId",
                        column: x => x.TurnoId,
                        principalTable: "Turnos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Traslados",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductoId = table.Column<int>(nullable: false),
                    Cantidad = table.Column<double>(nullable: false),
                    OrigenId = table.Column<int>(nullable: false),
                    DestinoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Traslados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Traslados_Bares_DestinoId",
                        column: x => x.DestinoId,
                        principalTable: "Bares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Traslados_Bares_OrigenId",
                        column: x => x.OrigenId,
                        principalTable: "Bares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Traslados_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetallesControlesDeExistencias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ControlId = table.Column<int>(nullable: false),
                    ProductoId = table.Column<int>(nullable: false),
                    Cantidad = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesControlesDeExistencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetallesControlesDeExistencias_ControlesDeExistencias_ControlId",
                        column: x => x.ControlId,
                        principalTable: "ControlesDeExistencias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetallesControlesDeExistencias_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetallesPedidosDeAlmacen",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductoId = table.Column<int>(nullable: false),
                    Cantidad = table.Column<double>(nullable: false),
                    Atendido = table.Column<bool>(nullable: false),
                    PedidoAlmacenId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesPedidosDeAlmacen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetallesPedidosDeAlmacen_PedidosDeAlmacen_PedidoAlmacenId",
                        column: x => x.PedidoAlmacenId,
                        principalTable: "PedidosDeAlmacen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetallesPedidosDeAlmacen_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ControlesDeExistencias_TurnoId",
                table: "ControlesDeExistencias",
                column: "TurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_Dependientes_UsuarioId1",
                table: "Dependientes",
                column: "UsuarioId1");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesControlesDeExistencias_ControlId",
                table: "DetallesControlesDeExistencias",
                column: "ControlId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesControlesDeExistencias_ProductoId",
                table: "DetallesControlesDeExistencias",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesPedidosDeAlmacen_PedidoAlmacenId",
                table: "DetallesPedidosDeAlmacen",
                column: "PedidoAlmacenId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesPedidosDeAlmacen_ProductoId",
                table: "DetallesPedidosDeAlmacen",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_EntregasDeAlmacen_ProductoId",
                table: "EntregasDeAlmacen",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_EntregasDeAlmacen_TurnoId",
                table: "EntregasDeAlmacen",
                column: "TurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidosDeAlmacen_TurnoId",
                table: "PedidosDeAlmacen",
                column: "TurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_FamiliaId",
                table: "Productos",
                column: "FamiliaId");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_UnidadId",
                table: "Productos",
                column: "UnidadId");

            migrationBuilder.CreateIndex(
                name: "IX_Traslados_DestinoId",
                table: "Traslados",
                column: "DestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_Traslados_OrigenId",
                table: "Traslados",
                column: "OrigenId");

            migrationBuilder.CreateIndex(
                name: "IX_Traslados_ProductoId",
                table: "Traslados",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_BarId",
                table: "Turnos",
                column: "BarId");

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_DependienteId",
                table: "Turnos",
                column: "DependienteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetallesControlesDeExistencias");

            migrationBuilder.DropTable(
                name: "DetallesPedidosDeAlmacen");

            migrationBuilder.DropTable(
                name: "EntregasDeAlmacen");

            migrationBuilder.DropTable(
                name: "Traslados");

            migrationBuilder.DropTable(
                name: "ControlesDeExistencias");

            migrationBuilder.DropTable(
                name: "PedidosDeAlmacen");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Turnos");

            migrationBuilder.DropTable(
                name: "FamiliasDeProductos");

            migrationBuilder.DropTable(
                name: "UnidadesDeMedidas");

            migrationBuilder.DropTable(
                name: "Bares");

            migrationBuilder.DropTable(
                name: "Dependientes");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");
        }
    }
}
