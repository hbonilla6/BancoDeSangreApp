using BancoDeSangreApp.Business;
using BancoDeSangreApp.Components;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BancoDeSangreApp.Forms
{
    public partial class FrmUsuarios : Form
    {
        private UsuarioBLL _usuariosBLL;
        private DataTable _dtUsuarios;
        private DataTable _dtUsuariosFiltrados;
        private const string PLACEHOLDER_TEXT = "Buscar usuario...";

        // Paginación
        private int _paginaActual = 1;
        private int _registrosPorPagina = 15;
        private int _totalPaginas = 0;
        private int _totalRegistros = 0;

        public FrmUsuarios()
        {
            InitializeComponent();
            _usuariosBLL = new UsuarioBLL();
        }

        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigurarDataGridView();
                ConfigurarComboFiltro();
                dgvUsuarios.DataError += dgvUsuarios_DataError;
                dgvUsuarios.SelectionChanged += dgvUsuarios_SelectionChanged;
                CargarUsuarios();
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
            cmbFiltroEstado.Items.Add("Usuarios Activos");
            cmbFiltroEstado.Items.Add("Usuarios Inactivos");
            cmbFiltroEstado.SelectedIndex = 0; // Por defecto mostrar activos

            // Configurar combo de registros por página
            cmbRegistrosPorPagina.SelectedIndex = 0; // Seleccionar "15" por defecto
        }

        private void dgvUsuarios_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"DataError: {e.Exception.Message}");
            e.ThrowException = false;
            e.Cancel = false;
        }

        private void dgvUsuarios_SelectionChanged(object sender, EventArgs e)
        {
            ActualizarEstadoBotones();
        }

        private void ActualizarEstadoBotones()
        {
            bool haySeleccion = dgvUsuarios.SelectedRows.Count > 0;
            btnEditar.Enabled = haySeleccion;
            btnCambiarEstado.Enabled = haySeleccion;

            if (haySeleccion)
            {
                // Obtener el estado del usuario seleccionado
                DataGridViewRow filaSeleccionada = dgvUsuarios.SelectedRows[0];

                // Buscar el estado real en el DataTable
                int idUsuario = Convert.ToInt32(filaSeleccionada.Cells["IdUsuario"].Value);
                DataRow[] rows = _dtUsuariosFiltrados.Select($"IdUsuario = {idUsuario}");

                if (rows.Length > 0 && rows[0]["Activo"] != DBNull.Value)
                {
                    bool estadoActual = Convert.ToBoolean(rows[0]["Activo"]);

                    // Cambiar el texto del botón según el estado
                    if (estadoActual)
                    {
                        btnCambiarEstado.Text = "  Inhabilitar";
                        btnCambiarEstado.IconChar = FontAwesome.Sharp.IconChar.Ban;
                    }
                    else
                    {
                        btnCambiarEstado.Text = "  Habilitar";
                        btnCambiarEstado.IconChar = FontAwesome.Sharp.IconChar.CheckCircle;
                    }
                }
            }
        }

        private void dgvUsuarios_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (dgvUsuarios.Columns.Count <= e.ColumnIndex || e.ColumnIndex < 0)
                    return;

                if (e.RowIndex < 0 || e.RowIndex >= dgvUsuarios.Rows.Count)
                    return;

                string columnName = dgvUsuarios.Columns[e.ColumnIndex].Name;

                if (columnName == "Estado" || columnName == "Activo")
                {
                    if (e.Value == null || e.Value == DBNull.Value)
                    {
                        e.Value = "DESCONOCIDO";
                        e.CellStyle.BackColor = Color.FromArgb(149, 165, 166);
                        e.CellStyle.ForeColor = Color.White;
                        e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        e.FormattingApplied = true;
                        return;
                    }

                    bool activo = Convert.ToBoolean(e.Value);

                    if (activo)
                    {
                        e.Value = "ACTIVO";
                        e.CellStyle.BackColor = Color.FromArgb(46, 204, 113);
                        e.CellStyle.ForeColor = Color.White;
                    }
                    else
                    {
                        e.Value = "INACTIVO";
                        e.CellStyle.BackColor = Color.FromArgb(231, 76, 60);
                        e.CellStyle.ForeColor = Color.White;
                    }

                    e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                    e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    e.FormattingApplied = true;
                }
                else if (columnName == "UltimoAcceso")
                {
                    if (e.Value == null || e.Value == DBNull.Value)
                    {
                        e.Value = "Nunca";
                        e.FormattingApplied = true;
                        e.CellStyle.ForeColor = Color.Gray;
                        e.CellStyle.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
                    }
                }
                else if (columnName == "Correo" || columnName == "Telefono" || columnName == "Roles")
                {
                    if (e.Value == null || e.Value == DBNull.Value || string.IsNullOrWhiteSpace(e.Value.ToString()))
                    {
                        e.Value = "-";
                        e.FormattingApplied = true;
                        e.CellStyle.ForeColor = Color.LightGray;
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en CellFormatting: {ex.Message}");
            }
        }

        private void ConfigurarDataGridView()
        {
            dgvUsuarios.EnableHeadersVisualStyles = false;
            dgvUsuarios.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            dgvUsuarios.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvUsuarios.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvUsuarios.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvUsuarios.ColumnHeadersHeight = 40;

            dgvUsuarios.DefaultCellStyle.SelectionBackColor = Color.FromArgb(41, 128, 185);
            dgvUsuarios.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvUsuarios.DefaultCellStyle.Font = new Font("Segoe UI", 9F);

            dgvUsuarios.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            dgvUsuarios.RowTemplate.Height = 35;
            dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsuarios.RowHeadersVisible = false;
            dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsuarios.MultiSelect = false;
            dgvUsuarios.AllowUserToResizeRows = false;
            dgvUsuarios.BorderStyle = BorderStyle.None;
        }

        private async void CargarUsuarios()
        {
            try
            {
                this.ShowLoading("Cargando usuarios...");
                await Task.Run(() => System.Threading.Thread.Sleep(100));

                DataTable dtTemp = await Task.Run(() => _usuariosBLL.ObtenerUsuarios(Program.UsuarioActual.IdEntidad));
                _dtUsuarios = dtTemp;

                if (_dtUsuarios == null || _dtUsuarios.Rows.Count == 0)
                {
                    dgvUsuarios.DataSource = null;
                    ActualizarInfoPaginacion();
                    ActualizarEstadoBotones();
                    this.HideLoading();
                    MessageBox.Show("No hay usuarios registrados.", "Información",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                AplicarFiltros();
                _paginaActual = 1;
                MostrarPagina();
                ActualizarEstadoBotones();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar usuarios:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.HideLoading();
            }
        }

        private void MostrarPagina()
        {
            try
            {
                if (_dtUsuariosFiltrados == null || _dtUsuariosFiltrados.Rows.Count == 0)
                {
                    dgvUsuarios.DataSource = null;
                    ActualizarInfoPaginacion();
                    ActualizarEstadoBotones();
                    return;
                }

                _totalRegistros = _dtUsuariosFiltrados.Rows.Count;
                _totalPaginas = (int)Math.Ceiling((double)_totalRegistros / _registrosPorPagina);

                if (_paginaActual > _totalPaginas && _totalPaginas > 0)
                    _paginaActual = _totalPaginas;
                if (_paginaActual < 1)
                    _paginaActual = 1;

                int inicio = (_paginaActual - 1) * _registrosPorPagina;
                int cantidad = Math.Min(_registrosPorPagina, _totalRegistros - inicio);

                DataTable dtPagina = _dtUsuariosFiltrados.Clone();
                for (int i = inicio; i < inicio + cantidad && i < _dtUsuariosFiltrados.Rows.Count; i++)
                {
                    dtPagina.ImportRow(_dtUsuariosFiltrados.Rows[i]);
                }

                dgvUsuarios.DataSource = null;
                dgvUsuarios.Columns.Clear();
                dgvUsuarios.DataSource = dtPagina;

                ConfigurarColumnas();
                ActualizarInfoPaginacion();
                ActualizarBotonesPaginacion();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al mostrar página:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarColumnas()
        {
            if (dgvUsuarios.Columns.Contains("IdUsuario"))
                dgvUsuarios.Columns["IdUsuario"].Visible = false;

            if (dgvUsuarios.Columns.Contains("IdEntidad"))
                dgvUsuarios.Columns["IdEntidad"].Visible = false;

            if (dgvUsuarios.Columns.Contains("ClaveHash"))
                dgvUsuarios.Columns["ClaveHash"].Visible = false;

            if (dgvUsuarios.Columns.Contains("FechaModificacion"))
                dgvUsuarios.Columns["FechaModificacion"].Visible = false;

            if (dgvUsuarios.Columns.Contains("Usuario"))
            {
                dgvUsuarios.Columns["Usuario"].HeaderText = "Usuario";
                dgvUsuarios.Columns["Usuario"].Width = 120;
            }

            if (dgvUsuarios.Columns.Contains("NombreCompleto"))
            {
                dgvUsuarios.Columns["NombreCompleto"].HeaderText = "Nombre Completo";
                dgvUsuarios.Columns["NombreCompleto"].Width = 200;
            }

            if (dgvUsuarios.Columns.Contains("Correo"))
            {
                dgvUsuarios.Columns["Correo"].HeaderText = "Correo";
                dgvUsuarios.Columns["Correo"].Width = 180;
            }

            if (dgvUsuarios.Columns.Contains("Telefono"))
            {
                dgvUsuarios.Columns["Telefono"].HeaderText = "Teléfono";
                dgvUsuarios.Columns["Telefono"].Width = 100;
            }

            if (dgvUsuarios.Columns.Contains("Roles"))
            {
                dgvUsuarios.Columns["Roles"].HeaderText = "Roles";
                dgvUsuarios.Columns["Roles"].Width = 150;
            }

            if (dgvUsuarios.Columns.Contains("Activo"))
            {
                int indexOriginal = dgvUsuarios.Columns["Activo"].Index;

                DataGridViewTextBoxColumn colEstado = new DataGridViewTextBoxColumn();
                colEstado.Name = "Estado";
                colEstado.HeaderText = "Estado";
                colEstado.DataPropertyName = "Activo";
                colEstado.Width = 100;
                colEstado.ReadOnly = true;
                colEstado.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvUsuarios.Columns.Remove("Activo");
                dgvUsuarios.Columns.Insert(indexOriginal, colEstado);
            }

            if (dgvUsuarios.Columns.Contains("UltimoAcceso"))
            {
                dgvUsuarios.Columns["UltimoAcceso"].HeaderText = "Último Acceso";
                dgvUsuarios.Columns["UltimoAcceso"].Width = 140;
                dgvUsuarios.Columns["UltimoAcceso"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }

            if (dgvUsuarios.Columns.Contains("FechaCreacion"))
            {
                dgvUsuarios.Columns["FechaCreacion"].HeaderText = "Fecha Creación";
                dgvUsuarios.Columns["FechaCreacion"].Width = 130;
                dgvUsuarios.Columns["FechaCreacion"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }

            dgvUsuarios.Refresh();
        }

        private void ActualizarInfoPaginacion()
        {
            int registrosActuales = dgvUsuarios.Rows.Count;
            int inicio = (_paginaActual - 1) * _registrosPorPagina + 1;
            int fin = Math.Min(_paginaActual * _registrosPorPagina, _totalRegistros);

            if (_totalRegistros == 0)
            {
                lblPaginacion.Text = "Sin registros";
                lblTitulo.Text = "Gestión de Usuarios (0)";
            }
            else
            {
                lblPaginacion.Text = $"Mostrando {inicio} - {fin} de {_totalRegistros} | Página {_paginaActual} de {_totalPaginas}";
                lblTitulo.Text = $"Gestión de Usuarios ({_totalRegistros})";
            }
        }

        private void ActualizarBotonesPaginacion()
        {
            btnPrimera.Enabled = _paginaActual > 1;
            btnAnterior.Enabled = _paginaActual > 1;
            btnSiguiente.Enabled = _paginaActual < _totalPaginas;
            btnUltima.Enabled = _paginaActual < _totalPaginas;
        }

        private void AplicarFiltros()
        {
            if (_dtUsuarios == null || _dtUsuarios.Rows.Count == 0)
            {
                _dtUsuariosFiltrados = _dtUsuarios?.Copy();
                return;
            }

            DataView dv = _dtUsuarios.DefaultView;
            string filtroCompleto = "";

            // Filtro de búsqueda de texto
            string textoBusqueda = txtBuscar.Text.Trim();
            if (!string.IsNullOrEmpty(textoBusqueda) && textoBusqueda != PLACEHOLDER_TEXT)
            {
                textoBusqueda = textoBusqueda.Replace("'", "''");
                filtroCompleto = $"(NombreCompleto LIKE '%{textoBusqueda}%' OR Usuario LIKE '%{textoBusqueda}%')";

                if (_dtUsuarios.Columns.Contains("Correo"))
                {
                    filtroCompleto += $" OR (Correo IS NOT NULL AND Correo LIKE '%{textoBusqueda}%')";
                }
            }

            // Filtro de estado - SOLO activos o SOLO inactivos
            if (cmbFiltroEstado.SelectedIndex == 0) // Usuarios Activos
            {
                if (!string.IsNullOrEmpty(filtroCompleto))
                    filtroCompleto += " AND ";
                filtroCompleto += "Activo = 1";
            }
            else if (cmbFiltroEstado.SelectedIndex == 1) // Usuarios Inactivos
            {
                if (!string.IsNullOrEmpty(filtroCompleto))
                    filtroCompleto += " AND ";
                filtroCompleto += "Activo = 0";
            }

            try
            {
                dv.RowFilter = filtroCompleto;
                _dtUsuariosFiltrados = dv.ToTable();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en filtro: {ex.Message}");
                _dtUsuariosFiltrados = _dtUsuarios.Copy();
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
                BuscarUsuarios();
                e.Handled = true;
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                using (var frmDetalle = new FrmUsuarioDetalle())
                {
                    if (frmDetalle.ShowDialog() == DialogResult.OK)
                    {
                        CargarUsuarios();
                        MessageBox.Show("✅ Usuario creado exitosamente.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear usuario:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            EditarUsuarioSeleccionado();
        }

        private void dgvUsuarios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                EditarUsuarioSeleccionado();
            }
        }

        private void EditarUsuarioSeleccionado()
        {
            try
            {
                if (dgvUsuarios.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Seleccione un usuario para editar.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idUsuario = Convert.ToInt32(dgvUsuarios.SelectedRows[0].Cells["IdUsuario"].Value);

                using (var frmDetalle = new FrmUsuarioDetalle(idUsuario))
                {
                    if (frmDetalle.ShowDialog() == DialogResult.OK)
                    {
                        CargarUsuarios();
                        MessageBox.Show("✅ Usuario actualizado exitosamente.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al editar usuario:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnCambiarEstado_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvUsuarios.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Seleccione un usuario para cambiar su estado.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataGridViewRow filaSeleccionada = dgvUsuarios.SelectedRows[0];
                int idUsuario = Convert.ToInt32(filaSeleccionada.Cells["IdUsuario"].Value);
                string nombreUsuario = filaSeleccionada.Cells["NombreCompleto"].Value?.ToString() ?? "Usuario";

                // ✅ OBTENER EL ESTADO ACTUAL DEL USUARIO
                DataRow[] rows = _dtUsuariosFiltrados.Select($"IdUsuario = {idUsuario}");
                if (rows.Length == 0) return;

                bool estadoActual = Convert.ToBoolean(rows[0]["Activo"]);
                bool nuevoEstado = !estadoActual; // ✅ Alternar el estado

                // ✅ VALIDAR: No puede cambiar su propio estado
                if (idUsuario == Program.UsuarioActual.IdUsuario)
                {
                    MessageBox.Show("No puede cambiar el estado de su propio usuario.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string accion = estadoActual ? "inhabilitar" : "habilitar";

                DialogResult confirmacion = MessageBox.Show(
                    $"¿Está seguro que desea {accion} al usuario '{nombreUsuario}'?\n\n" +
                    $"Esta acción {(nuevoEstado ? "permitirá" : "bloqueará")} el acceso del usuario al sistema.",
                    $"Confirmar {accion}",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmacion == DialogResult.Yes)
                {
                    this.ShowLoading($"Procesando...");
                    await Task.Delay(200);

                    // ✅ USAR EL MÉTODO CORRECTO: CambiarEstadoUsuario
                    var resultado = await Task.Run(() => _usuariosBLL.CambiarEstadoUsuario(idUsuario, nuevoEstado));

                    this.HideLoading();

                    if (resultado.exito)
                    {
                        MessageBox.Show($"✅ Usuario {(nuevoEstado ? "habilitado" : "inhabilitado")} exitosamente.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarUsuarios();
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
                MessageBox.Show($"Error al cambiar estado del usuario:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarUsuarios();
        }

        private void BuscarUsuarios()
        {
            _paginaActual = 1;
            AplicarFiltros();
            MostrarPagina();
        }

        private void btnRecargar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = PLACEHOLDER_TEXT;
            txtBuscar.ForeColor = Color.Gray;
            cmbFiltroEstado.SelectedIndex = 0;
            CargarUsuarios();
        }

        private void cmbFiltroEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_dtUsuarios != null)
            {
                _paginaActual = 1;
                AplicarFiltros();
                MostrarPagina();
            }
        }

        private void btnPrimera_Click(object sender, EventArgs e)
        {
            _paginaActual = 1;
            MostrarPagina();
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (_paginaActual > 1)
            {
                _paginaActual--;
                MostrarPagina();
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (_paginaActual < _totalPaginas)
            {
                _paginaActual++;
                MostrarPagina();
            }
        }

        private void btnUltima_Click(object sender, EventArgs e)
        {
            _paginaActual = _totalPaginas;
            MostrarPagina();
        }

        private void cmbRegistrosPorPagina_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(cmbRegistrosPorPagina.SelectedItem?.ToString(), out int registros))
            {
                _registrosPorPagina = registros;
                _paginaActual = 1;
                MostrarPagina();
            }
        }
    }
}