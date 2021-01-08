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
    public partial class FrmProductos : Form
    {
        public bool agregarProductoMesa { get; set; }
        private Producto producto = null;
        private ProductoBLL productoBLL = new ProductoBLL();
        public int numMesa { get; set; }

        public FrmProductos()
        {
            InitializeComponent();
            AgregarCabecerasGrid();
        }

        public void AgregarCabecerasGrid()
        {
            this.dgvProductos.ColumnCount = 6;
            this.dgvProductos.Columns[0].HeaderText = "No. Producto";
            this.dgvProductos.Columns[1].HeaderText = "Codigo";
            this.dgvProductos.Columns[2].HeaderText = "Nombre Producto";
            this.dgvProductos.Columns[3].HeaderText = "Fecha de Alta";
            this.dgvProductos.Columns[4].HeaderText = "Precio";
            this.dgvProductos.Columns[5].HeaderText = "Existencia";


            DataGridViewImageColumn dgvInformacionProducto = new DataGridViewImageColumn();
            dgvInformacionProducto.HeaderText = "Información Producto";
            dgvInformacionProducto.ImageLayout = DataGridViewImageCellLayout.Normal;
            dgvInformacionProducto.Image = Properties.Resources.circle_customer_help_info_information_service_support_icon_123208;
            dgvInformacionProducto.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvInformacionProducto.Tag = "InformacionProducto";
            this.dgvProductos.Columns.Insert(6, dgvInformacionProducto);
            

            DataGridViewImageColumn dgvEliminarProducto = new DataGridViewImageColumn();
            dgvEliminarProducto.HeaderText = "Eliminar Producto";
            dgvEliminarProducto.ImageLayout = DataGridViewImageCellLayout.Normal;
            dgvEliminarProducto.Image = Properties.Resources.remove_delete_minus_icon_160894;
            dgvEliminarProducto.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvEliminarProducto.Tag = "EliminarProducto";
            this.dgvProductos.Columns.Insert(7, dgvEliminarProducto);

            
            DataGridViewImageColumn dgvAgregarProductoMesa = new DataGridViewImageColumn();
            dgvAgregarProductoMesa.HeaderText = "Agregar Producto";
            dgvAgregarProductoMesa.ImageLayout = DataGridViewImageCellLayout.Normal;
            dgvAgregarProductoMesa.Image = Properties.Resources.add_insert_icon_160936;
            dgvAgregarProductoMesa.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvAgregarProductoMesa.Tag = "AgregarProducto";
            this.dgvProductos.Columns.Insert(8, dgvAgregarProductoMesa);
            


            EstiloGrid(this.dgvProductos);

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
                dg.DefaultCellStyle.SelectionBackColor = Color.Silver;
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


        public void CargaProductosGrid(string nombreProducto)
        {
            try
            {
                List<Entidades.Producto> productos = new List<Entidades.Producto>();
                productos = productoBLL.ObtenerProductos(nombreProducto);

                if (this.dgvProductos.RowCount > productos.Count)
                    this.dgvProductos.Rows.Clear();
                this.dgvProductos.RowCount = productos.Count;

                int i = 0;

                foreach (var producto in productos)
                {
                    this.dgvProductos.DefaultCellStyle.SelectionBackColor = Color.Silver;
                    this.dgvProductos.DefaultCellStyle.SelectionForeColor = Color.Black;
                    this.dgvProductos.SelectionMode = DataGridViewSelectionMode.CellSelect;
                    this.producto = null;
                    this.producto = producto;

                    this.dgvProductos[0, i].Value = this.producto.idProducto;
                    this.dgvProductos[1, i].Value = this.producto.codigo;
                    this.dgvProductos[2, i].Value = this.producto.nombreProducto;
                    this.dgvProductos[3, i].Value = this.producto.fechaAlta.ToShortDateString();
                    this.dgvProductos[4, i].Value = "$ " + this.producto.precio;
                    this.dgvProductos[5, i].Value = this.producto.cantidadExistencia;

                    if (agregarProductoMesa)
                    {
                        dgvProductos.Columns[6].Visible =
                        dgvProductos.Columns[7].Visible =
                        false;
                        dgvProductos.Columns[8].Visible = true;
                    }
                    else
                    {


                        dgvProductos.Columns[6].Visible =
                        dgvProductos.Columns[7].Visible =
                        //dgvProductos.Columns[7].Visible = 
                        true;
                        dgvProductos.Columns[8].Visible = false;
                    }
                    i++;
                }


            }


            catch (Exception ex)
            {
                Utilidades.MuestraErrores(ex.Message);

            }
        }

        private void FrmProductos_Load(object sender, EventArgs e)
        {
            try
            {
                this.CargaProductosGrid("");
                if (agregarProductoMesa)
                {
                    btnAgregarProducto.Visible = false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 5)
                {
                    string NombreFrm = this.dgvProductos.Columns[e.ColumnIndex].Tag.ToString().Replace(" ", "");
                    switch (NombreFrm)
                    {
                        case "InformacionProducto": //"";
                            
                            FrmAgregarProducto frmAgregarProducto = new FrmAgregarProducto();
                            frmAgregarProducto.producto = productoBLL.ObtenerInfoProducto(Convert.ToInt64( dgvProductos[0, e.RowIndex].Value));
                            frmAgregarProducto.mostrarInfo = true;

                            frmAgregarProducto.ShowDialog();
                          

                            break;
                        
                        case "EditarProducto": //"";
                            /*
                            FrmAsignarMesa frmAsignarMesaEditar = new FrmAsignarMesa();
                            frmAsignarMesaEditar.numMesa = Convert.ToInt32(dgvProductos[0, e.RowIndex].Value);
                            frmAsignarMesaEditar.nombreMesa = dgvProductos[1, e.RowIndex].Value.ToString();
                            frmAsignarMesaEditar.reservar = false;
                            frmAsignarMesaEditar.editarMesa = true;
                            frmAsignarMesaEditar.eliminarMesa = false;

                            frmAsignarMesaEditar.ShowDialog();
                            */
                            MessageBox.Show("Editar Producto");

                            break;

                        case "EliminarProducto": //"";
                            /*
                            Result resultado = new Result();
                            producto.idProducto = Convert.ToInt32(dgvProductos[0, e.RowIndex].Value);
                            producto.nombreProducto = dgvProductos[1, e.RowIndex].Value.ToString();
                            
                            bool editarMesa = false;
                            bool eliminarMesa = true;
                            DialogResult confirmarOperacion = Utilidades.MuestraPregunta("¿Estas Seguro de eliminar el producto " + producto.idProducto.ToString() + "?");

                            if (confirmarOperacion == DialogResult.OK)
                            {
                                resultado = productoBLL.AsignarMesas(producto, editarMesa, eliminarMesa);
                                Utilidades.MuestraInfo(resultado.mensaje);
                            }
                            */
                            MessageBox.Show("Eliminar Producto");

                            break;

                        case "AgregarProducto":
                            //MessageBox.Show("Agregar Producto");
                            MesaBLL mesaBLL = new MesaBLL();
                            Result resultado = new Result();
                            resultado = mesaBLL.AgregarProductoAMesas(numMesa, Convert.ToInt64(dgvProductos[1, e.RowIndex].Value));
                            Utilidades.MuestraInfo(resultado.mensaje);
                            break;

                        default:
                            break;
                    }
                    this.CargaProductosGrid("");
                }
            }
            catch (Exception ex)
            {

                throw ex;
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

        

        private void txtProducto_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.CargaProductosGrid(txtProducto.Text);
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
                FrmAgregarProducto frmAgregarProducto = new FrmAgregarProducto();
                frmAgregarProducto.mostrarInfo = false;
                frmAgregarProducto.ShowDialog();
            }
            catch (Exception ex)
            {

                Utilidades.MuestraErrores(ex.Message);
            }
        }

        private void btnAgregarProducto_MouseHover(object sender, EventArgs e)
        {
            try
            {
                toolTip1.Show("Agregar Producto", btnAgregarProducto);
            }
            catch (Exception ex)
            {

                Utilidades.MuestraErrores(ex.Message);
            }
        }

        private void pbBuscar_MouseHover(object sender, EventArgs e)
        {
            try
            {
               // toolTip1.Show("Buscar", btnAgregarProducto);
            }
            catch (Exception ex)
            {

                Utilidades.MuestraErrores(ex.Message);
            }
        }
    }
}
