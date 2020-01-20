using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class BDConn
    {

        public static IDbConnection Connector() {

            try
            {
                string cadena = "Server=.\\SQLEXPRESS;Initial Catalog=Chatarra;Integrated Security=true";

                OrmLiteConnectionFactory con = new OrmLiteConnectionFactory(cadena, SqlServerDialect.Provider);
                IDbConnection db = con.OpenDbConnection();
                return db;
            }
            catch (Exception)
            {

                throw;
            }
        }







    }       
}