using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Datos.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCompleto = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contadores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jugadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cedula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Apellidos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jugadores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EquipoJugador",
                columns: table => new
                {
                    EquiposId = table.Column<int>(type: "int", nullable: false),
                    JugadoresId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipoJugador", x => new { x.EquiposId, x.JugadoresId });
                    table.ForeignKey(
                        name: "FK_EquipoJugador_Jugadores_JugadoresId",
                        column: x => x.JugadoresId,
                        principalTable: "Jugadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Equipos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreEquipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caratula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TorneoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EquiposPartidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipoId = table.Column<int>(type: "int", nullable: true),
                    PuntajePartido = table.Column<int>(type: "int", nullable: false),
                    Marcador = table.Column<int>(type: "int", nullable: false),
                    SetsGanado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquiposPartidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquiposPartidos_Equipos_EquipoId",
                        column: x => x.EquipoId,
                        principalTable: "Equipos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Torneos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Modalidad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SetsMax = table.Column<int>(type: "int", nullable: false),
                    PuntajeMax = table.Column<int>(type: "int", nullable: false),
                    Lugar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EquipoPartidoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Torneos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Torneos_EquiposPartidos_EquipoPartidoId",
                        column: x => x.EquipoPartidoId,
                        principalTable: "EquiposPartidos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Partidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipoLocalId = table.Column<int>(type: "int", nullable: true),
                    EquipoVisitanteId = table.Column<int>(type: "int", nullable: true),
                    SetActual = table.Column<int>(type: "int", nullable: false),
                    PartidoSiguienteId = table.Column<int>(type: "int", nullable: false),
                    TorneoId = table.Column<int>(type: "int", nullable: true),
                    NombreCancha = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Posición = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Partidos_EquiposPartidos_EquipoLocalId",
                        column: x => x.EquipoLocalId,
                        principalTable: "EquiposPartidos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Partidos_EquiposPartidos_EquipoVisitanteId",
                        column: x => x.EquipoVisitanteId,
                        principalTable: "EquiposPartidos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Partidos_Torneos_TorneoId",
                        column: x => x.TorneoId,
                        principalTable: "Torneos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EquipoJugador_JugadoresId",
                table: "EquipoJugador",
                column: "JugadoresId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipos_TorneoId",
                table: "Equipos",
                column: "TorneoId");

            migrationBuilder.CreateIndex(
                name: "IX_EquiposPartidos_EquipoId",
                table: "EquiposPartidos",
                column: "EquipoId");

            migrationBuilder.CreateIndex(
                name: "IX_Partidos_EquipoLocalId",
                table: "Partidos",
                column: "EquipoLocalId");

            migrationBuilder.CreateIndex(
                name: "IX_Partidos_EquipoVisitanteId",
                table: "Partidos",
                column: "EquipoVisitanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Partidos_TorneoId",
                table: "Partidos",
                column: "TorneoId");

            migrationBuilder.CreateIndex(
                name: "IX_Torneos_EquipoPartidoId",
                table: "Torneos",
                column: "EquipoPartidoId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipoJugador_Equipos_EquiposId",
                table: "EquipoJugador",
                column: "EquiposId",
                principalTable: "Equipos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Equipos_Torneos_TorneoId",
                table: "Equipos",
                column: "TorneoId",
                principalTable: "Torneos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquiposPartidos_Equipos_EquipoId",
                table: "EquiposPartidos");

            migrationBuilder.DropTable(
                name: "Contadores");

            migrationBuilder.DropTable(
                name: "EquipoJugador");

            migrationBuilder.DropTable(
                name: "Partidos");

            migrationBuilder.DropTable(
                name: "Jugadores");

            migrationBuilder.DropTable(
                name: "Equipos");

            migrationBuilder.DropTable(
                name: "Torneos");

            migrationBuilder.DropTable(
                name: "EquiposPartidos");
        }
    }
}
