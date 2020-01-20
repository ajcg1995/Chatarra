using Entidades;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Historial
{
    public class HistorialDAL
    {
        public static Resultado InsertarHistorial(Entidades.Factura historial) {

            try
            {
                var db = BDConn.Connector();

                var result = db.Insert(historial);

                if (result > 0)
                {
                    return new Resultado
                    {
                        Codigo = 1,
                        Mensaje = "Insertado correctamente"
                    };

                }
                else
                {
                    return new Resultado
                    {
                        Codigo = 99,
                        Mensaje = "Problemas al insertar"
                    };
                }
            }

            catch (Exception)
            {
                throw;
            }


        }

        public static Factura ultimoRegistro() {

            try
            {
                var db = BDConn.Connector();

                Factura result = db.Select<Factura>().OrderByDescending(x => x.NumeroFactura).FirstOrDefault();


                return result;

            }

            catch (Exception)
            {
                throw;
            }


        }


    }
}
