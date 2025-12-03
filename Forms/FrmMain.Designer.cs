namespace BancoDeSangreApp.Forms
{
    partial class FrmMain
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
            this.panelMenu = new System.Windows.Forms.Panel();
            this.btnCerrarSesion = new System.Windows.Forms.Button();
            this.panelMenuScroll = new System.Windows.Forms.Panel(); // <-- NUEVO PANEL
            this.panelUsuario = new System.Windows.Forms.Panel();
            this.lblEntidad = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.panelContenedor = new System.Windows.Forms.Panel();
            this.panelMenu.SuspendLayout();
            this.panelUsuario.SuspendLayout();
            this.SuspendLayout();

            // panelMenu
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(46)))), ((int)(((byte)(59)))));
            this.panelMenu.Controls.Add(this.btnCerrarSesion);
            this.panelMenu.Controls.Add(this.panelMenuScroll); // <-- AGREGAR
            this.panelMenu.Controls.Add(this.panelUsuario);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(250, 700);
            this.panelMenu.TabIndex = 0;

            // panelMenuScroll - NUEVO
            this.panelMenuScroll.AutoScroll = true;
            this.panelMenuScroll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(46)))), ((int)(((byte)(59)))));
            this.panelMenuScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMenuScroll.Location = new System.Drawing.Point(0, 145);
            this.panelMenuScroll.Name = "panelMenuScroll";
            this.panelMenuScroll.Size = new System.Drawing.Size(250, 505);
            this.panelMenuScroll.TabIndex = 9;

            // btnCerrarSesion
            this.btnCerrarSesion.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnCerrarSesion.FlatAppearance.BorderSize = 0;
            this.btnCerrarSesion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrarSesion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCerrarSesion.ForeColor = System.Drawing.Color.White;
            this.btnCerrarSesion.Location = new System.Drawing.Point(0, 650);
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnCerrarSesion.Size = new System.Drawing.Size(250, 50);
            this.btnCerrarSesion.TabIndex = 8;
            this.btnCerrarSesion.Text = "🚪  Cerrar Sesión";
            this.btnCerrarSesion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCerrarSesion.UseVisualStyleBackColor = true;
            this.btnCerrarSesion.Click += new System.EventHandler(this.btnCerrarSesion_Click);

            // panelUsuario
            this.panelUsuario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.panelUsuario.Controls.Add(this.lblEntidad);
            this.panelUsuario.Controls.Add(this.lblUsuario);
            this.panelUsuario.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelUsuario.Location = new System.Drawing.Point(0, 0);
            this.panelUsuario.Name = "panelUsuario";
            this.panelUsuario.Padding = new System.Windows.Forms.Padding(15, 20, 15, 20);
            this.panelUsuario.Size = new System.Drawing.Size(250, 145);
            this.panelUsuario.TabIndex = 0;

            // lblEntidad
            this.lblEntidad.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblEntidad.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lblEntidad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.lblEntidad.Location = new System.Drawing.Point(15, 59);
            this.lblEntidad.Name = "lblEntidad";
            this.lblEntidad.Size = new System.Drawing.Size(220, 40);
            this.lblEntidad.TabIndex = 1;
            this.lblEntidad.Text = "🏥 Entidad";
            this.lblEntidad.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // lblUsuario
            this.lblUsuario.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblUsuario.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblUsuario.ForeColor = System.Drawing.Color.White;
            this.lblUsuario.Location = new System.Drawing.Point(15, 20);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(220, 39);
            this.lblUsuario.TabIndex = 0;
            this.lblUsuario.Text = "👤 Usuario";
            this.lblUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // panelContenedor
            this.panelContenedor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.panelContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenedor.Location = new System.Drawing.Point(250, 0);
            this.panelContenedor.Name = "panelContenedor";
            this.panelContenedor.Size = new System.Drawing.Size(1050, 700);
            this.panelContenedor.TabIndex = 1;

            // FrmMain
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1300, 700);
            this.Controls.Add(this.panelContenedor);
            this.Controls.Add(this.panelMenu);
            this.MinimumSize = new System.Drawing.Size(1280, 720);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema de Banco de Sangre";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.panelMenu.ResumeLayout(false);
            this.panelUsuario.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Panel panelMenuScroll;
        private System.Windows.Forms.Panel panelUsuario;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label lblEntidad;
        private System.Windows.Forms.Button btnCerrarSesion;
        private System.Windows.Forms.Panel panelContenedor;
    }
}