namespace BancoDeSangreApp.Forms
{
    partial class FrmRolDetalle
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
            this.groupDatosRol = new System.Windows.Forms.GroupBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblContador = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.lblContadorDesc = new System.Windows.Forms.Label();
            this.lblNivelAcceso = new System.Windows.Forms.Label();
            this.cmbNivelAcceso = new System.Windows.Forms.ComboBox();
            this.lblInfoNivel = new System.Windows.Forms.Label();
            this.panelBotones = new System.Windows.Forms.Panel();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.panelSuperior.SuspendLayout();
            this.panelContenido.SuspendLayout();
            this.groupDatosRol.SuspendLayout();
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
            this.panelSuperior.Size = new System.Drawing.Size(600, 60);
            this.panelSuperior.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(20, 15);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(142, 30);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "📝 Nuevo Rol";
            // 
            // panelContenido
            // 
            this.panelContenido.BackColor = System.Drawing.Color.White;
            this.panelContenido.Controls.Add(this.groupDatosRol);
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenido.Location = new System.Drawing.Point(0, 60);
            this.panelContenido.Name = "panelContenido";
            this.panelContenido.Padding = new System.Windows.Forms.Padding(20);
            this.panelContenido.Size = new System.Drawing.Size(600, 340);
            this.panelContenido.TabIndex = 1;
            // 
            // groupDatosRol
            // 
            this.groupDatosRol.Controls.Add(this.lblInfoNivel);
            this.groupDatosRol.Controls.Add(this.cmbNivelAcceso);
            this.groupDatosRol.Controls.Add(this.lblNivelAcceso);
            this.groupDatosRol.Controls.Add(this.lblContadorDesc);
            this.groupDatosRol.Controls.Add(this.txtDescripcion);
            this.groupDatosRol.Controls.Add(this.lblDescripcion);
            this.groupDatosRol.Controls.Add(this.lblContador);
            this.groupDatosRol.Controls.Add(this.txtNombre);
            this.groupDatosRol.Controls.Add(this.lblNombre);
            this.groupDatosRol.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.groupDatosRol.Location = new System.Drawing.Point(23, 23);
            this.groupDatosRol.Name = "groupDatosRol";
            this.groupDatosRol.Size = new System.Drawing.Size(554, 294);
            this.groupDatosRol.TabIndex = 0;
            this.groupDatosRol.TabStop = false;
            this.groupDatosRol.Text = "Información del Rol";
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNombre.Location = new System.Drawing.Point(15, 30);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(97, 15);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "Nombre del Rol *";
            // 
            // txtNombre
            // 
            this.txtNombre.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtNombre.Location = new System.Drawing.Point(18, 48);
            this.txtNombre.MaxLength = 50;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(400, 23);
            this.txtNombre.TabIndex = 1;
            this.txtNombre.TextChanged += new System.EventHandler(this.txtNombre_TextChanged);
            // 
            // lblContador
            // 
            this.lblContador.AutoSize = true;
            this.lblContador.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblContador.ForeColor = System.Drawing.Color.Gray;
            this.lblContador.Location = new System.Drawing.Point(424, 51);
            this.lblContador.Name = "lblContador";
            this.lblContador.Size = new System.Drawing.Size(30, 13);
            this.lblContador.TabIndex = 2;
            this.lblContador.Text = "0/50";
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDescripcion.Location = new System.Drawing.Point(15, 85);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(69, 15);
            this.lblDescripcion.TabIndex = 3;
            this.lblDescripcion.Text = "Descripción";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDescripcion.Location = new System.Drawing.Point(18, 103);
            this.txtDescripcion.MaxLength = 200;
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescripcion.Size = new System.Drawing.Size(515, 60);
            this.txtDescripcion.TabIndex = 4;
            this.txtDescripcion.TextChanged += new System.EventHandler(this.txtDescripcion_TextChanged);
            // 
            // lblContadorDesc
            // 
            this.lblContadorDesc.AutoSize = true;
            this.lblContadorDesc.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblContadorDesc.ForeColor = System.Drawing.Color.Gray;
            this.lblContadorDesc.Location = new System.Drawing.Point(18, 166);
            this.lblContadorDesc.Name = "lblContadorDesc";
            this.lblContadorDesc.Size = new System.Drawing.Size(38, 13);
            this.lblContadorDesc.TabIndex = 5;
            this.lblContadorDesc.Text = "0/200";
            // 
            // lblNivelAcceso
            // 
            this.lblNivelAcceso.AutoSize = true;
            this.lblNivelAcceso.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNivelAcceso.Location = new System.Drawing.Point(15, 195);
            this.lblNivelAcceso.Name = "lblNivelAcceso";
            this.lblNivelAcceso.Size = new System.Drawing.Size(96, 15);
            this.lblNivelAcceso.TabIndex = 6;
            this.lblNivelAcceso.Text = "Nivel de Acceso *";
            // 
            // cmbNivelAcceso
            // 
            this.cmbNivelAcceso.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNivelAcceso.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbNivelAcceso.FormattingEnabled = true;
            this.cmbNivelAcceso.Location = new System.Drawing.Point(18, 213);
            this.cmbNivelAcceso.Name = "cmbNivelAcceso";
            this.cmbNivelAcceso.Size = new System.Drawing.Size(250, 23);
            this.cmbNivelAcceso.TabIndex = 7;
            this.cmbNivelAcceso.SelectedIndexChanged += new System.EventHandler(this.cmbNivelAcceso_SelectedIndexChanged);
            // 
            // lblInfoNivel
            // 
            this.lblInfoNivel.AutoSize = true;
            this.lblInfoNivel.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblInfoNivel.ForeColor = System.Drawing.Color.Gray;
            this.lblInfoNivel.Location = new System.Drawing.Point(18, 245);
            this.lblInfoNivel.Name = "lblInfoNivel";
            this.lblInfoNivel.Size = new System.Drawing.Size(0, 15);
            this.lblInfoNivel.TabIndex = 8;
            // 
            // panelBotones
            // 
            this.panelBotones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.panelBotones.Controls.Add(this.btnCancelar);
            this.panelBotones.Controls.Add(this.btnGuardar);
            this.panelBotones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBotones.Location = new System.Drawing.Point(0, 400);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(600, 70);
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
            this.btnGuardar.Location = new System.Drawing.Point(355, 15);
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
            this.btnCancelar.Location = new System.Drawing.Point(475, 15);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(110, 40);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "❌ Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // FrmRolDetalle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 470);
            this.Controls.Add(this.panelContenido);
            this.Controls.Add(this.panelBotones);
            this.Controls.Add(this.panelSuperior);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRolDetalle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Detalle de Rol";
            this.Load += new System.EventHandler(this.FrmRolDetalle_Load);
            this.panelSuperior.ResumeLayout(false);
            this.panelSuperior.PerformLayout();
            this.panelContenido.ResumeLayout(false);
            this.groupDatosRol.ResumeLayout(false);
            this.groupDatosRol.PerformLayout();
            this.panelBotones.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel panelContenido;
        private System.Windows.Forms.GroupBox groupDatosRol;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblContador;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label lblContadorDesc;
        private System.Windows.Forms.Label lblNivelAcceso;
        private System.Windows.Forms.ComboBox cmbNivelAcceso;
        private System.Windows.Forms.Label lblInfoNivel;
        private System.Windows.Forms.Panel panelBotones;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
    }
}