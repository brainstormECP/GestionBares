using Microsoft.EntityFrameworkCore.Migrations;

namespace GestionBares.Migrations
{
    public partial class AgregandoAprobadoControles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Aprobado",
                table: "ControlesDeExistenciasVenta",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Aprobado",
                table: "ControlesDeExistencias",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "0cea201f-6323-4738-bd04-983bf0b2eaea");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "3f1af597-e8e1-4db9-b7d6-485838cafc0e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "40f658d3-ef25-4290-9ebe-95625f31af05");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "0b851e7e-4a52-43d1-bacd-9bddf5b6de6e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5", "65ab7714-53c3-415c-82e9-0697a4bc5f18", "ECONOMIA", "ECONOMIA" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "5", "65ab7714-53c3-415c-82e9-0697a4bc5f18" });

            migrationBuilder.DropColumn(
                name: "Aprobado",
                table: "ControlesDeExistenciasVenta");

            migrationBuilder.DropColumn(
                name: "Aprobado",
                table: "ControlesDeExistencias");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "ca670a2d-0122-47c7-a47e-502ee9ec5dae");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "06aeef6a-7868-4c6e-98a4-513910ec835f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "72213629-4387-46f6-9c06-1c0c284c83e3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "73405285-ee03-45dc-ac67-2b3f3e59e328");
        }
    }
}
