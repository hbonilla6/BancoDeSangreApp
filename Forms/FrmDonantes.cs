using BancoDeSangreApp.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace BancoDeSangreApp.Forms
{
    public partial class FrmDonantes : Form
    {
        private int? _idDonanteSeleccionado = null;

        public FrmDonantes()
        {
            InitializeComponent();
        }

        private void FrmDonantes_Load(object sender, EventArgs e)
        {
            ConfigurarDataGridView();
            CargarTiposSangre();
            CargarDonantes();
        }

        private void ConfigurarDataGridView()
        {
            dgvDonantes.AutoGenerateColumns = false;
            dgvDonantes.Columns.Clear();

            dgvDonantes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdDonante",
                DataPropertyName = "IdDonante",
                HeaderText = "ID",
                Width = 50
            });

            dgvDonantes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                DataPropertyName = "Nombre",
                HeaderText = "Nombre Completo",
                Width = 200
            });

            dgvDonantes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Documento",
                DataPropertyName = "Documento",
                HeaderText = "Documento",
                Width = 100
            });

            dgvDonantes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Genero",
                DataPropertyName = "Genero",
                HeaderText = "Gen.",
                Width = 50
            });

            dgvDonantes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TipoSangre",
                DataPropertyName = "TipoSangre",
                HeaderText = "Sangre",
                Width = 80
            });

            dgvDonantes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaNacimiento",
                DataPropertyName = "FechaNacimiento",
                HeaderText = "F. Nacimiento",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            dgvDonantes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Telefono",
                DataPropertyName = "Telefono",
                HeaderText = "Teléfono",
                Width = 100
            });

            dgvDonantes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Correo",
                DataPropertyName = "Correo",
                HeaderText = "Correo",
                Width = 150
            });
            dgvDonantes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Direccion",
                DataPropertyName = "Direccion",
                HeaderText = "Dirección",
                Width = 200
            });

            dgvDonantes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDonantes.MultiSelect = false;
            dgvDonantes.ReadOnly = true;
            dgvDonantes.AllowUserToAddRows = false;
            dgvDonantes.RowHeadersVisible = false;
        }

        private void CargarTiposSangre()
        {
            try
            {
                string query = "SELECT IdTipoSangre, Codigo FROM TiposSangre ORDER BY Codigo";
                DataTable dt = ConexionDB.Instancia.EjecutarConsulta(query);

                // Combo Formulario
                cmbTipoSangre.DataSource = dt.Copy();
                cmbTipoSangre.DisplayMember = "Codigo";
                cmbTipoSangre.ValueMember = "IdTipoSangre";
                cmbTipoSangre.SelectedIndex = -1;

                // Combo Filtro
                DataTable dtFiltro = dt.Copy();
                DataRow rowTodos = dtFiltro.NewRow();
                rowTodos["IdTipoSangre"] = 0;
                rowTodos["Codigo"] = "Todos";
                dtFiltro.Rows.InsertAt(rowTodos, 0);

                cmbFiltroSangre.DataSource = dtFiltro;
                cmbFiltroSangre.DisplayMember = "Codigo";
                cmbFiltroSangre.ValueMember = "IdTipoSangre";
                cmbFiltroSangre.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar tipos de sangre: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarDonantes()
        {
            try
            {
                string query = @"
                    SELECT 
                        d.IdDonante,
                        d.Nombre,
                        d.Documento,
                        d.Genero,
                        d.FechaNacimiento,
                        d.Telefono,
                        d.Correo,
                        d.Direccion,
                        d.IdTipoSangre,
                        ts.Codigo AS TipoSangre
                    FROM Donantes d
                    INNER JOIN TiposSangre ts ON d.IdTipoSangre = ts.IdTipoSangre
                    WHERE d.Activo = 1 AND d.IdEntidad = @IdEntidad";

                if (!string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    query += " AND (d.Nombre LIKE @Busqueda OR d.Documento LIKE @Busqueda)";
                }

                if (cmbFiltroSangre.SelectedIndex > 0)
                {
                    query += " AND d.IdTipoSangre = @IdTipoSangre";
                }

                query += " ORDER BY d.Nombre";

                var parametros = new System.Collections.Generic.List<SqlParameter>
                {
                    new SqlParameter("@IdEntidad", Program.UsuarioActual.IdEntidad)
                };

                if (!string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    parametros.Add(new SqlParameter("@Busqueda", $"%{txtBuscar.Text}%"));
                }

                if (cmbFiltroSangre.SelectedIndex > 0)
                {
                    parametros.Add(new SqlParameter("@IdTipoSangre", cmbFiltroSangre.SelectedValue));
                }

                DataTable dt = ConexionDB.Instancia.EjecutarConsulta(query, parametros.ToArray());
                dgvDonantes.DataSource = dt;

                lblTotalDonantes.Text = $"Total: {dt.Rows.Count} donantes";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar donantes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            HabilitarControles(true);
            _idDonanteSeleccionado = null;
            txtNombre.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (_idDonanteSeleccionado == null) return;
            HabilitarControles(true);
            txtNombre.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            HabilitarControles(false);
            dgvDonantes.ClearSelection();
            _idDonanteSeleccionado = null;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Validaciones
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show("El nombre es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtDocumento.Text))
                {
                    MessageBox.Show("El Documento/DUI es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (cmbGenero.SelectedIndex == -1)
                {
                    MessageBox.Show("Seleccione el género.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (cmbTipoSangre.SelectedIndex == -1)
                {
                    MessageBox.Show("Seleccione el tipo de sangre.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string query = "";
                var parametros = new System.Collections.Generic.List<SqlParameter>();

                // 2. Insert o Update
                if (_idDonanteSeleccionado == null)
                {
                    query = @"
                        INSERT INTO Donantes (IdEntidad, Nombre, Documento, IdTipoSangre, Genero, FechaNacimiento, Telefono, Correo, Direccion, Activo, CreadoPor)
                        VALUES (@IdEntidad, @Nombre, @Documento, @IdTipoSangre, @Genero, @FechaNacimiento, @Telefono, @Correo, @Direccion, 1, @CreadoPor)";

                    // Solo en Insert agregamos el CreadoPor
                    parametros.Add(new SqlParameter("@CreadoPor", Program.UsuarioActual.IdUsuario));
                }
                else
                {
                    query = @"
                        UPDATE Donantes 
                        SET Nombre = @Nombre, 
                            Documento = @Documento, 
                            IdTipoSangre = @IdTipoSangre, 
                            Genero = @Genero,
                            FechaNacimiento = @FechaNacimiento, 
                            Telefono = @Telefono, 
                            Correo = @Correo,
                            Direccion = @Direccion
                        WHERE IdDonante = @IdDonante";

                    parametros.Add(new SqlParameter("@IdDonante", _idDonanteSeleccionado));
                }

                // 3. Parámetros comunes
                parametros.Add(new SqlParameter("@IdEntidad", Program.UsuarioActual.IdEntidad));
                parametros.Add(new SqlParameter("@Nombre", txtNombre.Text.Trim()));
                parametros.Add(new SqlParameter("@Documento", txtDocumento.Text.Trim()));
                parametros.Add(new SqlParameter("@IdTipoSangre", cmbTipoSangre.SelectedValue));
                parametros.Add(new SqlParameter("@Genero", cmbGenero.Text)); // "M" o "F"
                parametros.Add(new SqlParameter("@FechaNacimiento", dtpFechaNacimiento.Value.Date));

                // Manejo de nulos para campos opcionales si están vacíos
                parametros.Add(new SqlParameter("@Telefono", string.IsNullOrWhiteSpace(txtTelefono.Text) ? DBNull.Value : (object)txtTelefono.Text.Trim()));
                parametros.Add(new SqlParameter("@Correo", string.IsNullOrWhiteSpace(txtCorreo.Text) ? DBNull.Value : (object)txtCorreo.Text.Trim()));
                parametros.Add(new SqlParameter("@Direccion", string.IsNullOrWhiteSpace(txtDireccion.Text) ? DBNull.Value : (object)txtDireccion.Text.Trim()));

                ConexionDB.Instancia.EjecutarComando(query, parametros.ToArray());

                string mensaje = _idDonanteSeleccionado == null ? "Donante registrado correctamente." : "Donante actualizado correctamente.";
                MessageBox.Show(mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarFormulario();
                HabilitarControles(false);
                CargarDonantes();
            }
            catch (Exception ex)
            {
                // Manejo especial para error de DUPLICADO (UNIQUE constraint en Documento)
                if (ex.Message.Contains("UNIQUE") || ex.Message.Contains("Documento"))
                {
                    MessageBox.Show("Ya existe un donante registrado con este Documento/DUI.", "Error de Duplicidad", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"Error al guardar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_idDonanteSeleccionado == null) return;

            var confirmacion = MessageBox.Show(
                "¿Está seguro de eliminar este donante?\nSe ocultará de la lista pero mantendrá su historial.",
                "Confirmar Eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    string query = "UPDATE Donantes SET Activo = 0 WHERE IdDonante = @IdDonante";
                    SqlParameter[] parametros = {
                        new SqlParameter("@IdDonante", _idDonanteSeleccionado.Value)
                    };

                    ConexionDB.Instancia.EjecutarComando(query, parametros);

                    MessageBox.Show("Donante eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LimpiarFormulario();
                    HabilitarControles(false);
                    CargarDonantes();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarDonantes();
        }

        private void dgvDonantes_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDonantes.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvDonantes.SelectedRows[0];

                _idDonanteSeleccionado = Convert.ToInt32(row.Cells["IdDonante"].Value);

                // Modo visualización
                if (!txtNombre.Enabled)
                {
                    txtNombre.Text = row.Cells["Nombre"].Value?.ToString();
                    txtDocumento.Text = row.Cells["Documento"].Value?.ToString();
                    cmbGenero.Text = row.Cells["Genero"].Value?.ToString();
                    dtpFechaNacimiento.Value = row.Cells["FechaNacimiento"].Value != DBNull.Value
                        ? Convert.ToDateTime(row.Cells["FechaNacimiento"].Value)
                        : DateTime.Now;

                    txtTelefono.Text = row.Cells["Telefono"].Value?.ToString();
                    txtCorreo.Text = row.Cells["Correo"].Value?.ToString();
                    txtDireccion.Text = row.Cells["Direccion"].Value?.ToString();

                    if (row.Cells["TipoSangre"].Value != null)
                    {
                        cmbTipoSangre.Text = row.Cells["TipoSangre"].Value.ToString();
                    }
                }

                btnEditar.Enabled = true;
                btnEliminar.Enabled = true;
            }
            else
            {
                _idDonanteSeleccionado = null;
                btnEditar.Enabled = false;
                btnEliminar.Enabled = false;
                if (!txtNombre.Enabled) LimpiarFormulario();
            }
        }

        private void LimpiarFormulario()
        {
            txtNombre.Clear();
            txtDocumento.Clear();
            cmbGenero.SelectedIndex = -1;
            txtTelefono.Clear();
            txtCorreo.Clear();
            txtDireccion.Clear();
            dtpFechaNacimiento.Value = DateTime.Now.AddYears(-18);
            cmbTipoSangre.SelectedIndex = -1;
        }

        private void HabilitarControles(bool habilitar)
        {
            txtNombre.Enabled = habilitar;
            txtDocumento.Enabled = habilitar;
            cmbGenero.Enabled = habilitar;
            txtTelefono.Enabled = habilitar;
            txtCorreo.Enabled = habilitar;
            txtDireccion.Enabled = habilitar;
            dtpFechaNacimiento.Enabled = habilitar;
            cmbTipoSangre.Enabled = habilitar;

            btnGuardar.Enabled = habilitar;
            btnCancelar.Enabled = habilitar;

            btnNuevo.Enabled = !habilitar;
            btnEditar.Enabled = !habilitar && _idDonanteSeleccionado != null;
            btnEliminar.Enabled = !habilitar && _idDonanteSeleccionado != null;
            dgvDonantes.Enabled = !habilitar;
        }

        private void pnlTop_Paint(object sender, PaintEventArgs e)
        {

        }

        private void grpDatosDonante_Enter(object sender, EventArgs e)
        {

        }
    }
}