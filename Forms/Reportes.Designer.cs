namespace BancoDeSangreApp.Forms
{
    partial class FrmReportes
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TabControl tabReportes;

        // Tab Inventario
        private System.Windows.Forms.TabPage tabInventario;
        private System.Windows.Forms.DataGridView dgvInventario;
        private System.Windows.Forms.Button btnGenerarInventario;
        private System.Windows.Forms.Label lblTotalInventario;

        // Tab Donaciones
        private System.Windows.Forms.TabPage tabDonaciones;
        private System.Windows.Forms.DataGridView dgvDonaciones;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.ComboBox cmbTipoSangre;
        private System.Windows.Forms.ComboBox cmbDonante;
        private System.Windows.Forms.Button btnGenerarDonaciones;
        private System.Windows.Forms.Label lblTotalDonaciones;
        private System.Windows.Forms.Label lblDistribucion;

        // Tab Solicitudes
        private System.Windows.Forms.TabPage tabSolicitudes;
        private System.Windows.Forms.DataGridView dgvSolicitudes;
        private System.Windows.Forms.ComboBox cmbEstadoSolicitud;
        private System.Windows.Forms.Button btnGenerarSolicitudes;
        private System.Windows.Forms.Label lblTotalSolicitudes;
        private System.Windows.Forms.Label lblTiempoPromedio;

        // Tab Caducidad
        private System.Windows.Forms.TabPage tabCaducidad;
        private System.Windows.Forms.DataGridView dgvCaducidad;
        private System.Windows.Forms.NumericUpDown numDiasAnticipacion;
        private System.Windows.Forms.Button btnGenerarCaducidad;
        private System.Windows.Forms.Label lblTotalCaducidad;
        private System.Windows.Forms.Label lblPerdidaPotencial;

        // Tab Consolidado
        private System.Windows.Forms.TabPage tabConsolidado;
        private System.Windows.Forms.TextBox txtReporteConsolidado;
        private System.Windows.Forms.Button btnGenerarConsolidado;

        // Botones generales
        private System.Windows.Forms.Button btnExportarPDF;
        private System.Windows.Forms.Button btnExportarExcel;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Panel panelBotones;
        private System.Windows.Forms.DataGridView dgvReporte;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tabReportes = new System.Windows.Forms.TabControl();
            this.tabInventario = new System.Windows.Forms.TabPage();
            this.dgvInventario = new System.Windows.Forms.DataGridView();
            this.btnGenerarInventario = new System.Windows.Forms.Button();
            this.lblTotalInventario = new System.Windows.Forms.Label();

            this.tabDonaciones = new System.Windows.Forms.TabPage();
            this.dgvDonaciones = new System.Windows.Forms.DataGridView();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.cmbTipoSangre = new System.Windows.Forms.ComboBox();
            this.cmbDonante = new System.Windows.Forms.ComboBox();
            this.btnGenerarDonaciones = new System.Windows.Forms.Button();
            this.lblTotalDonaciones = new System.Windows.Forms.Label();
            this.lblDistribucion = new System.Windows.Forms.Label();

            this.tabSolicitudes = new System.Windows.Forms.TabPage();
            this.dgvSolicitudes = new System.Windows.Forms.DataGridView();
            this.cmbEstadoSolicitud = new System.Windows.Forms.ComboBox();
            this.btnGenerarSolicitudes = new System.Windows.Forms.Button();
            this.lblTotalSolicitudes = new System.Windows.Forms.Label();
            this.lblTiempoPromedio = new System.Windows.Forms.Label();

            this.tabCaducidad = new System.Windows.Forms.TabPage();
            this.dgvCaducidad = new System.Windows.Forms.DataGridView();
            this.numDiasAnticipacion = new System.Windows.Forms.NumericUpDown();
            this.btnGenerarCaducidad = new System.Windows.Forms.Button();
            this.lblTotalCaducidad = new System.Windows.Forms.Label();
            this.lblPerdidaPotencial = new System.Windows.Forms.Label();

            this.tabConsolidado = new System.Windows.Forms.TabPage();
            this.txtReporteConsolidado = new System.Windows.Forms.TextBox();
            this.btnGenerarConsolidado = new System.Windows.Forms.Button();

            this.panelBotones = new System.Windows.Forms.Panel();
            this.btnExportarPDF = new System.Windows.Forms.Button();
            this.btnExportarExcel = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.dgvReporte = new System.Windows.Forms.DataGridView();

            ((System.ComponentModel.ISupportInitialize)(this.dgvInventario)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDonaciones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSolicitudes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCaducidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDiasAnticipacion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReporte)).BeginInit();
            this.tabReportes.SuspendLayout();
            this.tabInventario.SuspendLayout();
            this.tabDonaciones.SuspendLayout();
            this.tabSolicitudes.SuspendLayout();
            this.tabCaducidad.SuspendLayout();
            this.tabConsolidado.SuspendLayout();
            this.panelBotones.SuspendLayout();
            this.SuspendLayout();

            // 
            // tabReportes
            // 
            this.tabReportes.Controls.Add(this.tabInventario);
            this.tabReportes.Controls.Add(this.tabDonaciones);
            this.tabReportes.Controls.Add(this.tabSolicitudes);
            this.tabReportes.Controls.Add(this.tabCaducidad);
            this.tabReportes.Controls.Add(this.tabConsolidado);
            this.tabReportes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabReportes.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tabReportes.Location = new System.Drawing.Point(0, 0);
            this.tabReportes.Name = "tabReportes";
            this.tabReportes.SelectedIndex = 0;
            this.tabReportes.Size = new System.Drawing.Size(1200, 600);
            this.tabReportes.TabIndex = 0;
            this.tabReportes.SelectedIndexChanged += new System.EventHandler(this.tabReportes_SelectedIndexChanged);

            // ===== TAB INVENTARIO =====
            this.tabInventario.Controls.Add(this.dgvInventario);
            this.tabInventario.Controls.Add(this.btnGenerarInventario);
            this.tabInventario.Controls.Add(this.lblTotalInventario);
            this.tabInventario.Location = new System.Drawing.Point(4, 28);
            this.tabInventario.Name = "tabInventario";
            this.tabInventario.Padding = new System.Windows.Forms.Padding(10);
            this.tabInventario.Size = new System.Drawing.Size(1192, 568);
            this.tabInventario.TabIndex = 0;
            this.tabInventario.Text = "📊 Inventario";
            this.tabInventario.UseVisualStyleBackColor = true;

            this.dgvInventario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInventario.Location = new System.Drawing.Point(10, 45);
            this.dgvInventario.Name = "dgvInventario";
            this.dgvInventario.Size = new System.Drawing.Size(1172, 468);
            this.dgvInventario.TabIndex = 0;

            this.btnGenerarInventario.BackColor = System.Drawing.Color.FromArgb(51, 122, 183);
            this.btnGenerarInventario.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnGenerarInventario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerarInventario.ForeColor = System.Drawing.Color.White;
            this.btnGenerarInventario.Location = new System.Drawing.Point(10, 10);
            this.btnGenerarInventario.Name = "btnGenerarInventario";
            this.btnGenerarInventario.Size = new System.Drawing.Size(1172, 35);
            this.btnGenerarInventario.TabIndex = 1;
            this.btnGenerarInventario.Text = "🔄 Generar Reporte";
            this.btnGenerarInventario.UseVisualStyleBackColor = false;
            this.btnGenerarInventario.Click += new System.EventHandler(this.btnGenerarInventario_Click);

            this.lblTotalInventario.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTotalInventario.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotalInventario.Location = new System.Drawing.Point(10, 513);
            this.lblTotalInventario.Name = "lblTotalInventario";
            this.lblTotalInventario.Size = new System.Drawing.Size(1172, 45);
            this.lblTotalInventario.TabIndex = 2;
            this.lblTotalInventario.Text = "Total: 0";
            this.lblTotalInventario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ===== TAB DONACIONES =====
            this.tabDonaciones.Controls.Add(this.dgvDonaciones);
            this.tabDonaciones.Controls.Add(this.lblDistribucion);
            this.tabDonaciones.Controls.Add(this.lblTotalDonaciones);
            this.tabDonaciones.Location = new System.Drawing.Point(4, 28);
            this.tabDonaciones.Name = "tabDonaciones";
            this.tabDonaciones.Padding = new System.Windows.Forms.Padding(10);
            this.tabDonaciones.Size = new System.Drawing.Size(1192, 568);
            this.tabDonaciones.TabIndex = 1;
            this.tabDonaciones.Text = "🩸 Donaciones";
            this.tabDonaciones.UseVisualStyleBackColor = true;

            // Panel superior con filtros
            var panelFiltrosDonaciones = new System.Windows.Forms.Panel();
            panelFiltrosDonaciones.Dock = System.Windows.Forms.DockStyle.Top;
            panelFiltrosDonaciones.Height = 80;
            panelFiltrosDonaciones.Padding = new System.Windows.Forms.Padding(10);
            this.tabDonaciones.Controls.Add(panelFiltrosDonaciones);

            var lblFechaInicio = new System.Windows.Forms.Label();
            lblFechaInicio.Text = "Fecha Inicio:";
            lblFechaInicio.Location = new System.Drawing.Point(10, 10);
            lblFechaInicio.AutoSize = true;
            panelFiltrosDonaciones.Controls.Add(lblFechaInicio);

            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaInicio.Location = new System.Drawing.Point(10, 35);
            this.dtpFechaInicio.Width = 120;
            panelFiltrosDonaciones.Controls.Add(this.dtpFechaInicio);

            var lblFechaFin = new System.Windows.Forms.Label();
            lblFechaFin.Text = "Fecha Fin:";
            lblFechaFin.Location = new System.Drawing.Point(150, 10);
            lblFechaFin.AutoSize = true;
            panelFiltrosDonaciones.Controls.Add(lblFechaFin);

            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(150, 35);
            this.dtpFechaFin.Width = 120;
            panelFiltrosDonaciones.Controls.Add(this.dtpFechaFin);

            var lblTipoSangre = new System.Windows.Forms.Label();
            lblTipoSangre.Text = "Tipo Sangre:";
            lblTipoSangre.Location = new System.Drawing.Point(290, 10);
            lblTipoSangre.AutoSize = true;
            panelFiltrosDonaciones.Controls.Add(lblTipoSangre);

            this.cmbTipoSangre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoSangre.Location = new System.Drawing.Point(290, 35);
            this.cmbTipoSangre.Width = 100;
            panelFiltrosDonaciones.Controls.Add(this.cmbTipoSangre);

            var lblDonante = new System.Windows.Forms.Label();
            lblDonante.Text = "Donante:";
            lblDonante.Location = new System.Drawing.Point(410, 10);
            lblDonante.AutoSize = true;
            panelFiltrosDonaciones.Controls.Add(lblDonante);

            this.cmbDonante.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDonante.Location = new System.Drawing.Point(410, 35);
            this.cmbDonante.Width = 200;
            panelFiltrosDonaciones.Controls.Add(this.cmbDonante);

            this.btnGenerarDonaciones.BackColor = System.Drawing.Color.FromArgb(51, 122, 183);
            this.btnGenerarDonaciones.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerarDonaciones.ForeColor = System.Drawing.Color.White;
            this.btnGenerarDonaciones.Location = new System.Drawing.Point(630, 30);
            this.btnGenerarDonaciones.Size = new System.Drawing.Size(120, 35);
            this.btnGenerarDonaciones.Text = "🔍 Buscar";
            this.btnGenerarDonaciones.Click += new System.EventHandler(this.btnGenerarDonaciones_Click);
            panelFiltrosDonaciones.Controls.Add(this.btnGenerarDonaciones);

            this.dgvDonaciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDonaciones.Name = "dgvDonaciones";

            this.lblDistribucion.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblDistribucion.Height = 25;
            this.lblDistribucion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.lblTotalDonaciones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTotalDonaciones.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotalDonaciones.Height = 25;
            this.lblTotalDonaciones.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ===== TAB SOLICITUDES =====
            this.tabSolicitudes.Controls.Add(this.dgvSolicitudes);
            this.tabSolicitudes.Controls.Add(this.lblTiempoPromedio);
            this.tabSolicitudes.Controls.Add(this.lblTotalSolicitudes);
            this.tabSolicitudes.Location = new System.Drawing.Point(4, 28);
            this.tabSolicitudes.Name = "tabSolicitudes";
            this.tabSolicitudes.Padding = new System.Windows.Forms.Padding(10);
            this.tabSolicitudes.Size = new System.Drawing.Size(1192, 568);
            this.tabSolicitudes.TabIndex = 2;
            this.tabSolicitudes.Text = "📋 Solicitudes";
            this.tabSolicitudes.UseVisualStyleBackColor = true;

            var panelFiltrosSolicitudes = new System.Windows.Forms.Panel();
            panelFiltrosSolicitudes.Dock = System.Windows.Forms.DockStyle.Top;
            panelFiltrosSolicitudes.Height = 80;
            panelFiltrosSolicitudes.Padding = new System.Windows.Forms.Padding(10);
            this.tabSolicitudes.Controls.Add(panelFiltrosSolicitudes);

            var lblEstado = new System.Windows.Forms.Label();
            lblEstado.Text = "Estado:";
            lblEstado.Location = new System.Drawing.Point(10, 10);
            lblEstado.AutoSize = true;
            panelFiltrosSolicitudes.Controls.Add(lblEstado);

            this.cmbEstadoSolicitud = new System.Windows.Forms.ComboBox();
            this.cmbEstadoSolicitud.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEstadoSolicitud.Items.AddRange(new object[] { "Todos", "Pendiente", "Aprobada", "Rechazada", "Atendida" });
            this.cmbEstadoSolicitud.Location = new System.Drawing.Point(10, 35);
            this.cmbEstadoSolicitud.Width = 150;
            this.cmbEstadoSolicitud.SelectedIndex = 0;
            panelFiltrosSolicitudes.Controls.Add(this.cmbEstadoSolicitud);

            this.btnGenerarSolicitudes.BackColor = System.Drawing.Color.FromArgb(51, 122, 183);
            this.btnGenerarSolicitudes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerarSolicitudes.ForeColor = System.Drawing.Color.White;
            this.btnGenerarSolicitudes.Location = new System.Drawing.Point(180, 30);
            this.btnGenerarSolicitudes.Size = new System.Drawing.Size(120, 35);
            this.btnGenerarSolicitudes.Text = "🔍 Buscar";
            this.btnGenerarSolicitudes.Click += new System.EventHandler(this.btnGenerarSolicitudes_Click);
            panelFiltrosSolicitudes.Controls.Add(this.btnGenerarSolicitudes);

            this.dgvSolicitudes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSolicitudes.Name = "dgvSolicitudes";

            this.lblTiempoPromedio.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTiempoPromedio.Height = 25;
            this.lblTiempoPromedio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.lblTotalSolicitudes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTotalSolicitudes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotalSolicitudes.Height = 25;
            this.lblTotalSolicitudes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ===== TAB CADUCIDAD =====
            this.tabCaducidad.Controls.Add(this.dgvCaducidad);
            this.tabCaducidad.Controls.Add(this.lblPerdidaPotencial);
            this.tabCaducidad.Controls.Add(this.lblTotalCaducidad);
            this.tabCaducidad.Location = new System.Drawing.Point(4, 28);
            this.tabCaducidad.Name = "tabCaducidad";
            this.tabCaducidad.Padding = new System.Windows.Forms.Padding(10);
            this.tabCaducidad.Size = new System.Drawing.Size(1192, 568);
            this.tabCaducidad.TabIndex = 3;
            this.tabCaducidad.Text = "⏰ Caducidad";
            this.tabCaducidad.UseVisualStyleBackColor = true;

            var panelFiltrosCaducidad = new System.Windows.Forms.Panel();
            panelFiltrosCaducidad.Dock = System.Windows.Forms.DockStyle.Top;
            panelFiltrosCaducidad.Height = 80;
            panelFiltrosCaducidad.Padding = new System.Windows.Forms.Padding(10);
            this.tabCaducidad.Controls.Add(panelFiltrosCaducidad);

            var lblDias = new System.Windows.Forms.Label();
            lblDias.Text = "Días de Anticipación:";
            lblDias.Location = new System.Drawing.Point(10, 10);
            lblDias.AutoSize = true;
            panelFiltrosCaducidad.Controls.Add(lblDias);

            this.numDiasAnticipacion.Location = new System.Drawing.Point(10, 35);
            this.numDiasAnticipacion.Minimum = 1;
            this.numDiasAnticipacion.Maximum = 90;
            this.numDiasAnticipacion.Value = 30;
            this.numDiasAnticipacion.Width = 80;
            panelFiltrosCaducidad.Controls.Add(this.numDiasAnticipacion);

            this.btnGenerarCaducidad.BackColor = System.Drawing.Color.FromArgb(51, 122, 183);
            this.btnGenerarCaducidad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerarCaducidad.ForeColor = System.Drawing.Color.White;
            this.btnGenerarCaducidad.Location = new System.Drawing.Point(110, 30);
            this.btnGenerarCaducidad.Size = new System.Drawing.Size(120, 35);
            this.btnGenerarCaducidad.Text = "🔍 Buscar";
            this.btnGenerarCaducidad.Click += new System.EventHandler(this.btnGenerarCaducidad_Click);
            panelFiltrosCaducidad.Controls.Add(this.btnGenerarCaducidad);

            this.dgvCaducidad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCaducidad.Name = "dgvCaducidad";

            this.lblPerdidaPotencial.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblPerdidaPotencial.Height = 25;
            this.lblPerdidaPotencial.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.lblTotalCaducidad.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTotalCaducidad.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotalCaducidad.Height = 25;
            this.lblTotalCaducidad.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ===== TAB CONSOLIDADO =====
            this.tabConsolidado.Controls.Add(this.txtReporteConsolidado);
            this.tabConsolidado.Location = new System.Drawing.Point(4, 28);
            this.tabConsolidado.Name = "tabConsolidado";
            this.tabConsolidado.Padding = new System.Windows.Forms.Padding(10);
            this.tabConsolidado.Size = new System.Drawing.Size(1192, 568);
            this.tabConsolidado.TabIndex = 4;
            this.tabConsolidado.Text = "📑 Consolidado";
            this.tabConsolidado.UseVisualStyleBackColor = true;

            this.btnGenerarConsolidado.BackColor = System.Drawing.Color.FromArgb(51, 122, 183);
            this.btnGenerarConsolidado.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnGenerarConsolidado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerarConsolidado.ForeColor = System.Drawing.Color.White;
            this.btnGenerarConsolidado.Height = 40;
            this.btnGenerarConsolidado.Text = "📊 Generar Reporte Consolidado";
            this.btnGenerarConsolidado.Click += new System.EventHandler(this.btnGenerarConsolidado_Click);
            this.tabConsolidado.Controls.Add(this.btnGenerarConsolidado);

            this.txtReporteConsolidado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReporteConsolidado.Font = new System.Drawing.Font("Courier New", 9F);
            this.txtReporteConsolidado.Multiline = true;
            this.txtReporteConsolidado.ReadOnly = true;
            this.txtReporteConsolidado.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtReporteConsolidado.WordWrap = false;

            // ===== PANEL BOTONES =====
            this.panelBotones.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.panelBotones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBotones.Height = 60;
            this.panelBotones.Padding = new System.Windows.Forms.Padding(10);

            this.btnExportarExcel.BackColor = System.Drawing.Color.FromArgb(92, 184, 92);
            this.btnExportarExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportarExcel.ForeColor = System.Drawing.Color.White;
            this.btnExportarExcel.Location = new System.Drawing.Point(10, 10);
            this.btnExportarExcel.Size = new System.Drawing.Size(150, 40);
            this.btnExportarExcel.Text = "📊 Exportar CSV";
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            this.panelBotones.Controls.Add(this.btnExportarExcel);

            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(240, 173, 78);
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.ForeColor = System.Drawing.Color.White;
            this.btnLimpiar.Location = new System.Drawing.Point(350, 10);
            this.btnLimpiar.Size = new System.Drawing.Size(150, 40);
            this.btnLimpiar.Text = "🧹 Limpiar Filtros";
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            this.panelBotones.Controls.Add(this.btnLimpiar);

            // 
            // FrmReportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 660);
            this.Controls.Add(this.tabReportes);
            this.Controls.Add(this.panelBotones);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "FrmReportes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reportes e Información - Banco de Sangre";
            this.Load += new System.EventHandler(this.FrmReportes_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dgvInventario)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDonaciones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSolicitudes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCaducidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDiasAnticipacion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReporte)).EndInit();
            this.tabReportes.ResumeLayout(false);
            this.tabInventario.ResumeLayout(false);
            this.tabDonaciones.ResumeLayout(false);
            this.tabSolicitudes.ResumeLayout(false);
            this.tabCaducidad.ResumeLayout(false);
            this.tabConsolidado.ResumeLayout(false);
            this.panelBotones.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}