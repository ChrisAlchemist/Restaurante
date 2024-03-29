﻿
using PuntoDeVenta.DAO;
using PuntoDeVenta.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeVenta.BLL
{

    public class MesaBLL
    {
        private MesaDAO mesaDAO;

        public MesaBLL()
        {
            try
            {
                mesaDAO = new MesaDAO();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<Mesa> ObtenerMesas(bool asiganda)
        {
            try
            {
                return mesaDAO.ObtenerMesas(asiganda);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Result AgregarMesas(Mesa mesa)
        {
            try
            {
                return mesaDAO.AgregarMesas(mesa);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Result AsignarMesas(Mesa mesa, bool editarMesa, bool eliminarMesa)
        {
            try
            {
                return mesaDAO.AsignarMesas(mesa, editarMesa, eliminarMesa);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public Result AgregarProductoAMesas(int numMesa, Int64 codigoProducto)
        {
            try
            {
                return mesaDAO.AgregarProductoAMesas(numMesa, codigoProducto);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<Producto> ObtenerProductosMesa(Mesa mesa)
        {
            try
            {
                return mesaDAO.ObtenerProductoAMesa(mesa);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Result PagarCuenta(string ticket)
        {
            try
            {
                return mesaDAO.PagarCuenta(ticket);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
