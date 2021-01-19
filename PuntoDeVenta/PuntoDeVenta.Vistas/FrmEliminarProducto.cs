using CMV.BancaAdmin.UTILS;
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
using PuntoDeVenta.BLL;

namespace PuntoDeVenta.Vistas
{
    public partial class FrmEliminarProducto : Form
    {
        public Mesa mesa { get; set; }
        public Producto producto { get; set; }

        ProductoBLL productoBLL = new ProductoBLL();

        public FrmEliminarProducto()
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

        private void FrmEliminarProducto_Load(object sender, EventArgs e)
        {
            try
            {
                lblTitulo.Text = "Eliminar producto de " + mesa.nombreMesa;
                lblNomProd.Text = producto.nombreProducto.ToString();
            }
            catch (Exception ex)
            {

                Utilidades.MuestraErrores(ex.Message);
            }
        }

        private void TxtCantidadEliminar_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {

                Utilidades.MuestraErrores(ex.Message);
            }
        }

        private void btnEliminarProductos_Click(object sender, EventArgs e)
        {
            try
            {
                if (TxtCantidadEliminar.Text == "")
                {
                    Utilidades.MuestraAdvertencias("Debes de ingresar una cantidad de productos a eliminar");
                    return;
                }

                int productosEliminar = Convert.ToInt32(TxtCantidadEliminar.Text.ToString());

                if (productosEliminar > producto.cantidadProductos)
                {
                    Utilidades.MuestraAdvertencias("Debes de ingresar una cantidad menor de productos");
                    return;
                }

                

                DialogResult confirmarOperacion = Utilidades.MuestraPregunta("¿Estas Seguro de eliminar "+productosEliminar+" producto(s)?");
                if (confirmarOperacion == DialogResult.OK)
                {
                    for (int i = 0; i < productosEliminar; i++)
                    {
                        productoBLL.EliminarProductoMesa(producto, mesa);
                    }
                    Utilidades.MuestraInfo(productosEliminar + " producto(s) eliminado(s)");
                    this.Close();
                }
            }
            catch (Exception ex)
            {

                Utilidades.MuestraErrores(ex.Message);
            }
        }
    }
}
