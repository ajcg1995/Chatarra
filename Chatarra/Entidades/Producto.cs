using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Historial
{
    [Alias("Productos")]

    public class Producto
    {
        [PrimaryKey]
        [AutoIncrement]
        public int IdProducto { get; set; }

        public string NombreProducto { get; set; }

        public string Codigo { get; set; }

        public double Precio{ get; set; }
    }
}
