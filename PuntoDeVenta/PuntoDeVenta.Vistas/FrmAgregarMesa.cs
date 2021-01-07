using CMV.BancaAdmin.UTILS;
using PuntoDeVenta.BLL;
using PuntoDeVenta.Entidades;
using PuntoDeVenta.Vistas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVenta
{
    public partial class FrmAgregarMesa : Form
    {
        public FrmAgregarMesa()
        {
            InitializeComponent();
        }

      

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAgregarMesa_Click(object sender, EventArgs e)
        {
            try
            {
                Mesa mesa = new Mesa();

                if(TxtNumMesa.Text == "")
                {
                    TxtNumMesa.Text = "0";
                }
                mesa.numeroMesa = Convert.ToInt32(TxtNumMesa.Text.ToString());
                mesa.nombreMesa = TxtNombreMesa.Text.ToString();
                if (mesa.numeroMesa != 0 && mesa.nombreMesa != "")
                {
                    Result resultado = new Result();
                    MesaBLL mesaBLL = new MesaBLL();
                    resultado = mesaBLL.AgregarMesas(mesa);
                    Utilidades.MuestraInfo(resultado.mensaje);
                    
                    if(resultado.estatus == 200)
                    {
                        this.Close();                        
                    }
                }
                else
                {
                    if (mesa.numeroMesa == 0)
                    {
                        
                        Utilidades.MuestraAdvertencias("Debes de ingresar un número de Mesa");
                    }
                    else if(mesa.nombreMesa == "")
                    {
                        Utilidades.MuestraAdvertencias("Debes de ingresar un nombre de Mesa");
                    }
                }
            }
            catch (Exception ex)
            {

                Utilidades.MuestraErrores(ex.Message);
            }
        }

        private void TxtNumMesa_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Char.IsDigit(e.KeyChar))
                    e.Handled = false;
                else if ((int)e.KeyChar == (int)ConsoleKey.Backspace)
                    e.Handled = false;
                else
                    e.Handled = true;
            }
            catch (Exception ex)
            {
                Utilidades.MuestraErrores(ex.Message);
            }
        }
    }
}
