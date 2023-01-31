using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class EquipoPartido
    {
        public int Id { get; set; }
        public Equipo Equipo { get; set; }

        public int PuntajePartido { get; set; }
        public int Marcador { get; set; }
        public int SetsGanado { get; set; }
    }
}
