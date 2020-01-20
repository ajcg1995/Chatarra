using Entidades;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configuracion
{
    public class ConfiguracionDAL
    {
        public static Resultado AgregarConfiguracion(Entidades.Configuracion configuracion) {

            try
            {
                var db = BDConn.Connector();

                var verificar = db.Select<Entidades.Configuracion>().Count();

                if (verificar > 0)
                { // Ya existe el registro
                    var result = db.Update(configuracion);
                    
                    if (result > 0)
                    {
                        return new Resultado
                        {
                            Codigo = 1,
                            Mensaje = "Modificado correctamente"
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
                else
                {//Si no existe se inserta por primera vez
                    var result = db.Insert(configuracion);

                    if (result > 0)
                    {
                        return new Resultado
                        {
                            Codigo = 1,
                            Mensaje = "Insertar correctamente"
                        };

                    }
                    else
                    {
                        return new Resultado
                        {
                            Codigo = 99,
                            Mensaje = "Problemas al modificar"
                        };
                    }
                }           

            }

            catch (Exception)
            {
                throw;
            }

        }


        public static Entidades.Configuracion BuscarConfiguracion() {


            try
            {
                var db = BDConn.Connector();
                Entidades.Configuracion c = db.Select<Entidades.Configuracion>().FirstOrDefault();
                return c;
            }
            catch (Exception)
            {

                throw;
            }


        }

    }

}
