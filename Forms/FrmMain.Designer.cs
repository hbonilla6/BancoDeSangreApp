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
            this.btnReportes = new System.Windows.Forms.Button();
            this.btnSolicitudes = new System.Windows.Forms.Button();
            this.btnInventario = new System.Windows.Forms.Button();
            this.btnCentros = new System.Windows.Forms.Button(); // <--- Declaración Nuevo Botón
            this.btnDonaciones = new System.Windows.Forms.Button();
            this.btnDonantes = new System.Windows.Forms.Button();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.panelUsuario = new System.Windows.Forms.Panel();
            this.lblEntidad = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.panelContenedor = new System.Windows.Forms.Panel();
            this.panelMenu.SuspendLayout();
            this.panelUsuario.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(46)))), ((int)(((byte)(59)))));
            this.panelMenu.Controls.Add(this.btnCerrarSesion);
            this.panelMenu.Controls.Add(this.btnReportes);
            this.panelMenu.Controls.Add(this.btnSolicitudes);
            this.panelMenu.Controls.Add(this.btnInventario);
            this.panelMenu.Controls.Add(this.btnCentros); // <--- Agregado al panel
            this.panelMenu.Controls.Add(this.btnDonaciones);
            this.panelMenu.Controls.Add(this.btnDonantes);
            this.panelMenu.Controls.Add(this.btnDashboard);
            this.panelMenu.Controls.Add(this.panelUsuario);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(250, 700);
            this.panelMenu.TabIndex = 0;
            // 
            // btnCerrarSesion
            // 
            this.btnCerrarSesion.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnCerrarSesion.FlatAppearance.BorderSize = 0;
            this.btnCerrarSesion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrarSesion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            // 
            // btnReportes
            // 
            this.btnReportes.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnReportes.FlatAppearance.BorderSize = 0;
            this.btnReportes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReportes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReportes.ForeColor = System.Drawing.Color.White;
            this.btnReportes.Location = new System.Drawing.Point(0, 415); // <--- Posición ajustada
            this.btnReportes.Name = "btnReportes";
            this.btnReportes.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnReportes.Size = new System.Drawing.Size(250, 45);
            this.btnReportes.TabIndex = 7;
            this.btnReportes.Text = "📊  Reportes";
            this.btnReportes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReportes.UseVisualStyleBackColor = true;
            this.btnReportes.Click += new System.EventHandler(this.btnReportes_Click);
            // 
            // btnSolicitudes
            // 
            this.btnSolicitudes.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSolicitudes.FlatAppearance.BorderSize = 0;
            this.btnSolicitudes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSolicitudes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSolicitudes.ForeColor = System.Drawing.Color.White;
            this.btnSolicitudes.Location = new System.Drawing.Point(0, 370); // <--- Posición ajustada
            this.btnSolicitudes.Name = "btnSolicitudes";
            this.btnSolicitudes.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnSolicitudes.Size = new System.Drawing.Size(250, 45);
            this.btnSolicitudes.TabIndex = 6;
            this.btnSolicitudes.Text = "📋  Solicitudes";
            this.btnSolicitudes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSolicitudes.UseVisualStyleBackColor = true;
            this.btnSolicitudes.Click += new System.EventHandler(this.btnSolicitudes_Click);
            // 
            // btnInventario
            // 
            this.btnInventario.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnInventario.FlatAppearance.BorderSize = 0;
            this.btnInventario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInventario.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInventario.ForeColor = System.Drawing.Color.White;
            this.btnInventario.Location = new System.Drawing.Point(0, 325); // <--- Posición ajustada
            this.btnInventario.Name = "btnInventario";
            this.btnInventario.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnInventario.Size = new System.Drawing.Size(250, 45);
            this.btnInventario.TabIndex = 5;
            this.btnInventario.Text = "📦  Inventario";
            this.btnInventario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInventario.UseVisualStyleBackColor = true;
            this.btnInventario.Click += new System.EventHandler(this.btnInventario_Click);
            // 
            // btnCentros
            // 
            this.btnCentros.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCentros.FlatAppearance.BorderSize = 0;
            this.btnCentros.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCentros.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCentros.ForeColor = System.Drawing.Color.White;
            this.btnCentros.Location = new System.Drawing.Point(0, 280); // <--- Nueva Posición
            this.btnCentros.Name = "btnCentros";
            this.btnCentros.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnCentros.Size = new System.Drawing.Size(250, 45);
            this.btnCentros.TabIndex = 4;
            this.btnCentros.Text = "🏥  Centros"; // Icono y Texto
            this.btnCentros.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCentros.UseVisualStyleBackColor = true;
            this.btnCentros.Click += new System.EventHandler(this.btnCentros_Click); // Evento Click
            // 
            // btnDonaciones
            // 
            this.btnDonaciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDonaciones.FlatAppearance.BorderSize = 0;
            this.btnDonaciones.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDonaciones.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDonaciones.ForeColor = System.Drawing.Color.White;
            this.btnDonaciones.Location = new System.Drawing.Point(0, 235);
            this.btnDonaciones.Name = "btnDonaciones";
            this.btnDonaciones.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnDonaciones.Size = new System.Drawing.Size(250, 45);
            this.btnDonaciones.TabIndex = 3;
            this.btnDonaciones.Text = "💉  Donaciones";
            this.btnDonaciones.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDonaciones.UseVisualStyleBackColor = true;
            this.btnDonaciones.Click += new System.EventHandler(this.btnDonaciones_Click);
            // 
            // btnDonantes
            // 
            this.btnDonantes.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDonantes.FlatAppearance.BorderSize = 0;
            this.btnDonantes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDonantes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDonantes.ForeColor = System.Drawing.Color.White;
            this.btnDonantes.Location = new System.Drawing.Point(0, 190);
            this.btnDonantes.Name = "btnDonantes";
            this.btnDonantes.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnDonantes.Size = new System.Drawing.Size(250, 45);
            this.btnDonantes.TabIndex = 2;
            this.btnDonantes.Text = "👥  Donantes";
            this.btnDonantes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDonantes.UseVisualStyleBackColor = true;
            this.btnDonantes.Click += new System.EventHandler(this.btnDonantes_Click);
            // 
            // btnDashboard
            // 
            this.btnDashboard.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDashboard.FlatAppearance.BorderSize = 0;
            this.btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDashboard.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDashboard.ForeColor = System.Drawing.Color.White;
            this.btnDashboard.Location = new System.Drawing.Point(0, 145);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnDashboard.Size = new System.Drawing.Size(250, 45);
            this.btnDashboard.TabIndex = 1;
            this.btnDashboard.Text = "🏠  Dashboard";
            this.btnDashboard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDashboard.UseVisualStyleBackColor = true;
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click);
            // 
            // panelUsuario
            // 
            this.panelUsuario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.panelUsuario.Controls.Add(this.lblEntidad);
            this.panelUsuario.Controls.Add(this.lblUsuario);
            this.panelUsuario.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelUsuario.Location = new System.Drawing.Point(0, 0);
            this.panelUsuario.Name = "panelUsuario";
            this.panelUsuario.Padding = new System.Windows.Forms.Padding(15, 20, 15, 20);
            this.panelUsuario.Size = new System.Drawing.Size(250, 145);
            this.panelUsuario.TabIndex = 0;
            // 
            // lblEntidad
            // 
            this.lblEntidad.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblEntidad.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntidad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.lblEntidad.Location = new System.Drawing.Point(15, 59);
            this.lblEntidad.Name = "lblEntidad";
            this.lblEntidad.Size = new System.Drawing.Size(220, 40);
            this.lblEntidad.TabIndex = 1;
            this.lblEntidad.Text = "🏥 Entidad";
            this.lblEntidad.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblUsuario
            // 
            this.lblUsuario.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblUsuario.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuario.ForeColor = System.Drawing.Color.White;
            this.lblUsuario.Location = new System.Drawing.Point(15, 20);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(220, 39);
            this.lblUsuario.TabIndex = 0;
            this.lblUsuario.Text = "👤 Usuario";
            this.lblUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelContenedor
            // 
            this.panelContenedor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.panelContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenedor.Location = new System.Drawing.Point(250, 0);
            this.panelContenedor.Name = "panelContenedor";
            this.panelContenedor.Size = new System.Drawing.Size(1050, 700);
            this.panelContenedor.TabIndex = 1;
            this.panelContenedor.Paint += new System.Windows.Forms.PaintEventHandler(this.panelContenedor_Paint);
            // 
            // FrmMain
            // 
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
        private System.Windows.Forms.Panel panelUsuario;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label lblEntidad;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Button btnDonantes;
        private System.Windows.Forms.Button btnDonaciones;
        private System.Windows.Forms.Button btnCentros; // <--- Nuevo botón
        private System.Windows.Forms.Button btnInventario;
        private System.Windows.Forms.Button btnSolicitudes;
        private System.Windows.Forms.Button btnReportes;
        private System.Windows.Forms.Button btnCerrarSesion;
        private System.Windows.Forms.Panel panelContenedor;
    }
}