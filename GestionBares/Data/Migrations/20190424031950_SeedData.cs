using Microsoft.EntityFrameworkCore.Migrations;

namespace GestionBares.Data.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", "247f141d-66f0-4dcf-8069-a62648263641", "ADMINISTRADOR", "ADMINISTRADOR" },
                    { "2", "08e06816-9757-40a1-9b80-0d66741397e5", "DEPENDIENTE", "DEPENDIENTE" },
                    { "3", "bb954aad-a844-494a-8f8c-ccb47adc5d4f", "ALMACEN", "ALMACEN" },
                    { "4", "84267adf-c7a1-4b02-807e-790e78725619", "ECONOMIA", "ECONOMIA" },
                    { "5", "bf27967b-2d71-4dcd-9e43-58f692ab5971", "A+B", "A+B" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "Activo", "Nombre" },
                values: new object[] { "f42559a2-2776-4e9b-9ba1-268597eff72b", 0, "36fd2616-8e8a-4cc6-8a5a-52d963207836", "Usuario", "admin@patriarca.cu", false, false, null, "ADMIN@PATRIARCA.CU", "ADMIN", "AQAAAAEAACcQAAAAEP4OedI6m26WUn/2C4AcBkzdT6SnL/6E+xakQ/9mGAkqqp3t9PwyIR6l9obLouKIVg==", null, false, "43VMKYQKNTENYZVJNU2TII26X23H5PGV", false, "admin", true, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "f42559a2-2776-4e9b-9ba1-268597eff72b", "1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "2", "08e06816-9757-40a1-9b80-0d66741397e5" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "3", "bb954aad-a844-494a-8f8c-ccb47adc5d4f" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "4", "84267adf-c7a1-4b02-807e-790e78725619" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "5", "bf27967b-2d71-4dcd-9e43-58f692ab5971" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "f42559a2-2776-4e9b-9ba1-268597eff72b", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "1", "247f141d-66f0-4dcf-8069-a62648263641" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "f42559a2-2776-4e9b-9ba1-268597eff72b", "36fd2616-8e8a-4cc6-8a5a-52d963207836" });

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
