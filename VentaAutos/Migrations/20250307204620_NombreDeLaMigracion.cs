using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VentaAutos.Migrations
{
    /// <inheritdoc />
    public partial class NombreDeLaMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Capacidad_de_carga",
                table: "Autos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Motor",
                table: "Autos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Seguridad",
                table: "Autos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Transmision",
                table: "Autos",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capacidad_de_carga",
                table: "Autos");

            migrationBuilder.DropColumn(
                name: "Motor",
                table: "Autos");

            migrationBuilder.DropColumn(
                name: "Seguridad",
                table: "Autos");

            migrationBuilder.DropColumn(
                name: "Transmision",
                table: "Autos");
        }
    }
}
