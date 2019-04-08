using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GestionBares.Data.Migrations
{
    public partial class CambioTraslado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha",
                table: "Traslados",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TurnoId",
                table: "Traslados",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "Traslados",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Traslados_TurnoId",
                table: "Traslados",
                column: "TurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_Traslados_UsuarioId",
                table: "Traslados",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Traslados_Turnos_TurnoId",
                table: "Traslados",
                column: "TurnoId",
                principalTable: "Turnos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Traslados_AspNetUsers_UsuarioId",
                table: "Traslados",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Traslados_Turnos_TurnoId",
                table: "Traslados");

            migrationBuilder.DropForeignKey(
                name: "FK_Traslados_AspNetUsers_UsuarioId",
                table: "Traslados");

            migrationBuilder.DropIndex(
                name: "IX_Traslados_TurnoId",
                table: "Traslados");

            migrationBuilder.DropIndex(
                name: "IX_Traslados_UsuarioId",
                table: "Traslados");

            migrationBuilder.DropColumn(
                name: "Fecha",
                table: "Traslados");

            migrationBuilder.DropColumn(
                name: "TurnoId",
                table: "Traslados");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Traslados");
        }
    }
}
