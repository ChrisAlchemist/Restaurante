
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
    }
}
