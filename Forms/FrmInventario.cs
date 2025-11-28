using System;
using BancoDeSangreApp.Business;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace BancoDeSangreApp.Forms
{
    public partial class FrmInventario : Form
    {
        private readonly InventarioBLL _inventarioBLL;
        private Timer _timerActualizacion;

        public FrmInventario()
        {
            InitializeComponent();
            _inventarioBLL = new InventarioBLL();
            InicializarTimer();
        }

        private void InicializarTimer()
        {
            _timerActualizacion = new Timer();
            _timerActualizacion.Interval = 60000; // 1 minuto
            _timerActualizacion.Tick += TimerActualizacion_Tick;
            _timerActualizacion.Start();
        }

        private void TimerActualizacion_Tick(object sender, EventArgs e)
        {
            ActualizarInventarioAutomatico();
        }

        private void FrmInventario_Load(object sender, EventArgs e)
        {
            ConfigurarDataGridViews();
            CargarDatos();
        }

        private void ConfigurarDataGridViews()
        {
            // Configurar dgvInventario
            dgvInventario.AutoGenerateColumns = false;
            dgvInventario.Columns.Clear();

            dgvInventario.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TipoSangre",
                DataPropertyName = "TipoSangreCodigo",
                HeaderText = "Tipo Sangre",
                Width = 100
            });

            dgvInventario.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Descripcion",
                DataPropertyName = "TipoSangreDescripcion",
                HeaderText = "Descripción",
                Width = 150
            });

            dgvInventario.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cantidad",
                DataPropertyName = "CantidadUnidades",
                HeaderText = "Unidades",
                Width = 100
            });

            dgvInventario.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "EstadoStock",
                DataPropertyName = "EstadoStock",
                HeaderText = "Estado",
                Width = 100
            });

            dgvInventario.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "UltimaActualizacion",
                DataPropertyName = "UltimaActualizacion",
                HeaderText = "Última Actualización",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" }
            });

            // Configurar dgvAlertas
            dgvAlertas.AutoGenerateColumns = false;
            dgvAlertas.Columns.Clear();

            dgvAlertas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TipoSangre",
                DataPropertyName = "TipoSangreCodigo",
                HeaderText = "Tipo Sangre",
                Width = 100
            });

            dgvAlertas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cantidad",
                DataPropertyName = "CantidadUnidades",
                HeaderText = "Unidades",
                Width = 100
            });

            dgvAlertas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NivelAlerta",
                DataPropertyName = "NivelAlerta",
                HeaderText = "Nivel Alerta",
                Width = 120
            });

            dgvAlertas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Entidad",
                DataPropertyName = "NombreEntidad",
                HeaderText = "Entidad",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            // Configurar dgvProximasVencer
            dgvProximasVencer.AutoGenerateColumns = false;
            dgvProximasVencer.Columns.Clear();

            dgvProximasVencer.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdDonacion",
                DataPropertyName = "IdDonacion",
                HeaderText = "ID",
                Width = 60
            });

            dgvProximasVencer.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TipoSangre",
                DataPropertyName = "TipoSangre",
                HeaderText = "Tipo",
                Width = 80
            });

            dgvProximasVencer.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CantidadML",
                DataPropertyName = "CantidadML",
                HeaderText = "Cantidad (ml)",
                Width = 100
            });

            dgvProximasVencer.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaCaducidad",
                DataPropertyName = "FechaCaducidad",
                HeaderText = "Fecha Caducidad",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            dgvProximasVencer.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DiasRestantes",
                DataPropertyName = "DiasRestantes",
                HeaderText = "Días Restantes",
                Width = 100
            });

            dgvProximasVencer.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Donante",
                DataPropertyName = "NombreDonante",
                HeaderText = "Donante",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            // Configuración común
            foreach (DataGridView dgv in new[] { dgvInventario, dgvAlertas, dgvProximasVencer })
            {
                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgv.MultiSelect = false;
                dgv.ReadOnly = true;
                dgv.AllowUserToAddRows = false;
                dgv.RowHeadersVisible = false;
            }
        }

        private void CargarDatos()
        {
            CargarInventario();
            CargarAlertas();
            CargarDonacionesProximasVencer();
            ActualizarEstadisticas();
            ActualizarUltimaActualizacion();
        }

        private void CargarInventario()
        {
            try
            {
                int? idEntidad = Program.UsuarioActual?.IdEntidad;
                DataTable dt = _inventarioBLL.ObtenerInventario(idEntidad);
                dgvInventario.DataSource = dt;

                // Colorear filas según estado
                foreach (DataGridViewRow row in dgvInventario.Rows)
                {
                    string estado = row.Cells["EstadoStock"].Value?.ToString();

                    if (estado == "Crítico")
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 200, 200);
                    else if (estado == "Bajo")
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 235, 180);
                    else
                        row.DefaultCellStyle.BackColor = Color.FromArgb(230, 255, 230);
                }

                lblTotalTipos.Text = $"Total tipos: {dt.Rows.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar inventario: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarAlertas()
        {
            try
            {
                int? idEntidad = Program.UsuarioActual?.IdEntidad;
                DataTable dt = _inventarioBLL.ObtenerAlertasStockBajo(idEntidad);
                dgvAlertas.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    lblSinAlertas.Visible = true;
                    dgvAlertas.Visible = false;
                }
                else
                {
                    lblSinAlertas.Visible = false;
                    dgvAlertas.Visible = true;

                    // Colorear según nivel de alerta
                    foreach (DataGridViewRow row in dgvAlertas.Rows)
                    {
                        string nivel = row.Cells["NivelAlerta"].Value?.ToString();

                        if (nivel == "CRÍTICO")
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 200, 200);
                        else if (nivel == "BAJO")
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 235, 180);
                    }
                }

                lblTotalAlertas.Text = $"Alertas: {dt.Rows.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar alertas: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarDonacionesProximasVencer()
        {
            try
            {
                DataTable dt = _inventarioBLL.ObtenerDonacionesProximasVencer(30);
                dgvProximasVencer.DataSource = dt;

                // Colorear según días restantes
                foreach (DataGridViewRow row in dgvProximasVencer.Rows)
                {
                    if (row.Cells["DiasRestantes"].Value != DBNull.Value)
                    {
                        int dias = Convert.ToInt32(row.Cells["DiasRestantes"].Value);

                        if (dias <= 3)
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 200, 200);
                        else if (dias <= 7)
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 235, 180);
                    }
                }

                lblTotalProximasVencer.Text = $"Por vencer: {dt.Rows.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar donaciones próximas a vencer: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarEstadisticas()
        {
            try
            {
                int? idEntidad = Program.UsuarioActual?.IdEntidad;
                var stats = _inventarioBLL.ObtenerEstadisticasInventario(idEntidad);

                lblTotalUnidades.Text = stats.ContainsKey("TotalUnidades")
                    ? stats["TotalUnidades"].ToString()
                    : "0";

                lblTiposCriticos.Text = stats.ContainsKey("TiposCriticos")
                    ? stats["TiposCriticos"].ToString()
                    : "0";

                lblTiposBajos.Text = stats.ContainsKey("TiposBajos")
                    ? stats["TiposBajos"].ToString()
                    : "0";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al actualizar estadísticas: {ex.Message}");
            }
        }

        private void ActualizarUltimaActualizacion()
        {
            lblUltimaActualizacion.Text = $"Última actualización: {DateTime.Now:dd/MM/yyyy HH:mm:ss}";
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void btnActualizarInventario_Click(object sender, EventArgs e)
        {
            try
            {
                btnActualizarInventario.Enabled = false;
                btnActualizarInventario.Text = "Actualizando...";

                var resultado = _inventarioBLL.ActualizarInventario(Program.UsuarioActual?.IdUsuario);

                if (resultado.exito)
                {
                    MessageBox.Show(resultado.mensaje, "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarDatos();
                }
                else
                {
                    MessageBox.Show(resultado.mensaje, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar inventario: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnActualizarInventario.Enabled = true;
                btnActualizarInventario.Text = "🔄 Actualizar Inventario";
            }
        }

        private void ActualizarInventarioAutomatico()
        {
            try
            {
                _inventarioBLL.ActualizarInventario(Program.UsuarioActual?.IdUsuario);
                CargarInventario();
                ActualizarEstadisticas();
                ActualizarUltimaActualizacion();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en actualización automática: {ex.Message}");
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "Archivo CSV|*.csv|Archivo de texto|*.txt",
                    Title = "Exportar inventario",
                    FileName = $"Inventario_{DateTime.Now:yyyyMMdd_HHmmss}"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    ExportarACSV(dgvInventario, saveDialog.FileName);
                    MessageBox.Show("Inventario exportado exitosamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al exportar: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportarACSV(DataGridView dgv, string rutaArchivo)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            // Encabezados
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                sb.Append(dgv.Columns[i].HeaderText);
                if (i < dgv.Columns.Count - 1)
                    sb.Append(",");
            }
            sb.AppendLine();

            // Datos
            foreach (DataGridViewRow row in dgv.Rows)
            {
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    sb.Append(row.Cells[i].Value?.ToString() ?? "");
                    if (i < dgv.Columns.Count - 1)
                        sb.Append(",");
                }
                sb.AppendLine();
            }

            System.IO.File.WriteAllText(rutaArchivo, sb.ToString());
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _timerActualizacion?.Stop();
            _timerActualizacion?.Dispose();
            base.OnFormClosing(e);
        }
    }
}