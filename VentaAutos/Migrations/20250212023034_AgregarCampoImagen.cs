using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VentaAutos.Migrations
{
    /// <inheritdoc />
    public partial class AgregarCampoImagen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IrlImage",
                table: "Autos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IrlImage",
                table: "Autos");
        }
    }
}
