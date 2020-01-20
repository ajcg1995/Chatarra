using DAL.Historial;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Historial
{
    public class ProductoBLL
    {
        public static Resultado AgregarProducto(Producto producto)
        {
            try
            {
                return ProductosDAL.AgregarProducto(producto);

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
                return ProductosDAL.ModificarProducto(producto);
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
                return ProductosDAL.EliminarProducto(producto);
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
                return ProductosDAL.ListadoProdutos();
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
                return ProductosDAL.BuscarProductoId(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static Producto BuscarProductoIdentificacion(int id) {

            try
            {
                return ProductosDAL.BuscarProductoIdentificacion(id); 
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static Resultado InsertarLinea(LineaDetalle lineaDetalle)
        {
            try
            {
                return ProductosDAL.InsertarLinea(lineaDetalle);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static List<ProductosLineaDetalle> ListadoProductosLineaDetalle(int numFactura)
        {
            
            try
            {              
                return ProductosDAL.ListadoProductosLineaDetalle(numFactura);

            }
            catch (Exception)
            {

                throw;
            }

        }


    }
}
