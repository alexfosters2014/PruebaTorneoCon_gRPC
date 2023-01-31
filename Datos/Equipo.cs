using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class Equipo
    {
        public int Id { get; set; }
        public string NombreEquipo { get; set; }
        public string Caratula { get; set; }
        public List<Jugador> Jugadores { get; set; }
        public List<Torneo> Torneos { get; set; }
    }
}
