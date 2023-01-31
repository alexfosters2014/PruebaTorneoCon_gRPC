﻿// <auto-generated />
using System;
using Datos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Datos.Migrations
{
    [DbContext(typeof(TorneoBDContext))]
    [Migration("20230131005149_Inicial")]
    partial class Inicial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Datos.Equipo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Caratula")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreEquipo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Equipos");
                });

            modelBuilder.Entity("Datos.EquipoPartido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("EquipoId")
                        .HasColumnType("int");

                    b.Property<int>("Marcador")
                        .HasColumnType("int");

                    b.Property<int>("PuntajePartido")
                        .HasColumnType("int");

                    b.Property<int>("SetsGanado")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EquipoId");

                    b.ToTable("EquiposPartidos");
                });

            modelBuilder.Entity("Datos.Jugador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Apellidos")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cedula")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EquipoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombres")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EquipoId");

                    b.ToTable("Jugadores");
                });

            modelBuilder.Entity("Datos.Partido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("EquipoLocalId")
                        .HasColumnType("int");

                    b.Property<int?>("EquipoVisitanteId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("NombreCancha")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PartidoSiguienteId")
                        .HasColumnType("int");

                    b.Property<int>("Posición")
                        .HasColumnType("int");

                    b.Property<int>("SetActual")
                        .HasColumnType("int");

                    b.Property<int?>("TorneoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EquipoLocalId");

                    b.HasIndex("EquipoVisitanteId");

                    b.HasIndex("TorneoId");

                    b.ToTable("Partidos");
                });

            modelBuilder.Entity("Datos.Torneo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Lugar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Modalidad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PuntajeMax")
                        .HasColumnType("int");

                    b.Property<int>("SetsMax")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Torneos");
                });

            modelBuilder.Entity("Datos.UsuarioContador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("NombreCompleto")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Contadores");
                });

            modelBuilder.Entity("EquipoTorneo", b =>
                {
                    b.Property<int>("EquiposInscriptosId")
                        .HasColumnType("int");

                    b.Property<int>("TorneosId")
                        .HasColumnType("int");

                    b.HasKey("EquiposInscriptosId", "TorneosId");

                    b.HasIndex("TorneosId");

                    b.ToTable("EquipoTorneo");
                });

            modelBuilder.Entity("Datos.EquipoPartido", b =>
                {
                    b.HasOne("Datos.Equipo", "Equipo")
                        .WithMany()
                        .HasForeignKey("EquipoId");

                    b.Navigation("Equipo");
                });

            modelBuilder.Entity("Datos.Jugador", b =>
                {
                    b.HasOne("Datos.Equipo", null)
                        .WithMany("Jugadores")
                        .HasForeignKey("EquipoId");
                });

            modelBuilder.Entity("Datos.Partido", b =>
                {
                    b.HasOne("Datos.EquipoPartido", "EquipoLocal")
                        .WithMany()
                        .HasForeignKey("EquipoLocalId");

                    b.HasOne("Datos.EquipoPartido", "EquipoVisitante")
                        .WithMany()
                        .HasForeignKey("EquipoVisitanteId");

                    b.HasOne("Datos.Torneo", "Torneo")
                        .WithMany("Fixture")
                        .HasForeignKey("TorneoId");

                    b.Navigation("EquipoLocal");

                    b.Navigation("EquipoVisitante");

                    b.Navigation("Torneo");
                });

            modelBuilder.Entity("EquipoTorneo", b =>
                {
                    b.HasOne("Datos.Equipo", null)
                        .WithMany()
                        .HasForeignKey("EquiposInscriptosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Datos.Torneo", null)
                        .WithMany()
                        .HasForeignKey("TorneosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Datos.Equipo", b =>
                {
                    b.Navigation("Jugadores");
                });

            modelBuilder.Entity("Datos.Torneo", b =>
                {
                    b.Navigation("Fixture");
                });
#pragma warning restore 612, 618
        }
    }
}
