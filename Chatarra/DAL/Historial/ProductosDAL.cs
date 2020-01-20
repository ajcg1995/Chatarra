using Entidades;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Historial
{
    public class ProductosDAL
    {
        public static Resultado AgregarProducto(Producto producto) {
            try
            {
                var db = BDConn.Connector();

                //Verificar si  existe

                var existe = db.Select<Producto>(x => x.Codigo.Equals(producto.Codigo)).Count();

                if (existe > 0)
                {
                    return new Resultado
                    {
                        Codigo = 99,
                        Mensaje = "Codigo Existente"
                    };

                }
                else
                {
                    var result = db.Insert(producto);
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




            
            }
            catch (Exception)
            {
                throw;
            }
        }


        public static Resultado ModificarProducto(Producto producto)
        {
            try
            {
                var db = BDConn.Connector();

                var result = db.Update(producto);

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

        public static Resultado EliminarProducto(Producto producto)
        {
            try
            {
                var db = BDConn.Connector();

                var result = db.Delete(producto);

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

        public static List<Producto> ListadoProdutos()
        {
            try
            {
                var db = BDConn.Connector();

                var result = db.Select<Producto>();


                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static Producto BuscarProductoId(string id)
        {
            try
            {
                var db = BDConn.Connector();

                var result = db.Select<Producto>(x => x.Codigo.Equals(id)).FirstOrDefault();


                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static Producto BuscarProductoIdentificacion(int id)
        {
            try
            {
                var db = BDConn.Connector();

                var result = db.Select<Producto>(x => x.IdProducto == id).FirstOrDefault();


                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public static Resultado InsertarLinea(LineaDetalle lineaDetalle) {

            try
            {
                var db = BDConn.Connector();

                var result = db.Insert(lineaDetalle);

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


        public static List<ProductosLineaDetalle> ListadoProductosLineaDetalle(int numFactura) {


            try
            {               
                var db = BDConn.Connector();


                List<ProductosLineaDetalle> listado = db.SqlList<ProductosLineaDetalle>(string.Format("EXEC SpProductosPorFactura " +
                 "@NumFactura = {0}",

                 numFactura
                 ));

                return listado;

            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}
