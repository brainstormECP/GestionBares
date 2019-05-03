using Microsoft.EntityFrameworkCore.Migrations;

namespace GestionBares.Migrations
{
    public partial class AddVentas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesPedidosDeAlmacen_PedidosDeAlmacen_PedidoAlmacenId",
                table: "DetallesPedidosDeAlmacen");

            migrationBuilder.DropIndex(
                name: "IX_DetallesPedidosDeAlmacen_PedidoAlmacenId",
                table: "DetallesPedidosDeAlmacen");

            migrationBuilder.DropColumn(
                name: "PedidoAlmacenId",
                table: "DetallesPedidosDeAlmacen");

            migrationBuilder.AddColumn<int>(
                name: "PedidoId",
                table: "DetallesPedidosDeAlmacen",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "25704d96-678c-4abd-99e4-91350c52d81c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "f05aa8c0-1ced-46ce-8fbf-fb8d99a2c04b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "b95d15ad-140e-461b-aa66-2a7c60c96864");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "87dcf036-c5fc-419c-831f-a6e680c6acdc");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesPedidosDeAlmacen_PedidoId",
                table: "DetallesPedidosDeAlmacen",
                column: "PedidoId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesPedidosDeAlmacen_PedidosDeAlmacen_PedidoId",
                table: "DetallesPedidosDeAlmacen",
                column: "PedidoId",
                principalTable: "PedidosDeAlmacen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesPedidosDeAlmacen_PedidosDeAlmacen_PedidoId",
                table: "DetallesPedidosDeAlmacen");

            migrationBuilder.DropIndex(
                name: "IX_DetallesPedidosDeAlmacen_PedidoId",
                table: "DetallesPedidosDeAlmacen");

            migrationBuilder.DropColumn(
                name: "PedidoId",
                table: "DetallesPedidosDeAlmacen");

            migrationBuilder.AddColumn<int>(
                name: "PedidoAlmacenId",
                table: "DetallesPedidosDeAlmacen",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "d6a67ca5-6f23-4120-bde2-59b69fbac0d5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "1ba0a0bd-7eb0-4e5f-a4c6-d8f07a777cf3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "6a289b72-d0c8-4d6a-9b5f-dc7dad3bf03c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "9b5a0b6d-e6d4-4bb6-b46b-69cc1c9c69dd");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesPedidosDeAlmacen_PedidoAlmacenId",
                table: "DetallesPedidosDeAlmacen",
                column: "PedidoAlmacenId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesPedidosDeAlmacen_PedidosDeAlmacen_PedidoAlmacenId",
                table: "DetallesPedidosDeAlmacen",
                column: "PedidoAlmacenId",
                principalTable: "PedidosDeAlmacen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
