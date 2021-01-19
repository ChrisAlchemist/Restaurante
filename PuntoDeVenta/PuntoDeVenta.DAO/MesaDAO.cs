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
    public class MesaDAO
    {
        #region Variables
        private DBManager db = new DBManager(System.Configuration.ConfigurationManager.AppSettings["servidorBD"]);
        #endregion Variables
        #region Funciones

        public List<Mesa> ObtenerMesas(bool asiganda)
        {
            List<Mesa> mesas = new List<Mesa>();
            try
            {
                using (db = new DBManager(System.Configuration.ConfigurationManager.AppSettings["Instancia"]))
                {
                    db.Open();
                    db.CreateParameters(1);
                    db.AddParameters(0,"@asiganda", asiganda);

                    db.ExecuteReader(CommandType.StoredProcedure, "SP_RESTAURANTE_OBTENER_MESAS");
                    while (db.DataReader.Read())
                    {
                        Mesa mesa = new Mesa();

                        mesa.idMesa = Convert.ToInt32(db.DataReader["ID_MESA"].ToString().ToUpper());
                        mesa.numeroMesa = Convert.ToInt32(db.DataReader["NUM_MESA"].ToString().ToUpper());
                        mesa.nombreMesa = db.DataReader["NOMBRE_MESA"].ToString().ToUpper();
                        mesa.reservada = Convert.ToBoolean(db.DataReader["RESERVADA"].ToString());
                        mesa.fechaAlta = Convert.ToDateTime(db.DataReader["FECHA_ALTA"].ToString());
                        //mesa.horaAsignacion = Convert.ToDateTime(db.DataReader["HORA_ASIGNACION"].ToString());
                        mesa.horaAsignacion = db.DataReader["HORA_ASIGNACION"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(db.DataReader["HORA_ASIGNACION"].ToString());
                        mesa.totalCuenta = db.DataReader["TOTAL_CUENTA"] == DBNull.Value ? 0: Convert.ToDouble(db.DataReader["TOTAL_CUENTA"].ToString());
                        mesa.asignada = Convert.ToBoolean(db.DataReader["ASIGNADA"].ToString());

                        mesas.Add(mesa);
                        
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return mesas;
        }

        public Result AgregarMesas(Mesa mesa)
        {
            Result resultado = new Result();
            try
            {
                using (db = new DBManager(System.Configuration.ConfigurationManager.AppSettings["Instancia"]))
                {
                    db.Open();
                    db.CreateParameters(2);
                    db.AddParameters(0, "@numMesa", mesa.numeroMesa);
                    db.AddParameters(1, "@nombreMesa", mesa.nombreMesa);

                    db.ExecuteReader(CommandType.StoredProcedure, "SP_RESTAURANTE_AGREGAR_MESA");

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

        public Result AsignarMesas(Mesa mesa, bool editarMesa, bool eliminarMesa)
        {
            Result resultado = new Result();
            try
            {
                using (db = new DBManager(System.Configuration.ConfigurationManager.AppSettings["Instancia"]))
                {
                    db.Open();
                    db.CreateParameters(5);
                    db.AddParameters(0, "@numMesa", mesa.numeroMesa);
                    db.AddParameters(1, "@nombreMesa", mesa.nombreMesa);
                    db.AddParameters(2, "@reservar", mesa.reservada);
                    db.AddParameters(3, "@editar", editarMesa);
                    db.AddParameters(4, "@elimiar", eliminarMesa);

                    db.ExecuteReader(CommandType.StoredProcedure, "SP_RESTAURANTE_EDITAR_MESA");

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
        public Result AgregarProductoAMesas(int numMesa, Int64 codigoProducto)
        {
            Result resultado = new Result();
            try
            {
                using (db = new DBManager(System.Configuration.ConfigurationManager.AppSettings["Instancia"]))
                {
                    db.Open();
                    db.CreateParameters(2);
                    db.AddParameters(0, "@numMesa", numMesa);
                    db.AddParameters(1, "@codigoProducto", codigoProducto);
                    

                    db.ExecuteReader(CommandType.StoredProcedure, "SP_RESTAURANTE_AGREGAR_PRODUCTO_A_MESA");

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

        public List<Producto> ObtenerProductoAMesa(Mesa mesa)
        {
            List<Producto> mesas = new List<Producto>();
            try
            {
                using (db = new DBManager(System.Configuration.ConfigurationManager.AppSettings["Instancia"]))
                {
                    db.Open();
                    db.CreateParameters(1);
                    db.AddParameters(0, "@numMesa", mesa.numeroMesa);

                    db.ExecuteReader(CommandType.StoredProcedure, "SP_RESTAURANTE_OBTENERL_PRODUCTOS_AGRUPADOS_MESA");
                    while (db.DataReader.Read())
                    {
                        Producto producto = new Producto();

                        producto.codigo = Convert.ToInt64(db.DataReader["CODIGO_PRODUCTO"].ToString().ToUpper());                        
                        producto.nombreProducto = db.DataReader["NOMBRE_PRODUCTO"].ToString().ToUpper();                        
                        //producto.fechaAlta = Convert.ToDateTime(db.DataReader["FECHA_ALTA"].ToString());
                        producto.precio = db.DataReader["PRECIO_PRODUCTO"] == DBNull.Value ? 0 : Convert.ToDouble(db.DataReader["PRECIO_PRODUCTO"].ToString());
                        
                        producto.totalProductos = db.DataReader["TOTAL_PRODUCTOS"] == DBNull.Value ? 0 : Convert.ToDouble(db.DataReader["TOTAL_PRODUCTOS"].ToString());
                        producto.cantidadProductos = Convert.ToInt32(db.DataReader["CANTIDAD_PRODUCTOS"].ToString());

                        mesas.Add(producto);

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return mesas;
        }

        #endregion
    }
}
