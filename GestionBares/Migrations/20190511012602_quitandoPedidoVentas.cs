using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GestionBares.Migrations
{
    public partial class quitandoPedidoVentas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetallesPedidosDeAlmacenVenta");

            migrationBuilder.DropTable(
                name: "PedidosDeAlmacenVenta");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "2aceb298-4a38-4c2f-9ed8-45013737ecf0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "a58b8d48-e08b-4b79-8b81-7db7c8e764f7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "d2245787-5450-4869-b657-f0d1a7370526");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "7f322c9e-3809-453d-8080-66475fb0d66c");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "DetallesPedidosDeAlmacenVenta",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Atendido = table.Column<bool>(nullable: false),
                    Cantidad = table.Column<double>(nullable: false),
                    PedidoId = table.Column<int>(nullable: false),
                    ProductoId = table.Column<int>(nullable: false)
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
                value: "a4de5bff-93f8-48fa-b1f7-e0a9bde2931c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "350d1d46-990f-4fab-af3c-f8765630b912");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "a3ab6f8f-1595-46d9-90a8-89c86a33d399");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "9d2a2430-9f54-4801-8eb0-18d995b8d829");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesPedidosDeAlmacenVenta_PedidoId",
                table: "DetallesPedidosDeAlmacenVenta",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesPedidosDeAlmacenVenta_ProductoId",
                table: "DetallesPedidosDeAlmacenVenta",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidosDeAlmacenVenta_TurnoId",
                table: "PedidosDeAlmacenVenta",
                column: "TurnoId");
        }
    }
}
