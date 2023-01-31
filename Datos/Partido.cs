using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class Partido
    {
        public int Id { get; set; }
        public EquipoPartido EquipoLocal { get; set; }
        public EquipoPartido EquipoVisitante { get; set; }
        public int SetActual { get; set; }
        public int PartidoSiguienteId { get; set; }
        public Torneo Torneo { get; set; }
        public string NombreCancha { get; set; }
        public DateTime Fecha { get; set; }
        public int Posición { get; set; }
    }
}
