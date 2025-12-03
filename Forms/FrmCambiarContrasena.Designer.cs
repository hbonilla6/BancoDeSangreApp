namespace BancoDeSangreApp.Forms
{
    partial class FrmCambiarContrasena
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
            this.panelSuperior = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panelPrincipal = new System.Windows.Forms.Panel();
            this.lblFortaleza = new System.Windows.Forms.Label();
            this.chkMostrarContrasenas = new System.Windows.Forms.CheckBox();
            this.lblUsuarioInfo = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.txtConfirmarContrasena = new System.Windows.Forms.TextBox();
            this.lblConfirmarContrasena = new System.Windows.Forms.Label();
            this.txtNuevaContrasena = new System.Windows.Forms.TextBox();
            this.lblNuevaContrasena = new System.Windows.Forms.Label();
            this.lblInstrucciones = new System.Windows.Forms.Label();
            this.panelSuperior.SuspendLayout();
            this.panelPrincipal.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSuperior
            // 
            this.panelSuperior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(156)))), ((int)(((byte)(18)))));
            this.panelSuperior.Controls.Add(this.lblTitulo);
            this.panelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSuperior.Location = new System.Drawing.Point(0, 0);
            this.panelSuperior.Name = "panelSuperior";
            this.panelSuperior.Size = new System.Drawing.Size(550, 60);
            this.panelSuperior.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(12, 15);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(286, 30);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "🔑 Cambiar Contraseña";
            // 
            // panelPrincipal
            // 
            this.panelPrincipal.BackColor = System.Drawing.Color.White;
            this.panelPrincipal.Controls.Add(this.lblFortaleza);
            this.panelPrincipal.Controls.Add(this.chkMostrarContrasenas);
            this.panelPrincipal.Controls.Add(this.lblUsuarioInfo);
            this.panelPrincipal.Controls.Add(this.btnCancelar);
            this.panelPrincipal.Controls.Add(this.btnGuardar);
            this.panelPrincipal.Controls.Add(this.txtConfirmarContrasena);
            this.panelPrincipal.Controls.Add(this.lblConfirmarContrasena);
            this.panelPrincipal.Controls.Add(this.txtNuevaContrasena);
            this.panelPrincipal.Controls.Add(this.lblNuevaContrasena);
            this.panelPrincipal.Controls.Add(this.lblInstrucciones);
            this.panelPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPrincipal.Location = new System.Drawing.Point(0, 60);
            this.panelPrincipal.Name = "panelPrincipal";
            this.panelPrincipal.Padding = new System.Windows.Forms.Padding(20);
            this.panelPrincipal.Size = new System.Drawing.Size(550, 390);
            this.panelPrincipal.TabIndex = 1;
            // 
            // lblFortaleza
            // 
            this.lblFortaleza.AutoSize = true;
            this.lblFortaleza.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFortaleza.ForeColor = System.Drawing.Color.Gray;
            this.lblFortaleza.Location = new System.Drawing.Point(40, 198);
            this.lblFortaleza.Name = "lblFortaleza";
            this.lblFortaleza.Size = new System.Drawing.Size(0, 15);
            this.lblFortaleza.TabIndex = 9;
            // 
            // chkMostrarContrasenas
            // 
            this.chkMostrarContrasenas.AutoSize = true;
            this.chkMostrarContrasenas.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkMostrarContrasenas.Location = new System.Drawing.Point(43, 280);
            this.chkMostrarContrasenas.Name = "chkMostrarContrasenas";
            this.chkMostrarContrasenas.Size = new System.Drawing.Size(139, 19);
            this.chkMostrarContrasenas.TabIndex = 8;
            this.chkMostrarContrasenas.Text = "Mostrar contraseñas";
            this.chkMostrarContrasenas.UseVisualStyleBackColor = true;
            this.chkMostrarContrasenas.CheckedChanged += new System.EventHandler(this.chkMostrarContrasenas_CheckedChanged);
            // 
            // lblUsuarioInfo
            // 
            this.lblUsuarioInfo.AutoSize = true;
            this.lblUsuarioInfo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblUsuarioInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblUsuarioInfo.Location = new System.Drawing.Point(40, 90);
            this.lblUsuarioInfo.Name = "lblUsuarioInfo";
            this.lblUsuarioInfo.Size = new System.Drawing.Size(68, 19);
            this.lblUsuarioInfo.TabIndex = 7;
            this.lblUsuarioInfo.Text = "Usuario: ";
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(280, 320);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(120, 40);
            this.btnCancelar.TabIndex = 6;
            this.btnCancelar.Text = "❌ Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(140, 320);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(120, 40);
            this.btnGuardar.TabIndex = 5;
            this.btnGuardar.Text = "💾 Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // txtConfirmarContrasena
            // 
            this.txtConfirmarContrasena.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtConfirmarContrasena.Location = new System.Drawing.Point(43, 245);
            this.txtConfirmarContrasena.MaxLength = 100;
            this.txtConfirmarContrasena.Name = "txtConfirmarContrasena";
            this.txtConfirmarContrasena.Size = new System.Drawing.Size(460, 27);
            this.txtConfirmarContrasena.TabIndex = 4;
            this.txtConfirmarContrasena.UseSystemPasswordChar = true;
            // 
            // lblConfirmarContrasena
            // 
            this.lblConfirmarContrasena.AutoSize = true;
            this.lblConfirmarContrasena.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblConfirmarContrasena.Location = new System.Drawing.Point(40, 220);
            this.lblConfirmarContrasena.Name = "lblConfirmarContrasena";
            this.lblConfirmarContrasena.Size = new System.Drawing.Size(169, 19);
            this.lblConfirmarContrasena.TabIndex = 3;
            this.lblConfirmarContrasena.Text = "Confirmar Contraseña:";
            // 
            // txtNuevaContrasena
            // 
            this.txtNuevaContrasena.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtNuevaContrasena.Location = new System.Drawing.Point(43, 165);
            this.txtNuevaContrasena.MaxLength = 100;
            this.txtNuevaContrasena.Name = "txtNuevaContrasena";
            this.txtNuevaContrasena.Size = new System.Drawing.Size(460, 27);
            this.txtNuevaContrasena.TabIndex = 2;
            this.txtNuevaContrasena.UseSystemPasswordChar = true;
            this.txtNuevaContrasena.TextChanged += new System.EventHandler(this.txtNuevaContrasena_TextChanged);
            // 
            // lblNuevaContrasena
            // 
            this.lblNuevaContrasena.AutoSize = true;
            this.lblNuevaContrasena.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblNuevaContrasena.Location = new System.Drawing.Point(40, 140);
            this.lblNuevaContrasena.Name = "lblNuevaContrasena";
            this.lblNuevaContrasena.Size = new System.Drawing.Size(146, 19);
            this.lblNuevaContrasena.TabIndex = 1;
            this.lblNuevaContrasena.Text = "Nueva Contraseña:";
            // 
            // lblInstrucciones
            // 
            this.lblInstrucciones.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblInstrucciones.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.lblInstrucciones.Location = new System.Drawing.Point(40, 30);
            this.lblInstrucciones.Name = "lblInstrucciones";
            this.lblInstrucciones.Size = new System.Drawing.Size(470, 50);
            this.lblInstrucciones.TabIndex = 0;
            this.lblInstrucciones.Text = "La contraseña debe tener al menos 8 caracteres e incluir: mayúsculas, minúsculas," +
    " números y caracteres especiales (!@#$%^&*).";
            // 
            // FrmCambiarContrasena
            // 
            this.AcceptButton = this.btnGuardar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 450);
            this.Controls.Add(this.panelPrincipal);
            this.Controls.Add(this.panelSuperior);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCambiarContrasena";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cambiar Contraseña";
            this.Load += new System.EventHandler(this.FrmCambiarContrasena_Load);
            this.panelSuperior.ResumeLayout(false);
            this.panelSuperior.PerformLayout();
            this.panelPrincipal.ResumeLayout(false);
            this.panelPrincipal.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel panelPrincipal;
        private System.Windows.Forms.Label lblInstrucciones;
        private System.Windows.Forms.TextBox txtNuevaContrasena;
        private System.Windows.Forms.Label lblNuevaContrasena;
        private System.Windows.Forms.TextBox txtConfirmarContrasena;
        private System.Windows.Forms.Label lblConfirmarContrasena;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblUsuarioInfo;
        private System.Windows.Forms.CheckBox chkMostrarContrasenas;
        private System.Windows.Forms.Label lblFortaleza;
    }
}