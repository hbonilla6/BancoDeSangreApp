namespace BancoDeSangreApp.Forms
{
    partial class FrmDonaciones
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.grpNuevaDonacion = new System.Windows.Forms.GroupBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGuardarDonacion = new System.Windows.Forms.Button();
            this.dtpFechaRecoleccion = new System.Windows.Forms.DateTimePicker();
            this.lblFechaRecoleccion = new System.Windows.Forms.Label();
            this.numCantidadML = new System.Windows.Forms.NumericUpDown();
            this.lblCantidadML = new System.Windows.Forms.Label();
            this.cmbTipoSangre = new System.Windows.Forms.ComboBox();
            this.lblTipoSangre = new System.Windows.Forms.Label();
            this.cmbDonante = new System.Windows.Forms.ComboBox();
            this.lblDonante = new System.Windows.Forms.Label();
            this.btnNuevaDonacion = new System.Windows.Forms.Button();
            this.btnNuevoDonante = new System.Windows.Forms.Button();
            this.grpListaDonaciones = new System.Windows.Forms.GroupBox();
            this.dgvDonaciones = new System.Windows.Forms.DataGridView();
            this.pnlFiltros = new System.Windows.Forms.Panel();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.cmbEstadoFiltro = new System.Windows.Forms.ComboBox();
            this.lblEstadoFiltro = new System.Windows.Forms.Label();
            this.cmbTipoSangreFiltro = new System.Windows.Forms.ComboBox();
            this.lblTipoSangreFiltro = new System.Windows.Forms.Label();
            this.pnlAcciones = new System.Windows.Forms.Panel();
            this.btnMarcarVencida = new System.Windows.Forms.Button();
            this.btnMarcarUsada = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.lblTotalDonaciones = new System.Windows.Forms.Label();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.grpNuevaDonacion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCantidadML)).BeginInit();
            this.grpListaDonaciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDonaciones)).BeginInit();
            this.pnlFiltros.SuspendLayout();
            this.pnlAcciones.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.pnlTop.Controls.Add(this.lblTitulo);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1370, 80);
            this.pnlTop.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(12, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(211, 37);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "💉 Donaciones";
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 80);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.grpNuevaDonacion);
            this.splitContainer.Panel1.Padding = new System.Windows.Forms.Padding(10);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.grpListaDonaciones);
            this.splitContainer.Panel2.Padding = new System.Windows.Forms.Padding(10, 10, 10, 50);
            this.splitContainer.Size = new System.Drawing.Size(1370, 619);
            this.splitContainer.SplitterDistance = 411;
            this.splitContainer.TabIndex = 1;
            // 
            // grpNuevaDonacion
            // 
            this.grpNuevaDonacion.Controls.Add(this.btnCancelar);
            this.grpNuevaDonacion.Controls.Add(this.btnGuardarDonacion);
            this.grpNuevaDonacion.Controls.Add(this.dtpFechaRecoleccion);
            this.grpNuevaDonacion.Controls.Add(this.lblFechaRecoleccion);
            this.grpNuevaDonacion.Controls.Add(this.numCantidadML);
            this.grpNuevaDonacion.Controls.Add(this.lblCantidadML);
            this.grpNuevaDonacion.Controls.Add(this.cmbTipoSangre);
            this.grpNuevaDonacion.Controls.Add(this.lblTipoSangre);
            this.grpNuevaDonacion.Controls.Add(this.cmbDonante);
            this.grpNuevaDonacion.Controls.Add(this.lblDonante);
            this.grpNuevaDonacion.Controls.Add(this.btnNuevaDonacion);
            this.grpNuevaDonacion.Controls.Add(this.btnNuevoDonante);
            this.grpNuevaDonacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpNuevaDonacion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpNuevaDonacion.Location = new System.Drawing.Point(10, 10);
            this.grpNuevaDonacion.Name = "grpNuevaDonacion";
            this.grpNuevaDonacion.Size = new System.Drawing.Size(391, 599);
            this.grpNuevaDonacion.TabIndex = 0;
            this.grpNuevaDonacion.TabStop = false;
            this.grpNuevaDonacion.Text = "Nueva Donación";
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
            this.btnCancelar.TabIndex = 11;
            this.btnCancelar.Text = "❌ Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGuardarDonacion
            // 
            this.btnGuardarDonacion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnGuardarDonacion.Enabled = false;
            this.btnGuardarDonacion.FlatAppearance.BorderSize = 0;
            this.btnGuardarDonacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarDonacion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnGuardarDonacion.ForeColor = System.Drawing.Color.White;
            this.btnGuardarDonacion.Location = new System.Drawing.Point(20, 530);
            this.btnGuardarDonacion.Name = "btnGuardarDonacion";
            this.btnGuardarDonacion.Size = new System.Drawing.Size(170, 45);
            this.btnGuardarDonacion.TabIndex = 10;
            this.btnGuardarDonacion.Text = "💾 Guardar";
            this.btnGuardarDonacion.UseVisualStyleBackColor = false;
            this.btnGuardarDonacion.Click += new System.EventHandler(this.btnGuardarDonacion_Click);
            // 
            // dtpFechaRecoleccion
            // 
            this.dtpFechaRecoleccion.Enabled = false;
            this.dtpFechaRecoleccion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpFechaRecoleccion.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaRecoleccion.Location = new System.Drawing.Point(20, 300);
            this.dtpFechaRecoleccion.Name = "dtpFechaRecoleccion";
            this.dtpFechaRecoleccion.Size = new System.Drawing.Size(360, 25);
            this.dtpFechaRecoleccion.TabIndex = 9;
            // 
            // lblFechaRecoleccion
            // 
            this.lblFechaRecoleccion.AutoSize = true;
            this.lblFechaRecoleccion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblFechaRecoleccion.Location = new System.Drawing.Point(20, 270);
            this.lblFechaRecoleccion.Name = "lblFechaRecoleccion";
            this.lblFechaRecoleccion.Size = new System.Drawing.Size(156, 19);
            this.lblFechaRecoleccion.TabIndex = 8;
            this.lblFechaRecoleccion.Text = "Fecha de Recolección:";
            // 
            // numCantidadML
            // 
            this.numCantidadML.Enabled = false;
            this.numCantidadML.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numCantidadML.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numCantidadML.Location = new System.Drawing.Point(20, 230);
            this.numCantidadML.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numCantidadML.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numCantidadML.Name = "numCantidadML";
            this.numCantidadML.Size = new System.Drawing.Size(360, 25);
            this.numCantidadML.TabIndex = 7;
            this.numCantidadML.Value = new decimal(new int[] {
            450,
            0,
            0,
            0});
            // 
            // lblCantidadML
            // 
            this.lblCantidadML.AutoSize = true;
            this.lblCantidadML.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCantidadML.Location = new System.Drawing.Point(20, 200);
            this.lblCantidadML.Name = "lblCantidadML";
            this.lblCantidadML.Size = new System.Drawing.Size(104, 19);
            this.lblCantidadML.TabIndex = 6;
            this.lblCantidadML.Text = "Cantidad (ml):";
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
            this.cmbTipoSangre.TabIndex = 5;
            // 
            // lblTipoSangre
            // 
            this.lblTipoSangre.AutoSize = true;
            this.lblTipoSangre.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTipoSangre.Location = new System.Drawing.Point(20, 130);
            this.lblTipoSangre.Name = "lblTipoSangre";
            this.lblTipoSangre.Size = new System.Drawing.Size(115, 19);
            this.lblTipoSangre.TabIndex = 4;
            this.lblTipoSangre.Text = "Tipo de Sangre:";
            // 
            // cmbDonante
            // 
            this.cmbDonante.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDonante.Enabled = false;
            this.cmbDonante.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbDonante.FormattingEnabled = true;
            this.cmbDonante.Location = new System.Drawing.Point(20, 90);
            this.cmbDonante.Name = "cmbDonante";
            this.cmbDonante.Size = new System.Drawing.Size(270, 25);
            this.cmbDonante.TabIndex = 3;
            // 
            // lblDonante
            // 
            this.lblDonante.AutoSize = true;
            this.lblDonante.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDonante.Location = new System.Drawing.Point(20, 60);
            this.lblDonante.Name = "lblDonante";
            this.lblDonante.Size = new System.Drawing.Size(69, 19);
            this.lblDonante.TabIndex = 2;
            this.lblDonante.Text = "Donante:";
            // 
            // btnNuevaDonacion
            // 
            this.btnNuevaDonacion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnNuevaDonacion.FlatAppearance.BorderSize = 0;
            this.btnNuevaDonacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevaDonacion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnNuevaDonacion.ForeColor = System.Drawing.Color.White;
            this.btnNuevaDonacion.Location = new System.Drawing.Point(20, 480);
            this.btnNuevaDonacion.Name = "btnNuevaDonacion";
            this.btnNuevaDonacion.Size = new System.Drawing.Size(360, 40);
            this.btnNuevaDonacion.TabIndex = 0;
            this.btnNuevaDonacion.Text = "➕ Nueva Donación";
            this.btnNuevaDonacion.UseVisualStyleBackColor = false;
            this.btnNuevaDonacion.Click += new System.EventHandler(this.btnNuevaDonacion_Click);
            // 
            // btnNuevoDonante
            // 
            this.btnNuevoDonante.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.btnNuevoDonante.Enabled = false;
            this.btnNuevoDonante.FlatAppearance.BorderSize = 0;
            this.btnNuevoDonante.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevoDonante.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnNuevoDonante.ForeColor = System.Drawing.Color.White;
            this.btnNuevoDonante.Location = new System.Drawing.Point(300, 87);
            this.btnNuevoDonante.Name = "btnNuevoDonante";
            this.btnNuevoDonante.Size = new System.Drawing.Size(80, 28);
            this.btnNuevoDonante.TabIndex = 1;
            this.btnNuevoDonante.Text = "➕ Nuevo";
            this.btnNuevoDonante.UseVisualStyleBackColor = false;
            this.btnNuevoDonante.Click += new System.EventHandler(this.btnNuevoDonante_Click);
            // 
            // grpListaDonaciones
            // 
            this.grpListaDonaciones.Controls.Add(this.dgvDonaciones);
            this.grpListaDonaciones.Controls.Add(this.pnlFiltros);
            this.grpListaDonaciones.Controls.Add(this.pnlAcciones);
            this.grpListaDonaciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpListaDonaciones.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpListaDonaciones.Location = new System.Drawing.Point(10, 10);
            this.grpListaDonaciones.Name = "grpListaDonaciones";
            this.grpListaDonaciones.Size = new System.Drawing.Size(935, 559);
            this.grpListaDonaciones.TabIndex = 0;
            this.grpListaDonaciones.TabStop = false;
            this.grpListaDonaciones.Text = "Lista de Donaciones";
            // 
            // dgvDonaciones
            // 
            this.dgvDonaciones.BackgroundColor = System.Drawing.Color.White;
            this.dgvDonaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDonaciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDonaciones.Location = new System.Drawing.Point(3, 81);
            this.dgvDonaciones.Name = "dgvDonaciones";
            this.dgvDonaciones.Size = new System.Drawing.Size(929, 415);
            this.dgvDonaciones.TabIndex = 2;
            this.dgvDonaciones.SelectionChanged += new System.EventHandler(this.dgvDonaciones_SelectionChanged);
            // 
            // pnlFiltros
            // 
            this.pnlFiltros.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.pnlFiltros.Controls.Add(this.btnBuscar);
            this.pnlFiltros.Controls.Add(this.cmbEstadoFiltro);
            this.pnlFiltros.Controls.Add(this.lblEstadoFiltro);
            this.pnlFiltros.Controls.Add(this.cmbTipoSangreFiltro);
            this.pnlFiltros.Controls.Add(this.lblTipoSangreFiltro);
            this.pnlFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFiltros.Location = new System.Drawing.Point(3, 21);
            this.pnlFiltros.Name = "pnlFiltros";
            this.pnlFiltros.Size = new System.Drawing.Size(929, 60);
            this.pnlFiltros.TabIndex = 1;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnBuscar.FlatAppearance.BorderSize = 0;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(720, 15);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(120, 30);
            this.btnBuscar.TabIndex = 4;
            this.btnBuscar.Text = "🔍 Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // cmbEstadoFiltro
            // 
            this.cmbEstadoFiltro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEstadoFiltro.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbEstadoFiltro.FormattingEnabled = true;
            this.cmbEstadoFiltro.Location = new System.Drawing.Point(460, 17);
            this.cmbEstadoFiltro.Name = "cmbEstadoFiltro";
            this.cmbEstadoFiltro.Size = new System.Drawing.Size(230, 25);
            this.cmbEstadoFiltro.TabIndex = 3;
            // 
            // lblEstadoFiltro
            // 
            this.lblEstadoFiltro.AutoSize = true;
            this.lblEstadoFiltro.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblEstadoFiltro.Location = new System.Drawing.Point(380, 20);
            this.lblEstadoFiltro.Name = "lblEstadoFiltro";
            this.lblEstadoFiltro.Size = new System.Drawing.Size(57, 19);
            this.lblEstadoFiltro.TabIndex = 2;
            this.lblEstadoFiltro.Text = "Estado:";
            // 
            // cmbTipoSangreFiltro
            // 
            this.cmbTipoSangreFiltro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoSangreFiltro.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbTipoSangreFiltro.FormattingEnabled = true;
            this.cmbTipoSangreFiltro.Location = new System.Drawing.Point(120, 17);
            this.cmbTipoSangreFiltro.Name = "cmbTipoSangreFiltro";
            this.cmbTipoSangreFiltro.Size = new System.Drawing.Size(230, 25);
            this.cmbTipoSangreFiltro.TabIndex = 1;
            // 
            // lblTipoSangreFiltro
            // 
            this.lblTipoSangreFiltro.AutoSize = true;
            this.lblTipoSangreFiltro.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTipoSangreFiltro.Location = new System.Drawing.Point(20, 20);
            this.lblTipoSangreFiltro.Name = "lblTipoSangreFiltro";
            this.lblTipoSangreFiltro.Size = new System.Drawing.Size(94, 19);
            this.lblTipoSangreFiltro.TabIndex = 0;
            this.lblTipoSangreFiltro.Text = "Tipo Sangre:";
            // 
            // pnlAcciones
            // 
            this.pnlAcciones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.pnlAcciones.Controls.Add(this.btnMarcarVencida);
            this.pnlAcciones.Controls.Add(this.btnMarcarUsada);
            this.pnlAcciones.Controls.Add(this.btnEliminar);
            this.pnlAcciones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlAcciones.Location = new System.Drawing.Point(3, 496);
            this.pnlAcciones.Name = "pnlAcciones";
            this.pnlAcciones.Size = new System.Drawing.Size(929, 60);
            this.pnlAcciones.TabIndex = 0;
            // 
            // btnMarcarVencida
            // 
            this.btnMarcarVencida.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(156)))), ((int)(((byte)(18)))));
            this.btnMarcarVencida.Enabled = false;
            this.btnMarcarVencida.FlatAppearance.BorderSize = 0;
            this.btnMarcarVencida.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMarcarVencida.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnMarcarVencida.ForeColor = System.Drawing.Color.White;
            this.btnMarcarVencida.Location = new System.Drawing.Point(195, 10);
            this.btnMarcarVencida.Name = "btnMarcarVencida";
            this.btnMarcarVencida.Size = new System.Drawing.Size(170, 40);
            this.btnMarcarVencida.TabIndex = 2;
            this.btnMarcarVencida.Text = "⏰ Marcar Vencida";
            this.btnMarcarVencida.UseVisualStyleBackColor = false;
            this.btnMarcarVencida.Click += new System.EventHandler(this.btnMarcarVencida_Click);
            // 
            // btnMarcarUsada
            // 
            this.btnMarcarUsada.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnMarcarUsada.Enabled = false;
            this.btnMarcarUsada.FlatAppearance.BorderSize = 0;
            this.btnMarcarUsada.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMarcarUsada.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnMarcarUsada.ForeColor = System.Drawing.Color.White;
            this.btnMarcarUsada.Location = new System.Drawing.Point(10, 10);
            this.btnMarcarUsada.Name = "btnMarcarUsada";
            this.btnMarcarUsada.Size = new System.Drawing.Size(170, 40);
            this.btnMarcarUsada.TabIndex = 1;
            this.btnMarcarUsada.Text = "✅ Marcar Usada";
            this.btnMarcarUsada.UseVisualStyleBackColor = false;
            this.btnMarcarUsada.Click += new System.EventHandler(this.btnMarcarUsada_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnEliminar.Enabled = false;
            this.btnEliminar.FlatAppearance.BorderSize = 0;
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.Location = new System.Drawing.Point(380, 10);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(170, 40);
            this.btnEliminar.TabIndex = 0;
            this.btnEliminar.Text = "🗑️ Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.pnlBottom.Controls.Add(this.lblTotalDonaciones);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 699);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1370, 50);
            this.pnlBottom.TabIndex = 2;
            // 
            // lblTotalDonaciones
            // 
            this.lblTotalDonaciones.AutoSize = true;
            this.lblTotalDonaciones.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalDonaciones.Location = new System.Drawing.Point(20, 15);
            this.lblTotalDonaciones.Name = "lblTotalDonaciones";
            this.lblTotalDonaciones.Size = new System.Drawing.Size(138, 19);
            this.lblTotalDonaciones.TabIndex = 0;
            this.lblTotalDonaciones.Text = "Total: 0 donaciones";
            // 
            // FrmDonaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 749);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlTop);
            this.MinimumSize = new System.Drawing.Size(1200, 700);
            this.Name = "FrmDonaciones";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Donaciones - Banco de Sangre";
            this.Load += new System.EventHandler(this.FrmDonaciones_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.grpNuevaDonacion.ResumeLayout(false);
            this.grpNuevaDonacion.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCantidadML)).EndInit();
            this.grpListaDonaciones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDonaciones)).EndInit();
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
    private System.Windows.Forms.GroupBox grpNuevaDonacion;
    private System.Windows.Forms.Button btnNuevaDonacion;
    private System.Windows.Forms.Label lblDonante;
    private System.Windows.Forms.ComboBox cmbDonante;
    private System.Windows.Forms.Button btnNuevoDonante;
    private System.Windows.Forms.ComboBox cmbTipoSangre;
    private System.Windows.Forms.Label lblTipoSangre;
    private System.Windows.Forms.NumericUpDown numCantidadML;
    private System.Windows.Forms.Label lblCantidadML;
    private System.Windows.Forms.DateTimePicker dtpFechaRecoleccion;
    private System.Windows.Forms.Label lblFechaRecoleccion;
    private System.Windows.Forms.Button btnGuardarDonacion;
    private System.Windows.Forms.Button btnCancelar;
    private System.Windows.Forms.GroupBox grpListaDonaciones;
    private System.Windows.Forms.DataGridView dgvDonaciones;
    private System.Windows.Forms.Panel pnlFiltros;
    private System.Windows.Forms.Button btnBuscar;
    private System.Windows.Forms.ComboBox cmbEstadoFiltro;
    private System.Windows.Forms.Label lblEstadoFiltro;
    private System.Windows.Forms.ComboBox cmbTipoSangreFiltro;
    private System.Windows.Forms.Label lblTipoSangreFiltro;
    private System.Windows.Forms.Panel pnlAcciones;
    private System.Windows.Forms.Button btnMarcarVencida;
    private System.Windows.Forms.Button btnMarcarUsada;
    private System.Windows.Forms.Button btnEliminar;
    private System.Windows.Forms.Panel pnlBottom;
    private System.Windows.Forms.Label lblTotalDonaciones;
}
}