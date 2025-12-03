namespace BancoDeSangreApp.Forms
{
    partial class FrmUsuarios
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelSuperior = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panelBusqueda = new System.Windows.Forms.Panel();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.panelPrincipal = new System.Windows.Forms.Panel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.groupBoxLista = new System.Windows.Forms.GroupBox();
            this.panelPaginacion = new System.Windows.Forms.Panel();
            this.btnPaginaSiguiente = new System.Windows.Forms.Button();
            this.lblPaginacion = new System.Windows.Forms.Label();
            this.btnPaginaAnterior = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.dgvUsuarios = new System.Windows.Forms.DataGridView();
            this.groupBoxDatos = new System.Windows.Forms.GroupBox();
            this.chkMostrarContrasena = new System.Windows.Forms.CheckBox();
            this.clbRoles = new System.Windows.Forms.CheckedListBox();
            this.lblRoles = new System.Windows.Forms.Label();
            this.cmbEntidad = new System.Windows.Forms.ComboBox();
            this.lblEntidad = new System.Windows.Forms.Label();
            this.chkActivo = new System.Windows.Forms.CheckBox();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.lblTelefono = new System.Windows.Forms.Label();
            this.txtCorreo = new System.Windows.Forms.TextBox();
            this.lblCorreo = new System.Windows.Forms.Label();
            this.txtConfirmarContrasena = new System.Windows.Forms.TextBox();
            this.lblConfirmarContrasena = new System.Windows.Forms.Label();
            this.txtContrasena = new System.Windows.Forms.TextBox();
            this.lblContrasena = new System.Windows.Forms.Label();
            this.txtNombreCompleto = new System.Windows.Forms.TextBox();
            this.lblNombreCompleto = new System.Windows.Forms.Label();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.panelBotones = new System.Windows.Forms.Panel();
            this.btnCambiarContrasena = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.panelSuperior.SuspendLayout();
            this.panelBusqueda.SuspendLayout();
            this.panelPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.groupBoxLista.SuspendLayout();
            this.panelPaginacion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).BeginInit();
            this.groupBoxDatos.SuspendLayout();
            this.panelBotones.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSuperior
            // 
            this.panelSuperior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.panelSuperior.Controls.Add(this.lblTitulo);
            this.panelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSuperior.Location = new System.Drawing.Point(0, 0);
            this.panelSuperior.Name = "panelSuperior";
            this.panelSuperior.Size = new System.Drawing.Size(1400, 60);
            this.panelSuperior.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(12, 14);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(283, 32);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Gestión de Usuarios 👤";
            // 
            // panelBusqueda
            // 
            this.panelBusqueda.BackColor = System.Drawing.Color.White;
            this.panelBusqueda.Controls.Add(this.btnBuscar);
            this.panelBusqueda.Controls.Add(this.txtBuscar);
            this.panelBusqueda.Controls.Add(this.lblBuscar);
            this.panelBusqueda.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBusqueda.Location = new System.Drawing.Point(0, 60);
            this.panelBusqueda.Name = "panelBusqueda";
            this.panelBusqueda.Padding = new System.Windows.Forms.Padding(10);
            this.panelBusqueda.Size = new System.Drawing.Size(1400, 50);
            this.panelBusqueda.TabIndex = 1;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(420, 10);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(100, 30);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "🔍 Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBuscar.Location = new System.Drawing.Point(140, 12);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(270, 25);
            this.txtBuscar.TabIndex = 1;
            // 
            // lblBuscar
            // 
            this.lblBuscar.AutoSize = true;
            this.lblBuscar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblBuscar.Location = new System.Drawing.Point(13, 15);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(112, 19);
            this.lblBuscar.TabIndex = 0;
            this.lblBuscar.Text = "Buscar Usuario:";
            // 
            // panelPrincipal
            // 
            this.panelPrincipal.Controls.Add(this.splitContainer);
            this.panelPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPrincipal.Location = new System.Drawing.Point(0, 110);
            this.panelPrincipal.Name = "panelPrincipal";
            this.panelPrincipal.Padding = new System.Windows.Forms.Padding(10);
            this.panelPrincipal.Size = new System.Drawing.Size(1400, 639);
            this.panelPrincipal.TabIndex = 2;
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(10, 10);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.groupBoxLista);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.groupBoxDatos);
            this.splitContainer.Size = new System.Drawing.Size(1380, 619);
            this.splitContainer.SplitterDistance = 780;
            this.splitContainer.TabIndex = 0;
            // 
            // groupBoxLista
            // 
            this.groupBoxLista.Controls.Add(this.panelPaginacion);
            this.groupBoxLista.Controls.Add(this.lblTotal);
            this.groupBoxLista.Controls.Add(this.dgvUsuarios);
            this.groupBoxLista.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxLista.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.groupBoxLista.Location = new System.Drawing.Point(0, 0);
            this.groupBoxLista.Name = "groupBoxLista";
            this.groupBoxLista.Padding = new System.Windows.Forms.Padding(10);
            this.groupBoxLista.Size = new System.Drawing.Size(780, 619);
            this.groupBoxLista.TabIndex = 0;
            this.groupBoxLista.TabStop = false;
            this.groupBoxLista.Text = "Lista de Usuarios";
            // 
            // panelPaginacion
            // 
            this.panelPaginacion.Controls.Add(this.btnPaginaSiguiente);
            this.panelPaginacion.Controls.Add(this.lblPaginacion);
            this.panelPaginacion.Controls.Add(this.btnPaginaAnterior);
            this.panelPaginacion.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelPaginacion.Location = new System.Drawing.Point(10, 549);
            this.panelPaginacion.Name = "panelPaginacion";
            this.panelPaginacion.Size = new System.Drawing.Size(760, 40);
            this.panelPaginacion.TabIndex = 2;
            // 
            // btnPaginaSiguiente
            // 
            this.btnPaginaSiguiente.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPaginaSiguiente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnPaginaSiguiente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPaginaSiguiente.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnPaginaSiguiente.ForeColor = System.Drawing.Color.White;
            this.btnPaginaSiguiente.Location = new System.Drawing.Point(440, 5);
            this.btnPaginaSiguiente.Name = "btnPaginaSiguiente";
            this.btnPaginaSiguiente.Size = new System.Drawing.Size(100, 30);
            this.btnPaginaSiguiente.TabIndex = 2;
            this.btnPaginaSiguiente.Text = "Siguiente ▶";
            this.btnPaginaSiguiente.UseVisualStyleBackColor = false;
            this.btnPaginaSiguiente.Click += new System.EventHandler(this.btnPaginaSiguiente_Click);
            // 
            // lblPaginacion
            // 
            this.lblPaginacion.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblPaginacion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPaginacion.Location = new System.Drawing.Point(240, 10);
            this.lblPaginacion.Name = "lblPaginacion";
            this.lblPaginacion.Size = new System.Drawing.Size(180, 20);
            this.lblPaginacion.TabIndex = 1;
            this.lblPaginacion.Text = "Página 1 de 1";
            this.lblPaginacion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnPaginaAnterior
            // 
            this.btnPaginaAnterior.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPaginaAnterior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnPaginaAnterior.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPaginaAnterior.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnPaginaAnterior.ForeColor = System.Drawing.Color.White;
            this.btnPaginaAnterior.Location = new System.Drawing.Point(120, 5);
            this.btnPaginaAnterior.Name = "btnPaginaAnterior";
            this.btnPaginaAnterior.Size = new System.Drawing.Size(100, 30);
            this.btnPaginaAnterior.TabIndex = 0;
            this.btnPaginaAnterior.Text = "◀ Anterior";
            this.btnPaginaAnterior.UseVisualStyleBackColor = false;
            this.btnPaginaAnterior.Click += new System.EventHandler(this.btnPaginaAnterior_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTotal.Location = new System.Drawing.Point(10, 589);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Padding = new System.Windows.Forms.Padding(5);
            this.lblTotal.Size = new System.Drawing.Size(760, 20);
            this.lblTotal.TabIndex = 1;
            this.lblTotal.Text = "Total: 0 usuarios";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgvUsuarios
            // 
            this.dgvUsuarios.AllowUserToAddRows = false;
            this.dgvUsuarios.AllowUserToDeleteRows = false;
            this.dgvUsuarios.BackgroundColor = System.Drawing.Color.White;
            this.dgvUsuarios.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvUsuarios.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvUsuarios.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvUsuarios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUsuarios.EnableHeadersVisualStyles = false;
            this.dgvUsuarios.Location = new System.Drawing.Point(10, 28);
            this.dgvUsuarios.MultiSelect = false;
            this.dgvUsuarios.Name = "dgvUsuarios";
            this.dgvUsuarios.ReadOnly = true;
            this.dgvUsuarios.RowHeadersVisible = false;
            this.dgvUsuarios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsuarios.Size = new System.Drawing.Size(760, 581);
            this.dgvUsuarios.TabIndex = 0;
            this.dgvUsuarios.SelectionChanged += new System.EventHandler(this.dgvUsuarios_SelectionChanged);
            // 
            // groupBoxDatos
            // 
            this.groupBoxDatos.Controls.Add(this.chkMostrarContrasena);
            this.groupBoxDatos.Controls.Add(this.clbRoles);
            this.groupBoxDatos.Controls.Add(this.lblRoles);
            this.groupBoxDatos.Controls.Add(this.cmbEntidad);
            this.groupBoxDatos.Controls.Add(this.lblEntidad);
            this.groupBoxDatos.Controls.Add(this.chkActivo);
            this.groupBoxDatos.Controls.Add(this.txtTelefono);
            this.groupBoxDatos.Controls.Add(this.lblTelefono);
            this.groupBoxDatos.Controls.Add(this.txtCorreo);
            this.groupBoxDatos.Controls.Add(this.lblCorreo);
            this.groupBoxDatos.Controls.Add(this.txtConfirmarContrasena);
            this.groupBoxDatos.Controls.Add(this.lblConfirmarContrasena);
            this.groupBoxDatos.Controls.Add(this.txtContrasena);
            this.groupBoxDatos.Controls.Add(this.lblContrasena);
            this.groupBoxDatos.Controls.Add(this.txtNombreCompleto);
            this.groupBoxDatos.Controls.Add(this.lblNombreCompleto);
            this.groupBoxDatos.Controls.Add(this.txtUsuario);
            this.groupBoxDatos.Controls.Add(this.lblUsuario);
            this.groupBoxDatos.Controls.Add(this.panelBotones);
            this.groupBoxDatos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxDatos.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.groupBoxDatos.Location = new System.Drawing.Point(0, 0);
            this.groupBoxDatos.Name = "groupBoxDatos";
            this.groupBoxDatos.Padding = new System.Windows.Forms.Padding(10);
            this.groupBoxDatos.Size = new System.Drawing.Size(596, 619);
            this.groupBoxDatos.TabIndex = 0;
            this.groupBoxDatos.TabStop = false;
            this.groupBoxDatos.Text = "Datos del Usuario";
            // 
            // chkMostrarContrasena
            // 
            this.chkMostrarContrasena.AutoSize = true;
            this.chkMostrarContrasena.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkMostrarContrasena.Location = new System.Drawing.Point(160, 300);
            this.chkMostrarContrasena.Name = "chkMostrarContrasena";
            this.chkMostrarContrasena.Size = new System.Drawing.Size(133, 19);
            this.chkMostrarContrasena.TabIndex = 18;
            this.chkMostrarContrasena.Text = "Mostrar contraseñas";
            this.chkMostrarContrasena.UseVisualStyleBackColor = true;
            this.chkMostrarContrasena.CheckedChanged += new System.EventHandler(this.chkMostrarContrasena_CheckedChanged);
            // 
            // clbRoles
            // 
            this.clbRoles.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.clbRoles.FormattingEnabled = true;
            this.clbRoles.Location = new System.Drawing.Point(160, 420);
            this.clbRoles.Name = "clbRoles";
            this.clbRoles.Size = new System.Drawing.Size(400, 76);
            this.clbRoles.TabIndex = 17;
            // 
            // lblRoles
            // 
            this.lblRoles.AutoSize = true;
            this.lblRoles.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblRoles.Location = new System.Drawing.Point(20, 420);
            this.lblRoles.Name = "lblRoles";
            this.lblRoles.Size = new System.Drawing.Size(44, 19);
            this.lblRoles.TabIndex = 16;
            this.lblRoles.Text = "Roles:";
            // 
            // cmbEntidad
            // 
            this.cmbEntidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEntidad.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbEntidad.FormattingEnabled = true;
            this.cmbEntidad.Location = new System.Drawing.Point(160, 380);
            this.cmbEntidad.Name = "cmbEntidad";
            this.cmbEntidad.Size = new System.Drawing.Size(400, 25);
            this.cmbEntidad.TabIndex = 15;
            // 
            // lblEntidad
            // 
            this.lblEntidad.AutoSize = true;
            this.lblEntidad.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEntidad.Location = new System.Drawing.Point(20, 383);
            this.lblEntidad.Name = "lblEntidad";
            this.lblEntidad.Size = new System.Drawing.Size(58, 19);
            this.lblEntidad.TabIndex = 14;
            this.lblEntidad.Text = "Entidad:";
            // 
            // chkActivo
            // 
            this.chkActivo.AutoSize = true;
            this.chkActivo.Checked = true;
            this.chkActivo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActivo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkActivo.Location = new System.Drawing.Point(160, 515);
            this.chkActivo.Name = "chkActivo";
            this.chkActivo.Size = new System.Drawing.Size(66, 23);
            this.chkActivo.TabIndex = 13;
            this.chkActivo.Text = "Activo";
            this.chkActivo.UseVisualStyleBackColor = true;
            // 
            // txtTelefono
            // 
            this.txtTelefono.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTelefono.Location = new System.Drawing.Point(160, 345);
            this.txtTelefono.MaxLength = 20;
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(400, 25);
            this.txtTelefono.TabIndex = 12;
            // 
            // lblTelefono
            // 
            this.lblTelefono.AutoSize = true;
            this.lblTelefono.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTelefono.Location = new System.Drawing.Point(20, 348);
            this.lblTelefono.Name = "lblTelefono";
            this.lblTelefono.Size = new System.Drawing.Size(63, 19);
            this.lblTelefono.TabIndex = 11;
            this.lblTelefono.Text = "Teléfono:";
            // 
            // txtCorreo
            // 
            this.txtCorreo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCorreo.Location = new System.Drawing.Point(160, 170);
            this.txtCorreo.MaxLength = 100;
            this.txtCorreo.Name = "txtCorreo";
            this.txtCorreo.Size = new System.Drawing.Size(400, 25);
            this.txtCorreo.TabIndex = 10;
            // 
            // lblCorreo
            // 
            this.lblCorreo.AutoSize = true;
            this.lblCorreo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCorreo.Location = new System.Drawing.Point(20, 173);
            this.lblCorreo.Name = "lblCorreo";
            this.lblCorreo.Size = new System.Drawing.Size(54, 19);
            this.lblCorreo.TabIndex = 9;
            this.lblCorreo.Text = "Correo:";
            // 
            // txtConfirmarContrasena
            // 
            this.txtConfirmarContrasena.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtConfirmarContrasena.Location = new System.Drawing.Point(160, 265);
            this.txtConfirmarContrasena.MaxLength = 100;
            this.txtConfirmarContrasena.Name = "txtConfirmarContrasena";
            this.txtConfirmarContrasena.Size = new System.Drawing.Size(400, 25);
            this.txtConfirmarContrasena.TabIndex = 8;
            this.txtConfirmarContrasena.UseSystemPasswordChar = true;
            // 
            // lblConfirmarContrasena
            // 
            this.lblConfirmarContrasena.AutoSize = true;
            this.lblConfirmarContrasena.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblConfirmarContrasena.Location = new System.Drawing.Point(20, 268);
            this.lblConfirmarContrasena.Name = "lblConfirmarContrasena";
            this.lblConfirmarContrasena.Size = new System.Drawing.Size(73, 19);
            this.lblConfirmarContrasena.TabIndex = 7;
            this.lblConfirmarContrasena.Text = "Confirmar:";
            // 
            // txtContrasena
            // 
            this.txtContrasena.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtContrasena.Location = new System.Drawing.Point(160, 230);
            this.txtContrasena.MaxLength = 100;
            this.txtContrasena.Name = "txtContrasena";
            this.txtContrasena.Size = new System.Drawing.Size(400, 25);
            this.txtContrasena.TabIndex = 6;
            this.txtContrasena.UseSystemPasswordChar = true;
            // 
            // lblContrasena
            // 
            this.lblContrasena.AutoSize = true;
            this.lblContrasena.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblContrasena.Location = new System.Drawing.Point(20, 233);
            this.lblContrasena.Name = "lblContrasena";
            this.lblContrasena.Size = new System.Drawing.Size(82, 19);
            this.lblContrasena.TabIndex = 5;
            this.lblContrasena.Text = "Contraseña:";
            // 
            // txtNombreCompleto
            // 
            this.txtNombreCompleto.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNombreCompleto.Location = new System.Drawing.Point(160, 135);
            this.txtNombreCompleto.MaxLength = 100;
            this.txtNombreCompleto.Name = "txtNombreCompleto";
            this.txtNombreCompleto.Size = new System.Drawing.Size(400, 25);
            this.txtNombreCompleto.TabIndex = 4;
            // 
            // lblNombreCompleto
            // 
            this.lblNombreCompleto.AutoSize = true;
            this.lblNombreCompleto.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNombreCompleto.Location = new System.Drawing.Point(20, 138);
            this.lblNombreCompleto.Name = "lblNombreCompleto";
            this.lblNombreCompleto.Size = new System.Drawing.Size(126, 19);
            this.lblNombreCompleto.TabIndex = 3;
            this.lblNombreCompleto.Text = "Nombre Completo:";
            // 
            // txtUsuario
            // 
            this.txtUsuario.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtUsuario.Location = new System.Drawing.Point(160, 100);
            this.txtUsuario.MaxLength = 50;
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(400, 25);
            this.txtUsuario.TabIndex = 2;
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblUsuario.Location = new System.Drawing.Point(20, 103);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(59, 19);
            this.lblUsuario.TabIndex = 1;
            this.lblUsuario.Text = "Usuario:";
            // 
            // panelBotones
            // 
            this.panelBotones.Controls.Add(this.btnCambiarContrasena);
            this.panelBotones.Controls.Add(this.btnCancelar);
            this.panelBotones.Controls.Add(this.btnEliminar);
            this.panelBotones.Controls.Add(this.btnGuardar);
            this.panelBotones.Controls.Add(this.btnEditar);
            this.panelBotones.Controls.Add(this.btnNuevo);
            this.panelBotones.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBotones.Location = new System.Drawing.Point(10, 28);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(576, 60);
            this.panelBotones.TabIndex = 0;
            // 
            // btnCambiarContrasena
            // 
            this.btnCambiarContrasena.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(156)))), ((int)(((byte)(18)))));
            this.btnCambiarContrasena.Enabled = false;
            this.btnCambiarContrasena.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCambiarContrasena.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCambiarContrasena.ForeColor = System.Drawing.Color.White;
            this.btnCambiarContrasena.Location = new System.Drawing.Point(285, 10);
            this.btnCambiarContrasena.Name = "btnCambiarContrasena";
            this.btnCambiarContrasena.Size = new System.Drawing.Size(85, 40);
            this.btnCambiarContrasena.TabIndex = 5;
            this.btnCambiarContrasena.Text = "🔑 Cambiar Clave";
            this.btnCambiarContrasena.UseVisualStyleBackColor = false;
            this.btnCambiarContrasena.Click += new System.EventHandler(this.btnCambiarContrasena_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnCancelar.Enabled = false;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(475, 10);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(85, 40);
            this.btnCancelar.TabIndex = 4;
            this.btnCancelar.Text = "❌ Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnEliminar.Enabled = false;
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.Location = new System.Drawing.Point(190, 10);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(85, 40);
            this.btnEliminar.TabIndex = 3;
            this.btnEliminar.Text = "🗑️ Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.btnGuardar.Enabled = false;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(380, 10);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(85, 40);
            this.btnGuardar.TabIndex = 2;
            this.btnGuardar.Text = "💾 Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(196)))), ((int)(((byte)(15)))));
            this.btnEditar.Enabled = false;
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEditar.ForeColor = System.Drawing.Color.White;
            this.btnEditar.Location = new System.Drawing.Point(95, 10);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(85, 40);
            this.btnEditar.TabIndex = 1;
            this.btnEditar.Text = "✏️ Editar";
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnNuevo.ForeColor = System.Drawing.Color.White;
            this.btnNuevo.Location = new System.Drawing.Point(0, 10);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(85, 40);
            this.btnNuevo.TabIndex = 0;
            this.btnNuevo.Text = "➕ Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // FrmUsuarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(1400, 749);
            this.Controls.Add(this.panelPrincipal);
            this.Controls.Add(this.panelBusqueda);
            this.Controls.Add(this.panelSuperior);
            this.MinimumSize = new System.Drawing.Size(1200, 700);
            this.Name = "FrmUsuarios";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestión de Usuarios";
            this.Load += new System.EventHandler(this.Frmusuarios_Load);
            this.panelSuperior.ResumeLayout(false);
            this.panelSuperior.PerformLayout();
            this.panelBusqueda.ResumeLayout(false);
            this.panelBusqueda.PerformLayout();
            this.panelPrincipal.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.groupBoxLista.ResumeLayout(false);
            this.panelPaginacion.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).EndInit();
            this.groupBoxDatos.ResumeLayout(false);
            this.groupBoxDatos.PerformLayout();
            this.panelBotones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel panelBusqueda;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.Panel panelPrincipal;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.GroupBox groupBoxLista;
        private System.Windows.Forms.DataGridView dgvUsuarios;
        private System.Windows.Forms.GroupBox groupBoxDatos;
        private System.Windows.Forms.Panel panelBotones;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.TextBox txtNombreCompleto;
        private System.Windows.Forms.Label lblNombreCompleto;
        private System.Windows.Forms.TextBox txtContrasena;
        private System.Windows.Forms.Label lblContrasena;
        private System.Windows.Forms.TextBox txtConfirmarContrasena;
        private System.Windows.Forms.Label lblConfirmarContrasena;
        private System.Windows.Forms.TextBox txtCorreo;
        private System.Windows.Forms.Label lblCorreo;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.CheckBox chkActivo;
        private System.Windows.Forms.ComboBox cmbEntidad;
        private System.Windows.Forms.Label lblEntidad;
        private System.Windows.Forms.CheckedListBox clbRoles;
        private System.Windows.Forms.Label lblRoles;
        private System.Windows.Forms.CheckBox chkMostrarContrasena;
        private System.Windows.Forms.Button btnCambiarContrasena;
        private System.Windows.Forms.Panel panelPaginacion;
        private System.Windows.Forms.Button btnPaginaAnterior;
        private System.Windows.Forms.Button btnPaginaSiguiente;
        private System.Windows.Forms.Label lblPaginacion;
    }
}