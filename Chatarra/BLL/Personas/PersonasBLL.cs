using DAL.Personas;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Personas
{
    public class PersonasBLL
    {

        public static Resultado InsertarPersonas(Persona persona)
        {
            try
            {
                return PersonasDAL.InsertarPersonas(persona);
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
                return PersonasDAL.EliminarPersonas(persona);

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
                return PersonasDAL.ActualizarPersonas(persona);
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
                return PersonasDAL.BuscarUnaPersona(idPersona);

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
                return PersonasDAL.Listar();
            }
            catch (Exception)
            {
                throw;
            }
        }        
    }
}
