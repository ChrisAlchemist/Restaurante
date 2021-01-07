using CMV.BancaAdmin.UTILS;
using PuntoDeVenta.BLL;
using PuntoDeVenta.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVenta.Vistas
{
    public partial class FrmAsignarMesa : Form
    {
        public int numMesa { get; set; }
        public bool reservar { get; set; }
        public string nombreMesa { get; set; }
        public bool editarMesa { get; set; }
        public bool eliminarMesa { get; set; }

        MesaBLL mesaBLL = new MesaBLL();
        Mesa mesa = new Mesa();
        
        public FrmAsignarMesa()
        {
            InitializeComponent();
                        
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {

                Utilidades.MuestraErrores(ex.Message);
            }
        }

        private void btnAsignarMesa_Click(object sender, EventArgs e)
        {
            try
            {
                Result resultado = new Result();
                if(TxtNombreMesa.Text == "")
                {
                    
                    Utilidades.MuestraAdvertencias("Debes de ingresar un nombre para la mesa seleccionada");
                }
                else
                {
                    mesa.nombreMesa = TxtNombreMesa.Text.ToString();
                    resultado = mesaBLL.AsignarMesas(mesa, editarMesa, eliminarMesa);
                }
                Utilidades.MuestraInfo(resultado.mensaje);
                
                if(resultado.estatus == 200)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {

                Utilidades.MuestraAdvertencias(ex.Message);
            }
        }

        private void FrmAsignarMesa_Load(object sender, EventArgs e)
        {
            try
            {
                mesa.reservada = reservar;
                mesa.numeroMesa = numMesa;
                mesa.nombreMesa = nombreMesa;
                TxtNombreMesa.Text = mesa.nombreMesa.ToString();
                lblNumMesa.Text = mesa.numeroMesa.ToString();
                if (reservar)
                {

                    btnAsignarMesa.Image = Properties.Resources.man_user_icon_160878;
                    btnAsignarMesa.Text = 
                    lblTitulo.Text = "Reservar Mesa";
                }
                else if (editarMesa)
                {
                    btnAsignarMesa.Image = Properties.Resources.edit_object_icon_160911;
                    btnAsignarMesa.Text =
                    lblTitulo.Text = "Editar Mesa";
                }
                else
                {
                    btnAsignarMesa.Image = Properties.Resources.registrar;
                    btnAsignarMesa.Text =
                    lblTitulo.Text = "Asignar Mesa";
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
