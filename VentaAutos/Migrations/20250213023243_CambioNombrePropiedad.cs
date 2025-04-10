using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VentaAutos.Migrations
{
    /// <inheritdoc />
    public partial class CambioNombrePropiedad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IrlImage",
                table: "Autos");

            migrationBuilder.AddColumn<string>(
                name: "ImagenUrl",
                table: "Autos",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagenUrl",
                table: "Autos");

            migrationBuilder.AddColumn<string>(
                name: "IrlImage",
                table: "Autos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
