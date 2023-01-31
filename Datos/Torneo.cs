using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class Torneo
    {
        public int Id { get; set; }
        public List<Equipo> EquiposInscriptos { get; set; }
        public List<Partido> Fixture { get; set; }
        public string Modalidad { get; set; }
        public int SetsMax { get; set; }
        public int PuntajeMax { get; set; }
        public string Lugar { get; set; }
    }
}
