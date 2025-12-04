using BancoDeSangreApp.Business;
using BancoDeSangreApp.Components;
using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BancoDeSangreApp.Forms
{
    public partial class FrmRoles : Form
    {
        private RolBLL _rolBLL;
        private DataTable _dtRoles;
        private const string PLACEHOLDER_TEXT = "Buscar rol...";

        public FrmRoles()
        {
            InitializeComponent();
            _rolBLL = new RolBLL();
        }

        private void FrmRoles_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigurarDataGridView();
                dgvRoles.DataError += dgvRoles_DataError;
                dgvRoles.SelectionChanged += dgvRoles_SelectionChanged;
                CargarRoles();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el formulario:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvRoles_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"DataError: {e.Exception.Message}");
            e.ThrowException = false;
            e.Cancel = false;
        }

        private void dgvRoles_SelectionChanged(object sender, EventArgs e)
        {
            ActualizarEstadoBotones();
        }

        private void ActualizarEstadoBotones()
        {
            bool haySeleccion = dgvRoles.SelectedRows.Count > 0;
            btnEditar.Enabled = haySeleccion;
            btnEliminar.Enabled = haySeleccion;
        }

        private void ConfigurarDataGridView()
        {
            dgvRoles.EnableHeadersVisualStyles = false;
            dgvRoles.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            dgvRoles.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvRoles.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvRoles.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvRoles.ColumnHeadersHeight = 40;

            dgvRoles.DefaultCellStyle.SelectionBackColor = Color.FromArgb(41, 128, 185);
            dgvRoles.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvRoles.DefaultCellStyle.Font = new Font("Segoe UI", 9F);

            dgvRoles.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            dgvRoles.RowTemplate.Height = 35;
            dgvRoles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRoles.RowHeadersVisible = false;
            dgvRoles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRoles.MultiSelect = false;
            dgvRoles.AllowUserToResizeRows = false;
            dgvRoles.BorderStyle = BorderStyle.None;
        }

        private async void CargarRoles()
        {
            try
            {
                this.ShowLoading("Cargando roles...");
                await Task.Run(() => System.Threading.Thread.Sleep(100));

                DataTable dtTemp = await Task.Run(() => _rolBLL.ObtenerRoles());
                _dtRoles = dtTemp;

                if (_dtRoles == null || _dtRoles.Rows.Count == 0)
                {
                    dgvRoles.DataSource = null;
                    ActualizarEstadoBotones();
                    this.HideLoading();
                    MessageBox.Show("No hay roles registrados.", "Información",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                AplicarFiltros();
                ActualizarEstadoBotones();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar roles:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.HideLoading();
            }
        }

        private void AplicarFiltros()
        {
            if (_dtRoles == null || _dtRoles.Rows.Count == 0)
            {
                dgvRoles.DataSource = null;
                lblTitulo.Text = "Gestión de Roles (0)";
                return;
            }

            DataView dv = _dtRoles.DefaultView;
            string filtro = "";

            string textoBusqueda = txtBuscar.Text.Trim();
            if (!string.IsNullOrEmpty(textoBusqueda) && textoBusqueda != PLACEHOLDER_TEXT)
            {
                textoBusqueda = textoBusqueda.Replace("'", "''");
                filtro = $"(Nombre LIKE '%{textoBusqueda}%' OR Descripcion LIKE '%{textoBusqueda}%')";
            }

            try
            {
                dv.RowFilter = filtro;
                dgvRoles.DataSource = dv.ToTable();
                ConfigurarColumnas();
                lblTitulo.Text = $"Gestión de Roles ({dv.Count})";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en filtro: {ex.Message}");
                dgvRoles.DataSource = _dtRoles;
                ConfigurarColumnas();
            }
        }

        private void ConfigurarColumnas()
        {
            if (dgvRoles.Columns.Contains("IdRol"))
                dgvRoles.Columns["IdRol"].Visible = false;

            if (dgvRoles.Columns.Contains("Nombre"))
            {
                dgvRoles.Columns["Nombre"].HeaderText = "Nombre del Rol";
                dgvRoles.Columns["Nombre"].Width = 150;
            }

            if (dgvRoles.Columns.Contains("Descripcion"))
            {
                dgvRoles.Columns["Descripcion"].HeaderText = "Descripción";
                dgvRoles.Columns["Descripcion"].Width = 300;
            }

            if (dgvRoles.Columns.Contains("NivelAcceso"))
            {
                dgvRoles.Columns["NivelAcceso"].HeaderText = "Nivel Acceso";
                dgvRoles.Columns["NivelAcceso"].Width = 100;
                dgvRoles.Columns["NivelAcceso"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvRoles.Columns.Contains("CantidadUsuarios"))
            {
                dgvRoles.Columns["CantidadUsuarios"].HeaderText = "Usuarios";
                dgvRoles.Columns["CantidadUsuarios"].Width = 100;
                dgvRoles.Columns["CantidadUsuarios"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvRoles.Columns.Contains("FechaCreacion"))
            {
                dgvRoles.Columns["FechaCreacion"].HeaderText = "Fecha Creación";
                dgvRoles.Columns["FechaCreacion"].Width = 130;
                dgvRoles.Columns["FechaCreacion"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }

            dgvRoles.Refresh();
        }

        private void txtBuscar_Enter(object sender, EventArgs e)
        {
            if (txtBuscar.Text == PLACEHOLDER_TEXT)
            {
                txtBuscar.Text = "";
                txtBuscar.ForeColor = Color.Black;
            }
        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                txtBuscar.Text = PLACEHOLDER_TEXT;
                txtBuscar.ForeColor = Color.Gray;
            }
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                AplicarFiltros();
                e.Handled = true;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        private void btnRecargar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = PLACEHOLDER_TEXT;
            txtBuscar.ForeColor = Color.Gray;
            CargarRoles();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                using (var frmDetalle = new FrmRolDetalle())
                {
                    if (frmDetalle.ShowDialog() == DialogResult.OK)
                    {
                        CargarRoles();
                        MessageBox.Show("✅ Rol creado exitosamente.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear rol:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            EditarRolSeleccionado();
        }

        private void dgvRoles_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                EditarRolSeleccionado();
            }
        }

        private void EditarRolSeleccionado()
        {
            try
            {
                if (dgvRoles.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Seleccione un rol para editar.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idRol = Convert.ToInt32(dgvRoles.SelectedRows[0].Cells["IdRol"].Value);

                using (var frmDetalle = new FrmRolDetalle(idRol))
                {
                    if (frmDetalle.ShowDialog() == DialogResult.OK)
                    {
                        CargarRoles();
                        MessageBox.Show("✅ Rol actualizado exitosamente.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al editar rol:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvRoles.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Seleccione un rol para eliminar.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataGridViewRow filaSeleccionada = dgvRoles.SelectedRows[0];
                int idRol = Convert.ToInt32(filaSeleccionada.Cells["IdRol"].Value);
                string nombreRol = filaSeleccionada.Cells["Nombre"].Value?.ToString() ?? "Rol";
                int cantidadUsuarios = Convert.ToInt32(filaSeleccionada.Cells["CantidadUsuarios"].Value);

                if (cantidadUsuarios > 0)
                {
                    MessageBox.Show($"No se puede eliminar el rol '{nombreRol}' porque tiene {cantidadUsuarios} usuario(s) asignado(s).\n\n" +
                        "Primero reasigne los usuarios a otro rol.", "No se puede eliminar",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult confirmacion = MessageBox.Show(
                    $"¿Está seguro que desea eliminar el rol '{nombreRol}'?\n\n" +
                    "Esta acción no se puede deshacer.",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmacion == DialogResult.Yes)
                {
                    this.ShowLoading("Eliminando rol...");
                    await Task.Delay(200);

                    var resultado = await Task.Run(() => _rolBLL.EliminarRol(idRol));

                    this.HideLoading();

                    if (resultado.exito)
                    {
                        MessageBox.Show("✅ Rol eliminado exitosamente.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarRoles();
                    }
                    else
                    {
                        MessageBox.Show($"❌ {resultado.mensaje}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                this.HideLoading();
                MessageBox.Show($"Error al eliminar rol:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}