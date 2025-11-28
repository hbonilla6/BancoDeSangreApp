using BancoDeSangreApp.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BancoDeSangreApp.Forms
{
    public partial class FrmDashboard : Form
    {
        private Timer timerActualizacion;
        private Panel panelCargando;
        private Label lblCargando;

        public FrmDashboard()
        {
            InitializeComponent();
            InicializarPanelCargando();
            InicializarDataGridViews();
            ConfigurarTimer();
        }

        private void InicializarPanelCargando()
        {
            panelCargando = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(150, Color.Gray),
                Visible = false
            };

            lblCargando = new Label
            {
                Text = "Cargando...",
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.White
            };

            panelCargando.Controls.Add(lblCargando);
            this.Controls.Add(panelCargando);
            panelCargando.BringToFront();
        }

        // ✅ MÉTODO CORREGIDO - Operaciones DB en paralelo
        private async Task CargarDashboardAsync()
        {
            if (panelCargando.Visible) return; // Evitar múltiples cargas simultáneas

            try
            {
                panelCargando.Visible = true;

                // Ejecutar TODAS las consultas en paralelo (no secuencialmente)
                var taskEstadisticas = Task.Run(() => ObtenerEstadisticasGenerales());
                var taskInventario = Task.Run(() => ObtenerInventarioSangre());
                var taskSolicitudes = Task.Run(() => ObtenerSolicitudesPendientes());
                var taskDonaciones = Task.Run(() => ObtenerDonacionesRecientes());
                var taskAlertas = Task.Run(() => ObtenerAlertasInventario());
                var taskDonantesPorTipo = Task.Run(() => ObtenerEstadisticasDonantes());

                // Esperar a que TODAS terminen
                await Task.WhenAll(
                    taskEstadisticas,
                    taskInventario,
                    taskSolicitudes,
                    taskDonaciones,
                    taskAlertas,
                    taskDonantesPorTipo
                );

                // Actualizar UI con los resultados (ya en el hilo UI)
                ActualizarEstadisticas(await taskEstadisticas);
                ActualizarInventario(await taskInventario);
                ActualizarSolicitudes(await taskSolicitudes);
                ActualizarDonaciones(await taskDonaciones);
                ActualizarAlertas(await taskAlertas);
                ActualizarDonantesPorTipo(await taskDonantesPorTipo);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar dashboard: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                panelCargando.Visible = false;
            }
        }

        private async void FrmDashboard_Load(object sender, EventArgs e)
        {
            await CargarDashboardAsync();
        }

        // ========== MÉTODOS QUE OBTIENEN DATOS (sin tocar UI) ==========

        private (int donantes, int donaciones, int solicitudes, int porVencer) ObtenerEstadisticasGenerales()
        {
            using (SqlConnection conn = ConexionDB.Instancia.ObtenerConexion())
            {
                conn.Open();

                // Consulta combinada para reducir roundtrips
                string query = @"
                    SELECT 
                        (SELECT COUNT(*) FROM Donantes 
                         WHERE Activo = 1 AND IdEntidad = @IdEntidad) AS TotalDonantes,
                        
                        (SELECT COUNT(*) FROM Donaciones d
                         INNER JOIN Donantes dn ON d.IdDonante = dn.IdDonante
                         WHERE d.Estado = 'Disponible' AND dn.IdEntidad = @IdEntidad
                         AND d.FechaCaducidad > GETDATE()) AS TotalDonaciones,
                        
                        (SELECT COUNT(*) FROM SolicitudesMedicas 
                         WHERE Estado = 'Pendiente' AND IdEntidad = @IdEntidad) AS SolicitudesPendientes,
                        
                        (SELECT COUNT(*) FROM Donaciones d
                         INNER JOIN Donantes dn ON d.IdDonante = dn.IdDonante
                         WHERE d.Estado = 'Disponible' AND dn.IdEntidad = @IdEntidad
                         AND d.FechaCaducidad BETWEEN GETDATE() AND DATEADD(DAY, 7, GETDATE())) AS PorVencer";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdEntidad", Program.UsuarioActual.IdEntidad);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return (
                                reader.GetInt32(0),
                                reader.GetInt32(1),
                                reader.GetInt32(2),
                                reader.GetInt32(3)
                            );
                        }
                    }
                }
            }
            return (0, 0, 0, 0);
        }

        private DataTable ObtenerInventarioSangre()
        {
            string query = @"
                SELECT 
                    ts.Codigo AS TipoSangre,
                    ISNULL(inv.CantidadUnidades, 0) AS Unidades,
                    CASE 
                        WHEN ISNULL(inv.CantidadUnidades, 0) = 0 THEN 'Crítico'
                        WHEN ISNULL(inv.CantidadUnidades, 0) < 5 THEN 'Bajo'
                        WHEN ISNULL(inv.CantidadUnidades, 0) < 10 THEN 'Medio'
                        ELSE 'Normal'
                    END AS Estado
                FROM TiposSangre ts
                LEFT JOIN InventarioSangre inv 
                    ON ts.IdTipoSangre = inv.IdTipoSangre 
                    AND inv.IdEntidad = @IdEntidad
                ORDER BY ts.Codigo";

            return ConexionDB.Instancia.EjecutarConsulta(query,
                new SqlParameter("@IdEntidad", Program.UsuarioActual.IdEntidad));
        }

        private DataTable ObtenerSolicitudesPendientes()
        {
            string query = @"
                SELECT 
                    sm.IdSolicitud,
                    sm.Solicitante,
                    ts.Codigo AS TipoSangre,
                    sm.CantidadSolicitada AS Cantidad,
                    sm.Prioridad,
                    CONVERT(VARCHAR, sm.FechaSolicitud, 103) AS Fecha
                FROM SolicitudesMedicas sm
                INNER JOIN TiposSangre ts ON sm.IdTipoSangre = ts.IdTipoSangre
                WHERE sm.Estado = 'Pendiente' AND sm.IdEntidad = @IdEntidad
                ORDER BY 
                    CASE sm.Prioridad 
                        WHEN 'Emergencia' THEN 1 
                        WHEN 'Programada' THEN 2 
                        ELSE 3 
                    END,
                    sm.FechaSolicitud";

            return ConexionDB.Instancia.EjecutarConsulta(query,
                new SqlParameter("@IdEntidad", Program.UsuarioActual.IdEntidad));
        }

        private DataTable ObtenerDonacionesRecientes()
        {
            string query = @"
                SELECT TOP 10
                    dn.Nombre AS Donante,
                    ts.Codigo AS TipoSangre,
                    d.CantidadML AS 'Cantidad (ml)',
                    CONVERT(VARCHAR, d.FechaRecoleccion, 103) AS Fecha,
                    d.Estado
                FROM Donaciones d
                INNER JOIN Donantes dn ON d.IdDonante = dn.IdDonante
                INNER JOIN TiposSangre ts ON d.IdTipoSangre = ts.IdTipoSangre
                WHERE dn.IdEntidad = @IdEntidad
                ORDER BY d.FechaRecoleccion DESC";

            return ConexionDB.Instancia.EjecutarConsulta(query,
                new SqlParameter("@IdEntidad", Program.UsuarioActual.IdEntidad));
        }

        private DataTable ObtenerAlertasInventario()
        {
            string query = @"
                SELECT 
                    ts.Codigo AS 'Tipo de Sangre',
                    ISNULL(inv.CantidadUnidades, 0) AS 'Unidades Disponibles',
                    CASE 
                        WHEN ISNULL(inv.CantidadUnidades, 0) = 0 THEN 'SE NECESITA URGENTE'
                        ELSE 'Inventario Bajo'
                    END AS Alerta
                FROM TiposSangre ts
                LEFT JOIN InventarioSangre inv 
                    ON ts.IdTipoSangre = inv.IdTipoSangre 
                    AND inv.IdEntidad = @IdEntidad
                WHERE ISNULL(inv.CantidadUnidades, 0) < 5
                ORDER BY ISNULL(inv.CantidadUnidades, 0)";

            return ConexionDB.Instancia.EjecutarConsulta(query,
                new SqlParameter("@IdEntidad", Program.UsuarioActual.IdEntidad));
        }

        private DataTable ObtenerEstadisticasDonantes()
        {
            string query = @"
                SELECT 
                    ts.Codigo AS 'Tipo de Sangre',
                    COUNT(d.IdDonante) AS 'Cantidad de Donantes'
                FROM TiposSangre ts
                LEFT JOIN Donantes d ON ts.IdTipoSangre = d.IdTipoSangre 
                    AND d.Activo = 1 AND d.IdEntidad = @IdEntidad
                GROUP BY ts.Codigo
                ORDER BY ts.Codigo";

            return ConexionDB.Instancia.EjecutarConsulta(query,
                new SqlParameter("@IdEntidad", Program.UsuarioActual.IdEntidad));
        }

        // ========== MÉTODOS QUE ACTUALIZAN UI ==========

        private void ActualizarEstadisticas((int donantes, int donaciones, int solicitudes, int porVencer) stats)
        {
            lblTotalDonantes.Text = stats.donantes.ToString("N0");
            lblTotalDonaciones.Text = stats.donaciones.ToString("N0");
            lblSolicitudesPendientes.Text = stats.solicitudes.ToString("N0");
            lblSolicitudesPendientes.ForeColor = stats.solicitudes > 0 ? Color.Red : Color.Green;
            lblUnidadesPorVencer.Text = stats.porVencer.ToString("N0");
            lblUnidadesPorVencer.ForeColor = stats.porVencer > 0 ? Color.Orange : Color.Green;
        }

        private void ActualizarInventario(DataTable dt)
        {
            dgvInventario.DataSource = dt;

            foreach (DataGridViewRow row in dgvInventario.Rows)
            {
                string estado = row.Cells["Estado"].Value?.ToString();
                Color color;
                if (estado == "Crítico")
                    color = Color.LightCoral;
                else if (estado == "Bajo")
                    color = Color.LightYellow;
                else if (estado == "Medio")
                    color = Color.LightBlue;
                else
                    color = Color.LightGreen;

                row.DefaultCellStyle.BackColor = color;
            }
        }

        private void ActualizarSolicitudes(DataTable dt)
        {
            dgvSolicitudes.DataSource = dt;
        }

        private void ActualizarDonaciones(DataTable dt)
        {
            dgvDonacionesRecientes.DataSource = dt;
        }

        private void ActualizarAlertas(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                dgvAlertas.DataSource = dt;
                dgvAlertas.Visible = true;
                lblSinAlertas.Visible = false;

                foreach (DataGridViewRow row in dgvAlertas.Rows)
                {
                    row.DefaultCellStyle.BackColor = Color.LightCoral;
                }
            }
            else
            {
                dgvAlertas.Visible = false;
                lblSinAlertas.Visible = true;
                lblSinAlertas.Text = "✓ No hay alertas de inventario";
                lblSinAlertas.ForeColor = Color.Green;
            }
        }

        private void ActualizarDonantesPorTipo(DataTable dt)
        {
            dgvDonantesPorTipo.DataSource = dt;
        }

        // ========== RESTO DE CÓDIGO ==========

        private void InicializarDataGridViews()
        {
            // dgvSolicitudes
            dgvSolicitudes.AutoGenerateColumns = false;
            dgvSolicitudes.Columns.Clear();
            dgvSolicitudes.Columns.Add(new DataGridViewTextBoxColumn { Name = "IdSolicitud", DataPropertyName = "IdSolicitud", Visible = false });
            dgvSolicitudes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Solicitante", DataPropertyName = "Solicitante", HeaderText = "Solicitante", Width = 150 });
            dgvSolicitudes.Columns.Add(new DataGridViewTextBoxColumn { Name = "TipoSangre", DataPropertyName = "TipoSangre", HeaderText = "Tipo", Width = 80 });
            dgvSolicitudes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Cantidad", DataPropertyName = "Cantidad", HeaderText = "Cantidad", Width = 80 });
            dgvSolicitudes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Prioridad", DataPropertyName = "Prioridad", HeaderText = "Prioridad", Width = 100 });
            dgvSolicitudes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Fecha", DataPropertyName = "Fecha", HeaderText = "Fecha", Width = 100 });

            // dgvInventario
            dgvInventario.AutoGenerateColumns = false;
            dgvInventario.Columns.Clear();
            dgvInventario.Columns.Add(new DataGridViewTextBoxColumn { Name = "TipoSangre", DataPropertyName = "TipoSangre", HeaderText = "Tipo", Width = 80 });
            dgvInventario.Columns.Add(new DataGridViewTextBoxColumn { Name = "Unidades", DataPropertyName = "Unidades", HeaderText = "Unidades", Width = 100 });
            dgvInventario.Columns.Add(new DataGridViewTextBoxColumn { Name = "Estado", DataPropertyName = "Estado", HeaderText = "Estado", Width = 100 });

            // dgvDonacionesRecientes
            dgvDonacionesRecientes.AutoGenerateColumns = false;
            dgvDonacionesRecientes.Columns.Clear();
            dgvDonacionesRecientes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Donante", DataPropertyName = "Donante", HeaderText = "Donante", Width = 150 });
            dgvDonacionesRecientes.Columns.Add(new DataGridViewTextBoxColumn { Name = "TipoSangre", DataPropertyName = "TipoSangre", HeaderText = "Tipo", Width = 80 });
            dgvDonacionesRecientes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Cantidad (ml)", DataPropertyName = "Cantidad (ml)", HeaderText = "Cantidad (ml)", Width = 100 });
            dgvDonacionesRecientes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Fecha", DataPropertyName = "Fecha", HeaderText = "Fecha", Width = 100 });
            dgvDonacionesRecientes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Estado", DataPropertyName = "Estado", HeaderText = "Estado", Width = 100 });

            // dgvAlertas
            dgvAlertas.AutoGenerateColumns = false;
            dgvAlertas.Columns.Clear();
            dgvAlertas.Columns.Add(new DataGridViewTextBoxColumn { Name = "Tipo de Sangre", DataPropertyName = "Tipo de Sangre", HeaderText = "Tipo de Sangre", Width = 100 });
            dgvAlertas.Columns.Add(new DataGridViewTextBoxColumn { Name = "Unidades Disponibles", DataPropertyName = "Unidades Disponibles", HeaderText = "Unidades Disponibles", Width = 100 });
            dgvAlertas.Columns.Add(new DataGridViewTextBoxColumn { Name = "Alerta", DataPropertyName = "Alerta", HeaderText = "Alerta", Width = 150 });

            // dgvDonantesPorTipo
            dgvDonantesPorTipo.AutoGenerateColumns = false;
            dgvDonantesPorTipo.Columns.Clear();
            dgvDonantesPorTipo.Columns.Add(new DataGridViewTextBoxColumn { Name = "Tipo de Sangre", DataPropertyName = "Tipo de Sangre", HeaderText = "Tipo de Sangre", Width = 100 });
            dgvDonantesPorTipo.Columns.Add(new DataGridViewTextBoxColumn { Name = "Cantidad de Donantes", DataPropertyName = "Cantidad de Donantes", HeaderText = "Cantidad de Donantes", Width = 100 });
        }

        private void ConfigurarTimer()
        {
            timerActualizacion = new Timer { Interval = 30000 };
            timerActualizacion.Tick += async (s, e) => await CargarDashboardAsync();
            timerActualizacion.Start();
        }

        private async void btnRefrescar_Click(object sender, EventArgs e)
        {
            await CargarDashboardAsync();
            MessageBox.Show("Dashboard actualizado correctamente.", "Información",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            timerActualizacion?.Stop();
            timerActualizacion?.Dispose();
            base.OnFormClosing(e);
        }

        private void lblTitulo_Click(object sender, EventArgs e)
        {

        }
    }
}