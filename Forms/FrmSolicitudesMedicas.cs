using BancoDeSangreApp.Business;
using BancoDeSangreApp.Data;
using BancoDeSangreApp.Models;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace BancoDeSangreApp.Forms
{
    public partial class FrmSolicitudesMedicas : Form
    {
        private readonly SolicitudMedicaBLL _solicitudBLL;
        private readonly InventarioBLL _inventarioBLL;
        private int? _idSolicitudSeleccionada = null;

        public FrmSolicitudesMedicas()
        {
            InitializeComponent();
            _solicitudBLL = new SolicitudMedicaBLL();
            _inventarioBLL = new InventarioBLL();
        }

        private void FrmSolicitudesMedicas_Load(object sender, EventArgs e)
        {
            ConfigurarDataGridView();
            CargarTiposSangre();
            CargarEstados();
            CargarPrioridades();
            CargarSolicitudes();
        }

        private void ConfigurarDataGridView()
        {
            dgvSolicitudes.AutoGenerateColumns = false;
            dgvSolicitudes.Columns.Clear();

            dgvSolicitudes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdSolicitud",
                DataPropertyName = "IdSolicitud",
                HeaderText = "ID",
                Width = 60
            });

            dgvSolicitudes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Solicitante",
                DataPropertyName = "Solicitante",
                HeaderText = "Solicitante",
                Width = 150
            });

            dgvSolicitudes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TipoSangre",
                DataPropertyName = "TipoSangreCodigo",
                HeaderText = "Tipo Sangre",
                Width = 80
            });

            dgvSolicitudes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cantidad",
                DataPropertyName = "CantidadSolicitada",
                HeaderText = "Cantidad",
                Width = 80
            });

            dgvSolicitudes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Prioridad",
                DataPropertyName = "Prioridad",
                HeaderText = "Prioridad",
                Width = 100
            });

            dgvSolicitudes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Estado",
                DataPropertyName = "Estado",
                HeaderText = "Estado",
                Width = 100
            });

            dgvSolicitudes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaSolicitud",
                DataPropertyName = "FechaSolicitud",
                HeaderText = "Fecha Solicitud",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" }
            });

            dgvSolicitudes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Observaciones",
                DataPropertyName = "Observaciones",
                HeaderText = "Observaciones",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvSolicitudes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSolicitudes.MultiSelect = false;
            dgvSolicitudes.ReadOnly = true;
            dgvSolicitudes.AllowUserToAddRows = false;
            dgvSolicitudes.RowHeadersVisible = false;
        }

        private void CargarTiposSangre()
        {
            try
            {
                string query = "SELECT IdTipoSangre, Codigo FROM TiposSangre ORDER BY Codigo";
                DataTable dt = ConexionDB.Instancia.EjecutarConsulta(query);

                cmbTipoSangre.DataSource = dt;
                cmbTipoSangre.DisplayMember = "Codigo";
                cmbTipoSangre.ValueMember = "IdTipoSangre";
                cmbTipoSangre.SelectedIndex = -1;
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
            cmbEstadoFiltro.Items.Add("Pendiente");
            cmbEstadoFiltro.Items.Add("Aprobada");
            cmbEstadoFiltro.Items.Add("Rechazada");
            cmbEstadoFiltro.Items.Add("Atendida");
            cmbEstadoFiltro.SelectedIndex = 0;
        }

        private void CargarPrioridades()
        {
            cmbPrioridad.Items.Clear();
            cmbPrioridad.Items.Add("Normal");
            cmbPrioridad.Items.Add("Programada");
            cmbPrioridad.Items.Add("Emergencia");
            cmbPrioridad.SelectedIndex = 0;
        }

        private void CargarSolicitudes()
        {
            try
            {
                string estadoFiltro = cmbEstadoFiltro.SelectedItem?.ToString();
                if (estadoFiltro == "Todos") estadoFiltro = null;

                int? idEntidad = Program.UsuarioActual?.IdEntidad;
                DataTable dt = _solicitudBLL.ObtenerSolicitudes(idEntidad, estadoFiltro);

                dgvSolicitudes.DataSource = dt;
                lblTotalSolicitudes.Text = $"Total: {dt.Rows.Count} solicitudes";

                // Colorear filas según prioridad y estado
                foreach (DataGridViewRow row in dgvSolicitudes.Rows)
                {
                    string prioridad = row.Cells["Prioridad"].Value?.ToString();
                    string estado = row.Cells["Estado"].Value?.ToString();

                    if (prioridad == "Emergencia")
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 230);
                    else if (estado == "Pendiente")
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 250, 205);
                    else if (estado == "Atendida")
                        row.DefaultCellStyle.BackColor = Color.FromArgb(230, 255, 230);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar solicitudes: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNuevaSolicitud_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            HabilitarControles(true);
            txtSolicitante.Focus();
        }

        private void btnGuardarSolicitud_Click(object sender, EventArgs e)
        {
            try
            {
                // Validaciones existentes...
                if (string.IsNullOrWhiteSpace(txtSolicitante.Text))
                {
                    MessageBox.Show("Ingrese el nombre del solicitante.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSolicitante.Focus();
                    return;
                }

                if (cmbTipoSangre.SelectedIndex == -1)
                {
                    MessageBox.Show("Seleccione un tipo de sangre.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (numCantidad.Value <= 0)
                {
                    MessageBox.Show("La cantidad debe ser mayor a cero.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ✅ NUEVA VALIDACIÓN: Verificar disponibilidad en inventario
                int idTipoSangre = Convert.ToInt32(cmbTipoSangre.SelectedValue);
                int cantidadSolicitada = Convert.ToInt32(numCantidad.Value);

                string queryInventario = @"
            SELECT ISNULL(CantidadUnidades, 0) AS Disponible
            FROM InventarioSangre
            WHERE IdTipoSangre = @IdTipoSangre 
            AND IdEntidad = @IdEntidad";

                var paramInventario = new System.Data.SqlClient.SqlParameter[]
                {
            new System.Data.SqlClient.SqlParameter("@IdTipoSangre", idTipoSangre),
            new System.Data.SqlClient.SqlParameter("@IdEntidad", Program.UsuarioActual.IdEntidad)
                };

                DataTable dtInventario = ConexionDB.Instancia.EjecutarConsulta(queryInventario, paramInventario);

                int disponible = 0;
                if (dtInventario.Rows.Count > 0)
                {
                    disponible = Convert.ToInt32(dtInventario.Rows[0]["Disponible"]);
                }

                // Advertir si no hay stock suficiente
                if (disponible == 0)
                {
                    var resultadoSinStock = MessageBox.Show(
                        "⚠️ ADVERTENCIA: No hay unidades disponibles de este tipo de sangre.\n\n" +
                        "¿Desea registrar la solicitud de todas formas?\n" +
                        "(La solicitud quedará como PENDIENTE hasta que haya stock disponible)",
                        "Sin Stock Disponible",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (resultadoSinStock == DialogResult.No)
                        return;
                }
                else if (cantidadSolicitada > disponible)
                {
                    var resultadoInsuficiente = MessageBox.Show(
                        $"⚠️ ADVERTENCIA: Stock insuficiente.\n\n" +
                        $"Solicitado: {cantidadSolicitada} unidades\n" +
                        $"Disponible: {disponible} unidades\n\n" +
                        $"¿Desea registrar la solicitud de todas formas?",
                        "Stock Insuficiente",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (resultadoInsuficiente == DialogResult.No)
                        return;
                }

                // Crear objeto solicitud (código existente)
                SolicitudMedica solicitud = new SolicitudMedica
                {
                    IdEntidad = Program.UsuarioActual.IdEntidad,
                    Solicitante = txtSolicitante.Text.Trim(),
                    IdTipoSangre = idTipoSangre,
                    CantidadSolicitada = cantidadSolicitada,
                    Prioridad = cmbPrioridad.SelectedItem.ToString(),
                    Estado = "Pendiente",
                    Observaciones = txtObservaciones.Text.Trim(),
                    CreadoPor = Program.UsuarioActual.IdUsuario
                };

                var resultado = _solicitudBLL.RegistrarSolicitud(solicitud);

                if (resultado.exito)
                {
                    string mensajeExito = resultado.mensaje;

                    // Agregar nota sobre disponibilidad
                    if (disponible == 0)
                    {
                        mensajeExito += "\n\n⚠️ NOTA: No hay stock disponible actualmente.";
                    }
                    else if (cantidadSolicitada > disponible)
                    {
                        mensajeExito += $"\n\n⚠️ NOTA: Solo hay {disponible} unidades disponibles.";
                    }

                    MessageBox.Show(mensajeExito, "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LimpiarFormulario();
                    HabilitarControles(false);
                    CargarSolicitudes();
                }
                else
                {
                    MessageBox.Show(resultado.mensaje, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar solicitud: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAprobar_Click(object sender, EventArgs e)
        {
            CambiarEstadoSolicitud("Aprobada");
        }

        private void btnRechazar_Click(object sender, EventArgs e)
        {
            CambiarEstadoSolicitud("Rechazada");
        }

        private void btnAtender_Click(object sender, EventArgs e)
        {
            if (_idSolicitudSeleccionada == null)
            {
                MessageBox.Show("Seleccione una solicitud.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Abrir formulario para asignar donaciones
            FrmAsignarDonaciones frmAsignar = new FrmAsignarDonaciones(_idSolicitudSeleccionada.Value);
            if (frmAsignar.ShowDialog() == DialogResult.OK)
            {
                CargarSolicitudes();
                MessageBox.Show("Donaciones asignadas exitosamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void CambiarEstadoSolicitud(string nuevoEstado)
        {
            if (_idSolicitudSeleccionada == null)
            {
                MessageBox.Show("Seleccione una solicitud.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmacion = MessageBox.Show(
                $"¿Está seguro de cambiar el estado a '{nuevoEstado}'?",
                "Confirmación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                var resultado = _solicitudBLL.ActualizarEstadoSolicitud(
                    _idSolicitudSeleccionada.Value,
                    nuevoEstado,
                    Program.UsuarioActual.IdUsuario);

                if (resultado.exito)
                {
                    MessageBox.Show(resultado.mensaje, "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarSolicitudes();
                }
                else
                {
                    MessageBox.Show(resultado.mensaje, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvSolicitudes_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSolicitudes.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvSolicitudes.SelectedRows[0];
                _idSolicitudSeleccionada = Convert.ToInt32(row.Cells["IdSolicitud"].Value);

                string estado = row.Cells["Estado"].Value?.ToString();

                // Habilitar/deshabilitar botones según estado
                btnAprobar.Enabled = estado == "Pendiente";
                btnRechazar.Enabled = estado == "Pendiente";
                btnAtender.Enabled = estado == "Aprobada";
            }
            else
            {
                _idSolicitudSeleccionada = null;
                btnAprobar.Enabled = false;
                btnRechazar.Enabled = false;
                btnAtender.Enabled = false;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarSolicitudes();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            HabilitarControles(false);
        }

        private void LimpiarFormulario()
        {
            txtSolicitante.Clear();
            cmbTipoSangre.SelectedIndex = -1;
            numCantidad.Value = 1;
            cmbPrioridad.SelectedIndex = 0;
            txtObservaciones.Clear();
        }

        private void HabilitarControles(bool habilitar)
        {
            txtSolicitante.Enabled = habilitar;
            cmbTipoSangre.Enabled = habilitar;
            numCantidad.Enabled = habilitar;
            cmbPrioridad.Enabled = habilitar;
            txtObservaciones.Enabled = habilitar;
            btnGuardarSolicitud.Enabled = habilitar;
            btnCancelar.Enabled = habilitar;
        }

        private void cmbTipoSangre_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTipoSangre.SelectedIndex != -1)
            {
                try
                {
                    int idTipoSangre = Convert.ToInt32(cmbTipoSangre.SelectedValue);
                    int? idEntidad = Program.UsuarioActual?.IdEntidad;

                    // Consulta para obtener disponibilidad específica del tipo de sangre
                    string query = @"
                SELECT ISNULL(CantidadUnidades, 0) AS Disponible
                FROM InventarioSangre
                WHERE IdTipoSangre = @IdTipoSangre 
                AND IdEntidad = @IdEntidad";

                    var parametros = new System.Data.SqlClient.SqlParameter[]
                    {
                new System.Data.SqlClient.SqlParameter("@IdTipoSangre", idTipoSangre),
                new System.Data.SqlClient.SqlParameter("@IdEntidad", idEntidad)
                    };

                    DataTable dt = ConexionDB.Instancia.EjecutarConsulta(query, parametros);

                    int disponible = 0;
                    if (dt.Rows.Count > 0)
                    {
                        disponible = Convert.ToInt32(dt.Rows[0]["Disponible"]);
                    }

                    // Mostrar disponibilidad con colores
                    if (disponible == 0)
                    {
                        lblDisponibilidad.Text = "⚠️ SIN STOCK DISPONIBLE";
                        lblDisponibilidad.ForeColor = Color.Red;
                        lblDisponibilidad.Font = new Font(lblDisponibilidad.Font, FontStyle.Bold);
                    }
                    else if (disponible < 3)
                    {
                        lblDisponibilidad.Text = $"⚠️ Stock crítico: {disponible} unidades";
                        lblDisponibilidad.ForeColor = Color.Orange;
                        lblDisponibilidad.Font = new Font(lblDisponibilidad.Font, FontStyle.Bold);
                    }
                    else
                    {
                        lblDisponibilidad.Text = $"✓ Disponible: {disponible} unidades";
                        lblDisponibilidad.ForeColor = Color.Green;
                        lblDisponibilidad.Font = new Font(lblDisponibilidad.Font, FontStyle.Regular);
                    }
                }
                catch (Exception ex)
                {
                    lblDisponibilidad.Text = "Error al verificar disponibilidad";
                    lblDisponibilidad.ForeColor = Color.Gray;
                    System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
                }
            }
            else
            {
                lblDisponibilidad.Text = "";
            }
        }

        private void FrmSolicitudesMedicas_Load_1(object sender, EventArgs e)
        {

        }
    }
}