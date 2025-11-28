namespace BancoDeSangreApp.Forms
{
    partial class FrmInventario
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
            this.lblUltimaActualizacion = new System.Windows.Forms.Label();
            this.btnExportar = new System.Windows.Forms.Button();
            this.btnActualizarInventario = new System.Windows.Forms.Button();
            this.btnRefrescar = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.pnlEstadisticas = new System.Windows.Forms.Panel();
            this.pnlTiposBajos = new System.Windows.Forms.Panel();
            this.lblTiposBajos = new System.Windows.Forms.Label();
            this.lblTiposBajosLabel = new System.Windows.Forms.Label();
            this.pnlTiposCriticos = new System.Windows.Forms.Panel();
            this.lblTiposCriticos = new System.Windows.Forms.Label();
            this.lblTiposCriticosLabel = new System.Windows.Forms.Label();
            this.pnlTotalUnidades = new System.Windows.Forms.Panel();
            this.lblTotalUnidades = new System.Windows.Forms.Label();
            this.lblTotalUnidadesLabel = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabInventario = new System.Windows.Forms.TabPage();
            this.dgvInventario = new System.Windows.Forms.DataGridView();
            this.pnlInventarioBottom = new System.Windows.Forms.Panel();
            this.lblTotalTipos = new System.Windows.Forms.Label();
            this.tabAlertas = new System.Windows.Forms.TabPage();
            this.lblSinAlertas = new System.Windows.Forms.Label();
            this.dgvAlertas = new System.Windows.Forms.DataGridView();
            this.pnlAlertasBottom = new System.Windows.Forms.Panel();
            this.lblTotalAlertas = new System.Windows.Forms.Label();
            this.tabProximasVencer = new System.Windows.Forms.TabPage();
            this.dgvProximasVencer = new System.Windows.Forms.DataGridView();
            this.pnlProximasVencerBottom = new System.Windows.Forms.Panel();
            this.lblTotalProximasVencer = new System.Windows.Forms.Label();
            this.pnlTop.SuspendLayout();
            this.pnlEstadisticas.SuspendLayout();
            this.pnlTiposBajos.SuspendLayout();
            this.pnlTiposCriticos.SuspendLayout();
            this.pnlTotalUnidades.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabInventario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventario)).BeginInit();
            this.pnlInventarioBottom.SuspendLayout();
            this.tabAlertas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlertas)).BeginInit();
            this.pnlAlertasBottom.SuspendLayout();
            this.tabProximasVencer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProximasVencer)).BeginInit();
            this.pnlProximasVencerBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.pnlTop.Controls.Add(this.lblUltimaActualizacion);
            this.pnlTop.Controls.Add(this.btnExportar);
            this.pnlTop.Controls.Add(this.btnActualizarInventario);
            this.pnlTop.Controls.Add(this.btnRefrescar);
            this.pnlTop.Controls.Add(this.lblTitulo);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1200, 100);
            this.pnlTop.TabIndex = 0;
            // 
            // lblUltimaActualizacion
            // 
            this.lblUltimaActualizacion.AutoSize = true;
            this.lblUltimaActualizacion.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUltimaActualizacion.ForeColor = System.Drawing.Color.White;
            this.lblUltimaActualizacion.Location = new System.Drawing.Point(12, 70);
            this.lblUltimaActualizacion.Name = "lblUltimaActualizacion";
            this.lblUltimaActualizacion.Size = new System.Drawing.Size(200, 15);
            this.lblUltimaActualizacion.TabIndex = 4;
            this.lblUltimaActualizacion.Text = "Última actualización: --/--/---- --:--:--";
            // 
            // btnExportar
            // 
            this.btnExportar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnExportar.FlatAppearance.BorderSize = 0;
            this.btnExportar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnExportar.ForeColor = System.Drawing.Color.White;
            this.btnExportar.Location = new System.Drawing.Point(1045, 30);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(140, 40);
            this.btnExportar.TabIndex = 3;
            this.btnExportar.Text = "📊 Exportar";
            this.btnExportar.UseVisualStyleBackColor = false;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // btnActualizarInventario
            // 
            this.btnActualizarInventario.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnActualizarInventario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnActualizarInventario.FlatAppearance.BorderSize = 0;
            this.btnActualizarInventario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizarInventario.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnActualizarInventario.ForeColor = System.Drawing.Color.White;
            this.btnActualizarInventario.Location = new System.Drawing.Point(860, 30);
            this.btnActualizarInventario.Name = "btnActualizarInventario";
            this.btnActualizarInventario.Size = new System.Drawing.Size(175, 40);
            this.btnActualizarInventario.TabIndex = 2;
            this.btnActualizarInventario.Text = "🔄 Actualizar Inventario";
            this.btnActualizarInventario.UseVisualStyleBackColor = false;
            this.btnActualizarInventario.Click += new System.EventHandler(this.btnActualizarInventario_Click);
            // 
            // btnRefrescar
            // 
            this.btnRefrescar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefrescar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnRefrescar.FlatAppearance.BorderSize = 0;
            this.btnRefrescar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefrescar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnRefrescar.ForeColor = System.Drawing.Color.White;
            this.btnRefrescar.Location = new System.Drawing.Point(720, 30);
            this.btnRefrescar.Name = "btnRefrescar";
            this.btnRefrescar.Size = new System.Drawing.Size(130, 40);
            this.btnRefrescar.TabIndex = 1;
            this.btnRefrescar.Text = "🔃 Refrescar";
            this.btnRefrescar.UseVisualStyleBackColor = false;
            this.btnRefrescar.Click += new System.EventHandler(this.btnRefrescar_Click);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(12, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(337, 37);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "📦 Inventario de Sangre";
            // 
            // pnlEstadisticas
            // 
            this.pnlEstadisticas.Controls.Add(this.pnlTiposBajos);
            this.pnlEstadisticas.Controls.Add(this.pnlTiposCriticos);
            this.pnlEstadisticas.Controls.Add(this.pnlTotalUnidades);
            this.pnlEstadisticas.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEstadisticas.Location = new System.Drawing.Point(0, 100);
            this.pnlEstadisticas.Name = "pnlEstadisticas";
            this.pnlEstadisticas.Padding = new System.Windows.Forms.Padding(10);
            this.pnlEstadisticas.Size = new System.Drawing.Size(1200, 100);
            this.pnlEstadisticas.TabIndex = 1;
            // 
            // pnlTiposBajos
            // 
            this.pnlTiposBajos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(196)))), ((int)(((byte)(15)))));
            this.pnlTiposBajos.Controls.Add(this.lblTiposBajos);
            this.pnlTiposBajos.Controls.Add(this.lblTiposBajosLabel);
            this.pnlTiposBajos.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlTiposBajos.Location = new System.Drawing.Point(810, 10);
            this.pnlTiposBajos.Name = "pnlTiposBajos";
            this.pnlTiposBajos.Size = new System.Drawing.Size(380, 80);
            this.pnlTiposBajos.TabIndex = 2;
            // 
            // lblTiposBajos
            // 
            this.lblTiposBajos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTiposBajos.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTiposBajos.ForeColor = System.Drawing.Color.White;
            this.lblTiposBajos.Location = new System.Drawing.Point(0, 30);
            this.lblTiposBajos.Name = "lblTiposBajos";
            this.lblTiposBajos.Size = new System.Drawing.Size(380, 50);
            this.lblTiposBajos.TabIndex = 1;
            this.lblTiposBajos.Text = "0";
            this.lblTiposBajos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTiposBajosLabel
            // 
            this.lblTiposBajosLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTiposBajosLabel.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblTiposBajosLabel.ForeColor = System.Drawing.Color.White;
            this.lblTiposBajosLabel.Location = new System.Drawing.Point(0, 0);
            this.lblTiposBajosLabel.Name = "lblTiposBajosLabel";
            this.lblTiposBajosLabel.Size = new System.Drawing.Size(380, 30);
            this.lblTiposBajosLabel.TabIndex = 0;
            this.lblTiposBajosLabel.Text = "⚠️ TIPOS CON STOCK BAJO";
            this.lblTiposBajosLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlTiposCriticos
            // 
            this.pnlTiposCriticos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.pnlTiposCriticos.Controls.Add(this.lblTiposCriticos);
            this.pnlTiposCriticos.Controls.Add(this.lblTiposCriticosLabel);
            this.pnlTiposCriticos.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlTiposCriticos.Location = new System.Drawing.Point(410, 10);
            this.pnlTiposCriticos.Name = "pnlTiposCriticos";
            this.pnlTiposCriticos.Size = new System.Drawing.Size(400, 80);
            this.pnlTiposCriticos.TabIndex = 1;
            // 
            // lblTiposCriticos
            // 
            this.lblTiposCriticos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTiposCriticos.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTiposCriticos.ForeColor = System.Drawing.Color.White;
            this.lblTiposCriticos.Location = new System.Drawing.Point(0, 30);
            this.lblTiposCriticos.Name = "lblTiposCriticos";
            this.lblTiposCriticos.Size = new System.Drawing.Size(400, 50);
            this.lblTiposCriticos.TabIndex = 1;
            this.lblTiposCriticos.Text = "0";
            this.lblTiposCriticos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTiposCriticosLabel
            // 
            this.lblTiposCriticosLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTiposCriticosLabel.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblTiposCriticosLabel.ForeColor = System.Drawing.Color.White;
            this.lblTiposCriticosLabel.Location = new System.Drawing.Point(0, 0);
            this.lblTiposCriticosLabel.Name = "lblTiposCriticosLabel";
            this.lblTiposCriticosLabel.Size = new System.Drawing.Size(400, 30);
            this.lblTiposCriticosLabel.TabIndex = 0;
            this.lblTiposCriticosLabel.Text = "🚨 TIPOS CON STOCK CRÍTICO";
            this.lblTiposCriticosLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlTotalUnidades
            // 
            this.pnlTotalUnidades.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.pnlTotalUnidades.Controls.Add(this.lblTotalUnidades);
            this.pnlTotalUnidades.Controls.Add(this.lblTotalUnidadesLabel);
            this.pnlTotalUnidades.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlTotalUnidades.Location = new System.Drawing.Point(10, 10);
            this.pnlTotalUnidades.Name = "pnlTotalUnidades";
            this.pnlTotalUnidades.Size = new System.Drawing.Size(400, 80);
            this.pnlTotalUnidades.TabIndex = 0;
            // 
            // lblTotalUnidades
            // 
            this.lblTotalUnidades.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalUnidades.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTotalUnidades.ForeColor = System.Drawing.Color.White;
            this.lblTotalUnidades.Location = new System.Drawing.Point(0, 30);
            this.lblTotalUnidades.Name = "lblTotalUnidades";
            this.lblTotalUnidades.Size = new System.Drawing.Size(400, 50);
            this.lblTotalUnidades.TabIndex = 1;
            this.lblTotalUnidades.Text = "0";
            this.lblTotalUnidades.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalUnidadesLabel
            // 
            this.lblTotalUnidadesLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTotalUnidadesLabel.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblTotalUnidadesLabel.ForeColor = System.Drawing.Color.White;
            this.lblTotalUnidadesLabel.Location = new System.Drawing.Point(0, 0);
            this.lblTotalUnidadesLabel.Name = "lblTotalUnidadesLabel";
            this.lblTotalUnidadesLabel.Size = new System.Drawing.Size(400, 30);
            this.lblTotalUnidadesLabel.TabIndex = 0;
            this.lblTotalUnidadesLabel.Text = "💉 TOTAL UNIDADES DISPONIBLES";
            this.lblTotalUnidadesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabInventario);
            this.tabControl.Controls.Add(this.tabAlertas);
            this.tabControl.Controls.Add(this.tabProximasVencer);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tabControl.Location = new System.Drawing.Point(0, 200);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1200, 450);
            this.tabControl.TabIndex = 2;
            // 
            // tabInventario
            // 
            this.tabInventario.Controls.Add(this.dgvInventario);
            this.tabInventario.Controls.Add(this.pnlInventarioBottom);
            this.tabInventario.Location = new System.Drawing.Point(4, 26);
            this.tabInventario.Name = "tabInventario";
            this.tabInventario.Padding = new System.Windows.Forms.Padding(3);
            this.tabInventario.Size = new System.Drawing.Size(1192, 420);
            this.tabInventario.TabIndex = 0;
            this.tabInventario.Text = "📋 Inventario General";
            this.tabInventario.UseVisualStyleBackColor = true;
            // 
            // dgvInventario
            // 
            this.dgvInventario.BackgroundColor = System.Drawing.Color.White;
            this.dgvInventario.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInventario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInventario.Location = new System.Drawing.Point(3, 3);
            this.dgvInventario.Name = "dgvInventario";
            this.dgvInventario.Size = new System.Drawing.Size(1186, 374);
            this.dgvInventario.TabIndex = 0;
            // 
            // pnlInventarioBottom
            // 
            this.pnlInventarioBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.pnlInventarioBottom.Controls.Add(this.lblTotalTipos);
            this.pnlInventarioBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlInventarioBottom.Location = new System.Drawing.Point(3, 377);
            this.pnlInventarioBottom.Name = "pnlInventarioBottom";
            this.pnlInventarioBottom.Size = new System.Drawing.Size(1186, 40);
            this.pnlInventarioBottom.TabIndex = 1;
            // 
            // lblTotalTipos
            // 
            this.lblTotalTipos.AutoSize = true;
            this.lblTotalTipos.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalTipos.Location = new System.Drawing.Point(10, 10);
            this.lblTotalTipos.Name = "lblTotalTipos";
            this.lblTotalTipos.Size = new System.Drawing.Size(89, 19);
            this.lblTotalTipos.TabIndex = 0;
            this.lblTotalTipos.Text = "Total tipos: 0";
            // 
            // tabAlertas
            // 
            this.tabAlertas.Controls.Add(this.lblSinAlertas);
            this.tabAlertas.Controls.Add(this.dgvAlertas);
            this.tabAlertas.Controls.Add(this.pnlAlertasBottom);
            this.tabAlertas.Location = new System.Drawing.Point(4, 26);
            this.tabAlertas.Name = "tabAlertas";
            this.tabAlertas.Padding = new System.Windows.Forms.Padding(3);
            this.tabAlertas.Size = new System.Drawing.Size(1192, 420);
            this.tabAlertas.TabIndex = 1;
            this.tabAlertas.Text = "🚨 Alertas de Stock";
            this.tabAlertas.UseVisualStyleBackColor = true;
            // 
            // lblSinAlertas
            // 
            this.lblSinAlertas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSinAlertas.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.lblSinAlertas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.lblSinAlertas.Location = new System.Drawing.Point(3, 3);
            this.lblSinAlertas.Name = "lblSinAlertas";
            this.lblSinAlertas.Size = new System.Drawing.Size(1186, 374);
            this.lblSinAlertas.TabIndex = 2;
            this.lblSinAlertas.Text = "✅ No hay alertas de stock en este momento";
            this.lblSinAlertas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSinAlertas.Visible = false;
            // 
            // dgvAlertas
            // 
            this.dgvAlertas.BackgroundColor = System.Drawing.Color.White;
            this.dgvAlertas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAlertas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAlertas.Location = new System.Drawing.Point(3, 3);
            this.dgvAlertas.Name = "dgvAlertas";
            this.dgvAlertas.Size = new System.Drawing.Size(1186, 374);
            this.dgvAlertas.TabIndex = 0;
            // 
            // pnlAlertasBottom
            // 
            this.pnlAlertasBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.pnlAlertasBottom.Controls.Add(this.lblTotalAlertas);
            this.pnlAlertasBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlAlertasBottom.Location = new System.Drawing.Point(3, 377);
            this.pnlAlertasBottom.Name = "pnlAlertasBottom";
            this.pnlAlertasBottom.Size = new System.Drawing.Size(1186, 40);
            this.pnlAlertasBottom.TabIndex = 1;
            // 
            // lblTotalAlertas
            // 
            this.lblTotalAlertas.AutoSize = true;
            this.lblTotalAlertas.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalAlertas.Location = new System.Drawing.Point(10, 10);
            this.lblTotalAlertas.Name = "lblTotalAlertas";
            this.lblTotalAlertas.Size = new System.Drawing.Size(70, 19);
            this.lblTotalAlertas.TabIndex = 0;
            this.lblTotalAlertas.Text = "Alertas: 0";
            // 
            // tabProximasVencer
            // 
            this.tabProximasVencer.Controls.Add(this.dgvProximasVencer);
            this.tabProximasVencer.Controls.Add(this.pnlProximasVencerBottom);
            this.tabProximasVencer.Location = new System.Drawing.Point(4, 26);
            this.tabProximasVencer.Name = "tabProximasVencer";
            this.tabProximasVencer.Size = new System.Drawing.Size(1192, 420);
            this.tabProximasVencer.TabIndex = 2;
            this.tabProximasVencer.Text = "⏰ Próximas a Vencer";
            this.tabProximasVencer.UseVisualStyleBackColor = true;
            // 
            // dgvProximasVencer
            // 
            this.dgvProximasVencer.BackgroundColor = System.Drawing.Color.White;
            this.dgvProximasVencer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProximasVencer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProximasVencer.Location = new System.Drawing.Point(0, 0);
            this.dgvProximasVencer.Name = "dgvProximasVencer";
            this.dgvProximasVencer.Size = new System.Drawing.Size(1192, 380);
            this.dgvProximasVencer.TabIndex = 0;
            // 
            // pnlProximasVencerBottom
            // 
            this.pnlProximasVencerBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.pnlProximasVencerBottom.Controls.Add(this.lblTotalProximasVencer);
            this.pnlProximasVencerBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlProximasVencerBottom.Location = new System.Drawing.Point(0, 380);
            this.pnlProximasVencerBottom.Name = "pnlProximasVencerBottom";
            this.pnlProximasVencerBottom.Size = new System.Drawing.Size(1192, 40);
            this.pnlProximasVencerBottom.TabIndex = 1;
            // 
            // lblTotalProximasVencer
            // 
            this.lblTotalProximasVencer.AutoSize = true;
            this.lblTotalProximasVencer.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalProximasVencer.Location = new System.Drawing.Point(10, 10);
            this.lblTotalProximasVencer.Name = "lblTotalProximasVencer";
            this.lblTotalProximasVencer.Size = new System.Drawing.Size(99, 19);
            this.lblTotalProximasVencer.TabIndex = 0;
            this.lblTotalProximasVencer.Text = "Por vencer: 0";
            // 
            // FrmInventario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 650);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.pnlEstadisticas);
            this.Controls.Add(this.pnlTop);
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "FrmInventario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inventario de Sangre - Banco de Sangre";
            this.Load += new System.EventHandler(this.FrmInventario_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlEstadisticas.ResumeLayout(false);
            this.pnlTiposBajos.ResumeLayout(false);
            this.pnlTiposCriticos.ResumeLayout(false);
            this.pnlTotalUnidades.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabInventario.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventario)).EndInit();
            this.pnlInventarioBottom.ResumeLayout(false);
            this.pnlInventarioBottom.PerformLayout();
            this.tabAlertas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlertas)).EndInit();
            this.pnlAlertasBottom.ResumeLayout(false);
            this.pnlAlertasBottom.PerformLayout();
            this.tabProximasVencer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProximasVencer)).EndInit();
            this.pnlProximasVencerBottom.ResumeLayout(false);
            this.pnlProximasVencerBottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Button btnRefrescar;
        private System.Windows.Forms.Button btnActualizarInventario;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.Label lblUltimaActualizacion;
        private System.Windows.Forms.Panel pnlEstadisticas;
        private System.Windows.Forms.Panel pnlTotalUnidades;
        private System.Windows.Forms.Label lblTotalUnidades;
        private System.Windows.Forms.Label lblTotalUnidadesLabel;
        private System.Windows.Forms.Panel pnlTiposCriticos;
        private System.Windows.Forms.Label lblTiposCriticos;
        private System.Windows.Forms.Label lblTiposCriticosLabel;
        private System.Windows.Forms.Panel pnlTiposBajos;
        private System.Windows.Forms.Label lblTiposBajos;
        private System.Windows.Forms.Label lblTiposBajosLabel;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabInventario;
        private System.Windows.Forms.DataGridView dgvInventario;
        private System.Windows.Forms.Panel pnlInventarioBottom;
        private System.Windows.Forms.Label lblTotalTipos;
        private System.Windows.Forms.TabPage tabAlertas;
        private System.Windows.Forms.DataGridView dgvAlertas;
        private System.Windows.Forms.Panel pnlAlertasBottom;
        private System.Windows.Forms.Label lblTotalAlertas;
        private System.Windows.Forms.Label lblSinAlertas;
        private System.Windows.Forms.TabPage tabProximasVencer;
        private System.Windows.Forms.DataGridView dgvProximasVencer;
        private System.Windows.Forms.Panel pnlProximasVencerBottom;
        private System.Windows.Forms.Label lblTotalProximasVencer;
    }
}