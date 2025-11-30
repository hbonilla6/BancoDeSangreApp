using BancoDeSangreApp.Business;
using BancoDeSangreApp.Data;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BancoDeSangreApp.Forms
{
    public partial class FrmReportes : Form
    {
        private readonly InventarioBLL _inventarioBLL;
        private readonly SolicitudMedicaBLL _solicitudBLL;
        private readonly AuditoriaBLL _auditoriaBLL;

        public FrmReportes()
        {
            InitializeComponent();
            _inventarioBLL = new InventarioBLL();
            _solicitudBLL = new SolicitudMedicaBLL();
            _auditoriaBLL = new AuditoriaBLL();
        }

        private void FrmReportes_Load(object sender, EventArgs e)
        {
            ConfigurarControles();
            CargarFiltros();
        }

        private void ConfigurarControles()
        {
            // Configurar DateTimePickers con rango del último mes
            dtpFechaInicio.Value = DateTime.Now.AddMonths(-1);
            dtpFechaFin.Value = DateTime.Now;

            // Configurar DataGridView común
            ConfigurarDataGridView(dgvReporte);
        }

        private void ConfigurarDataGridView(DataGridView dgv)
        {
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void CargarFiltros()
        {
            try
            {
                // Cargar tipos de sangre
                string queryTipos = "SELECT IdTipoSangre, Codigo FROM TiposSangre ORDER BY Codigo";
                DataTable dtTipos = ConexionDB.Instancia.EjecutarConsulta(queryTipos);

                DataRow rowTodos = dtTipos.NewRow();
                rowTodos["IdTipoSangre"] = 0;
                rowTodos["Codigo"] = "Todos";
                dtTipos.Rows.InsertAt(rowTodos, 0);

                cmbTipoSangre.DataSource = dtTipos;
                cmbTipoSangre.DisplayMember = "Codigo";
                cmbTipoSangre.ValueMember = "IdTipoSangre";
                cmbTipoSangre.SelectedIndex = 0;

                // Cargar donantes
                string queryDonantes = @"
                    SELECT IdDonante, Nombre 
                    FROM Donantes 
                    WHERE Activo = 1 AND IdEntidad = @IdEntidad
                    ORDER BY Nombre";

                var paramsDonantes = new System.Data.SqlClient.SqlParameter[] {
                    new System.Data.SqlClient.SqlParameter("@IdEntidad", Program.UsuarioActual.IdEntidad)
                };

                DataTable dtDonantes = ConexionDB.Instancia.EjecutarConsulta(queryDonantes, paramsDonantes);

                DataRow rowTodosDonantes = dtDonantes.NewRow();
                rowTodosDonantes["IdDonante"] = 0;
                rowTodosDonantes["Nombre"] = "Todos";
                dtDonantes.Rows.InsertAt(rowTodosDonantes, 0);

                cmbDonante.DataSource = dtDonantes;
                cmbDonante.DisplayMember = "Nombre";
                cmbDonante.ValueMember = "IdDonante";
                cmbDonante.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar filtros: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== TAB 1: INVENTARIO ====================
        private void tabReportes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabReportes.SelectedTab == tabInventario)
            {
                GenerarReporteInventario();
            }
        }

        private void btnGenerarInventario_Click(object sender, EventArgs e)
        {
            GenerarReporteInventario();
        }

        private void GenerarReporteInventario()
        {
            try
            {
                int? idEntidad = Program.UsuarioActual?.IdEntidad;
                DataTable dt = _inventarioBLL.GenerarReporteInventario(idEntidad);

                dgvInventario.AutoGenerateColumns = false;
                dgvInventario.Columns.Clear();

                dgvInventario.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "TipoSangre",
                    DataPropertyName = "TipoSangre",
                    HeaderText = "Tipo Sangre",
                    Width = 100
                });

                dgvInventario.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Disponibles",
                    DataPropertyName = "UnidadesDisponibles",
                    HeaderText = "Disponibles",
                    Width = 100
                });

                dgvInventario.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Reservadas",
                    DataPropertyName = "UnidadesReservadas",
                    HeaderText = "Reservadas",
                    Width = 100
                });

                dgvInventario.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "ProximasVencer",
                    DataPropertyName = "UnidadesProximasVencer",
                    HeaderText = "Próximas Vencer",
                    Width = 120
                });

                dgvInventario.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "UltimaActualizacion",
                    DataPropertyName = "UltimaActualizacion",
                    HeaderText = "Última Actualización",
                    Width = 150,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" }
                });

                dgvInventario.DataSource = dt;

                // Calcular totales
                int totalDisponibles = 0;
                int totalReservadas = 0;
                int totalProximas = 0;

                foreach (DataRow row in dt.Rows)
                {
                    totalDisponibles += Convert.ToInt32(row["UnidadesDisponibles"]);
                    totalReservadas += Convert.ToInt32(row["UnidadesReservadas"]);
                    totalProximas += Convert.ToInt32(row["UnidadesProximasVencer"]);
                }

                lblTotalInventario.Text = $"Total Disponibles: {totalDisponibles} | " +
                                         $"Reservadas: {totalReservadas} | " +
                                         $"Próximas a Vencer: {totalProximas}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar reporte de inventario: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== TAB 2: DONACIONES ====================
        private void btnGenerarDonaciones_Click(object sender, EventArgs e)
        {
            GenerarReporteDonaciones();
        }

        private void GenerarReporteDonaciones()
        {
            try
            {
                string query = @"
                    SELECT 
                        d.IdDonacion,
                        d.FechaRecoleccion,
                        don.Nombre AS Donante,
                        don.Documento,
                        ts.Codigo AS TipoSangre,
                        d.CantidadML,
                        d.Estado,
                        d.FechaCaducidad,
                        DATEDIFF(DAY, GETDATE(), d.FechaCaducidad) AS DiasRestantes,
                        c.Nombre AS CentroRecoleccion
                    FROM Donaciones d
                    INNER JOIN Donantes don ON d.IdDonante = don.IdDonante
                    INNER JOIN TiposSangre ts ON d.IdTipoSangre = ts.IdTipoSangre
                    LEFT JOIN CentrosRecoleccion c ON d.IdCentro = c.IdCentro
                    WHERE don.IdEntidad = @IdEntidad
                    AND d.FechaRecoleccion BETWEEN @FechaInicio AND @FechaFin";

                var parametros = new System.Collections.Generic.List<System.Data.SqlClient.SqlParameter>
                {
                    new System.Data.SqlClient.SqlParameter("@IdEntidad", Program.UsuarioActual.IdEntidad),
                    new System.Data.SqlClient.SqlParameter("@FechaInicio", dtpFechaInicio.Value.Date),
                    new System.Data.SqlClient.SqlParameter("@FechaFin", dtpFechaFin.Value.Date)
                };

                // Filtro por tipo de sangre
                if (cmbTipoSangre.SelectedIndex > 0)
                {
                    query += " AND d.IdTipoSangre = @IdTipoSangre";
                    parametros.Add(new System.Data.SqlClient.SqlParameter("@IdTipoSangre", cmbTipoSangre.SelectedValue));
                }

                // Filtro por donante
                if (cmbDonante.SelectedIndex > 0)
                {
                    query += " AND d.IdDonante = @IdDonante";
                    parametros.Add(new System.Data.SqlClient.SqlParameter("@IdDonante", cmbDonante.SelectedValue));
                }

                query += " ORDER BY d.FechaRecoleccion DESC";

                DataTable dt = ConexionDB.Instancia.EjecutarConsulta(query, parametros.ToArray());

                dgvDonaciones.AutoGenerateColumns = true;
                dgvDonaciones.DataSource = dt;

                // Calcular estadísticas
                int totalDonaciones = dt.Rows.Count;
                int totalML = 0;
                var porTipo = new System.Collections.Generic.Dictionary<string, int>();

                foreach (DataRow row in dt.Rows)
                {
                    totalML += Convert.ToInt32(row["CantidadML"]);
                    string tipo = row["TipoSangre"].ToString();

                    if (porTipo.ContainsKey(tipo))
                        porTipo[tipo]++;
                    else
                        porTipo[tipo] = 1;
                }

                lblTotalDonaciones.Text = $"Total Donaciones: {totalDonaciones} | " +
                                         $"Total ML: {totalML:N0} | " +
                                         $"Promedio: {(totalDonaciones > 0 ? totalML / totalDonaciones : 0)} ml/donación";

                // Mostrar distribución por tipo
                string distribucion = "Distribución: " +
                    string.Join(", ", porTipo.Select(x => $"{x.Key}={x.Value}"));
                lblDistribucion.Text = distribucion;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar reporte de donaciones: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== TAB 3: SOLICITUDES ====================
        private void btnGenerarSolicitudes_Click(object sender, EventArgs e)
        {
            GenerarReporteSolicitudes();
        }

        private void GenerarReporteSolicitudes()
        {
            try
            {
                string query = @"
                    SELECT 
                        s.IdSolicitud,
                        s.FechaSolicitud,
                        s.Solicitante,
                        ts.Codigo AS TipoSangre,
                        s.CantidadSolicitada,
                        s.Prioridad,
                        s.Estado,
                        s.FechaAtencion,
                        CASE 
                            WHEN s.FechaAtencion IS NOT NULL 
                            THEN DATEDIFF(HOUR, s.FechaSolicitud, s.FechaAtencion)
                            ELSE NULL
                        END AS HorasAtencion,
                        COUNT(sd.IdDonacion) AS UnidadesAsignadas
                    FROM SolicitudesMedicas s
                    INNER JOIN TiposSangre ts ON s.IdTipoSangre = ts.IdTipoSangre
                    LEFT JOIN SolicitudesDonaciones sd ON s.IdSolicitud = sd.IdSolicitud
                    WHERE s.IdEntidad = @IdEntidad
                    AND s.FechaSolicitud BETWEEN @FechaInicio AND @FechaFin";

                var parametros = new System.Collections.Generic.List<System.Data.SqlClient.SqlParameter>
                {
                    new System.Data.SqlClient.SqlParameter("@IdEntidad", Program.UsuarioActual.IdEntidad),
                    new System.Data.SqlClient.SqlParameter("@FechaInicio", dtpFechaInicio.Value.Date),
                    new System.Data.SqlClient.SqlParameter("@FechaFin", dtpFechaFin.Value.Date)
                };

                // Filtro por estado
                if (cmbEstadoSolicitud.SelectedIndex > 0)
                {
                    query += " AND s.Estado = @Estado";
                    parametros.Add(new System.Data.SqlClient.SqlParameter("@Estado",
                        cmbEstadoSolicitud.SelectedItem.ToString()));
                }

                query += @" GROUP BY s.IdSolicitud, s.FechaSolicitud, s.Solicitante, ts.Codigo,
                           s.CantidadSolicitada, s.Prioridad, s.Estado, s.FechaAtencion
                           ORDER BY s.FechaSolicitud DESC";

                DataTable dt = ConexionDB.Instancia.EjecutarConsulta(query, parametros.ToArray());

                dgvSolicitudes.AutoGenerateColumns = true;
                dgvSolicitudes.DataSource = dt;

                // Calcular estadísticas
                var stats = _solicitudBLL.ObtenerEstadisticasSolicitudes(Program.UsuarioActual.IdEntidad);

                lblTotalSolicitudes.Text = $"Total: {stats["Total"]} | " +
                                          $"Pendientes: {stats["Pendientes"]} | " +
                                          $"Atendidas: {stats["Atendidas"]} | " +
                                          $"Emergencias: {stats["Emergencias"]}";

                // Calcular tiempo promedio de atención
                var horasAtencion = dt.AsEnumerable()
                    .Where(r => r["HorasAtencion"] != DBNull.Value)
                    .Select(r => Convert.ToInt32(r["HorasAtencion"]))
                    .ToList();

                if (horasAtencion.Any())
                {
                    double promedio = horasAtencion.Average();
                    lblTiempoPromedio.Text = $"Tiempo Promedio Atención: {promedio:F1} horas";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar reporte de solicitudes: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== TAB 4: CADUCIDAD ====================
        private void btnGenerarCaducidad_Click(object sender, EventArgs e)
        {
            GenerarReporteCaducidad();
        }

        private void GenerarReporteCaducidad()
        {
            try
            {
                int diasAnticipacion = Convert.ToInt32(numDiasAnticipacion.Value);
                DataTable dt = _inventarioBLL.ObtenerDonacionesProximasVencer(diasAnticipacion);

                dgvCaducidad.AutoGenerateColumns = true;
                dgvCaducidad.DataSource = dt;

                // Colorear según urgencia
                foreach (DataGridViewRow row in dgvCaducidad.Rows)
                {
                    if (row.Cells["DiasRestantes"].Value != DBNull.Value)
                    {
                        int dias = Convert.ToInt32(row.Cells["DiasRestantes"].Value);

                        if (dias <= 3)
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 200, 200);
                        else if (dias <= 7)
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 235, 180);
                        else if (dias <= 14)
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 250, 205);
                    }
                }

                lblTotalCaducidad.Text = $"Total Unidades Próximas a Vencer: {dt.Rows.Count}";

                // Calcular pérdida potencial
                int mlTotales = dt.AsEnumerable()
                    .Sum(r => Convert.ToInt32(r["CantidadML"]));

                lblPerdidaPotencial.Text = $"Pérdida Potencial: {mlTotales:N0} ml";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar reporte de caducidad: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== TAB 5: CONSOLIDADO ====================
        private void btnGenerarConsolidado_Click(object sender, EventArgs e)
        {
            GenerarReporteConsolidado();
        }

        private void GenerarReporteConsolidado()
        {
            try
            {
                System.Text.StringBuilder reporte = new System.Text.StringBuilder();

                reporte.AppendLine("═══════════════════════════════════════════════════════");
                reporte.AppendLine("     REPORTE CONSOLIDADO - BANCO DE SANGRE");
                reporte.AppendLine("═══════════════════════════════════════════════════════");
                reporte.AppendLine($"Entidad: {Program.UsuarioActual.NombreEntidad}");
                reporte.AppendLine($"Período: {dtpFechaInicio.Value:dd/MM/yyyy} - {dtpFechaFin.Value:dd/MM/yyyy}");
                reporte.AppendLine($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                reporte.AppendLine("═══════════════════════════════════════════════════════");
                reporte.AppendLine();

                // INVENTARIO
                reporte.AppendLine("📊 INVENTARIO ACTUAL");
                reporte.AppendLine("───────────────────────────────────────────────────────");
                var statsInv = _inventarioBLL.ObtenerEstadisticasInventario(Program.UsuarioActual.IdEntidad);
                reporte.AppendLine($"  • Total Unidades: {statsInv["TotalUnidades"]}");
                reporte.AppendLine($"  • Tipos con Stock Crítico: {statsInv["TiposCriticos"]}");
                reporte.AppendLine($"  • Tipos con Stock Bajo: {statsInv["TiposBajos"]}");
                reporte.AppendLine();

                // SOLICITUDES
                reporte.AppendLine("📋 SOLICITUDES MÉDICAS");
                reporte.AppendLine("───────────────────────────────────────────────────────");
                var statsSol = _solicitudBLL.ObtenerEstadisticasSolicitudes(Program.UsuarioActual.IdEntidad);
                reporte.AppendLine($"  • Total: {statsSol["Total"]}");
                reporte.AppendLine($"  • Pendientes: {statsSol["Pendientes"]}");
                reporte.AppendLine($"  • Atendidas: {statsSol["Atendidas"]}");
                reporte.AppendLine($"  • Emergencias: {statsSol["Emergencias"]}");
                reporte.AppendLine();

                // AUDITORÍA
                reporte.AppendLine("🔒 ACTIVIDAD DEL SISTEMA");
                reporte.AppendLine("───────────────────────────────────────────────────────");
                var statsAud = _auditoriaBLL.ObtenerEstadisticasAuditoria(
                    dtpFechaInicio.Value, dtpFechaFin.Value);
                reporte.AppendLine($"  • Total Operaciones: {statsAud["TotalRegistros"]}");
                reporte.AppendLine($"  • Usuarios Activos: {statsAud["UsuariosActivos"]}");
                reporte.AppendLine($"  • Registros Creados: {statsAud["TotalInserts"]}");
                reporte.AppendLine($"  • Registros Modificados: {statsAud["TotalUpdates"]}");
                reporte.AppendLine();

                reporte.AppendLine("═══════════════════════════════════════════════════════");
                reporte.AppendLine("              FIN DEL REPORTE");
                reporte.AppendLine("═══════════════════════════════════════════════════════");

                txtReporteConsolidado.Text = reporte.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar reporte consolidado: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== EXPORTACIÓN ====================
        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Exportación a PDF\n\n" +
                "Esta funcionalidad requiere una librería externa como iTextSharp.\n" +
                "Por ahora, use la exportación a texto o CSV.",
                "Información",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "Archivo CSV|*.csv",
                    Title = "Exportar reporte",
                    FileName = $"Reporte_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    DataGridView dgvActual = ObtenerDataGridViewActual();

                    if (dgvActual != null)
                    {
                        ExportarACSV(dgvActual, saveDialog.FileName);
                        MessageBox.Show("Reporte exportado exitosamente.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al exportar: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataGridView ObtenerDataGridViewActual()
        {
            if (tabReportes.SelectedTab == tabInventario) return dgvInventario;
            if (tabReportes.SelectedTab == tabDonaciones) return dgvDonaciones;
            if (tabReportes.SelectedTab == tabSolicitudes) return dgvSolicitudes;
            if (tabReportes.SelectedTab == tabCaducidad) return dgvCaducidad;
            return null;
        }

        private void ExportarACSV(DataGridView dgv, string rutaArchivo)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            // Encabezados
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                sb.Append($"\"{dgv.Columns[i].HeaderText}\"");
                if (i < dgv.Columns.Count - 1)
                    sb.Append(",");
            }
            sb.AppendLine();

            // Datos
            foreach (DataGridViewRow row in dgv.Rows)
            {
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    string valor = row.Cells[i].Value?.ToString() ?? "";
                    sb.Append($"\"{valor.Replace("\"", "\"\"")}\"");
                    if (i < dgv.Columns.Count - 1)
                        sb.Append(",");
                }
                sb.AppendLine();
            }

            System.IO.File.WriteAllText(rutaArchivo, sb.ToString());
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            dtpFechaInicio.Value = DateTime.Now.AddMonths(-1);
            dtpFechaFin.Value = DateTime.Now;
            cmbTipoSangre.SelectedIndex = 0;
            cmbDonante.SelectedIndex = 0;
            if (cmbEstadoSolicitud.Items.Count > 0)
                cmbEstadoSolicitud.SelectedIndex = 0;
        }
    }
}