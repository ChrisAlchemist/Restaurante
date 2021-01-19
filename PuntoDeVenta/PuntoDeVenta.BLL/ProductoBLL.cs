using PuntoDeVenta.DAO;
using PuntoDeVenta.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeVenta.BLL
{
    public class ProductoBLL
    {
        private ProductoDAO productoDAO;

        public ProductoBLL()
        {
            try
            {
                productoDAO = new ProductoDAO();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<Producto> ObtenerProductos(string nombreProducto)
        {
            try
            {
                return productoDAO.ObtenerProductos(nombreProducto);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Result AgregarProducto(Producto producto)
        {
            try
            {
                return productoDAO.AgregarProducto(producto);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Producto ObtenerInfoProducto(Int64 numProducto)
        {
            try
            {
                return productoDAO.ObtenerInfoProducto(numProducto);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public Result EditarProducto(Producto producto, bool editarProducto, bool eliminarProdcuto)
        {
            try
            {
                return productoDAO.EditarProducto(producto, editarProducto, eliminarProdcuto);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Result EliminarProductoMesa(Producto producto, Mesa mesa)
        {
            try
            {
                return productoDAO.EliminarProductoMesa(producto, mesa);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
