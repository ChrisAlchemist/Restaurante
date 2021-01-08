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
    public partial class FrmMesas : Form
    {
        private Mesa mesa = null;
        private MesaBLL mesaBLL = new MesaBLL();
        public FrmMesas()
        {
            InitializeComponent();
            AgregarCabecerasGrid();
        }

        public void AgregarCabecerasGrid()
        {
            this.dgvMesasDisponibles.ColumnCount = 3;          
            this.dgvMesasDisponibles.Columns[0].HeaderText = "No. Mesa";
            this.dgvMesasDisponibles.Columns[1].HeaderText = "Nombre Mesa";
            this.dgvMesasDisponibles.Columns[2].HeaderText = "Fecha de Alta";
            
            DataGridViewImageColumn dgvAsignar = new DataGridViewImageColumn();
            dgvAsignar.HeaderText = "Asinar Mesa";
            dgvAsignar.ImageLayout = DataGridViewImageCellLayout.Normal;
            dgvAsignar.Image = Properties.Resources.registrar;
            dgvAsignar.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvAsignar.Tag = "AsignarMesa";
            this.dgvMesasDisponibles.Columns.Insert(3, dgvAsignar);

            DataGridViewImageColumn dgvReservar = new DataGridViewImageColumn();
            dgvReservar.HeaderText = "Reservar";
            dgvReservar.ImageLayout = DataGridViewImageCellLayout.Normal;
            dgvReservar.Image = Properties.Resources.man_user_icon_160878;
            dgvReservar.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvReservar.Tag = "Reservar";
            this.dgvMesasDisponibles.Columns.Insert(4, dgvReservar);

            DataGridViewImageColumn dgvEditarMesa = new DataGridViewImageColumn();
            dgvEditarMesa.HeaderText = "Editar Mesa";
            dgvEditarMesa.ImageLayout = DataGridViewImageCellLayout.Normal;
            dgvEditarMesa.Image = Properties.Resources.edit_object_icon_160911;
            dgvEditarMesa.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvEditarMesa.Tag = "EditarMesa";
            this.dgvMesasDisponibles.Columns.Insert(5, dgvEditarMesa);

            DataGridViewImageColumn dgvEliminarMesa = new DataGridViewImageColumn();
            dgvEliminarMesa.HeaderText = "Eliminar Mesa";
            dgvEliminarMesa.ImageLayout = DataGridViewImageCellLayout.Normal;
            dgvEliminarMesa.Image = Properties.Resources.remove_delete_minus_icon_160894;
            dgvEliminarMesa.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvEliminarMesa.Tag = "EliminarMesa";
            this.dgvMesasDisponibles.Columns.Insert(6, dgvEliminarMesa);


            EstiloGrid(this.dgvMesasDisponibles);

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


        public void CargaMesasGrid()
        {
            try
            {
                List<Entidades.Mesa> mesas = new List<Entidades.Mesa>();
                mesas = mesaBLL.ObtenerMesas(false);

                if (this.dgvMesasDisponibles.RowCount > mesas.Count)
                    this.dgvMesasDisponibles.Rows.Clear();
                this.dgvMesasDisponibles.RowCount = mesas.Count;

                int i = 0;

                foreach (var mesa in mesas)
                {
                    this.dgvMesasDisponibles.DefaultCellStyle.SelectionBackColor = Color.Silver;
                    this.dgvMesasDisponibles.DefaultCellStyle.SelectionForeColor = Color.Black;
                    this.dgvMesasDisponibles.SelectionMode = DataGridViewSelectionMode.CellSelect;
                    this.mesa = null;
                    this.mesa = mesa;

                    this.dgvMesasDisponibles[0, i].Value = this.mesa.numeroMesa;
                    this.dgvMesasDisponibles[1, i].Value = this.mesa.nombreMesa;
                    this.dgvMesasDisponibles[2, i].Value = this.mesa.fechaAlta;
                    
                    dgvMesasDisponibles.Columns[3].Visible =
                    dgvMesasDisponibles.Columns[4].Visible = true;
                    i++;
                }


            }


            catch (Exception ex)
            {
                Utilidades.MuestraErrores(ex.Message);
                
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

        private void FrmMesas_Load(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                
                FrmAgregarMesa frmAgregarMesa = new FrmAgregarMesa();
                frmAgregarMesa.ShowDialog();
                
            }
            catch (Exception ex )
            {
                Utilidades.MuestraErrores(ex.Message);
            }
            
        }

        private void dgvMesasDisponibles_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 3)
                {
                    string NombreFrm = this.dgvMesasDisponibles.Columns[e.ColumnIndex].Tag.ToString().Replace(" ", "");
                    switch (NombreFrm)
                    {

                        case "AsignarMesa"://"";                            
                            FrmAsignarMesa frmAsignarMesa = new FrmAsignarMesa();
                            frmAsignarMesa.numMesa = Convert.ToInt32(dgvMesasDisponibles[0, e.RowIndex].Value);
                            frmAsignarMesa.nombreMesa = dgvMesasDisponibles[1, e.RowIndex].Value.ToString();
                            frmAsignarMesa.reservar = false;
                            frmAsignarMesa.editarMesa = false;
                            frmAsignarMesa.eliminarMesa = false;
                            frmAsignarMesa.ShowDialog();

                            break;

                        case "Reservar": //"";
                            
                            FrmAsignarMesa frmAsignarMesaReservar = new FrmAsignarMesa();
                            frmAsignarMesaReservar.numMesa = Convert.ToInt32(dgvMesasDisponibles[0, e.RowIndex].Value);
                            frmAsignarMesaReservar.nombreMesa = dgvMesasDisponibles[1, e.RowIndex].Value.ToString();
                            frmAsignarMesaReservar.reservar = true;
                            frmAsignarMesaReservar.editarMesa = false;
                            frmAsignarMesaReservar.eliminarMesa = false;
                            frmAsignarMesaReservar.ShowDialog();

                            break;

                        case "EditarMesa": //"";
                            FrmAsignarMesa frmAsignarMesaEditar = new FrmAsignarMesa();
                            frmAsignarMesaEditar.numMesa = Convert.ToInt32(dgvMesasDisponibles[0, e.RowIndex].Value);
                            frmAsignarMesaEditar.nombreMesa = dgvMesasDisponibles[1, e.RowIndex].Value.ToString();
                            frmAsignarMesaEditar.reservar = false;
                            frmAsignarMesaEditar.editarMesa = true;
                            frmAsignarMesaEditar.eliminarMesa = false;

                            frmAsignarMesaEditar.ShowDialog();

                            break;
                        case "EliminarMesa": //"";
                            Result resultado = new Result();
                            mesa.numeroMesa = Convert.ToInt32(dgvMesasDisponibles[0, e.RowIndex].Value);
                            mesa.nombreMesa = dgvMesasDisponibles[1, e.RowIndex].Value.ToString();
                            mesa.reservada = false;
                            bool editarMesa = false;
                            bool eliminarMesa = true;
                            DialogResult confirmarOperacion = Utilidades.MuestraPregunta("¿Estas Seguro la mesa " + mesa.numeroMesa.ToString() + "?");

                            if (confirmarOperacion == DialogResult.OK)
                            {
                                resultado = mesaBLL.AsignarMesas(mesa, editarMesa, eliminarMesa);
                                Utilidades.MuestraInfo(resultado.mensaje);
                            }
                            

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

        private void button1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Agregar Mesa", button1);
        }
    }
}
