using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VentaAutos.Migrations
{
    /// <inheritdoc />
    public partial class Detalles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Detalles",
                table: "Autos",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Detalles",
                table: "Autos");
        }
    }
}
