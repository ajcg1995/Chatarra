using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [Alias("Configuracion")]
    public class Configuracion
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public string NombreInstitucion { get; set; }

        public string NombreImpresora { get; set; }
    }
}
