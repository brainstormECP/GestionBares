using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GestionBares.Migrations
{
    public partial class CambiosVentas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetallesVentas");

            migrationBuilder.DropTable(
                name: "Ventas");

            migrationBuilder.DropColumn(
                name: "LimiteParaSolicitar",
                table: "Productos");

            migrationBuilder.AddColumn<decimal>(
                name: "Costo",
                table: "Productos",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Precio",
                table: "Productos",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "ControlesDeExistenciasVenta",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TurnoId = table.Column<int>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Activo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlesDeExistenciasVenta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ControlesDeExistenciasVenta_Turnos_TurnoId",
                        column: x => x.TurnoId,
                        principalTable: "Turnos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntregasDeAlmacenVenta",
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
                    table.PrimaryKey("PK_EntregasDeAlmacenVenta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntregasDeAlmacenVenta_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntregasDeAlmacenVenta_Turnos_TurnoId",
                        column: x => x.TurnoId,
                        principalTable: "Turnos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PedidosDeAlmacenVenta",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TurnoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidosDeAlmacenVenta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PedidosDeAlmacenVenta_Turnos_TurnoId",
                        column: x => x.TurnoId,
                        principalTable: "Turnos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrasladosVenta",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TurnoId = table.Column<int>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    ProductoId = table.Column<int>(nullable: false),
                    Cantidad = table.Column<double>(nullable: false),
                    DestinoId = table.Column<int>(nullable: false),
                    UsuarioId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrasladosVenta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrasladosVenta_Bares_DestinoId",
                        column: x => x.DestinoId,
                        principalTable: "Bares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrasladosVenta_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrasladosVenta_Turnos_TurnoId",
                        column: x => x.TurnoId,
                        principalTable: "Turnos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrasladosVenta_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetallesControlesDeExistenciasVenta",
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
                    table.PrimaryKey("PK_DetallesControlesDeExistenciasVenta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetallesControlesDeExistenciasVenta_ControlesDeExistenciasVenta_ControlId",
                        column: x => x.ControlId,
                        principalTable: "ControlesDeExistenciasVenta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetallesControlesDeExistenciasVenta_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetallesPedidosDeAlmacenVenta",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PedidoId = table.Column<int>(nullable: false),
                    ProductoId = table.Column<int>(nullable: false),
                    Cantidad = table.Column<double>(nullable: false),
                    Atendido = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesPedidosDeAlmacenVenta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetallesPedidosDeAlmacenVenta_PedidosDeAlmacenVenta_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "PedidosDeAlmacenVenta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetallesPedidosDeAlmacenVenta_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "8d5bb64b-c8c5-48c2-9ffa-870bf7af4508");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "14dc3d7b-19c9-4206-aefc-e540695e0a12");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "9344561d-dc18-4294-b251-88a248fe4777");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "17f2c370-7010-4bf5-9c47-477842366e19");

            migrationBuilder.CreateIndex(
                name: "IX_ControlesDeExistenciasVenta_TurnoId",
                table: "ControlesDeExistenciasVenta",
                column: "TurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesControlesDeExistenciasVenta_ControlId",
                table: "DetallesControlesDeExistenciasVenta",
                column: "ControlId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesControlesDeExistenciasVenta_ProductoId",
                table: "DetallesControlesDeExistenciasVenta",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesPedidosDeAlmacenVenta_PedidoId",
                table: "DetallesPedidosDeAlmacenVenta",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesPedidosDeAlmacenVenta_ProductoId",
                table: "DetallesPedidosDeAlmacenVenta",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_EntregasDeAlmacenVenta_ProductoId",
                table: "EntregasDeAlmacenVenta",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_EntregasDeAlmacenVenta_TurnoId",
                table: "EntregasDeAlmacenVenta",
                column: "TurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidosDeAlmacenVenta_TurnoId",
                table: "PedidosDeAlmacenVenta",
                column: "TurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_TrasladosVenta_DestinoId",
                table: "TrasladosVenta",
                column: "DestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_TrasladosVenta_ProductoId",
                table: "TrasladosVenta",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_TrasladosVenta_TurnoId",
                table: "TrasladosVenta",
                column: "TurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_TrasladosVenta_UsuarioId",
                table: "TrasladosVenta",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetallesControlesDeExistenciasVenta");

            migrationBuilder.DropTable(
                name: "DetallesPedidosDeAlmacenVenta");

            migrationBuilder.DropTable(
                name: "EntregasDeAlmacenVenta");

            migrationBuilder.DropTable(
                name: "TrasladosVenta");

            migrationBuilder.DropTable(
                name: "ControlesDeExistenciasVenta");

            migrationBuilder.DropTable(
                name: "PedidosDeAlmacenVenta");

            migrationBuilder.DropColumn(
                name: "Costo",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Precio",
                table: "Productos");

            migrationBuilder.AddColumn<double>(
                name: "LimiteParaSolicitar",
                table: "Productos",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "Ventas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Fecha = table.Column<DateTime>(nullable: false),
                    TurnoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ventas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ventas_Turnos_TurnoId",
                        column: x => x.TurnoId,
                        principalTable: "Turnos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetallesVentas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cantidad = table.Column<double>(nullable: false),
                    Importe = table.Column<decimal>(nullable: false),
                    ProductoId = table.Column<int>(nullable: false),
                    VentaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesVentas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetallesVentas_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetallesVentas_Ventas_VentaId",
                        column: x => x.VentaId,
                        principalTable: "Ventas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "19b5717a-4a01-437d-9cd5-4e72cf8dad37");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "d7535852-9341-482f-a655-5d81425d3550");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "a651a587-22c9-47b7-ae0d-10c8679a5a51");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "6cf75d47-64aa-4bad-b65c-52855adadbba");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesVentas_ProductoId",
                table: "DetallesVentas",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesVentas_VentaId",
                table: "DetallesVentas",
                column: "VentaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_TurnoId",
                table: "Ventas",
                column: "TurnoId");
        }
    }
}
