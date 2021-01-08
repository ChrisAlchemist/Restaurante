using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeVenta.Entidades
{
    public class Producto
    {
        public Int64 idProducto { get; set; }
        public Int64 codigo { get; set; }
        public string nombreProducto { get; set; }
        public string descripcionProducto { get; set; }
        public DateTime fechaAlta { get; set; }
        public double precio { get; set; }
        public bool activo { get; set; }
        public double precioCompra { get; set; }
        public Int64 cantidadExistencia { get; set; }
    }
}
