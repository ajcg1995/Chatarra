using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class LineaDetalle
    {
        public int IdLinea { get; set; }

        public int  NumeroFactura { get; set; }

        public int IdProducto { get; set; }

        public double PrecioProducto { get; set; }

        public int Cantidad { get; set; }
    }
}
