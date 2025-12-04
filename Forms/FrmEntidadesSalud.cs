using BancoDeSangreApp.Business;
using BancoDeSangreApp.Components;
using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BancoDeSangreApp.Forms
{
    public partial class FrmEntidadesSalud : Form
    {
        private EntidadSaludBLL _entidadBLL;
        private DataTable _dtEntidades;
        private DataTable _dtEntidadesFiltradas;
        private const string PLACEHOLDER_TEXT = "Buscar entidad...";

        public FrmEntidadesSalud()
        {
            InitializeComponent();
            _entidadBLL = new EntidadSaludBLL();
        }

        private void FrmEntidadesSalud_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigurarDataGridView();
                ConfigurarComboFiltro();
                dgvEntidades.DataError += dgvEntidades_DataError;
                dgvEntidades.SelectionChanged += dgvEntidades_SelectionChanged;
                CargarEntidades();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el formulario:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarComboFiltro()
        {
            cmbFiltroEstado.Items.Clear();
            cmbFiltroEstado.Items.Add("Todas las Entidades");
            cmbFiltroEstado.Items.Add("Entidades Activas");
            cmbFiltroEstado.Items.Add("Entidades Inactivas");
            cmbFiltroEstado.SelectedIndex = 1; // Por defecto mostrar activas
        }

        private void dgvEntidades_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"DataError: {e.Exception.Message}");
            e.ThrowException = false;
            e.Cancel = false;
        }

        private void dgvEntidades_SelectionChanged(object sender, EventArgs e)
        {
            ActualizarEstadoBotones();
        }

        private void ActualizarEstadoBotones()
        {
            bool haySeleccion = dgvEntidades.SelectedRows.Count > 0;
            btnEditar.Enabled = haySeleccion;
            btnCambiarEstado.Enabled = haySeleccion;
            btnEliminar.Enabled = haySeleccion;

            if (haySeleccion)
            {
                DataGridViewRow filaSeleccionada = dgvEntidades.SelectedRows[0];
                int idEntidad = Convert.ToInt32(filaSeleccionada.Cells["IdEntidad"].Value);
                DataRow[] rows = _dtEntidadesFiltradas.Select($"IdEntidad = {idEntidad}");

                if (rows.Length > 0 && rows[0]["Activo"] != DBNull.Value)
                {
                    bool estadoActual = Convert.ToBoolean(rows[0]["Activo"]);

                    if (estadoActual)
                    {
                        btnCambiarEstado.Text = "  Desactivar";
                        btnCambiarEstado.IconChar = FontAwesome.Sharp.IconChar.Ban;
                        btnCambiarEstado.BackColor = Color.FromArgb(230, 126, 34);
                    }
                    else
                    {
                        btnCambiarEstado.Text = "  Activar";
                        btnCambiarEstado.IconChar = FontAwesome.Sharp.IconChar.CheckCircle;
                        btnCambiarEstado.BackColor = Color.FromArgb(46, 204, 113);
                    }
                }
            }
        }

        private void ConfigurarDataGridView()
        {
            dgvEntidades.EnableHeadersVisualStyles = false;
            dgvEntidades.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            dgvEntidades.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvEntidades.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvEntidades.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvEntidades.ColumnHeadersHeight = 40;

            dgvEntidades.DefaultCellStyle.SelectionBackColor = Color.FromArgb(41, 128, 185);
            dgvEntidades.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvEntidades.DefaultCellStyle.Font = new Font("Segoe UI", 9F);

            dgvEntidades.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            dgvEntidades.RowTemplate.Height = 35;
            dgvEntidades.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEntidades.RowHeadersVisible = false;
            dgvEntidades.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEntidades.MultiSelect = false;
            dgvEntidades.AllowUserToResizeRows = false;
            dgvEntidades.BorderStyle = BorderStyle.None;
        }

        private async void CargarEntidades()
        {
            try
            {
                this.ShowLoading("Cargando entidades...");
                await Task.Run(() => System.Threading.Thread.Sleep(100));

                DataTable dtTemp = await Task.Run(() => _entidadBLL.ObtenerEntidades());
                _dtEntidades = dtTemp;

                if (_dtEntidades == null || _dtEntidades.Rows.Count == 0)
                {
                    dgvEntidades.DataSource = null;
                    ActualizarEstadoBotones();
                    this.HideLoading();
                    MessageBox.Show("No hay entidades registradas.", "Información",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                AplicarFiltros();
                ActualizarEstadoBotones();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar entidades:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.HideLoading();
            }
        }

        private void AplicarFiltros()
        {
            if (_dtEntidades == null || _dtEntidades.Rows.Count == 0)
            {
                _dtEntidadesFiltradas = _dtEntidades?.Copy();
                dgvEntidades.DataSource = null;
                lblTitulo.Text = "Gestión de Entidades de Salud (0)";
                return;
            }

            DataView dv = _dtEntidades.DefaultView;
            string filtroCompleto = "";

            // Filtro de búsqueda
            string textoBusqueda = txtBuscar.Text.Trim();
            if (!string.IsNullOrEmpty(textoBusqueda) && textoBusqueda != PLACEHOLDER_TEXT)
            {
                textoBusqueda = textoBusqueda.Replace("'", "''");
                filtroCompleto = $"(Nombre LIKE '%{textoBusqueda}%' OR Codigo LIKE '%{textoBusqueda}%')";
            }

            // Filtro de estado
            if (cmbFiltroEstado.SelectedIndex == 1) // Activas
            {
                if (!string.IsNullOrEmpty(filtroCompleto))
                    filtroCompleto += " AND ";
                filtroCompleto += "Activo = 1";
            }
            else if (cmbFiltroEstado.SelectedIndex == 2) // Inactivas
            {
                if (!string.IsNullOrEmpty(filtroCompleto))
                    filtroCompleto += " AND ";
                filtroCompleto += "Activo = 0";
            }

            try
            {
                dv.RowFilter = filtroCompleto;
                _dtEntidadesFiltradas = dv.ToTable();
                dgvEntidades.DataSource = _dtEntidadesFiltradas;
                ConfigurarColumnas();
                lblTitulo.Text = $"Gestión de Entidades de Salud ({_dtEntidadesFiltradas.Rows.Count})";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en filtro: {ex.Message}");
                _dtEntidadesFiltradas = _dtEntidades.Copy();
                dgvEntidades.DataSource = _dtEntidadesFiltradas;
                ConfigurarColumnas();
            }
        }

        private void ConfigurarColumnas()
        {
            if (dgvEntidades.Columns.Contains("IdEntidad"))
                dgvEntidades.Columns["IdEntidad"].Visible = false;

            if (dgvEntidades.Columns.Contains("Codigo"))
            {
                dgvEntidades.Columns["Codigo"].HeaderText = "Código";
                dgvEntidades.Columns["Codigo"].Width = 100;
            }

            if (dgvEntidades.Columns.Contains("Nombre"))
            {
                dgvEntidades.Columns["Nombre"].HeaderText = "Nombre";
                dgvEntidades.Columns["Nombre"].Width = 200;
            }

            if (dgvEntidades.Columns.Contains("Direccion"))
            {
                dgvEntidades.Columns["Direccion"].HeaderText = "Dirección";
                dgvEntidades.Columns["Direccion"].Width = 200;
            }

            if (dgvEntidades.Columns.Contains("Telefono"))
            {
                dgvEntidades.Columns["Telefono"].HeaderText = "Teléfono";
                dgvEntidades.Columns["Telefono"].Width = 120;
            }

            if (dgvEntidades.Columns.Contains("Correo"))
            {
                dgvEntidades.Columns["Correo"].HeaderText = "Correo";
                dgvEntidades.Columns["Correo"].Width = 180;
            }

            if (dgvEntidades.Columns.Contains("Activo"))
            {
                int indexOriginal = dgvEntidades.Columns["Activo"].Index;

                DataGridViewTextBoxColumn colEstado = new DataGridViewTextBoxColumn();
                colEstado.Name = "Estado";
                colEstado.HeaderText = "Estado";
                colEstado.DataPropertyName = "Activo";
                colEstado.Width = 100;
                colEstado.ReadOnly = true;
                colEstado.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvEntidades.Columns.Remove("Activo");
                dgvEntidades.Columns.Insert(indexOriginal, colEstado);
            }

            if (dgvEntidades.Columns.Contains("CantidadUsuarios"))
            {
                dgvEntidades.Columns["CantidadUsuarios"].HeaderText = "Usuarios";
                dgvEntidades.Columns["CantidadUsuarios"].Width = 80;
                dgvEntidades.Columns["CantidadUsuarios"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvEntidades.Columns.Contains("FechaCreacion"))
            {
                dgvEntidades.Columns["FechaCreacion"].HeaderText = "Fecha Creación";
                dgvEntidades.Columns["FechaCreacion"].Width = 130;
                dgvEntidades.Columns["FechaCreacion"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }

            dgvEntidades.Refresh();
        }

        private void dgvEntidades_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (dgvEntidades.Columns.Count <= e.ColumnIndex || e.ColumnIndex < 0)
                    return;

                if (e.RowIndex < 0 || e.RowIndex >= dgvEntidades.Rows.Count)
                    return;

                string columnName = dgvEntidades.Columns[e.ColumnIndex].Name;

                if (columnName == "Estado" || columnName == "Activo")
                {
                    if (e.Value == null || e.Value == DBNull.Value)
                        return;

                    bool activo = Convert.ToBoolean(e.Value);

                    if (activo)
                    {
                        e.Value = "ACTIVA";
                        e.CellStyle.BackColor = Color.FromArgb(46, 204, 113);
                        e.CellStyle.ForeColor = Color.White;
                    }
                    else
                    {
                        e.Value = "INACTIVA";
                        e.CellStyle.BackColor = Color.FromArgb(231, 76, 60);
                        e.CellStyle.ForeColor = Color.White;
                    }

                    e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                    e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    e.FormattingApplied = true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en CellFormatting: {ex.Message}");
            }
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
            cmbFiltroEstado.SelectedIndex = 1;
            CargarEntidades();
        }

        private void cmbFiltroEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_dtEntidades != null)
            {
                AplicarFiltros();
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                using (var frmDetalle = new FrmEntidadSaludDetalle())
                {
                    if (frmDetalle.ShowDialog() == DialogResult.OK)
                    {
                        CargarEntidades();
                        MessageBox.Show("✅ Entidad de salud creada exitosamente.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear entidad:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            EditarEntidadSeleccionada();
        }

        private void dgvEntidades_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                EditarEntidadSeleccionada();
            }
        }

        private void EditarEntidadSeleccionada()
        {
            try
            {
                if (dgvEntidades.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Seleccione una entidad para editar.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idEntidad = Convert.ToInt32(dgvEntidades.SelectedRows[0].Cells["IdEntidad"].Value);

                using (var frmDetalle = new FrmEntidadSaludDetalle(idEntidad))
                {
                    if (frmDetalle.ShowDialog() == DialogResult.OK)
                    {
                        CargarEntidades();
                        MessageBox.Show("✅ Entidad actualizada exitosamente.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al editar entidad:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnCambiarEstado_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvEntidades.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Seleccione una entidad para cambiar su estado.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataGridViewRow filaSeleccionada = dgvEntidades.SelectedRows[0];
                int idEntidad = Convert.ToInt32(filaSeleccionada.Cells["IdEntidad"].Value);
                string nombreEntidad = filaSeleccionada.Cells["Nombre"].Value?.ToString() ?? "Entidad";

                DataRow[] rows = _dtEntidadesFiltradas.Select($"IdEntidad = {idEntidad}");
                if (rows.Length == 0) return;

                bool estadoActual = Convert.ToBoolean(rows[0]["Activo"]);
                bool nuevoEstado = !estadoActual;

                string accion = estadoActual ? "desactivar" : "activar";

                DialogResult confirmacion = MessageBox.Show(
                    $"¿Está seguro que desea {accion} la entidad '{nombreEntidad}'?\n\n" +
                    $"Esta acción {(nuevoEstado ? "habilitará" : "deshabilitará")} el acceso de todos los usuarios de esta entidad.",
                    $"Confirmar {accion}",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmacion == DialogResult.Yes)
                {
                    this.ShowLoading("Procesando...");
                    await Task.Delay(200);

                    var resultado = await Task.Run(() => _entidadBLL.CambiarEstadoEntidad(idEntidad, nuevoEstado));

                    this.HideLoading();

                    if (resultado.exito)
                    {
                        MessageBox.Show($"✅ Entidad {(nuevoEstado ? "activada" : "desactivada")} exitosamente.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarEntidades();
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
                MessageBox.Show($"Error al cambiar estado:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvEntidades.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Seleccione una entidad para eliminar.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataGridViewRow filaSeleccionada = dgvEntidades.SelectedRows[0];
                int idEntidad = Convert.ToInt32(filaSeleccionada.Cells["IdEntidad"].Value);
                string nombreEntidad = filaSeleccionada.Cells["Nombre"].Value?.ToString() ?? "Entidad";
                int cantidadUsuarios = Convert.ToInt32(filaSeleccionada.Cells["CantidadUsuarios"].Value);

                if (cantidadUsuarios > 0)
                {
                    MessageBox.Show($"No se puede eliminar la entidad '{nombreEntidad}' porque tiene {cantidadUsuarios} usuario(s) asignado(s).\n\n" +
                        "Primero reasigne o elimine los usuarios.", "No se puede eliminar",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult confirmacion = MessageBox.Show(
                    $"¿Está seguro que desea eliminar la entidad '{nombreEntidad}'?\n\n" +
                    "Esta acción no se puede deshacer.",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmacion == DialogResult.Yes)
                {
                    this.ShowLoading("Eliminando entidad...");
                    await Task.Delay(200);

                    var resultado = await Task.Run(() => _entidadBLL.EliminarEntidad(idEntidad));

                    this.HideLoading();

                    if (resultado.exito)
                    {
                        MessageBox.Show("✅ Entidad eliminada exitosamente.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarEntidades();
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
                MessageBox.Show($"Error al eliminar entidad:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}