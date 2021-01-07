using AccesoDatos;
using PuntoDeVenta.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeVenta.DAO
{
    

    public class ProductoDAO
    {
        #region Variables
        private DBManager db = new DBManager(System.Configuration.ConfigurationManager.AppSettings["servidorBD"]);
        #endregion Variables
        #region Funciones

        public List<Producto> ObtenerProductos(string nombreProducto)
        {
            List<Producto> productos = new List<Producto>();
            try
            {
                using (db = new DBManager(System.Configuration.ConfigurationManager.AppSettings["Instancia"]))
                {
                    db.Open();
                    db.CreateParameters(1);
                    db.AddParameters(0, "@nombreProducto", nombreProducto);

                    db.ExecuteReader(CommandType.StoredProcedure, "SP_RESTAURANTE_OBTENER_PRODUCTOS");
                    while (db.DataReader.Read())
                    {
                        Producto producto = new Producto();

                        producto.idProducto = Convert.ToInt64(db.DataReader["ID_PRODUCTO"].ToString().ToUpper());
                        producto.codigo = Convert.ToInt64(db.DataReader["CODIGO"].ToString().ToUpper());
                        producto.nombreProducto = db.DataReader["NOMBRE_PRODUCTO"].ToString().ToUpper();
                        producto.descripcionProducto = db.DataReader["DESCRIPCION_PRODUCTO"].ToString().ToUpper();
                        producto.fechaAlta = Convert.ToDateTime(db.DataReader["FECHA_ALTA"].ToString());
                        producto.precio = Convert.ToDouble(db.DataReader["PRECIO"].ToString());
                        producto.activo = Convert.ToBoolean(db.DataReader["ACTIVO"].ToString());
                        productos.Add(producto);

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return productos;
        }

        public Result AgregarProducto(Producto producto)
        {
            Result resultado = new Result();
            try
            {
                using (db = new DBManager(System.Configuration.ConfigurationManager.AppSettings["Instancia"]))
                {
                    db.Open();
                    db.CreateParameters(4);
                    db.AddParameters(0, "@codigo", producto.codigo);
                    db.AddParameters(1, "@nombreProducto", producto.nombreProducto);
                    db.AddParameters(2, "@descripcionProducto", producto.descripcionProducto);
                    db.AddParameters(3, "@precio", producto.precio);

                    db.ExecuteReader(CommandType.StoredProcedure, "SP_RESTAURANTE_AGREGAR_PRODUCTO");

                    if (db.DataReader.Read())
                    {
                        resultado.estatus = Convert.ToInt32(db.DataReader["ESTATUS"].ToString());

                        if (resultado.estatus == 200)
                        {
                            resultado.mensaje = db.DataReader["MENSAJE"].ToString();
                        }
                        else
                        {
                            resultado.mensaje = db.DataReader["error_message"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultado;
        }

        public Producto ObtenerInfoProducto(Int64 numProducto)
        {
            Producto producto = new Producto();

            try
            {
                using (db = new DBManager(System.Configuration.ConfigurationManager.AppSettings["Instancia"]))
                {
                    db.Open();
                    db.CreateParameters(1);
                    db.AddParameters(0, "@numProducto", numProducto);

                    db.ExecuteReader(CommandType.StoredProcedure, "SP_RESTAURANTE_OBTENER_INFORMACION_PRODUCTO");
                    if (db.DataReader.Read())
                    {
                        
                        producto.idProducto = Convert.ToInt64(db.DataReader["ID_PRODUCTO"].ToString().ToUpper());
                        producto.codigo = Convert.ToInt64(db.DataReader["CODIGO"].ToString().ToUpper());
                        producto.nombreProducto = db.DataReader["NOMBRE_PRODUCTO"].ToString().ToUpper();
                        producto.descripcionProducto = db.DataReader["DESCRIPCION_PRODUCTO"].ToString().ToUpper();
                        producto.fechaAlta = Convert.ToDateTime(db.DataReader["FECHA_ALTA"].ToString());
                        producto.precio = Convert.ToDouble(db.DataReader["PRECIO"].ToString());
                        producto.activo = Convert.ToBoolean(db.DataReader["ACTIVO"].ToString());
                        
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return producto;
        }

        public Result EditarProducto(Producto producto, bool editarProducto, bool eliminarProdcuto)
        {
            Result resultado = new Result();
            try
            {
                using (db = new DBManager(System.Configuration.ConfigurationManager.AppSettings["Instancia"]))
                {
                    db.Open();
                    db.CreateParameters(7);
                    db.AddParameters(0, "@numProducto", producto.idProducto);
                    db.AddParameters(1, "@codigo", producto.codigo);
                    db.AddParameters(2, "@nombreProducto", producto.nombreProducto);
                    db.AddParameters(3, "@precio", producto.precio);
                    db.AddParameters(4, "@descripcionProducto", producto.descripcionProducto);
                    db.AddParameters(5, "@editar", editarProducto);
                    db.AddParameters(6, "@elimiar", eliminarProdcuto);

                    db.ExecuteReader(CommandType.StoredProcedure, "SP_RESTAURANTE_EDITAR_PRODUCTO");

                    if (db.DataReader.Read())
                    {
                        resultado.estatus = Convert.ToInt32(db.DataReader["ESTATUS"].ToString());

                        if (resultado.estatus == 200)
                        {
                            resultado.mensaje = db.DataReader["MENSAJE"].ToString();
                        }
                        else
                        {
                            resultado.mensaje = db.DataReader["error_message"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultado;
        }


    }
    #endregion Funciones
}
