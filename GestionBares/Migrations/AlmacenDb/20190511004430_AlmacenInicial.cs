using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GestionBares.Migrations.AlmacenDb
{
    public partial class AlmacenInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Existencias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CodigoProducto = table.Column<string>(nullable: true),
                    Cantidad = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Existencias", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Existencias",
                columns: new[] { "Id", "Cantidad", "CodigoProducto" },
                values: new object[,]
                {
                    { 1, 10.0, "1" },
                    { 20, 10.0, "20" },
                    { 19, 10.0, "19" },
                    { 18, 10.0, "18" },
                    { 17, 10.0, "17" },
                    { 16, 10.0, "16" },
                    { 15, 10.0, "15" },
                    { 14, 10.0, "14" },
                    { 13, 10.0, "13" },
                    { 12, 10.0, "12" },
                    { 11, 10.0, "11" },
                    { 10, 10.0, "10" },
                    { 9, 10.0, "9" },
                    { 8, 10.0, "8" },
                    { 7, 10.0, "7" },
                    { 6, 30.0, "6" },
                    { 5, 20.0, "5" },
                    { 4, 10.0, "4" },
                    { 3, 30.0, "3" },
                    { 2, 20.0, "2" },
                    { 21, 10.0, "21" },
                    { 22, 10.0, "22" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Existencias");
        }
    }
}
