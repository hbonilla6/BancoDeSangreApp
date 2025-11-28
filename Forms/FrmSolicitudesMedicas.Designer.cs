namespace BancoDeSangreApp.Forms
{
    partial class FrmSolicitudesMedicas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.grpNuevaSolicitud = new System.Windows.Forms.GroupBox();
            this.lblDisponibilidad = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGuardarSolicitud = new System.Windows.Forms.Button();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.lblObservaciones = new System.Windows.Forms.Label();
            this.cmbPrioridad = new System.Windows.Forms.ComboBox();
            this.lblPrioridad = new System.Windows.Forms.Label();
            this.numCantidad = new System.Windows.Forms.NumericUpDown();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.cmbTipoSangre = new System.Windows.Forms.ComboBox();
            this.lblTipoSangre = new System.Windows.Forms.Label();
            this.txtSolicitante = new System.Windows.Forms.TextBox();
            this.lblSolicitante = new System.Windows.Forms.Label();
            this.btnNuevaSolicitud = new System.Windows.Forms.Button();
            this.grpListaSolicitudes = new System.Windows.Forms.GroupBox();
            this.dgvSolicitudes = new System.Windows.Forms.DataGridView();
            this.pnlFiltros = new System.Windows.Forms.Panel();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.cmbEstadoFiltro = new System.Windows.Forms.ComboBox();
            this.lblEstadoFiltro = new System.Windows.Forms.Label();
            this.pnlAcciones = new System.Windows.Forms.Panel();
            this.btnAtender = new System.Windows.Forms.Button();
            this.btnRechazar = new System.Windows.Forms.Button();
            this.btnAprobar = new System.Windows.Forms.Button();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.lblTotalSolicitudes = new System.Windows.Forms.Label();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.grpNuevaSolicitud.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCantidad)).BeginInit();
            this.grpListaSolicitudes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSolicitudes)).BeginInit();
            this.pnlFiltros.SuspendLayout();
            this.pnlAcciones.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(68)))), ((int)(((byte)(173)))));
            this.pnlTop.Controls.Add(this.lblTitulo);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1400, 80);
            this.pnlTop.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(12, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(346, 37);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "🏥 Solicitudes Médicas";
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 80);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.grpNuevaSolicitud);
            this.splitContainer.Panel1.Padding = new System.Windows.Forms.Padding(10);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.grpListaSolicitudes);
            this.splitContainer.Panel2.Padding = new System.Windows.Forms.Padding(10, 10, 10, 50);
            this.splitContainer.Size = new System.Drawing.Size(1400, 620);
            this.splitContainer.SplitterDistance = 420;
            this.splitContainer.TabIndex = 1;
            // 
            // grpNuevaSolicitud
            // 
            this.grpNuevaSolicitud.Controls.Add(this.lblDisponibilidad);
            this.grpNuevaSolicitud.Controls.Add(this.btnCancelar);
            this.grpNuevaSolicitud.Controls.Add(this.btnGuardarSolicitud);
            this.grpNuevaSolicitud.Controls.Add(this.txtObservaciones);
            this.grpNuevaSolicitud.Controls.Add(this.lblObservaciones);
            this.grpNuevaSolicitud.Controls.Add(this.cmbPrioridad);
            this.grpNuevaSolicitud.Controls.Add(this.lblPrioridad);
            this.grpNuevaSolicitud.Controls.Add(this.numCantidad);
            this.grpNuevaSolicitud.Controls.Add(this.lblCantidad);
            this.grpNuevaSolicitud.Controls.Add(this.cmbTipoSangre);
            this.grpNuevaSolicitud.Controls.Add(this.lblTipoSangre);
            this.grpNuevaSolicitud.Controls.Add(this.txtSolicitante);
            this.grpNuevaSolicitud.Controls.Add(this.lblSolicitante);
            this.grpNuevaSolicitud.Controls.Add(this.btnNuevaSolicitud);
            this.grpNuevaSolicitud.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpNuevaSolicitud.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpNuevaSolicitud.Location = new System.Drawing.Point(10, 10);
            this.grpNuevaSolicitud.Name = "grpNuevaSolicitud";
            this.grpNuevaSolicitud.Size = new System.Drawing.Size(400, 600);
            this.grpNuevaSolicitud.TabIndex = 0;
            this.grpNuevaSolicitud.TabStop = false;
            this.grpNuevaSolicitud.Text = "Nueva Solicitud";
            // 
            // lblDisponibilidad
            // 
            this.lblDisponibilidad.AutoSize = true;
            this.lblDisponibilidad.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblDisponibilidad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.lblDisponibilidad.Location = new System.Drawing.Point(20, 190);
            this.lblDisponibilidad.Name = "lblDisponibilidad";
            this.lblDisponibilidad.Size = new System.Drawing.Size(0, 15);
            this.lblDisponibilidad.TabIndex = 13;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnCancelar.Enabled = false;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(210, 530);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(170, 45);
            this.btnCancelar.TabIndex = 12;
            this.btnCancelar.Text = "❌ Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGuardarSolicitud
            // 
            this.btnGuardarSolicitud.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnGuardarSolicitud.Enabled = false;
            this.btnGuardarSolicitud.FlatAppearance.BorderSize = 0;
            this.btnGuardarSolicitud.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarSolicitud.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnGuardarSolicitud.ForeColor = System.Drawing.Color.White;
            this.btnGuardarSolicitud.Location = new System.Drawing.Point(20, 530);
            this.btnGuardarSolicitud.Name = "btnGuardarSolicitud";
            this.btnGuardarSolicitud.Size = new System.Drawing.Size(170, 45);
            this.btnGuardarSolicitud.TabIndex = 11;
            this.btnGuardarSolicitud.Text = "💾 Guardar";
            this.btnGuardarSolicitud.UseVisualStyleBackColor = false;
            this.btnGuardarSolicitud.Click += new System.EventHandler(this.btnGuardarSolicitud_Click);
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Enabled = false;
            this.txtObservaciones.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtObservaciones.Location = new System.Drawing.Point(20, 370);
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtObservaciones.Size = new System.Drawing.Size(360, 100);
            this.txtObservaciones.TabIndex = 10;
            // 
            // lblObservaciones
            // 
            this.lblObservaciones.AutoSize = true;
            this.lblObservaciones.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblObservaciones.Location = new System.Drawing.Point(20, 340);
            this.lblObservaciones.Name = "lblObservaciones";
            this.lblObservaciones.Size = new System.Drawing.Size(113, 19);
            this.lblObservaciones.TabIndex = 9;
            this.lblObservaciones.Text = "Observaciones:";
            // 
            // cmbPrioridad
            // 
            this.cmbPrioridad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPrioridad.Enabled = false;
            this.cmbPrioridad.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbPrioridad.FormattingEnabled = true;
            this.cmbPrioridad.Location = new System.Drawing.Point(20, 300);
            this.cmbPrioridad.Name = "cmbPrioridad";
            this.cmbPrioridad.Size = new System.Drawing.Size(360, 25);
            this.cmbPrioridad.TabIndex = 8;
            // 
            // lblPrioridad
            // 
            this.lblPrioridad.AutoSize = true;
            this.lblPrioridad.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPrioridad.Location = new System.Drawing.Point(20, 270);
            this.lblPrioridad.Name = "lblPrioridad";
            this.lblPrioridad.Size = new System.Drawing.Size(77, 19);
            this.lblPrioridad.TabIndex = 7;
            this.lblPrioridad.Text = "Prioridad:";
            // 
            // numCantidad
            // 
            this.numCantidad.Enabled = false;
            this.numCantidad.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numCantidad.Location = new System.Drawing.Point(20, 230);
            this.numCantidad.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numCantidad.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCantidad.Name = "numCantidad";
            this.numCantidad.Size = new System.Drawing.Size(360, 25);
            this.numCantidad.TabIndex = 6;
            this.numCantidad.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblCantidad
            // 
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCantidad.Location = new System.Drawing.Point(20, 200);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(160, 19);
            this.lblCantidad.TabIndex = 5;
            this.lblCantidad.Text = "Cantidad (unidades):";
            // 
            // cmbTipoSangre
            // 
            this.cmbTipoSangre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoSangre.Enabled = false;
            this.cmbTipoSangre.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbTipoSangre.FormattingEnabled = true;
            this.cmbTipoSangre.Location = new System.Drawing.Point(20, 160);
            this.cmbTipoSangre.Name = "cmbTipoSangre";
            this.cmbTipoSangre.Size = new System.Drawing.Size(360, 25);
            this.cmbTipoSangre.TabIndex = 4;
            this.cmbTipoSangre.SelectedIndexChanged += new System.EventHandler(this.cmbTipoSangre_SelectedIndexChanged);
            // 
            // lblTipoSangre
            // 
            this.lblTipoSangre.AutoSize = true;
            this.lblTipoSangre.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTipoSangre.Location = new System.Drawing.Point(20, 130);
            this.lblTipoSangre.Name = "lblTipoSangre";
            this.lblTipoSangre.Size = new System.Drawing.Size(122, 19);
            this.lblTipoSangre.TabIndex = 3;
            this.lblTipoSangre.Text = "Tipo de Sangre:";
            // 
            // txtSolicitante
            // 
            this.txtSolicitante.Enabled = false;
            this.txtSolicitante.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSolicitante.Location = new System.Drawing.Point(20, 90);
            this.txtSolicitante.Name = "txtSolicitante";
            this.txtSolicitante.Size = new System.Drawing.Size(360, 25);
            this.txtSolicitante.TabIndex = 2;
            // 
            // lblSolicitante
            // 
            this.lblSolicitante.AutoSize = true;
            this.lblSolicitante.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSolicitante.Location = new System.Drawing.Point(20, 60);
            this.lblSolicitante.Name = "lblSolicitante";
            this.lblSolicitante.Size = new System.Drawing.Size(87, 19);
            this.lblSolicitante.TabIndex = 1;
            this.lblSolicitante.Text = "Solicitante:";
            // 
            // btnNuevaSolicitud
            // 
            this.btnNuevaSolicitud.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnNuevaSolicitud.FlatAppearance.BorderSize = 0;
            this.btnNuevaSolicitud.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevaSolicitud.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnNuevaSolicitud.ForeColor = System.Drawing.Color.White;
            this.btnNuevaSolicitud.Location = new System.Drawing.Point(20, 480);
            this.btnNuevaSolicitud.Name = "btnNuevaSolicitud";
            this.btnNuevaSolicitud.Size = new System.Drawing.Size(360, 40);
            this.btnNuevaSolicitud.TabIndex = 0;
            this.btnNuevaSolicitud.Text = "➕ Nueva Solicitud";
            this.btnNuevaSolicitud.UseVisualStyleBackColor = false;
            this.btnNuevaSolicitud.Click += new System.EventHandler(this.btnNuevaSolicitud_Click);
            // 
            // grpListaSolicitudes
            // 
            this.grpListaSolicitudes.Controls.Add(this.dgvSolicitudes);
            this.grpListaSolicitudes.Controls.Add(this.pnlFiltros);
            this.grpListaSolicitudes.Controls.Add(this.pnlAcciones);
            this.grpListaSolicitudes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpListaSolicitudes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpListaSolicitudes.Location = new System.Drawing.Point(10, 10);
            this.grpListaSolicitudes.Name = "grpListaSolicitudes";
            this.grpListaSolicitudes.Size = new System.Drawing.Size(956, 560);
            this.grpListaSolicitudes.TabIndex = 0;
            this.grpListaSolicitudes.TabStop = false;
            this.grpListaSolicitudes.Text = "Lista de Solicitudes";
            // 
            // dgvSolicitudes
            // 
            this.dgvSolicitudes.BackgroundColor = System.Drawing.Color.White;
            this.dgvSolicitudes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSolicitudes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSolicitudes.Location = new System.Drawing.Point(3, 83);
            this.dgvSolicitudes.Name = "dgvSolicitudes";
            this.dgvSolicitudes.Size = new System.Drawing.Size(950, 414);
            this.dgvSolicitudes.TabIndex = 2;
            this.dgvSolicitudes.SelectionChanged += new System.EventHandler(this.dgvSolicitudes_SelectionChanged);
            // 
            // pnlFiltros
            // 
            this.pnlFiltros.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.pnlFiltros.Controls.Add(this.btnBuscar);
            this.pnlFiltros.Controls.Add(this.cmbEstadoFiltro);
            this.pnlFiltros.Controls.Add(this.lblEstadoFiltro);
            this.pnlFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFiltros.Location = new System.Drawing.Point(3, 23);
            this.pnlFiltros.Name = "pnlFiltros";
            this.pnlFiltros.Size = new System.Drawing.Size(950, 60);
            this.pnlFiltros.TabIndex = 1;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnBuscar.FlatAppearance.BorderSize = 0;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(370, 15);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(120, 30);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "🔍 Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // cmbEstadoFiltro
            // 
            this.cmbEstadoFiltro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEstadoFiltro.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbEstadoFiltro.FormattingEnabled = true;
            this.cmbEstadoFiltro.Location = new System.Drawing.Point(120, 17);
            this.cmbEstadoFiltro.Name = "cmbEstadoFiltro";
            this.cmbEstadoFiltro.Size = new System.Drawing.Size(230, 25);
            this.cmbEstadoFiltro.TabIndex = 1;
            // 
            // lblEstadoFiltro
            // 
            this.lblEstadoFiltro.AutoSize = true;
            this.lblEstadoFiltro.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblEstadoFiltro.Location = new System.Drawing.Point(20, 20);
            this.lblEstadoFiltro.Name = "lblEstadoFiltro";
            this.lblEstadoFiltro.Size = new System.Drawing.Size(94, 19);
            this.lblEstadoFiltro.TabIndex = 0;
            this.lblEstadoFiltro.Text = "Filtrar por: ";
            // 
            // pnlAcciones
            // 
            this.pnlAcciones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.pnlAcciones.Controls.Add(this.btnAtender);
            this.pnlAcciones.Controls.Add(this.btnRechazar);
            this.pnlAcciones.Controls.Add(this.btnAprobar);
            this.pnlAcciones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlAcciones.Location = new System.Drawing.Point(3, 497);
            this.pnlAcciones.Name = "pnlAcciones";
            this.pnlAcciones.Size = new System.Drawing.Size(950, 60);
            this.pnlAcciones.TabIndex = 0;
            // 
            // btnAtender
            // 
            this.btnAtender.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnAtender.Enabled = false;
            this.btnAtender.FlatAppearance.BorderSize = 0;
            this.btnAtender.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAtender.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAtender.ForeColor = System.Drawing.Color.White;
            this.btnAtender.Location = new System.Drawing.Point(380, 10);
            this.btnAtender.Name = "btnAtender";
            this.btnAtender.Size = new System.Drawing.Size(170, 40);
            this.btnAtender.TabIndex = 2;
            this.btnAtender.Text = "📦 Atender";
            this.btnAtender.UseVisualStyleBackColor = false;
            this.btnAtender.Click += new System.EventHandler(this.btnAtender_Click);
            // 
            // btnRechazar
            // 
            this.btnRechazar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnRechazar.Enabled = false;
            this.btnRechazar.FlatAppearance.BorderSize = 0;
            this.btnRechazar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRechazar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRechazar.ForeColor = System.Drawing.Color.White;
            this.btnRechazar.Location = new System.Drawing.Point(195, 10);
            this.btnRechazar.Name = "btnRechazar";
            this.btnRechazar.Size = new System.Drawing.Size(170, 40);
            this.btnRechazar.TabIndex = 1;
            this.btnRechazar.Text = "❌ Rechazar";
            this.btnRechazar.UseVisualStyleBackColor = false;
            this.btnRechazar.Click += new System.EventHandler(this.btnRechazar_Click);
            // 
            // btnAprobar
            // 
            this.btnAprobar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnAprobar.Enabled = false;
            this.btnAprobar.FlatAppearance.BorderSize = 0;
            this.btnAprobar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAprobar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAprobar.ForeColor = System.Drawing.Color.White;
            this.btnAprobar.Location = new System.Drawing.Point(10, 10);
            this.btnAprobar.Name = "btnAprobar";
            this.btnAprobar.Size = new System.Drawing.Size(170, 40);
            this.btnAprobar.TabIndex = 0;
            this.btnAprobar.Text = "✅ Aprobar";
            this.btnAprobar.UseVisualStyleBackColor = false;
            this.btnAprobar.Click += new System.EventHandler(this.btnAprobar_Click);
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.pnlBottom.Controls.Add(this.lblTotalSolicitudes);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 700);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1400, 50);
            this.pnlBottom.TabIndex = 2;
            //
            // lblTotalSolicitudes
            //
            this.lblTotalSolicitudes.AutoSize = true;
            this.lblTotalSolicitudes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalSolicitudes.Location = new System.Drawing.Point(20, 15);
            this.lblTotalSolicitudes.Name = "lblTotalSolicitudes";
            this.lblTotalSolicitudes.Size = new System.Drawing.Size(147, 19);
            this.lblTotalSolicitudes.TabIndex = 0;
            this.lblTotalSolicitudes.Text = "Total: 0 solicitudes";
            //
            // FrmSolicitudesMedicas
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 750);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlTop);
            this.MinimumSize = new System.Drawing.Size(1200, 700);
            this.Name = "FrmSolicitudesMedicas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Solicitudes Médicas - Banco de Sangre";
            this.Load += new System.EventHandler(this.FrmSolicitudesMedicas_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.grpNuevaSolicitud.ResumeLayout(false);
            this.grpNuevaSolicitud.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCantidad)).EndInit();
            this.grpListaSolicitudes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSolicitudes)).EndInit();
            this.pnlFiltros.ResumeLayout(false);
            this.pnlFiltros.PerformLayout();
            this.pnlAcciones.ResumeLayout(false);
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.GroupBox grpNuevaSolicitud;
        private System.Windows.Forms.Button btnNuevaSolicitud;
        private System.Windows.Forms.Label lblSolicitante;
        private System.Windows.Forms.TextBox txtSolicitante;
        private System.Windows.Forms.ComboBox cmbTipoSangre;
        private System.Windows.Forms.Label lblTipoSangre;
        private System.Windows.Forms.NumericUpDown numCantidad;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.ComboBox cmbPrioridad;
        private System.Windows.Forms.Label lblPrioridad;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.Label lblObservaciones;
        private System.Windows.Forms.Button btnGuardarSolicitud;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblDisponibilidad;
        private System.Windows.Forms.GroupBox grpListaSolicitudes;
        private System.Windows.Forms.DataGridView dgvSolicitudes;
        private System.Windows.Forms.Panel pnlFiltros;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.ComboBox cmbEstadoFiltro;
        private System.Windows.Forms.Label lblEstadoFiltro;
        private System.Windows.Forms.Panel pnlAcciones;
        private System.Windows.Forms.Button btnAtender;
        private System.Windows.Forms.Button btnRechazar;
        private System.Windows.Forms.Button btnAprobar;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Label lblTotalSolicitudes;
    }
}
