using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GestionBares.Data.Migrations
{
    public partial class AgregandoFechaAControlDeExistencia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha",
                table: "ControlesDeExistencias",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fecha",
                table: "ControlesDeExistencias");
        }
    }
}
