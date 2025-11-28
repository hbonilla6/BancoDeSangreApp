using BancoDeSangreApp.Business;
using BancoDeSangreApp.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BancoDeSangreApp.Forms
{
    public partial class FrmAsignarDonaciones : Form
    {
        private readonly SolicitudMedicaBLL _solicitudBLL;
        private readonly int _idSolicitud;
        private List<int> _donacionesSeleccionadas;

        public FrmAsignarDonaciones(int idSolicitud)
        {
            InitializeComponent();
            _solicitudBLL = new SolicitudMedicaBLL();
            _idSolicitud = idSolicitud;
            _donacionesSeleccionadas = new List<int>();
        }

        private void FrmAsignarDonaciones_Load(object sender, EventArgs e)
        {
            CargarDatosSolicitud();
            ConfigurarDataGridView();
            CargarDonacionesDisponibles();
        }

        private void CargarDatosSolicitud()
        {
            try
            {
                string query = @"
                    SELECT s.*, ts.Codigo AS TipoSangre
                    FROM SolicitudesMedicas s
                    INNER JOIN TiposSangre ts ON s.IdTipoSangre = ts.IdTipoSangre
                    WHERE s.IdSolicitud = @IdSolicitud";

                var parametros = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@IdSolicitud", _idSolicitud)
                };

                DataTable dt = ConexionDB.Instancia.EjecutarConsulta(query, parametros);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    lblIdSolicitud.Text = $"Solicitud #{row["IdSolicitud"]}";
                    lblSolicitante.Text = row["Solicitante"].ToString();
                    lblTipoSangre.Text = row["TipoSangre"].ToString();
                    lblCantidadSolicitada.Text = $"{row["CantidadSolicitada"]} unidades";
                    lblPrioridad.Text = row["Prioridad"].ToString();

                    // Colorear según prioridad
                    if (lblPrioridad.Text == "Emergencia")
                    {
                        lblPrioridad.ForeColor = Color.Red;
                        lblPrioridad.Font = new Font(lblPrioridad.Font, FontStyle.Bold);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos de solicitud: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarDataGridView()
        {
            dgvDonaciones.AutoGenerateColumns = false;
            dgvDonaciones.Columns.Clear();

            // Columna de checkbox para selección
            DataGridViewCheckBoxColumn chkColumn = new DataGridViewCheckBoxColumn
            {
                Name = "Seleccionar",
                HeaderText = "Sel.",
                Width = 50,
                ReadOnly = false
            };
            dgvDonaciones.Columns.Add(chkColumn);

            dgvDonaciones.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdDonacion",
                DataPropertyName = "IdDonacion",
                HeaderText = "ID",
                Width = 60
            });

            dgvDonaciones.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TipoSangre",
                DataPropertyName = "TipoSangre",
                HeaderText = "Tipo",
                Width = 70
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
                Width = 110,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            dgvDonaciones.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaCaducidad",
                DataPropertyName = "FechaCaducidad",
                HeaderText = "F. Caducidad",
                Width = 110,
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
                Name = "Donante",
                DataPropertyName = "NombreDonante",
                HeaderText = "Donante",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvDonaciones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDonaciones.MultiSelect = false;
            dgvDonaciones.AllowUserToAddRows = false;
            dgvDonaciones.RowHeadersVisible = false;

            // Evento para manejar selección de checkbox
            dgvDonaciones.CellContentClick += DgvDonaciones_CellContentClick;
        }

        private void CargarDonacionesDisponibles()
        {
            try
            {
                // Obtener el tipo de sangre de la solicitud
                string query = @"
                    SELECT 
                        d.IdDonacion,
                        ts.Codigo AS TipoSangre,
                        d.CantidadML,
                        d.FechaRecoleccion,
                        d.FechaCaducidad,
                        DATEDIFF(DAY, GETDATE(), d.FechaCaducidad) AS DiasRestantes,
                        don.Nombre AS NombreDonante
                    FROM Donaciones d
                    INNER JOIN TiposSangre ts ON d.IdTipoSangre = ts.IdTipoSangre
                    INNER JOIN Donantes don ON d.IdDonante = don.IdDonante
                    WHERE d.Estado = 'Disponible'
                    AND d.FechaCaducidad > GETDATE()
                    AND d.IdTipoSangre = (SELECT IdTipoSangre FROM SolicitudesMedicas WHERE IdSolicitud = @IdSolicitud)
                    ORDER BY d.FechaCaducidad ASC";

                var parametros = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@IdSolicitud", _idSolicitud)
                };

                DataTable dt = ConexionDB.Instancia.EjecutarConsulta(query, parametros);

                // Agregar columna de selección si no existe
                if (!dt.Columns.Contains("Seleccionar"))
                {
                    dt.Columns.Add("Seleccionar", typeof(bool));
                    foreach (DataRow row in dt.Rows)
                    {
                        row["Seleccionar"] = false;
                    }
                }

                dgvDonaciones.DataSource = dt;
                lblTotalDisponibles.Text = $"Disponibles: {dt.Rows.Count} unidades";

                // Colorear según días restantes
                foreach (DataGridViewRow row in dgvDonaciones.Rows)
                {
                    if (row.Cells["DiasRestantes"].Value != DBNull.Value)
                    {
                        int dias = Convert.ToInt32(row.Cells["DiasRestantes"].Value);

                        if (dias <= 7)
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 235, 180);
                        else if (dias <= 14)
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 250, 205);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar donaciones: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvDonaciones_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 0) // Columna de checkbox
            {
                bool isChecked = Convert.ToBoolean(dgvDonaciones.Rows[e.RowIndex].Cells[0].EditedFormattedValue);
                int idDonacion = Convert.ToInt32(dgvDonaciones.Rows[e.RowIndex].Cells["IdDonacion"].Value);

                if (isChecked)
                {
                    if (!_donacionesSeleccionadas.Contains(idDonacion))
                        _donacionesSeleccionadas.Add(idDonacion);
                }
                else
                {
                    _donacionesSeleccionadas.Remove(idDonacion);
                }

                ActualizarContadorSeleccionadas();
            }
        }

        private void ActualizarContadorSeleccionadas()
        {
            lblSeleccionadas.Text = $"Seleccionadas: {_donacionesSeleccionadas.Count}";
            btnAsignar.Enabled = _donacionesSeleccionadas.Count > 0;
        }

        private void btnSeleccionarTodo_Click(object sender, EventArgs e)
        {
            _donacionesSeleccionadas.Clear();

            foreach (DataGridViewRow row in dgvDonaciones.Rows)
            {
                row.Cells[0].Value = true;
                int idDonacion = Convert.ToInt32(row.Cells["IdDonacion"].Value);
                _donacionesSeleccionadas.Add(idDonacion);
            }

            ActualizarContadorSeleccionadas();
        }

        private void btnLimpiarSeleccion_Click(object sender, EventArgs e)
        {
            _donacionesSeleccionadas.Clear();

            foreach (DataGridViewRow row in dgvDonaciones.Rows)
            {
                row.Cells[0].Value = false;
            }

            ActualizarContadorSeleccionadas();
        }

        private void btnAsignar_Click(object sender, EventArgs e)
        {
            if (_donacionesSeleccionadas.Count == 0)
            {
                MessageBox.Show("Debe seleccionar al menos una donación.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmacion = MessageBox.Show(
                $"¿Está seguro de asignar {_donacionesSeleccionadas.Count} donación(es) a esta solicitud?",
                "Confirmación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    var resultado = _solicitudBLL.AsignarDonacionesASolicitud(
                        _idSolicitud,
                        _donacionesSeleccionadas,
                        Program.UsuarioActual?.IdUsuario);

                    if (resultado.exito)
                    {
                        MessageBox.Show(resultado.mensaje, "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(resultado.mensaje, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al asignar donaciones: {ex.Message}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (dgvDonaciones.DataSource is DataTable dt)
            {
                string filtro = txtBuscar.Text.Trim();

                if (string.IsNullOrEmpty(filtro))
                {
                    dt.DefaultView.RowFilter = "";
                }
                else
                {
                    dt.DefaultView.RowFilter = $"NombreDonante LIKE '%{filtro}%' OR IdDonacion LIKE '%{filtro}%'";
                }

                lblTotalDisponibles.Text = $"Disponibles: {dt.DefaultView.Count} unidades";
            }
        }
    }
}