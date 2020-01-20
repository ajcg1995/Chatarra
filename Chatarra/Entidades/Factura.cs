using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [Alias("Factura")]
    public class Factura
    {
        public string Identificacion { get; set; }

        public string Fecha { get; set; }

        public string Hora { get; set; }

        public double TotalVenta { get; set; }

        [PrimaryKey]
        [AutoIncrement]
        public int NumeroFactura { get; set; }

        public double Descuento { get; set; }

        public double SubTotal { get; set; }

        public double IVA { get; set; }

        public string PorcentajeIVA { get; set; }


    }
}
