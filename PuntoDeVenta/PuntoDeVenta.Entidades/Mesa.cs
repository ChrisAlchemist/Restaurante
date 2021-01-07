using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeVenta.Entidades
{
    public class Mesa
    {
        public Int64 idMesa { get; set; }
        public int numeroMesa { get; set; }
        public string nombreMesa { get; set; }
        public bool reservada { get; set; }
        public DateTime fechaAlta { get; set; }
        public DateTime horaAsignacion { get; set; }
        public double totalCuenta { get; set; }
        public bool asignada { get; set; }
    }
}
