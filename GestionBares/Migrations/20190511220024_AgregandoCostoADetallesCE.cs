using Microsoft.EntityFrameworkCore.Migrations;

namespace GestionBares.Migrations
{
    public partial class AgregandoCostoADetallesCE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Costo",
                table: "DetallesControlesDeExistenciasVenta",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Precio",
                table: "DetallesControlesDeExistenciasVenta",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Costo",
                table: "DetallesControlesDeExistencias",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "b7e4e111-395b-4625-899d-14e2db1f6034");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "6e41b9e0-1b0f-45e2-92e3-9e1909e0276f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "b646ee3e-86b1-4f13-9e12-aae7e3db3411");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "5f6f85c9-654f-4aad-914a-e65056a751a3");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Costo",
                table: "DetallesControlesDeExistenciasVenta");

            migrationBuilder.DropColumn(
                name: "Precio",
                table: "DetallesControlesDeExistenciasVenta");

            migrationBuilder.DropColumn(
                name: "Costo",
                table: "DetallesControlesDeExistencias");

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
    }
}
