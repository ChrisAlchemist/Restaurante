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
    public partial class FrmAgregarProducto : Form
    {
        public Producto producto { get; set; }
        public bool mostrarInfo { get; set; }
        ProductoBLL productoBLL = new ProductoBLL();
        public FrmAgregarProducto()
        {
            InitializeComponent();
        }

        public void MostrarInformacion()
        {
            producto = productoBLL.ObtenerInfoProducto(producto.idProducto);
            TxtFechaAlta.Text = producto.fechaAlta.ToShortDateString();
            TxtCodigo.Text = producto.codigo.ToString();
            TxtNombreProducto.Text = producto.nombreProducto.ToString();
            TxtPrecio.Text = producto.precio.ToString();
            txtDescripcion.Text = producto.descripcionProducto.ToString();
            lblTitulo.Text = "Información del Producto";

            TxtFechaAlta.ReadOnly =
            TxtCodigo.ReadOnly =
            TxtNombreProducto.ReadOnly =
            TxtPrecio.ReadOnly =
            txtDescripcion.ReadOnly =                        
            lblFechaAlta.Visible =
            TxtFechaAlta.Visible =            
            btnHabilitarEdicion.Visible = true;

            btnEditarProducto.Visible =
            btnAgregarProducto.Visible = false;
        }

        public void HabilitarEditarProducto()
        {
            TxtFechaAlta.ReadOnly =
            TxtCodigo.ReadOnly =
            TxtNombreProducto.ReadOnly =
            TxtPrecio.ReadOnly =
            txtDescripcion.ReadOnly =            
            btnHabilitarEdicion.Visible =
            lblFechaAlta.Visible =
            TxtFechaAlta.Visible = 
            btnAgregarProducto.Visible = 
            btnHabilitarEdicion.Visible = false;
            btnEditarProducto.Visible = true;
        }

        public bool Valido()
        {
            bool valido = false;
            try
            {
                if (TxtCodigo.Text == "")
                {
                    Utilidades.MuestraAdvertencias("Debes de ingresar un codigo de Producto");
                }
                else if (TxtPrecio.Text == "")
                {
                    Utilidades.MuestraAdvertencias("Debes de ingresar un precio de Producto");
                }
                else if (TxtNombreProducto.Text == "")
                {
                    Utilidades.MuestraAdvertencias("Debes de ingresar un nombre de Producto");
                }
                else if (txtDescripcion.Text == "")
                {
                    Utilidades.MuestraAdvertencias("Debes de ingresar un descripción de Producto");
                }
                else
                {
                    valido = true;
                }
            }
            catch (Exception ex)
            {

                Utilidades.MuestraErrores(ex.Message);
            }
            return valido;
            
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

        

        private void TxtCodigo_KeyPress(object sender, KeyPressEventArgs e)
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

        private void TxtPrecio_KeyPress(object sender, KeyPressEventArgs e)
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

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                Producto producto = new Producto();

                if (Valido())
                {
                 
                    producto.codigo = Convert.ToInt64(TxtCodigo.Text.ToString());
                    producto.nombreProducto = TxtNombreProducto.Text.ToString();
                    producto.descripcionProducto = txtDescripcion.Text.ToString();
                    producto.precio = Convert.ToDouble(TxtPrecio.Text.ToString());

                    Result resultado = new Result();
                    ProductoBLL productoBLL = new ProductoBLL();
                    resultado = productoBLL.AgregarProducto(producto);
                    Utilidades.MuestraInfo(resultado.mensaje);

                    if (resultado.estatus == 200)
                    {
                        this.Close();
                    }
                }

            }
            catch (Exception ex)
            {

                Utilidades.MuestraErrores(ex.Message);
            }
        }

        private void FrmAgregarProducto_Load(object sender, EventArgs e)
        {
            try
            {
                if (mostrarInfo)
                {
                    MostrarInformacion();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void btnEditarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                if (Valido())
                {
                    Result resultado = new Result();
                    producto.codigo = Convert.ToInt64(TxtCodigo.Text.ToString());
                    producto.nombreProducto = TxtNombreProducto.Text.ToString();
                    producto.precio = Convert.ToDouble(TxtPrecio.Text.ToString());
                    producto.descripcionProducto = txtDescripcion.Text.ToString();
                    resultado = productoBLL.EditarProducto(producto, true, false);
                    Utilidades.MuestraInfo(resultado.mensaje);
                    MostrarInformacion();
                }

                
            }
            catch (Exception ex)
            {
                Utilidades.MuestraErrores(ex.Message);
            }
        }

        private void btnHabilitarEdicion_Click(object sender, EventArgs e)
        {
            try
            {
                HabilitarEditarProducto();
            }
            catch (Exception ex)
            {

                Utilidades.MuestraErrores(ex.Message);
            }
        }
    }
}
