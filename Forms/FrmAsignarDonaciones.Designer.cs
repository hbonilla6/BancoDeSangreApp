namespace BancoDeSangreApp.Forms
{
    partial class FrmAsignarDonaciones
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.groupBoxSolicitud = new System.Windows.Forms.GroupBox();
            this.lblPrioridad = new System.Windows.Forms.Label();
            this.lblCantidadSolicitada = new System.Windows.Forms.Label();
            this.lblTipoSangre = new System.Windows.Forms.Label();
            this.lblSolicitante = new System.Windows.Forms.Label();
            this.lblIdSolicitud = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxDonaciones = new System.Windows.Forms.GroupBox();
            this.dgvDonaciones = new System.Windows.Forms.DataGridView();
            this.panelBusqueda = new System.Windows.Forms.Panel();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAsignar = new System.Windows.Forms.Button();
            this.btnLimpiarSeleccion = new System.Windows.Forms.Button();
            this.btnSeleccionarTodo = new System.Windows.Forms.Button();
            this.lblSeleccionadas = new System.Windows.Forms.Label();
            this.lblTotalDisponibles = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            this.groupBoxSolicitud.SuspendLayout();
            this.groupBoxDonaciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDonaciones)).BeginInit();
            this.panelBusqueda.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.panelTop.Controls.Add(this.lblTitulo);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1000, 60);
            this.panelTop.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(12, 15);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(241, 32);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Asignar Donaciones";
            // 
            // groupBoxSolicitud
            // 
            this.groupBoxSolicitud.Controls.Add(this.lblPrioridad);
            this.groupBoxSolicitud.Controls.Add(this.lblCantidadSolicitada);
            this.groupBoxSolicitud.Controls.Add(this.lblTipoSangre);
            this.groupBoxSolicitud.Controls.Add(this.lblSolicitante);
            this.groupBoxSolicitud.Controls.Add(this.lblIdSolicitud);
            this.groupBoxSolicitud.Controls.Add(this.label5);
            this.groupBoxSolicitud.Controls.Add(this.label4);
            this.groupBoxSolicitud.Controls.Add(this.label3);
            this.groupBoxSolicitud.Controls.Add(this.label2);
            this.groupBoxSolicitud.Controls.Add(this.label1);
            this.groupBoxSolicitud.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.groupBoxSolicitud.Location = new System.Drawing.Point(12, 70);
            this.groupBoxSolicitud.Name = "groupBoxSolicitud";
            this.groupBoxSolicitud.Size = new System.Drawing.Size(976, 100);
            this.groupBoxSolicitud.TabIndex = 1;
            this.groupBoxSolicitud.TabStop = false;
            this.groupBoxSolicitud.Text = "Información de la Solicitud";
            // 
            // lblPrioridad
            // 
            this.lblPrioridad.AutoSize = true;
            this.lblPrioridad.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblPrioridad.Location = new System.Drawing.Point(880, 63);
            this.lblPrioridad.Name = "lblPrioridad";
            this.lblPrioridad.Size = new System.Drawing.Size(12, 17);
            this.lblPrioridad.TabIndex = 9;
            this.lblPrioridad.Text = "-";
            // 
            // lblCantidadSolicitada
            // 
            this.lblCantidadSolicitada.AutoSize = true;
            this.lblCantidadSolicitada.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblCantidadSolicitada.Location = new System.Drawing.Point(880, 33);
            this.lblCantidadSolicitada.Name = "lblCantidadSolicitada";
            this.lblCantidadSolicitada.Size = new System.Drawing.Size(12, 17);
            this.lblCantidadSolicitada.TabIndex = 8;
            this.lblCantidadSolicitada.Text = "-";
            // 
            // lblTipoSangre
            // 
            this.lblTipoSangre.AutoSize = true;
            this.lblTipoSangre.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblTipoSangre.Location = new System.Drawing.Point(550, 63);
            this.lblTipoSangre.Name = "lblTipoSangre";
            this.lblTipoSangre.Size = new System.Drawing.Size(12, 17);
            this.lblTipoSangre.TabIndex = 7;
            this.lblTipoSangre.Text = "-";
            // 
            // lblSolicitante
            // 
            this.lblSolicitante.AutoSize = true;
            this.lblSolicitante.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblSolicitante.Location = new System.Drawing.Point(550, 33);
            this.lblSolicitante.Name = "lblSolicitante";
            this.lblSolicitante.Size = new System.Drawing.Size(12, 17);
            this.lblSolicitante.TabIndex = 6;
            this.lblSolicitante.Text = "-";
            // 
            // lblIdSolicitud
            // 
            this.lblIdSolicitud.AutoSize = true;
            this.lblIdSolicitud.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblIdSolicitud.Location = new System.Drawing.Point(100, 33);
            this.lblIdSolicitud.Name = "lblIdSolicitud";
            this.lblIdSolicitud.Size = new System.Drawing.Size(12, 17);
            this.lblIdSolicitud.TabIndex = 5;
            this.lblIdSolicitud.Text = "-";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(790, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "Prioridad:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(790, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "Cantidad:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(430, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Tipo de Sangre:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(430, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Solicitante:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(20, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Solicitud:";
            // 
            // groupBoxDonaciones
            // 
            this.groupBoxDonaciones.Controls.Add(this.dgvDonaciones);
            this.groupBoxDonaciones.Controls.Add(this.panelBusqueda);
            this.groupBoxDonaciones.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.groupBoxDonaciones.Location = new System.Drawing.Point(12, 180);
            this.groupBoxDonaciones.Name = "groupBoxDonaciones";
            this.groupBoxDonaciones.Size = new System.Drawing.Size(976, 420);
            this.groupBoxDonaciones.TabIndex = 2;
            this.groupBoxDonaciones.TabStop = false;
            this.groupBoxDonaciones.Text = "Donaciones Disponibles";
            // 
            // dgvDonaciones
            // 
            this.dgvDonaciones.BackgroundColor = System.Drawing.Color.White;
            this.dgvDonaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDonaciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDonaciones.Location = new System.Drawing.Point(3, 65);
            this.dgvDonaciones.Name = "dgvDonaciones";
            this.dgvDonaciones.Size = new System.Drawing.Size(970, 352);
            this.dgvDonaciones.TabIndex = 1;
            // 
            // panelBusqueda
            // 
            this.panelBusqueda.Controls.Add(this.txtBuscar);
            this.panelBusqueda.Controls.Add(this.lblBuscar);
            this.panelBusqueda.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBusqueda.Location = new System.Drawing.Point(3, 22);
            this.panelBusqueda.Name = "panelBusqueda";
            this.panelBusqueda.Size = new System.Drawing.Size(970, 43);
            this.panelBusqueda.TabIndex = 0;
            // 
            // txtBuscar
            // 
            this.txtBuscar.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtBuscar.Location = new System.Drawing.Point(70, 10);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(300, 25);
            this.txtBuscar.TabIndex = 1;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // lblBuscar
            // 
            this.lblBuscar.AutoSize = true;
            this.lblBuscar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblBuscar.Location = new System.Drawing.Point(10, 13);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(54, 17);
            this.lblBuscar.TabIndex = 0;
            this.lblBuscar.Text = "Buscar:";
            // 
            // panelBottom
            // 
            this.panelBottom.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelBottom.Controls.Add(this.btnCancelar);
            this.panelBottom.Controls.Add(this.btnAsignar);
            this.panelBottom.Controls.Add(this.btnLimpiarSeleccion);
            this.panelBottom.Controls.Add(this.btnSeleccionarTodo);
            this.panelBottom.Controls.Add(this.lblSeleccionadas);
            this.panelBottom.Controls.Add(this.lblTotalDisponibles);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 610);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(1000, 70);
            this.panelBottom.TabIndex = 3;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(880, 15);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(100, 40);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAsignar
            // 
            this.btnAsignar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.btnAsignar.Enabled = false;
            this.btnAsignar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAsignar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAsignar.ForeColor = System.Drawing.Color.White;
            this.btnAsignar.Location = new System.Drawing.Point(760, 15);
            this.btnAsignar.Name = "btnAsignar";
            this.btnAsignar.Size = new System.Drawing.Size(100, 40);
            this.btnAsignar.TabIndex = 4;
            this.btnAsignar.Text = "Asignar";
            this.btnAsignar.UseVisualStyleBackColor = false;
            this.btnAsignar.Click += new System.EventHandler(this.btnAsignar_Click);
            // 
            // btnLimpiarSeleccion
            // 
            this.btnLimpiarSeleccion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(156)))), ((int)(((byte)(18)))));
            this.btnLimpiarSeleccion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiarSeleccion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLimpiarSeleccion.ForeColor = System.Drawing.Color.White;
            this.btnLimpiarSeleccion.Location = new System.Drawing.Point(600, 15);
            this.btnLimpiarSeleccion.Name = "btnLimpiarSeleccion";
            this.btnLimpiarSeleccion.Size = new System.Drawing.Size(140, 40);
            this.btnLimpiarSeleccion.TabIndex = 3;
            this.btnLimpiarSeleccion.Text = "Limpiar Selección";
            this.btnLimpiarSeleccion.UseVisualStyleBackColor = false;
            this.btnLimpiarSeleccion.Click += new System.EventHandler(this.btnLimpiarSeleccion_Click);
            // 
            // btnSeleccionarTodo
            // 
            this.btnSeleccionarTodo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnSeleccionarTodo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSeleccionarTodo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSeleccionarTodo.ForeColor = System.Drawing.Color.White;
            this.btnSeleccionarTodo.Location = new System.Drawing.Point(440, 15);
            this.btnSeleccionarTodo.Name = "btnSeleccionarTodo";
            this.btnSeleccionarTodo.Size = new System.Drawing.Size(140, 40);
            this.btnSeleccionarTodo.TabIndex = 2;
            this.btnSeleccionarTodo.Text = "Seleccionar Todo";
            this.btnSeleccionarTodo.UseVisualStyleBackColor = false;
            this.btnSeleccionarTodo.Click += new System.EventHandler(this.btnSeleccionarTodo_Click);
            // 
            // lblSeleccionadas
            // 
            this.lblSeleccionadas.AutoSize = true;
            this.lblSeleccionadas.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSeleccionadas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblSeleccionadas.Location = new System.Drawing.Point(200, 25);
            this.lblSeleccionadas.Name = "lblSeleccionadas";
            this.lblSeleccionadas.Size = new System.Drawing.Size(119, 19);
            this.lblSeleccionadas.TabIndex = 1;
            this.lblSeleccionadas.Text = "Seleccionadas: 0";
            // 
            // lblTotalDisponibles
            // 
            this.lblTotalDisponibles.AutoSize = true;
            this.lblTotalDisponibles.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalDisponibles.Location = new System.Drawing.Point(20, 25);
            this.lblTotalDisponibles.Name = "lblTotalDisponibles";
            this.lblTotalDisponibles.Size = new System.Drawing.Size(103, 19);
            this.lblTotalDisponibles.TabIndex = 0;
            this.lblTotalDisponibles.Text = "Disponibles: 0";
            // 
            // FrmAsignarDonaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 680);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.groupBoxDonaciones);
            this.Controls.Add(this.groupBoxSolicitud);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAsignarDonaciones";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Asignar Donaciones a Solicitud";
            this.Load += new System.EventHandler(this.FrmAsignarDonaciones_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.groupBoxSolicitud.ResumeLayout(false);
            this.groupBoxSolicitud.PerformLayout();
            this.groupBoxDonaciones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDonaciones)).EndInit();
            this.panelBusqueda.ResumeLayout(false);
            this.panelBusqueda.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.GroupBox groupBoxSolicitud;
        private System.Windows.Forms.Label lblPrioridad;
        private System.Windows.Forms.Label lblCantidadSolicitada;
        private System.Windows.Forms.Label lblTipoSangre;
        private System.Windows.Forms.Label lblSolicitante;
        private System.Windows.Forms.Label lblIdSolicitud;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxDonaciones;
        private System.Windows.Forms.DataGridView dgvDonaciones;
        private System.Windows.Forms.Panel panelBusqueda;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAsignar;
        private System.Windows.Forms.Button btnLimpiarSeleccion;
        private System.Windows.Forms.Button btnSeleccionarTodo;
        private System.Windows.Forms.Label lblSeleccionadas;
        private System.Windows.Forms.Label lblTotalDisponibles;
    }
}