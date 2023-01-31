using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class TorneoBDContext : DbContext
    {
        public TorneoBDContext(DbContextOptions options) : base(options)
        {
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Equipo>().OwnsMany(
        //            p => p.Jugadores, a =>
        //            {
        //                a.ToJson();
        //            });
        //}

        public DbSet<Torneo> Torneos { get; set; }
        public DbSet<Partido> Partidos { get; set; }
        public DbSet<Jugador> Jugadores { get; set; }

        public DbSet<EquipoPartido> EquiposPartidos { get; set; }
        public DbSet<Equipo> Equipos { get; set; }
        public DbSet<UsuarioContador> Contadores { get; set; }

    }
}
