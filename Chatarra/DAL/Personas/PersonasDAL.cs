using Entidades;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Personas
{
    public class PersonasDAL
    {

        public static Resultado InsertarPersonas(Persona persona) {

            try
            {
                var db = BDConn.Connector();

                var result = db.Insert(persona);

                if (result > 0)
                {
                    return new Resultado
                    {
                        Codigo = 1,
                        Mensaje = "Insertado correctamente"
                    };

                }
                else {
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


        public static Resultado EliminarPersonas(Persona persona)
        {

            try
            {
                var db = BDConn.Connector();

                var result = db.Delete(persona);

                if (result > 0)
                {
                    return new Resultado
                    {
                        Codigo = 1,
                        Mensaje = "Eliminado correctamente"
                    };

                }
                else
                {
                    return new Resultado
                    {
                        Codigo = 99,
                        Mensaje = "Problemas al eliminar"
                    };

                }



            }
            catch (Exception)
            {

                throw;
            }

        }

        public static Resultado ActualizarPersonas(Persona persona)
        {

            try
            {
                var db = BDConn.Connector();

                var result = db.Update(persona);

                if (result > 0)
                {
                    return new Resultado
                    {
                        Codigo = 1,
                        Mensaje = "Actualizado correctamente"
                    };

                }
                else
                {
                    return new Resultado
                    {
                        Codigo = 99,
                        Mensaje = "Problemas al actualizar"
                    };
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public static Persona BuscarUnaPersona(string idPersona)
        {

            try
            {
                var db = BDConn.Connector();

                Persona result = db.Select<Persona>(x => x.Identificacion.Equals(idPersona)).FirstOrDefault();

                return result;
                

            }
            catch (Exception)
            {
                throw;
            }

        }


        public static List<Persona> Listar()
        {

            try
            {
                var db = BDConn.Connector();

                List<Persona> result = db.Select<Persona>();

                return result;


            }
            catch (Exception)
            {
                throw;
            }

        }       

    }
}
