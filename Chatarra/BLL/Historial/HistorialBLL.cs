using DAL.Historial;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Historial
{
    public class HistorialBLL
    {
        public static Resultado InsertarHistorial(Entidades.Factura historial)
        {

            try
            {
                return HistorialDAL.InsertarHistorial(historial);
            }

            catch (Exception)
            {
                throw;
            }
        }

        public static Factura ultimoRegistro()
        {
            try
            {
                return HistorialDAL.ultimoRegistro();
            }
            catch (Exception)
            {

                throw;
            }


        }

        }
}
