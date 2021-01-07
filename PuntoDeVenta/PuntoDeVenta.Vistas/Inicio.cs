
using CMV.BancaAdmin.UTILS;
using PuntoDeVenta.BLL;
using PuntoDeVenta.Entidades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PuntoDeVenta.Vistas
{
    public partial class Inicio : Form
    {
        private Mesa mesa = null;
        private MesaBLL mesaBLL = new MesaBLL();
        public Inicio()
        {
            InitializeComponent();
            AgregarCabecerasGrid();
        }

        public void AgregarCabecerasGrid()
        {
            this.dgvMesasAsignadas.ColumnCount = 5;
            //this.dgvMesasAsignadas.Columns[0].HeaderText = "Ticket";
            this.dgvMesasAsignadas.Columns[0].HeaderText = "No. Mesa";
            this.dgvMesasAsignadas.Columns[1].HeaderText = "Nombre Mesa";
            this.dgvMesasAsignadas.Columns[2].HeaderText = "Hora de Asinacion";
            this.dgvMesasAsignadas.Columns[3].HeaderText = "Reservada";
            this.dgvMesasAsignadas.Columns[4].HeaderText = "Total Cuenta";

            DataGridViewImageColumn dgvAgregarProducto = new DataGridViewImageColumn();
            dgvAgregarProducto.HeaderText = "Agregar Producto";
            dgvAgregarProducto.ImageLayout = DataGridViewImageCellLayout.Normal;
            dgvAgregarProducto.Image = Properties.Resources.carrito;
            dgvAgregarProducto.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvAgregarProducto.Tag = "AgregarProducto";
            this.dgvMesasAsignadas.Columns.Insert(5, dgvAgregarProducto);

            DataGridViewImageColumn dgvCobrarMesa = new DataGridViewImageColumn();
            dgvCobrarMesa.HeaderText = "Cobrar";
            dgvCobrarMesa.ImageLayout = DataGridViewImageCellLayout.Normal;
            dgvCobrarMesa.Image = Properties.Resources.monto;
            dgvCobrarMesa.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvCobrarMesa.Tag = "Cobrar";
            this.dgvMesasAsignadas.Columns.Insert(6, dgvCobrarMesa);


            EstiloGrid(this.dgvMesasAsignadas);

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
                dg.DefaultCellStyle.SelectionBackColor = Color.ForestGreen;
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

        



        private void pictureBox1_Click(object sender, EventArgs e)
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

        private void Inicio_Load(object sender, EventArgs e)
        {
            try
            {

                this.CargaMesasGrid();

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
                List<Entidades.Mesa> mesas = new List<Entidades.Mesa>();
                mesas = mesaBLL.ObtenerMesas(true);

                if (this.dgvMesasAsignadas.RowCount > mesas.Count)
                    this.dgvMesasAsignadas.Rows.Clear();
                this.dgvMesasAsignadas.RowCount = mesas.Count;

                int i = 0;

                foreach (var mesa in mesas)
                {
                    this.dgvMesasAsignadas.DefaultCellStyle.SelectionBackColor = Color.ForestGreen;
                    this.dgvMesasAsignadas.DefaultCellStyle.SelectionForeColor = Color.Black;
                    this.dgvMesasAsignadas.SelectionMode = DataGridViewSelectionMode.CellSelect;
                    this.mesa = null;
                    this.mesa = mesa;

                    //this.dgvMesasAsignadas[0, i].Value = this.mesa.idMesa;
                    this.dgvMesasAsignadas[0, i].Value = this.mesa.numeroMesa;
                    this.dgvMesasAsignadas[1, i].Value = this.mesa.nombreMesa;
                    this.dgvMesasAsignadas[2, i].Value = this.mesa.horaAsignacion.ToShortTimeString();
                    if (this.mesa.reservada)
                    {
                        this.dgvMesasAsignadas[3, i].Value = "Si";
                    }
                    else
                    {
                        this.dgvMesasAsignadas[3, i].Value = "No";
                    }
                    
                    this.dgvMesasAsignadas[4, i].Value = "$ " + this.mesa.totalCuenta;
                    dgvMesasAsignadas.Columns[5].Visible =
                    dgvMesasAsignadas.Columns[6].Visible = true;
                    i++;
                }


            }


            catch (Exception ex)
            {
                Utilidades.MuestraErrores(ex.Message);
            }
        }

        private void AsignarMesaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FrmAgregarMesa agregarMesa = new FrmAgregarMesa();
                agregarMesa.ShowDialog();
            }
            catch (Exception ex)
            {

                Utilidades.MuestraErrores(ex.Message);
            }
        }

        private void dgvMesasAsignadas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 5)
                {
                    string NombreFrm = this.dgvMesasAsignadas.Columns[e.ColumnIndex].Tag.ToString().Replace(" ", "");
                    switch (NombreFrm)
                    {
                        
                        case "AgregarProducto"://"Generar Token";
                            //MessageBox.Show(dgvMesasAsignadas[0, e.RowIndex].Value +"");
                            //FrmAgregarProducto frmAgregarProductoCuenta = new FrmAgregarProducto();
                            //frmAgregarProductoCuenta.socio = (Socio)this.dgvSocios[0, e.RowIndex].Tag;
                            //frmAgregarProductoCuenta.socio.TipoBitacora = TipoBitacora.reposicion_de_Token_NIP;
                            //frmAgregarProductoCuenta.ShowDialog();

                            FrmProductos frmProductos = new FrmProductos();
                            frmProductos.agregarProductoMesa = true;
                            frmProductos.ShowDialog();

                            break;

                        case "Cobrar": //"Desbloquear";
                            FrmGenerarCuenta frmGenerarCuenta = new FrmGenerarCuenta();
                            //frmGenerarCuenta.socio = (Socio)this.dgvSocios[0, e.RowIndex].Tag;
                            //frmGenerarCuenta.socio.TipoBitacora = TipoBitacora.desbloqueo_de_cuenta_desde_sucursal;
                            frmGenerarCuenta.ShowDialog();

                            break;
                        default:
                            break;
                    }
                    this.CargaMesasGrid();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        

        private void mesasDisponiblesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FrmMesas frmMesas = new FrmMesas();
                frmMesas.ShowDialog();
            }
            catch (Exception ex)
            {

                Utilidades.MuestraErrores(ex.Message);
            }
        }

        private void mesasDisponiblesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                FrmMesas mesasDisponibles = new FrmMesas();
                mesasDisponibles.ShowDialog();
                this.Inicio_Load(null,null);
            }
            catch (Exception ex)
            {

                Utilidades.MuestraErrores(ex.Message);
            }
        }

        private void mesaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FrmMesas mesasDisponibles = new FrmMesas();
                mesasDisponibles.ShowDialog();
                this.Inicio_Load(null, null);
            }
            catch (Exception ex)
            {

                Utilidades.MuestraErrores(ex.Message);
            }
        }

        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FrmProductos frmProductos = new FrmProductos();
                frmProductos.ShowDialog();
                this.Inicio_Load(null, null);
            }
            catch (Exception ex)
            {

                Utilidades.MuestraErrores(ex.Message);
            }
        }
    }
}
