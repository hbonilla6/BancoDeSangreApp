using BancoDeSangreApp.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BancoDeSangreApp.Forms
{
    public partial class FrmCentrosRecoleccion : Form
    {
        private int? _idCentroSeleccionado = null;

        public FrmCentrosRecoleccion()
        {
            InitializeComponent();
        }

        private void FrmCentrosRecoleccion_Load(object sender, EventArgs e)
        {
            ConfigurarDataGridView();
            CargarCentros();
        }

        private void ConfigurarDataGridView()
        {
            dgvCentros.AutoGenerateColumns = false;
            dgvCentros.Columns.Clear();

            dgvCentros.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdCentro",
                DataPropertyName = "IdCentro",
                HeaderText = "ID",
                Width = 50
            });

            dgvCentros.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                DataPropertyName = "Nombre",
                HeaderText = "Centro / Campaña",
                Width = 200
            });

            dgvCentros.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Responsable",
                DataPropertyName = "Responsable",
                HeaderText = "Responsable",
                Width = 150
            });

            dgvCentros.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Telefono",
                DataPropertyName = "Telefono",
                HeaderText = "Teléfono",
                Width = 100
            });

            dgvCentros.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Direccion",
                DataPropertyName = "Direccion",
                HeaderText = "Dirección",
                Width = 200
            });

            dgvCentros.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCentros.MultiSelect = false;
            dgvCentros.ReadOnly = true;
            dgvCentros.AllowUserToAddRows = false;
            dgvCentros.RowHeadersVisible = false;
        }

        private void CargarCentros()
        {
            try
            {
                string query = @"
                    SELECT IdCentro, Nombre, Direccion, Responsable, Telefono
                    FROM CentrosRecoleccion
                    WHERE Activo = 1 AND IdEntidad = @IdEntidad";

                if (!string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    query += " AND (Nombre LIKE @Busqueda OR Responsable LIKE @Busqueda)";
                }

                query += " ORDER BY Nombre";

                var parametros = new System.Collections.Generic.List<SqlParameter>
                {
                    new SqlParameter("@IdEntidad", Program.UsuarioActual.IdEntidad)
                };

                if (!string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    parametros.Add(new SqlParameter("@Busqueda", $"%{txtBuscar.Text}%"));
                }

                DataTable dt = ConexionDB.Instancia.EjecutarConsulta(query, parametros.ToArray());
                dgvCentros.DataSource = dt;

                lblTotal.Text = $"Total: {dt.Rows.Count} centros";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar centros: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            HabilitarControles(true);
            _idCentroSeleccionado = null;
            txtNombre.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (_idCentroSeleccionado == null) return;
            HabilitarControles(true);
            txtNombre.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            HabilitarControles(false);
            dgvCentros.ClearSelection();
            _idCentroSeleccionado = null;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Validaciones
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show("El nombre del centro es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string query = "";
                var parametros = new System.Collections.Generic.List<SqlParameter>();

                // 2. Insert o Update
                if (_idCentroSeleccionado == null)
                {
                    // Al ser INSERT incluimos FechaCreacion y CreadoPor
                    query = @"
                        INSERT INTO CentrosRecoleccion (IdEntidad, Nombre, Direccion, Responsable, Telefono, Activo, CreadoPor)
                        VALUES (@IdEntidad, @Nombre, @Direccion, @Responsable, @Telefono, 1, @CreadoPor)";

                    parametros.Add(new SqlParameter("@CreadoPor", Program.UsuarioActual.IdUsuario));
                }
                else
                {
                    query = @"
                        UPDATE CentrosRecoleccion 
                        SET Nombre = @Nombre, 
                            Direccion = @Direccion, 
                            Responsable = @Responsable, 
                            Telefono = @Telefono
                        WHERE IdCentro = @IdCentro";

                    parametros.Add(new SqlParameter("@IdCentro", _idCentroSeleccionado));
                }

                // 3. Parámetros comunes
                parametros.Add(new SqlParameter("@IdEntidad", Program.UsuarioActual.IdEntidad));
                parametros.Add(new SqlParameter("@Nombre", txtNombre.Text.Trim()));

                // Manejo de nulos
                parametros.Add(new SqlParameter("@Direccion", string.IsNullOrWhiteSpace(txtDireccion.Text) ? DBNull.Value : (object)txtDireccion.Text.Trim()));
                parametros.Add(new SqlParameter("@Responsable", string.IsNullOrWhiteSpace(txtResponsable.Text) ? DBNull.Value : (object)txtResponsable.Text.Trim()));
                parametros.Add(new SqlParameter("@Telefono", string.IsNullOrWhiteSpace(txtTelefono.Text) ? DBNull.Value : (object)txtTelefono.Text.Trim()));

                ConexionDB.Instancia.EjecutarComando(query, parametros.ToArray());

                string mensaje = _idCentroSeleccionado == null ? "Centro registrado correctamente." : "Centro actualizado correctamente.";
                MessageBox.Show(mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarFormulario();
                HabilitarControles(false);
                CargarCentros();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_idCentroSeleccionado == null) return;

            var confirmacion = MessageBox.Show(
                "¿Está seguro de eliminar este centro de recolección?",
                "Confirmar Eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    // Borrado lógico (Activo = 0)
                    string query = "UPDATE CentrosRecoleccion SET Activo = 0 WHERE IdCentro = @IdCentro";
                    SqlParameter[] parametros = {
                        new SqlParameter("@IdCentro", _idCentroSeleccionado.Value)
                    };

                    ConexionDB.Instancia.EjecutarComando(query, parametros);

                    MessageBox.Show("Centro eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LimpiarFormulario();
                    HabilitarControles(false);
                    CargarCentros();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarCentros();
        }

        private void dgvCentros_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCentros.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvCentros.SelectedRows[0];

                _idCentroSeleccionado = Convert.ToInt32(row.Cells["IdCentro"].Value);

                if (!txtNombre.Enabled)
                {
                    txtNombre.Text = row.Cells["Nombre"].Value?.ToString();
                    txtResponsable.Text = row.Cells["Responsable"].Value?.ToString();
                    txtTelefono.Text = row.Cells["Telefono"].Value?.ToString();
                    txtDireccion.Text = row.Cells["Direccion"].Value?.ToString();
                }

                btnEditar.Enabled = true;
                btnEliminar.Enabled = true;
            }
            else
            {
                _idCentroSeleccionado = null;
                btnEditar.Enabled = false;
                btnEliminar.Enabled = false;
                if (!txtNombre.Enabled) LimpiarFormulario();
            }
        }

        private void LimpiarFormulario()
        {
            txtNombre.Clear();
            txtDireccion.Clear();
            txtResponsable.Clear();
            txtTelefono.Clear();
        }

        private void HabilitarControles(bool habilitar)
        {
            txtNombre.Enabled = habilitar;
            txtDireccion.Enabled = habilitar;
            txtResponsable.Enabled = habilitar;
            txtTelefono.Enabled = habilitar;

            btnGuardar.Enabled = habilitar;
            btnCancelar.Enabled = habilitar;

            btnNuevo.Enabled = !habilitar;
            btnEditar.Enabled = !habilitar && _idCentroSeleccionado != null;
            btnEliminar.Enabled = !habilitar && _idCentroSeleccionado != null;
            dgvCentros.Enabled = !habilitar;
        }
    }
}