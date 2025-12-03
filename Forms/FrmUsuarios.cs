using BancoDeSangreApp.Business;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace BancoDeSangreApp.Forms
{
    public partial class FrmUsuarios : Form
    {
        private UsuarioBLL _usuariosBLL;
        private DataTable _dtUsuarios;
        private const string PLACEHOLDER_TEXT = "🔍 Buscar usuario...";

        public FrmUsuarios()
        {
            InitializeComponent();
            _usuariosBLL = new UsuarioBLL();

            // Importante: Deshabilitar CheckForIllegalCrossThreadCalls para evitar el error
            CheckForIllegalCrossThreadCalls = false;
        }

        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            ConfigurarDataGridView();

            // Agregar evento para formatear celdas
            dgvUsuarios.CellFormatting += dgvUsuarios_CellFormatting;

            CargarUsuarios();
        }

        private void dgvUsuarios_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                // Formatear la columna "Activo"
                if (dgvUsuarios.Columns[e.ColumnIndex].Name == "Activo" && e.Value != null)
                {
                    if (e.Value is bool)
                    {
                        bool activo = (bool)e.Value;
                        e.Value = activo ? "✅ Activo" : "❌ Inactivo";
                        e.FormattingApplied = true;

                        if (!activo)
                        {
                            e.CellStyle.ForeColor = Color.Gray;
                        }
                    }
                }
            }
            catch
            {
                // Ignorar errores de formato
            }
        }

        private void ConfigurarDataGridView()
        {
            dgvUsuarios.EnableHeadersVisualStyles = false;
            dgvUsuarios.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            dgvUsuarios.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvUsuarios.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvUsuarios.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvUsuarios.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dgvUsuarios.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvUsuarios.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgvUsuarios.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsuarios.RowHeadersVisible = false;
            dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsuarios.MultiSelect = false;
            dgvUsuarios.AllowUserToResizeRows = false;
        }

        private void CargarUsuarios()
        {
            try
            {
                _dtUsuarios = _usuariosBLL.ObtenerUsuarios(Program.UsuarioActual.IdEntidad);

                // Limpiar el DataGridView primero
                dgvUsuarios.DataSource = null;
                dgvUsuarios.Rows.Clear();
                dgvUsuarios.Columns.Clear();

                // Asignar el DataSource
                dgvUsuarios.DataSource = _dtUsuarios;

                if (dgvUsuarios.Columns.Count > 0)
                {
                    dgvUsuarios.Columns["IdUsuario"].Visible = false;

                    dgvUsuarios.Columns["Usuario"].HeaderText = "Usuario";
                    dgvUsuarios.Columns["Usuario"].Width = 120;

                    dgvUsuarios.Columns["NombreCompleto"].HeaderText = "Nombre Completo";
                    dgvUsuarios.Columns["NombreCompleto"].Width = 200;

                    dgvUsuarios.Columns["Correo"].HeaderText = "Correo";
                    dgvUsuarios.Columns["Correo"].Width = 180;

                    dgvUsuarios.Columns["Telefono"].HeaderText = "Teléfono";
                    dgvUsuarios.Columns["Telefono"].Width = 100;

                    dgvUsuarios.Columns["Roles"].HeaderText = "Roles";
                    dgvUsuarios.Columns["Roles"].Width = 150;

                    dgvUsuarios.Columns["Activo"].HeaderText = "Estado";
                    dgvUsuarios.Columns["Activo"].Width = 80;

                    dgvUsuarios.Columns["UltimoAcceso"].HeaderText = "Último Acceso";
                    dgvUsuarios.Columns["UltimoAcceso"].Width = 140;
                    if (dgvUsuarios.Columns["UltimoAcceso"].ValueType != null)
                    {
                        dgvUsuarios.Columns["UltimoAcceso"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                    }

                    dgvUsuarios.Columns["FechaCreacion"].HeaderText = "Fecha Creación";
                    dgvUsuarios.Columns["FechaCreacion"].Width = 140;
                    if (dgvUsuarios.Columns["FechaCreacion"].ValueType != null)
                    {
                        dgvUsuarios.Columns["FechaCreacion"].DefaultCellStyle.Format = "dd/MM/yyyy";
                    }

                    // NO formatear la columna Activo aquí - dejarla como viene de la BD
                }

                lblTitulo.Text = $"👥 Gestión de Usuarios ({dgvUsuarios.Rows.Count})";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar usuarios:\n{ex.Message}\n\nStack Trace:\n{ex.StackTrace}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show($"Error al abrir formulario: {ex.Message}", "Error",
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
                MessageBox.Show($"Error al editar usuario: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvUsuarios.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Seleccione un usuario para eliminar.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idUsuario = Convert.ToInt32(dgvUsuarios.SelectedRows[0].Cells["IdUsuario"].Value);
                string nombreUsuario = dgvUsuarios.SelectedRows[0].Cells["NombreCompleto"].Value.ToString();

                if (idUsuario == Program.UsuarioActual.IdUsuario)
                {
                    MessageBox.Show("No puede eliminar su propio usuario.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult confirmacion = MessageBox.Show(
                    $"¿Está seguro que desea eliminar al usuario:\n\n{nombreUsuario}?\n\n" +
                    "Esta acción desactivará el usuario pero conservará su información.",
                    "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirmacion == DialogResult.Yes)
                {
                    var resultado = _usuariosBLL.EliminarUsuario(idUsuario);

                    if (resultado.exito)
                    {
                        CargarUsuarios();
                        MessageBox.Show("✅ Usuario eliminado exitosamente.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(resultado.mensaje, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar usuario: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarUsuarios();
        }

        private void BuscarUsuarios()
        {
            try
            {
                string filtro = txtBuscar.Text.Trim();

                if (string.IsNullOrEmpty(filtro) || filtro == PLACEHOLDER_TEXT)
                {
                    // Restaurar todos los datos
                    dgvUsuarios.DataSource = null;
                    dgvUsuarios.DataSource = _dtUsuarios;
                }
                else
                {
                    // Filtrar datos
                    DataView dv = _dtUsuarios.DefaultView;
                    dv.RowFilter = $"NombreCompleto LIKE '%{filtro}%' OR Usuario LIKE '%{filtro}%' OR Correo LIKE '%{filtro}%'";

                    dgvUsuarios.DataSource = null;
                    dgvUsuarios.DataSource = dv.ToTable();
                }

                lblTitulo.Text = $"👥 Gestión de Usuarios ({dgvUsuarios.Rows.Count})";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}