using BancoDeSangreApp.Business;
using BancoDeSangreApp.Data;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace BancoDeSangreApp.Forms
{
    public partial class FrmAuditoria : Form
    {
        private readonly AuditoriaBLL _auditoriaBLL;
        private readonly UsuarioBLL _usuarioBLL;

        public FrmAuditoria()
        {
            InitializeComponent();
            _auditoriaBLL = new AuditoriaBLL();
            _usuarioBLL = new UsuarioBLL();
        }

        private void FrmAuditoria_Load(object sender, EventArgs e)
        {
            ConfigurarDataGridView();
            CargarFiltros();
            CargarAuditoria();
            ActualizarEstadisticas();
        }

        private void ConfigurarDataGridView()
        {
            dgvAuditoria.AutoGenerateColumns = false;
            dgvAuditoria.Columns.Clear();

            dgvAuditoria.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdAuditoria",
                DataPropertyName = "IdAuditoria",
                HeaderText = "ID",
                Width = 80
            });

            dgvAuditoria.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaAccion",
                DataPropertyName = "FechaAccion",
                HeaderText = "Fecha/Hora",
                Width = 140,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm:ss" }
            });

            dgvAuditoria.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Usuario",
                DataPropertyName = "NombreUsuario",
                HeaderText = "Usuario",
                Width = 150
            });

            dgvAuditoria.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Entidad",
                DataPropertyName = "Entidad",
                HeaderText = "Entidad",
                Width = 120
            });

            dgvAuditoria.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Operacion",
                DataPropertyName = "Operacion",
                HeaderText = "Operación",
                Width = 100
            });

            dgvAuditoria.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ClavePrimaria",
                DataPropertyName = "ClavePrimaria",
                HeaderText = "Clave",
                Width = 80
            });

            dgvAuditoria.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ValorNuevo",
                DataPropertyName = "ValorNuevo",
                HeaderText = "Descripción",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvAuditoria.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAuditoria.MultiSelect = false;
            dgvAuditoria.ReadOnly = true;
            dgvAuditoria.AllowUserToAddRows = false;
            dgvAuditoria.RowHeadersVisible = false;
        }

        private void CargarFiltros()
        {
            try
            {
                // Cargar Entidades
                cmbEntidad.Items.Clear();
                cmbEntidad.Items.Add("Todas");
                var entidades = _auditoriaBLL.ObtenerEntidades();
                foreach (var entidad in entidades)
                {
                    cmbEntidad.Items.Add(entidad);
                }
                cmbEntidad.SelectedIndex = 0;

                // Cargar Operaciones
                cmbOperacion.Items.Clear();
                cmbOperacion.Items.Add("Todas");
                var operaciones = _auditoriaBLL.ObtenerOperaciones();
                foreach (var operacion in operaciones)
                {
                    cmbOperacion.Items.Add(operacion);
                }
                cmbOperacion.SelectedIndex = 0;

                // Cargar Usuarios
                cmbUsuario.Items.Clear();
                cmbUsuario.Items.Add("Todos");
                cmbUsuario.DisplayMember = "NombreCompleto";
                cmbUsuario.ValueMember = "IdUsuario";

                string query = "SELECT IdUsuario, NombreCompleto FROM Usuarios ORDER BY NombreCompleto";
                DataTable dtUsuarios = ConexionDB.Instancia.EjecutarConsulta(query);

                foreach (DataRow row in dtUsuarios.Rows)
                {
                    cmbUsuario.Items.Add(new
                    {
                        IdUsuario = row["IdUsuario"],
                        NombreCompleto = row["NombreCompleto"]
                    });
                }
                cmbUsuario.SelectedIndex = 0;

                // Configurar fechas
                dtpFechaInicio.Value = DateTime.Now.AddDays(-30);
                dtpFechaFin.Value = DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar filtros: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarAuditoria()
        {
            try
            {
                DateTime? fechaInicio = chkFiltroFecha.Checked ? (DateTime?)dtpFechaInicio.Value.Date : null;
                DateTime? fechaFin = chkFiltroFecha.Checked ? (DateTime?)dtpFechaFin.Value.Date : null;

                int? idUsuario = null;
                if (cmbUsuario.SelectedIndex > 0)
                {
                    dynamic selectedUser = cmbUsuario.SelectedItem;
                    idUsuario = Convert.ToInt32(selectedUser.IdUsuario);
                }

                string entidad = cmbEntidad.SelectedIndex > 0 ? cmbEntidad.SelectedItem.ToString() : null;
                string operacion = cmbOperacion.SelectedIndex > 0 ? cmbOperacion.SelectedItem.ToString() : null;

                int limite = Convert.ToInt32(numLimite.Value);

                DataTable dt = _auditoriaBLL.ObtenerAuditoria(fechaInicio, fechaFin, idUsuario, entidad, operacion, limite);
                dgvAuditoria.DataSource = dt;

                // Colorear filas según operación
                foreach (DataGridViewRow row in dgvAuditoria.Rows)
                {
                    string op = row.Cells["Operacion"].Value?.ToString();

                    switch (op)
                    {
                        case "INSERT":
                            row.DefaultCellStyle.BackColor = Color.FromArgb(230, 255, 230);
                            break;
                        case "UPDATE":
                            row.DefaultCellStyle.BackColor = Color.FromArgb(230, 240, 255);
                            break;
                        case "DELETE":
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 230);
                            break;
                        case "LOGIN":
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 250, 205);
                            break;
                    }
                }

                lblTotalRegistros.Text = $"Total registros: {dt.Rows.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar auditoría: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarEstadisticas()
        {
            try
            {
                DateTime? fechaInicio = chkFiltroFecha.Checked ? (DateTime?)dtpFechaInicio.Value.Date : null;
                DateTime? fechaFin = chkFiltroFecha.Checked ? (DateTime?)dtpFechaFin.Value.Date : null;

                var stats = _auditoriaBLL.ObtenerEstadisticasAuditoria(fechaInicio, fechaFin);

                lblTotalAcciones.Text = stats.ContainsKey("TotalRegistros")
                    ? stats["TotalRegistros"].ToString()
                    : "0";

                lblUsuariosActivos.Text = stats.ContainsKey("UsuariosActivos")
                    ? stats["UsuariosActivos"].ToString()
                    : "0";

                lblInserts.Text = stats.ContainsKey("TotalInserts")
                    ? stats["TotalInserts"].ToString()
                    : "0";

                lblUpdates.Text = stats.ContainsKey("TotalUpdates")
                    ? stats["TotalUpdates"].ToString()
                    : "0";

                lblDeletes.Text = stats.ContainsKey("TotalDeletes")
                    ? stats["TotalDeletes"].ToString()
                    : "0";

                lblLogins.Text = stats.ContainsKey("TotalLogins")
                    ? stats["TotalLogins"].ToString()
                    : "0";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al actualizar estadísticas: {ex.Message}");
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarAuditoria();
            ActualizarEstadisticas();
        }

        private void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            cmbEntidad.SelectedIndex = 0;
            cmbOperacion.SelectedIndex = 0;
            cmbUsuario.SelectedIndex = 0;
            chkFiltroFecha.Checked = false;
            dtpFechaInicio.Value = DateTime.Now.AddDays(-30);
            dtpFechaFin.Value = DateTime.Now;
            numLimite.Value = 1000;

            CargarAuditoria();
            ActualizarEstadisticas();
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "Archivo de texto|*.txt|Archivo CSV|*.csv",
                    Title = "Exportar auditoría",
                    FileName = $"Auditoria_{DateTime.Now:yyyyMMdd_HHmmss}"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    if (dgvAuditoria.DataSource is DataTable dt)
                    {
                        if (saveDialog.FileName.EndsWith(".txt"))
                        {
                            string contenido = _auditoriaBLL.ExportarAuditoriaTexto(dt);
                            System.IO.File.WriteAllText(saveDialog.FileName, contenido);
                        }
                        else
                        {
                            ExportarACSV(dgvAuditoria, saveDialog.FileName);
                        }

                        MessageBox.Show("Auditoría exportada exitosamente.", "Éxito",
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

        private void dgvAuditoria_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvAuditoria.Rows[e.RowIndex];

                string detalle = $"ID Auditoría: {row.Cells["IdAuditoria"].Value}\n\n" +
                                $"Fecha/Hora: {row.Cells["FechaAccion"].Value}\n" +
                                $"Usuario: {row.Cells["Usuario"].Value}\n" +
                                $"Entidad: {row.Cells["Entidad"].Value}\n" +
                                $"Operación: {row.Cells["Operacion"].Value}\n" +
                                $"Clave Primaria: {row.Cells["ClavePrimaria"].Value}\n\n" +
                                $"Descripción:\n{row.Cells["ValorNuevo"].Value}";

                MessageBox.Show(detalle, "Detalle de Auditoría",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnVerDetalle_Click(object sender, EventArgs e)
        {
            if (dgvAuditoria.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvAuditoria.SelectedRows[0];

                string valorAnterior = "";
                string valorNuevo = "";

                if (dgvAuditoria.DataSource is DataTable dt)
                {
                    int rowIndex = row.Index;
                    valorAnterior = dt.Rows[rowIndex]["ValorAnterior"]?.ToString() ?? "N/A";
                    valorNuevo = dt.Rows[rowIndex]["ValorNuevo"]?.ToString() ?? "N/A";
                }

                string detalle = $"═══ DETALLE COMPLETO DE AUDITORÍA ═══\n\n" +
                                $"ID: {row.Cells["IdAuditoria"].Value}\n" +
                                $"Fecha/Hora: {row.Cells["FechaAccion"].Value}\n" +
                                $"Usuario: {row.Cells["Usuario"].Value}\n" +
                                $"Entidad: {row.Cells["Entidad"].Value}\n" +
                                $"Operación: {row.Cells["Operacion"].Value}\n" +
                                $"Clave: {row.Cells["ClavePrimaria"].Value}\n\n" +
                                $"═══ VALOR ANTERIOR ═══\n{valorAnterior}\n\n" +
                                $"═══ VALOR NUEVO ═══\n{valorNuevo}";

                Form frmDetalle = new Form
                {
                    Text = "Detalle de Auditoría",
                    Width = 600,
                    Height = 500,
                    StartPosition = FormStartPosition.CenterParent,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    MaximizeBox = false,
                    MinimizeBox = false
                };

                TextBox txtDetalle = new TextBox
                {
                    Multiline = true,
                    Dock = DockStyle.Fill,
                    ReadOnly = true,
                    ScrollBars = ScrollBars.Both,
                    Font = new Font("Courier New", 9),
                    Text = detalle
                };

                frmDetalle.Controls.Add(txtDetalle);
                frmDetalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("Seleccione un registro para ver el detalle.",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void chkFiltroFecha_CheckedChanged(object sender, EventArgs e)
        {
            dtpFechaInicio.Enabled = chkFiltroFecha.Checked;
            dtpFechaFin.Enabled = chkFiltroFecha.Checked;
        }
    }
}