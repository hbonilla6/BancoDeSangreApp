namespace BancoDeSangreApp.Forms
{
    partial class FrmUsuarioDetalle
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
            this.panelSuperior = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panelContenido = new System.Windows.Forms.Panel();
            this.groupDatosPersonales = new System.Windows.Forms.GroupBox();
            this.txtNombreCompleto = new System.Windows.Forms.TextBox();
            this.lblNombreCompleto = new System.Windows.Forms.Label();
            this.txtCorreo = new System.Windows.Forms.TextBox();
            this.lblCorreo = new System.Windows.Forms.Label();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.lblTelefono = new System.Windows.Forms.Label();
            this.groupCredenciales = new System.Windows.Forms.GroupBox();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.txtContrasena = new System.Windows.Forms.TextBox();
            this.lblContrasena = new System.Windows.Forms.Label();
            this.txtConfirmarContrasena = new System.Windows.Forms.TextBox();
            this.lblConfirmarContrasena = new System.Windows.Forms.Label();
            this.chkMostrarContrasenas = new System.Windows.Forms.CheckBox();
            this.lblFortaleza = new System.Windows.Forms.Label();
            this.groupRoles = new System.Windows.Forms.GroupBox();
            this.chkListRoles = new System.Windows.Forms.CheckedListBox();
            this.panelBotones = new System.Windows.Forms.Panel();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.panelSuperior.SuspendLayout();
            this.panelContenido.SuspendLayout();
            this.groupDatosPersonales.SuspendLayout();
            this.groupCredenciales.SuspendLayout();
            this.groupRoles.SuspendLayout();
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
            this.panelSuperior.Size = new System.Drawing.Size(700, 60);
            this.panelSuperior.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(20, 15);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(181, 30);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "📝 Nuevo Usuario";
            // 
            // panelContenido
            // 
            this.panelContenido.AutoScroll = true;
            this.panelContenido.BackColor = System.Drawing.Color.White;
            this.panelContenido.Controls.Add(this.groupRoles);
            this.panelContenido.Controls.Add(this.groupCredenciales);
            this.panelContenido.Controls.Add(this.groupDatosPersonales);
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenido.Location = new System.Drawing.Point(0, 60);
            this.panelContenido.Name = "panelContenido";
            this.panelContenido.Padding = new System.Windows.Forms.Padding(20);
            this.panelContenido.Size = new System.Drawing.Size(700, 540);
            this.panelContenido.TabIndex = 1;
            // 
            // groupDatosPersonales
            // 
            this.groupDatosPersonales.Controls.Add(this.lblNombreCompleto);
            this.groupDatosPersonales.Controls.Add(this.txtNombreCompleto);
            this.groupDatosPersonales.Controls.Add(this.lblCorreo);
            this.groupDatosPersonales.Controls.Add(this.txtCorreo);
            this.groupDatosPersonales.Controls.Add(this.lblTelefono);
            this.groupDatosPersonales.Controls.Add(this.txtTelefono);
            this.groupDatosPersonales.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.groupDatosPersonales.Location = new System.Drawing.Point(23, 23);
            this.groupDatosPersonales.Name = "groupDatosPersonales";
            this.groupDatosPersonales.Size = new System.Drawing.Size(640, 140);
            this.groupDatosPersonales.TabIndex = 0;
            this.groupDatosPersonales.TabStop = false;
            this.groupDatosPersonales.Text = "Datos Personales";
            // 
            // lblNombreCompleto
            // 
            this.lblNombreCompleto.AutoSize = true;
            this.lblNombreCompleto.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNombreCompleto.Location = new System.Drawing.Point(15, 30);
            this.lblNombreCompleto.Name = "lblNombreCompleto";
            this.lblNombreCompleto.Size = new System.Drawing.Size(116, 15);
            this.lblNombreCompleto.TabIndex = 0;
            this.lblNombreCompleto.Text = "Nombre Completo *";
            // 
            // txtNombreCompleto
            // 
            this.txtNombreCompleto.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtNombreCompleto.Location = new System.Drawing.Point(18, 48);
            this.txtNombreCompleto.Name = "txtNombreCompleto";
            this.txtNombreCompleto.Size = new System.Drawing.Size(600, 23);
            this.txtNombreCompleto.TabIndex = 1;
            // 
            // lblCorreo
            // 
            this.lblCorreo.AutoSize = true;
            this.lblCorreo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCorreo.Location = new System.Drawing.Point(15, 80);
            this.lblCorreo.Name = "lblCorreo";
            this.lblCorreo.Size = new System.Drawing.Size(105, 15);
            this.lblCorreo.TabIndex = 2;
            this.lblCorreo.Text = "Correo Electrónico";
            // 
            // txtCorreo
            // 
            this.txtCorreo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtCorreo.Location = new System.Drawing.Point(18, 98);
            this.txtCorreo.Name = "txtCorreo";
            this.txtCorreo.Size = new System.Drawing.Size(400, 23);
            this.txtCorreo.TabIndex = 3;
            // 
            // lblTelefono
            // 
            this.lblTelefono.AutoSize = true;
            this.lblTelefono.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTelefono.Location = new System.Drawing.Point(430, 80);
            this.lblTelefono.Name = "lblTelefono";
            this.lblTelefono.Size = new System.Drawing.Size(52, 15);
            this.lblTelefono.TabIndex = 4;
            this.lblTelefono.Text = "Teléfono";
            // 
            // txtTelefono
            // 
            this.txtTelefono.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTelefono.Location = new System.Drawing.Point(433, 98);
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(185, 23);
            this.txtTelefono.TabIndex = 5;
            // 
            // groupCredenciales
            // 
            this.groupCredenciales.Controls.Add(this.lblUsuario);
            this.groupCredenciales.Controls.Add(this.txtUsuario);
            this.groupCredenciales.Controls.Add(this.lblContrasena);
            this.groupCredenciales.Controls.Add(this.txtContrasena);
            this.groupCredenciales.Controls.Add(this.lblFortaleza);
            this.groupCredenciales.Controls.Add(this.lblConfirmarContrasena);
            this.groupCredenciales.Controls.Add(this.txtConfirmarContrasena);
            this.groupCredenciales.Controls.Add(this.chkMostrarContrasenas);
            this.groupCredenciales.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.groupCredenciales.Location = new System.Drawing.Point(23, 175);
            this.groupCredenciales.Name = "groupCredenciales";
            this.groupCredenciales.Size = new System.Drawing.Size(640, 170);
            this.groupCredenciales.TabIndex = 1;
            this.groupCredenciales.TabStop = false;
            this.groupCredenciales.Text = "Credenciales de Acceso";
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUsuario.Location = new System.Drawing.Point(15, 30);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(107, 15);
            this.lblUsuario.TabIndex = 0;
            this.lblUsuario.Text = "Nombre Usuario *";
            // 
            // txtUsuario
            // 
            this.txtUsuario.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtUsuario.Location = new System.Drawing.Point(18, 48);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(250, 23);
            this.txtUsuario.TabIndex = 1;
            // 
            // lblContrasena
            // 
            this.lblContrasena.AutoSize = true;
            this.lblContrasena.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblContrasena.Location = new System.Drawing.Point(15, 80);
            this.lblContrasena.Name = "lblContrasena";
            this.lblContrasena.Size = new System.Drawing.Size(76, 15);
            this.lblContrasena.TabIndex = 2;
            this.lblContrasena.Text = "Contraseña *";
            // 
            // txtContrasena
            // 
            this.txtContrasena.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtContrasena.Location = new System.Drawing.Point(18, 98);
            this.txtContrasena.Name = "txtContrasena";
            this.txtContrasena.Size = new System.Drawing.Size(250, 23);
            this.txtContrasena.TabIndex = 3;
            this.txtContrasena.UseSystemPasswordChar = true;
            this.txtContrasena.TextChanged += new System.EventHandler(this.txtContrasena_TextChanged);
            // 
            // lblFortaleza
            // 
            this.lblFortaleza.AutoSize = true;
            this.lblFortaleza.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFortaleza.Location = new System.Drawing.Point(280, 101);
            this.lblFortaleza.Name = "lblFortaleza";
            this.lblFortaleza.Size = new System.Drawing.Size(0, 15);
            this.lblFortaleza.TabIndex = 4;
            // 
            // lblConfirmarContrasena
            // 
            this.lblConfirmarContrasena.AutoSize = true;
            this.lblConfirmarContrasena.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblConfirmarContrasena.Location = new System.Drawing.Point(340, 80);
            this.lblConfirmarContrasena.Name = "lblConfirmarContrasena";
            this.lblConfirmarContrasena.Size = new System.Drawing.Size(131, 15);
            this.lblConfirmarContrasena.TabIndex = 5;
            this.lblConfirmarContrasena.Text = "Confirmar Contraseña *";
            // 
            // txtConfirmarContrasena
            // 
            this.txtConfirmarContrasena.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtConfirmarContrasena.Location = new System.Drawing.Point(343, 98);
            this.txtConfirmarContrasena.Name = "txtConfirmarContrasena";
            this.txtConfirmarContrasena.Size = new System.Drawing.Size(275, 23);
            this.txtConfirmarContrasena.TabIndex = 6;
            this.txtConfirmarContrasena.UseSystemPasswordChar = true;
            // 
            // chkMostrarContrasenas
            // 
            this.chkMostrarContrasenas.AutoSize = true;
            this.chkMostrarContrasenas.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkMostrarContrasenas.Location = new System.Drawing.Point(18, 135);
            this.chkMostrarContrasenas.Name = "chkMostrarContrasenas";
            this.chkMostrarContrasenas.Size = new System.Drawing.Size(139, 19);
            this.chkMostrarContrasenas.TabIndex = 7;
            this.chkMostrarContrasenas.Text = "👁️ Mostrar contraseñas";
            this.chkMostrarContrasenas.UseVisualStyleBackColor = true;
            this.chkMostrarContrasenas.CheckedChanged += new System.EventHandler(this.chkMostrarContrasenas_CheckedChanged);
            // 
            // groupRoles
            // 
            this.groupRoles.Controls.Add(this.chkListRoles);
            this.groupRoles.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.groupRoles.Location = new System.Drawing.Point(23, 360);
            this.groupRoles.Name = "groupRoles";
            this.groupRoles.Size = new System.Drawing.Size(640, 120);
            this.groupRoles.TabIndex = 2;
            this.groupRoles.TabStop = false;
            this.groupRoles.Text = "Roles *";
            // 
            // chkListRoles
            // 
            this.chkListRoles.CheckOnClick = true;
            this.chkListRoles.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkListRoles.FormattingEnabled = true;
            this.chkListRoles.Location = new System.Drawing.Point(18, 25);
            this.chkListRoles.Name = "chkListRoles";
            this.chkListRoles.Size = new System.Drawing.Size(600, 76);
            this.chkListRoles.TabIndex = 0;
            // 
            // panelBotones
            // 
            this.panelBotones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.panelBotones.Controls.Add(this.btnCancelar);
            this.panelBotones.Controls.Add(this.btnGuardar);
            this.panelBotones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBotones.Location = new System.Drawing.Point(0, 600);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(700, 70);
            this.panelBotones.TabIndex = 2;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(455, 15);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(110, 40);
            this.btnGuardar.TabIndex = 0;
            this.btnGuardar.Text = "💾 Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(575, 15);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(110, 40);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "❌ Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // FrmUsuarioDetalle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 670);
            this.Controls.Add(this.panelContenido);
            this.Controls.Add(this.panelBotones);
            this.Controls.Add(this.panelSuperior);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmUsuarioDetalle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Detalle de Usuario";
            this.Load += new System.EventHandler(this.FrmUsuarioDetalle_Load);
            this.panelSuperior.ResumeLayout(false);
            this.panelSuperior.PerformLayout();
            this.panelContenido.ResumeLayout(false);
            this.groupDatosPersonales.ResumeLayout(false);
            this.groupDatosPersonales.PerformLayout();
            this.groupCredenciales.ResumeLayout(false);
            this.groupCredenciales.PerformLayout();
            this.groupRoles.ResumeLayout(false);
            this.panelBotones.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel panelContenido;
        private System.Windows.Forms.GroupBox groupDatosPersonales;
        private System.Windows.Forms.Label lblNombreCompleto;
        private System.Windows.Forms.TextBox txtNombreCompleto;
        private System.Windows.Forms.Label lblCorreo;
        private System.Windows.Forms.TextBox txtCorreo;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.GroupBox groupCredenciales;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label lblContrasena;
        private System.Windows.Forms.TextBox txtContrasena;
        private System.Windows.Forms.Label lblFortaleza;
        private System.Windows.Forms.Label lblConfirmarContrasena;
        private System.Windows.Forms.TextBox txtConfirmarContrasena;
        private System.Windows.Forms.CheckBox chkMostrarContrasenas;
        private System.Windows.Forms.GroupBox groupRoles;
        private System.Windows.Forms.CheckedListBox chkListRoles;
        private System.Windows.Forms.Panel panelBotones;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
    }
}