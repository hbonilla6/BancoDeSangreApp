using BancoDeSangreApp.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace BancoDeSangreApp.Forms
{
    public partial class FrmDonaciones : Form
    {
        private int? _idDonacionSeleccionada = null;

        public FrmDonaciones()
        {
            InitializeComponent();
        }

        private void FrmDonaciones_Load(object sender, EventArgs e)
        {
            ConfigurarDataGridView();
            CargarDonantes();
            CargarTiposSangre();
            CargarEstados();
            CargarDonaciones();
        }

        private void ConfigurarDataGridView()
        {
            dgvDonaciones.AutoGenerateColumns = false;
            dgvDonaciones.Columns.Clear();

            dgvDonaciones.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdDonacion",
                DataPropertyName = "IdDonacion",
                HeaderText = "ID",
                Width = 60
            });

            dgvDonaciones.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Donante",
                DataPropertyName = "NombreDonante",
                HeaderText = "Donante",
                Width = 150
            });

            dgvDonaciones.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TipoSangre",
                DataPropertyName = "TipoSangre",
                HeaderText = "Tipo",
                Width = 80
            });

            dgvDonaciones.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CantidadML",
                DataPropertyName = "CantidadML",
                HeaderText = "Cantidad (ml)",
                Width = 100
            });

            dgvDonaciones.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaRecoleccion",
                DataPropertyName = "FechaRecoleccion",
                HeaderText = "F. Recolección",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            dgvDonaciones.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaCaducidad",
                DataPropertyName = "FechaCaducidad",
                HeaderText = "F. Caducidad",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            dgvDonaciones.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DiasRestantes",
                DataPropertyName = "DiasRestantes",
                HeaderText = "Días Rest.",
                Width = 80
            });

            dgvDonaciones.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Estado",
                DataPropertyName = "Estado",
                HeaderText = "Estado",
                Width = 100
            });

            dgvDonaciones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDonaciones.MultiSelect = false;
            dgvDonaciones.ReadOnly = true;
            dgvDonaciones.AllowUserToAddRows = false;
            dgvDonaciones.RowHeadersVisible = false;
        }

        private void CargarDonantes()
        {
            try
            {
                string query = @"
                    SELECT d.IdDonante, d.Nombre + ' (' + ts.Codigo + ')' AS NombreCompleto, d.IdTipoSangre
                    FROM Donantes d
                    INNER JOIN TiposSangre ts ON d.IdTipoSangre = ts.IdTipoSangre
                    WHERE d.Activo = 1 AND d.IdEntidad = @IdEntidad
                    ORDER BY d.Nombre";

                SqlParameter[] parametros = {
                    new SqlParameter("@IdEntidad", Program.UsuarioActual.IdEntidad)
                };

                DataTable dt = ConexionDB.Instancia.EjecutarConsulta(query, parametros);

                cmbDonante.DataSource = dt;
                cmbDonante.DisplayMember = "NombreCompleto";
                cmbDonante.ValueMember = "IdDonante";
                cmbDonante.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar donantes: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarTiposSangre()
        {
            try
            {
                string query = "SELECT IdTipoSangre, Codigo FROM TiposSangre ORDER BY Codigo";
                DataTable dt = ConexionDB.Instancia.EjecutarConsulta(query);

                // Para el formulario de nueva donación
                cmbTipoSangre.DataSource = dt.Copy();
                cmbTipoSangre.DisplayMember = "Codigo";
                cmbTipoSangre.ValueMember = "IdTipoSangre";
                cmbTipoSangre.SelectedIndex = -1;

                // Para el filtro
                DataTable dtFiltro = dt.Copy();
                DataRow rowTodos = dtFiltro.NewRow();
                rowTodos["IdTipoSangre"] = 0;
                rowTodos["Codigo"] = "Todos";
                dtFiltro.Rows.InsertAt(rowTodos, 0);

                cmbTipoSangreFiltro.DataSource = dtFiltro;
                cmbTipoSangreFiltro.DisplayMember = "Codigo";
                cmbTipoSangreFiltro.ValueMember = "IdTipoSangre";
                cmbTipoSangreFiltro.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar tipos de sangre: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarEstados()
        {
            cmbEstadoFiltro.Items.Clear();
            cmbEstadoFiltro.Items.Add("Todos");
            cmbEstadoFiltro.Items.Add("Disponible");
            cmbEstadoFiltro.Items.Add("Reservada");
            cmbEstadoFiltro.Items.Add("Usada");
            cmbEstadoFiltro.Items.Add("Vencida");
            cmbEstadoFiltro.SelectedIndex = 0;
        }

        private void CargarDonaciones()
        {
            try
            {
                string query = @"
                    SELECT 
                        d.IdDonacion,
                        don.Nombre AS NombreDonante,
                        ts.Codigo AS TipoSangre,
                        d.CantidadML,
                        d.FechaRecoleccion,
                        d.FechaCaducidad,
                        DATEDIFF(DAY, GETDATE(), d.FechaCaducidad) AS DiasRestantes,
                        d.Estado
                    FROM Donaciones d
                    INNER JOIN Donantes don ON d.IdDonante = don.IdDonante
                    INNER JOIN TiposSangre ts ON d.IdTipoSangre = ts.IdTipoSangre
                    WHERE don.IdEntidad = @IdEntidad";

                // Aplicar filtros
                if (cmbTipoSangreFiltro.SelectedIndex > 0)
                {
                    query += " AND d.IdTipoSangre = @IdTipoSangre";
                }

                string estadoFiltro = cmbEstadoFiltro.SelectedItem?.ToString();
                if (estadoFiltro != "Todos")
                {
                    query += " AND d.Estado = @Estado";
                }

                query += " ORDER BY d.FechaRecoleccion DESC";

                var parametros = new System.Collections.Generic.List<SqlParameter>
                {
                    new SqlParameter("@IdEntidad", Program.UsuarioActual.IdEntidad)
                };

                if (cmbTipoSangreFiltro.SelectedIndex > 0)
                {
                    parametros.Add(new SqlParameter("@IdTipoSangre", cmbTipoSangreFiltro.SelectedValue));
                }

                if (estadoFiltro != "Todos")
                {
                    parametros.Add(new SqlParameter("@Estado", estadoFiltro));
                }

                DataTable dt = ConexionDB.Instancia.EjecutarConsulta(query, parametros.ToArray());
                dgvDonaciones.DataSource = dt;

                lblTotalDonaciones.Text = $"Total: {dt.Rows.Count} donaciones";

                // Colorear filas según estado y días restantes
                foreach (DataGridViewRow row in dgvDonaciones.Rows)
                {
                    string estado = row.Cells["Estado"].Value?.ToString();

                    if (estado == "Vencida")
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 200, 200);
                    }
                    else if (estado == "Usada")
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(230, 255, 230);
                    }
                    else if (row.Cells["DiasRestantes"].Value != DBNull.Value)
                    {
                        int dias = Convert.ToInt32(row.Cells["DiasRestantes"].Value);
                        if (dias <= 7 && estado == "Disponible")
                        {
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 235, 180);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar donaciones: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNuevaDonacion_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            HabilitarControles(true);
            cmbDonante.Focus();
        }

        private void btnGuardarDonacion_Click(object sender, EventArgs e)
        {
            try
            {
                // Validaciones
                if (cmbDonante.SelectedIndex == -1)
                {
                    MessageBox.Show("Seleccione un donante.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cmbTipoSangre.SelectedIndex == -1)
                {
                    MessageBox.Show("Seleccione un tipo de sangre.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Calcular fecha de caducidad (42 días después de la recolección)
                DateTime fechaCaducidad = dtpFechaRecoleccion.Value.AddDays(42);

                string query = @"
                    INSERT INTO Donaciones (IdDonante, IdTipoSangre, CantidadML, FechaRecoleccion, FechaCaducidad, Estado)
                    VALUES (@IdDonante, @IdTipoSangre, @CantidadML, @FechaRecoleccion, @FechaCaducidad, 'Disponible')";

                SqlParameter[] parametros = {
                    new SqlParameter("@IdDonante", cmbDonante.SelectedValue),
                    new SqlParameter("@IdTipoSangre", cmbTipoSangre.SelectedValue),
                    new SqlParameter("@CantidadML", numCantidadML.Value),
                    new SqlParameter("@FechaRecoleccion", dtpFechaRecoleccion.Value.Date),
                    new SqlParameter("@FechaCaducidad", fechaCaducidad)
                };

                ConexionDB.Instancia.EjecutarComando(query, parametros);

                MessageBox.Show("Donación registrada exitosamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarFormulario();
                HabilitarControles(false);
                CargarDonaciones();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar donación: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMarcarUsada_Click(object sender, EventArgs e)
        {
            CambiarEstadoDonacion("Usada");
        }

        private void btnMarcarVencida_Click(object sender, EventArgs e)
        {
            CambiarEstadoDonacion("Vencida");
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_idDonacionSeleccionada == null) return;

            var confirmacion = MessageBox.Show(
                "¿Está seguro de eliminar esta donación?",
                "Confirmación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    string query = "DELETE FROM Donaciones WHERE IdDonacion = @IdDonacion";
                    SqlParameter[] parametros = {
                        new SqlParameter("@IdDonacion", _idDonacionSeleccionada.Value)
                    };

                    ConexionDB.Instancia.EjecutarComando(query, parametros);

                    MessageBox.Show("Donación eliminada exitosamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CargarDonaciones();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar: {ex.Message}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CambiarEstadoDonacion(string nuevoEstado)
        {
            if (_idDonacionSeleccionada == null) return;

            var confirmacion = MessageBox.Show(
                $"¿Marcar esta donación como '{nuevoEstado}'?",
                "Confirmación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    string query = "UPDATE Donaciones SET Estado = @Estado WHERE IdDonacion = @IdDonacion";
                    SqlParameter[] parametros = {
                        new SqlParameter("@Estado", nuevoEstado),
                        new SqlParameter("@IdDonacion", _idDonacionSeleccionada.Value)
                    };

                    ConexionDB.Instancia.EjecutarComando(query, parametros);

                    MessageBox.Show($"Donación marcada como {nuevoEstado}.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CargarDonaciones();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al actualizar: {ex.Message}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvDonaciones_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDonaciones.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvDonaciones.SelectedRows[0];
                _idDonacionSeleccionada = Convert.ToInt32(row.Cells["IdDonacion"].Value);

                string estado = row.Cells["Estado"].Value?.ToString();

                btnMarcarUsada.Enabled = estado == "Disponible" || estado == "Reservada";
                btnMarcarVencida.Enabled = estado == "Disponible";
                btnEliminar.Enabled = estado == "Disponible";
            }
            else
            {
                _idDonacionSeleccionada = null;
                btnMarcarUsada.Enabled = false;
                btnMarcarVencida.Enabled = false;
                btnEliminar.Enabled = false;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarDonaciones();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            HabilitarControles(false);
        }

        private void btnNuevoDonante_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Aquí se abriría el formulario de registro de donantes.\n\n" +
                "Por ahora, agregue donantes directamente en la base de datos.",
                "Información",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void LimpiarFormulario()
        {
            cmbDonante.SelectedIndex = -1;
            cmbTipoSangre.SelectedIndex = -1;
            numCantidadML.Value = 450;
            dtpFechaRecoleccion.Value = DateTime.Now;
        }

        private void HabilitarControles(bool habilitar)
        {
            cmbDonante.Enabled = habilitar;
            btnNuevoDonante.Enabled = habilitar;
            cmbTipoSangre.Enabled = habilitar;
            numCantidadML.Enabled = habilitar;
            dtpFechaRecoleccion.Enabled = habilitar;
            btnGuardarDonacion.Enabled = habilitar;
            btnCancelar.Enabled = habilitar;
        }
    }
}
