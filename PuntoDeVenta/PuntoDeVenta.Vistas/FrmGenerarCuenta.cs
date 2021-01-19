using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CMV.BancaAdmin.UTILS;
using PuntoDeVenta.BLL;
using PuntoDeVenta.Entidades;

namespace PuntoDeVenta.Vistas
{
    public partial class FrmGenerarCuenta : Form
    {
        public Mesa mesa { get; set; }
        private Producto producto = null;
        MesaBLL mesaBLL = new MesaBLL();
        public FrmGenerarCuenta()
        {
            InitializeComponent();
            AgregarCabecerasGrid();
        }

        public void AgregarCabecerasGrid()
        {
            this.dgvProductosMesa.ColumnCount = 5;
            //this.dgvMesasAsignadas.Columns[0].HeaderText = "Ticket";
            this.dgvProductosMesa.Columns[0].HeaderText = "Cantidad";
            this.dgvProductosMesa.Columns[1].HeaderText = "Codigo";
            this.dgvProductosMesa.Columns[2].HeaderText = "Producto";                        
            this.dgvProductosMesa.Columns[3].HeaderText = "Precio";            
            this.dgvProductosMesa.Columns[4].HeaderText = "Total del producto";            
              
            

            DataGridViewImageColumn dgvAgregarProducto = new DataGridViewImageColumn();
            dgvAgregarProducto.HeaderText = "Eliminar Producto";
            dgvAgregarProducto.ImageLayout = DataGridViewImageCellLayout.Normal;
            dgvAgregarProducto.Image = Properties.Resources.remove_delete_minus_icon_160894;
            dgvAgregarProducto.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvAgregarProducto.Tag = "EliminarProducto";
            this.dgvProductosMesa.Columns.Insert(5, dgvAgregarProducto);

           
            EstiloGrid(this.dgvProductosMesa);

        }

        public static void EstiloGrid(DataGridView dg)
        {
            try
            {
                dg.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dg.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dg.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dg.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
                dg.EnableHeadersVisualStyles = false;
                dg.DefaultCellStyle.SelectionBackColor = Color.DarkCyan;//ForestGreen;
                dg.DefaultCellStyle.SelectionForeColor = Color.Black;
                dg.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dg.MultiSelect = false;
                dg.GridColor = SystemColors.ControlLight;
                dg.AllowUserToAddRows = false;
                dg.AllowUserToDeleteRows = false;
                dg.AllowUserToResizeColumns = false;
                dg.AllowUserToResizeRows = false;
                dg.SelectionMode = DataGridViewSelectionMode.CellSelect;
                dg.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dg.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9, FontStyle.Bold);
                dg.ReadOnly = true;
                dg.RowHeadersVisible = false;
                dg.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                dg.BackgroundColor = Color.White;
                dg.BorderStyle = BorderStyle.None;
                dg.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dg.Dock = DockStyle.Fill;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void CargaMesasGrid()
        {
            try
            {
                List<Entidades.Producto> productos = new List<Entidades.Producto>();
                productos = mesaBLL.ObtenerProductosMesa(mesa);

                if (this.dgvProductosMesa.RowCount > productos.Count)
                    this.dgvProductosMesa.Rows.Clear();
                this.dgvProductosMesa.RowCount = productos.Count;

                int i = 0;

                foreach (var producto in productos)
                {
                    this.dgvProductosMesa.DefaultCellStyle.SelectionBackColor = Color.Silver;//ForestGreen;
                    this.dgvProductosMesa.DefaultCellStyle.SelectionForeColor = Color.Black;
                    this.dgvProductosMesa.SelectionMode = DataGridViewSelectionMode.CellSelect;
                    this.producto = null;
                    this.producto = producto;

                    //this.dgvMesasAsignadas[0, i].Value = this.mesa.idMesa;
                    this.dgvProductosMesa[0, i].Value = this.producto.cantidadProductos;
                    this.dgvProductosMesa[1, i].Value = this.producto.codigo;
                    this.dgvProductosMesa[2, i].Value = this.producto.nombreProducto;
                    this.dgvProductosMesa[3, i].Value = "$" + this.producto.precio;
                    this.dgvProductosMesa[4, i].Value = "$" + this.producto.totalProductos;
                    
                    dgvProductosMesa.Columns[5].Visible = true;
                    i++;
                }
            }
            catch (Exception ex)
            {
                Utilidades.MuestraErrores(ex.Message);
            }
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

        private void FrmGenerarCuenta_Load(object sender, EventArgs e)
        {
            try
            {
                if(mesa != null)
                {
                    lblTitulo.Text = "Cobro de mesa: " + mesa.nombreMesa;
                    LblTotalCuenta.Text = "$ " + mesa.totalCuenta;
                    this.CargaMesasGrid();
                }
            }
            catch (Exception ex)
            {

                Utilidades.MuestraErrores(ex.Message);
            }
        }

        private void BtnAgregarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                FrmProductos frmProductos = new FrmProductos();
                frmProductos.agregarProductoMesa = true;
                frmProductos.numMesa = Convert.ToInt32(mesa.numeroMesa);
                frmProductos.ShowDialog();
                CargaMesasGrid();
            }
            catch (Exception ex)
            {

                Utilidades.MuestraErrores(ex.Message);
            }
        }

        private void dgvProductosMesa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 5)
                {
                    string NombreFrm = this.dgvProductosMesa.Columns[e.ColumnIndex].Tag.ToString().Replace(" ", "");
                    switch (NombreFrm)
                    {


                        case "EliminarProducto": //"Desbloquear";
                            FrmEliminarProducto frmEliminarProducto = new FrmEliminarProducto();
                            Producto producto = new Producto();
                            frmEliminarProducto.mesa = mesa;

                            producto.nombreProducto = dgvProductosMesa[2, e.RowIndex].Value.ToString();
                            producto.codigo = Convert.ToInt64(dgvProductosMesa[1, e.RowIndex].Value.ToString());
                            producto.cantidadProductos = Convert.ToInt32(dgvProductosMesa[0, e.RowIndex].Value.ToString());
                            frmEliminarProducto.producto = producto;

                            frmEliminarProducto.ShowDialog();

                            //Utilidades.MuestraPregunta("¿Cantidad de " + dgvProductosMesa[2, e.RowIndex].Value.ToString()+ " que deseas eliminar de la cuenta?");

                            //dgvProductosMesa[1, e.RowIndex].Value.ToString();
                            

                            break;
                        default:
                            break;
                    }
                    //this.CargaMesasGrid();
                    FrmGenerarCuenta_Load(null,null);
                }
            }
            catch (Exception ex)
            {

                Utilidades.MuestraErrores(ex.Message);
            }
        }
    }
}
