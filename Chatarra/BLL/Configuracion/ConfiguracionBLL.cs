using DAL.Configuracion;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Configuracion
{
    public class ConfiguracionBLL
    {
        public static Resultado AgregarConfiguracion(Entidades.Configuracion configuracion)
        {
            try
            {
                return ConfiguracionDAL.AgregarConfiguracion(configuracion);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static Entidades.Configuracion BuscarConfiguracion()
        {


            try
            {
                return ConfiguracionDAL.BuscarConfiguracion();

            }
            catch (Exception)
            {

                throw;
            }


        }
    }
}
