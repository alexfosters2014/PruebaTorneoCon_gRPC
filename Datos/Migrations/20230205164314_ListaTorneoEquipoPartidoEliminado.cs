using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Datos.Migrations
{
    /// <inheritdoc />
    public partial class ListaTorneoEquipoPartidoEliminado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Torneos_EquiposPartidos_EquipoPartidoId",
                table: "Torneos");

            migrationBuilder.DropIndex(
                name: "IX_Torneos_EquipoPartidoId",
                table: "Torneos");

            migrationBuilder.DropColumn(
                name: "EquipoPartidoId",
                table: "Torneos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EquipoPartidoId",
                table: "Torneos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Torneos_EquipoPartidoId",
                table: "Torneos",
                column: "EquipoPartidoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Torneos_EquiposPartidos_EquipoPartidoId",
                table: "Torneos",
                column: "EquipoPartidoId",
                principalTable: "EquiposPartidos",
                principalColumn: "Id");
        }
    }
}
