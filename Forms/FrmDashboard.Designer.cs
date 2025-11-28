using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoDeSangreApp.Forms
{
    partial class FrmDashboard
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnRefrescar = new System.Windows.Forms.Button();
            this.lblUltimaActualizacion = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panelEstadisticas = new System.Windows.Forms.Panel();
            this.tableLayoutEstadisticas = new System.Windows.Forms.TableLayoutPanel();
            this.panelDonantes = new System.Windows.Forms.Panel();
            this.lblTotalDonantes = new System.Windows.Forms.Label();
            this.lblTituloDonantes = new System.Windows.Forms.Label();
            this.panelDonaciones = new System.Windows.Forms.Panel();
            this.lblTotalDonaciones = new System.Windows.Forms.Label();
            this.lblTituloDonaciones = new System.Windows.Forms.Label();
            this.panelSolicitudes = new System.Windows.Forms.Panel();
            this.lblSolicitudesPendientes = new System.Windows.Forms.Label();
            this.lblTituloSolicitudes = new System.Windows.Forms.Label();
            this.panelPorVencer = new System.Windows.Forms.Panel();
            this.lblUnidadesPorVencer = new System.Windows.Forms.Label();
            this.lblTituloPorVencer = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabInventario = new System.Windows.Forms.TabPage();
            this.dgvInventario = new System.Windows.Forms.DataGridView();
            this.tabSolicitudes = new System.Windows.Forms.TabPage();
            this.dgvSolicitudes = new System.Windows.Forms.DataGridView();
            this.tabDonaciones = new System.Windows.Forms.TabPage();
            this.dgvDonacionesRecientes = new System.Windows.Forms.DataGridView();
            this.tabAlertas = new System.Windows.Forms.TabPage();
            this.lblSinAlertas = new System.Windows.Forms.Label();
            this.dgvAlertas = new System.Windows.Forms.DataGridView();
            this.tabEstadisticas = new System.Windows.Forms.TabPage();
            this.dgvDonantesPorTipo = new System.Windows.Forms.DataGridView();
            this.panelTop.SuspendLayout();
            this.panelEstadisticas.SuspendLayout();
            this.tableLayoutEstadisticas.SuspendLayout();
            this.panelDonantes.SuspendLayout();
            this.panelDonaciones.SuspendLayout();
            this.panelSolicitudes.SuspendLayout();
            this.panelPorVencer.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabInventario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventario)).BeginInit();
            this.tabSolicitudes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSolicitudes)).BeginInit();
            this.tabDonaciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDonacionesRecientes)).BeginInit();
            this.tabAlertas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlertas)).BeginInit();
            this.tabEstadisticas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDonantesPorTipo)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.panelTop.Controls.Add(this.btnRefrescar);
            this.panelTop.Controls.Add(this.lblUltimaActualizacion);
            this.panelTop.Controls.Add(this.lblTitulo);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1020, 80);
            this.panelTop.TabIndex = 0;
            // 
            // btnRefrescar
            // 
            this.btnRefrescar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefrescar.BackColor = System.Drawing.Color.White;
            this.btnRefrescar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefrescar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRefrescar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnRefrescar.Location = new System.Drawing.Point(870, 20);
            this.btnRefrescar.Name = "btnRefrescar";
            this.btnRefrescar.Size = new System.Drawing.Size(130, 40);
            this.btnRefrescar.TabIndex = 2;
            this.btnRefrescar.Text = "🔄 Refrescar";
            this.btnRefrescar.UseVisualStyleBackColor = false;
            this.btnRefrescar.Click += new System.EventHandler(this.btnRefrescar_Click);
            // 
            // lblUltimaActualizacion
            // 
            this.lblUltimaActualizacion.AutoSize = true;
            this.lblUltimaActualizacion.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUltimaActualizacion.ForeColor = System.Drawing.Color.White;
            this.lblUltimaActualizacion.Location = new System.Drawing.Point(25, 50);
            this.lblUltimaActualizacion.Name = "lblUltimaActualizacion";
            this.lblUltimaActualizacion.Size = new System.Drawing.Size(156, 15);
            this.lblUltimaActualizacion.TabIndex = 1;
            this.lblUltimaActualizacion.Text = "Última actualización: --:--:--";
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(20, 15);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(394, 32);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "📊 Dashboard - Banco de Sangre";
            this.lblTitulo.Click += new System.EventHandler(this.lblTitulo_Click);
            // 
            // panelEstadisticas
            // 
            this.panelEstadisticas.Controls.Add(this.tableLayoutEstadisticas);
            this.panelEstadisticas.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEstadisticas.Location = new System.Drawing.Point(0, 80);
            this.panelEstadisticas.Name = "panelEstadisticas";
            this.panelEstadisticas.Padding = new System.Windows.Forms.Padding(20, 20, 20, 10);
            this.panelEstadisticas.Size = new System.Drawing.Size(1020, 140);
            this.panelEstadisticas.TabIndex = 1;
            // 
            // tableLayoutEstadisticas
            // 
            this.tableLayoutEstadisticas.ColumnCount = 4;
            this.tableLayoutEstadisticas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutEstadisticas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutEstadisticas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutEstadisticas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutEstadisticas.Controls.Add(this.panelDonantes, 0, 0);
            this.tableLayoutEstadisticas.Controls.Add(this.panelDonaciones, 1, 0);
            this.tableLayoutEstadisticas.Controls.Add(this.panelSolicitudes, 2, 0);
            this.tableLayoutEstadisticas.Controls.Add(this.panelPorVencer, 3, 0);
            this.tableLayoutEstadisticas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutEstadisticas.Location = new System.Drawing.Point(20, 20);
            this.tableLayoutEstadisticas.Name = "tableLayoutEstadisticas";
            this.tableLayoutEstadisticas.RowCount = 1;
            this.tableLayoutEstadisticas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutEstadisticas.Size = new System.Drawing.Size(980, 110);
            this.tableLayoutEstadisticas.TabIndex = 0;
            // 
            // panelDonantes
            // 
            this.panelDonantes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.panelDonantes.Controls.Add(this.lblTotalDonantes);
            this.panelDonantes.Controls.Add(this.lblTituloDonantes);
            this.panelDonantes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDonantes.Location = new System.Drawing.Point(5, 5);
            this.panelDonantes.Margin = new System.Windows.Forms.Padding(5);
            this.panelDonantes.Name = "panelDonantes";
            this.panelDonantes.Size = new System.Drawing.Size(235, 100);
            this.panelDonantes.TabIndex = 0;
            // 
            // lblTotalDonantes
            // 
            this.lblTotalDonantes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalDonantes.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblTotalDonantes.ForeColor = System.Drawing.Color.White;
            this.lblTotalDonantes.Location = new System.Drawing.Point(0, 23);
            this.lblTotalDonantes.Name = "lblTotalDonantes";
            this.lblTotalDonantes.Size = new System.Drawing.Size(235, 77);
            this.lblTotalDonantes.TabIndex = 1;
            this.lblTotalDonantes.Text = "0";
            this.lblTotalDonantes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTituloDonantes
            // 
            this.lblTituloDonantes.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTituloDonantes.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTituloDonantes.ForeColor = System.Drawing.Color.White;
            this.lblTituloDonantes.Location = new System.Drawing.Point(0, 0);
            this.lblTituloDonantes.Name = "lblTituloDonantes";
            this.lblTituloDonantes.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.lblTituloDonantes.Size = new System.Drawing.Size(235, 23);
            this.lblTituloDonantes.TabIndex = 0;
            this.lblTituloDonantes.Text = "👥 Total Donantes";
            this.lblTituloDonantes.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panelDonaciones
            // 
            this.panelDonaciones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.panelDonaciones.Controls.Add(this.lblTotalDonaciones);
            this.panelDonaciones.Controls.Add(this.lblTituloDonaciones);
            this.panelDonaciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDonaciones.Location = new System.Drawing.Point(250, 5);
            this.panelDonaciones.Margin = new System.Windows.Forms.Padding(5);
            this.panelDonaciones.Name = "panelDonaciones";
            this.panelDonaciones.Size = new System.Drawing.Size(235, 100);
            this.panelDonaciones.TabIndex = 1;
            // 
            // lblTotalDonaciones
            // 
            this.lblTotalDonaciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalDonaciones.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblTotalDonaciones.ForeColor = System.Drawing.Color.White;
            this.lblTotalDonaciones.Location = new System.Drawing.Point(0, 23);
            this.lblTotalDonaciones.Name = "lblTotalDonaciones";
            this.lblTotalDonaciones.Size = new System.Drawing.Size(235, 77);
            this.lblTotalDonaciones.TabIndex = 1;
            this.lblTotalDonaciones.Text = "0";
            this.lblTotalDonaciones.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTituloDonaciones
            // 
            this.lblTituloDonaciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTituloDonaciones.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTituloDonaciones.ForeColor = System.Drawing.Color.White;
            this.lblTituloDonaciones.Location = new System.Drawing.Point(0, 0);
            this.lblTituloDonaciones.Name = "lblTituloDonaciones";
            this.lblTituloDonaciones.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.lblTituloDonaciones.Size = new System.Drawing.Size(235, 23);
            this.lblTituloDonaciones.TabIndex = 0;
            this.lblTituloDonaciones.Text = "💉 Unidades Disponibles";
            this.lblTituloDonaciones.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panelSolicitudes
            // 
            this.panelSolicitudes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.panelSolicitudes.Controls.Add(this.lblSolicitudesPendientes);
            this.panelSolicitudes.Controls.Add(this.lblTituloSolicitudes);
            this.panelSolicitudes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSolicitudes.Location = new System.Drawing.Point(495, 5);
            this.panelSolicitudes.Margin = new System.Windows.Forms.Padding(5);
            this.panelSolicitudes.Name = "panelSolicitudes";
            this.panelSolicitudes.Size = new System.Drawing.Size(235, 100);
            this.panelSolicitudes.TabIndex = 2;
            // 
            // lblSolicitudesPendientes
            // 
            this.lblSolicitudesPendientes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSolicitudesPendientes.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblSolicitudesPendientes.ForeColor = System.Drawing.Color.White;
            this.lblSolicitudesPendientes.Location = new System.Drawing.Point(0, 23);
            this.lblSolicitudesPendientes.Name = "lblSolicitudesPendientes";
            this.lblSolicitudesPendientes.Size = new System.Drawing.Size(235, 77);
            this.lblSolicitudesPendientes.TabIndex = 1;
            this.lblSolicitudesPendientes.Text = "0";
            this.lblSolicitudesPendientes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTituloSolicitudes
            // 
            this.lblTituloSolicitudes.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTituloSolicitudes.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTituloSolicitudes.ForeColor = System.Drawing.Color.White;
            this.lblTituloSolicitudes.Location = new System.Drawing.Point(0, 0);
            this.lblTituloSolicitudes.Name = "lblTituloSolicitudes";
            this.lblTituloSolicitudes.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.lblTituloSolicitudes.Size = new System.Drawing.Size(235, 23);
            this.lblTituloSolicitudes.TabIndex = 0;
            this.lblTituloSolicitudes.Text = "📋 Solicitudes Pendientes";
            this.lblTituloSolicitudes.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panelPorVencer
            // 
            this.panelPorVencer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(196)))), ((int)(((byte)(15)))));
            this.panelPorVencer.Controls.Add(this.lblUnidadesPorVencer);
            this.panelPorVencer.Controls.Add(this.lblTituloPorVencer);
            this.panelPorVencer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPorVencer.Location = new System.Drawing.Point(740, 5);
            this.panelPorVencer.Margin = new System.Windows.Forms.Padding(5);
            this.panelPorVencer.Name = "panelPorVencer";
            this.panelPorVencer.Size = new System.Drawing.Size(235, 100);
            this.panelPorVencer.TabIndex = 3;
            // 
            // lblUnidadesPorVencer
            // 
            this.lblUnidadesPorVencer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUnidadesPorVencer.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblUnidadesPorVencer.ForeColor = System.Drawing.Color.White;
            this.lblUnidadesPorVencer.Location = new System.Drawing.Point(0, 23);
            this.lblUnidadesPorVencer.Name = "lblUnidadesPorVencer";
            this.lblUnidadesPorVencer.Size = new System.Drawing.Size(235, 77);
            this.lblUnidadesPorVencer.TabIndex = 1;
            this.lblUnidadesPorVencer.Text = "0";
            this.lblUnidadesPorVencer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTituloPorVencer
            // 
            this.lblTituloPorVencer.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTituloPorVencer.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTituloPorVencer.ForeColor = System.Drawing.Color.White;
            this.lblTituloPorVencer.Location = new System.Drawing.Point(0, 0);
            this.lblTituloPorVencer.Name = "lblTituloPorVencer";
            this.lblTituloPorVencer.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.lblTituloPorVencer.Size = new System.Drawing.Size(235, 23);
            this.lblTituloPorVencer.TabIndex = 0;
            this.lblTituloPorVencer.Text = "⚠️ Por Vencer (7 días)";
            this.lblTituloPorVencer.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabInventario);
            this.tabControl.Controls.Add(this.tabSolicitudes);
            this.tabControl.Controls.Add(this.tabDonaciones);
            this.tabControl.Controls.Add(this.tabAlertas);
            this.tabControl.Controls.Add(this.tabEstadisticas);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tabControl.Location = new System.Drawing.Point(0, 220);
            this.tabControl.Name = "tabControl";
            this.tabControl.Padding = new System.Drawing.Point(10, 5);
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1020, 480);
            this.tabControl.TabIndex = 2;
            // 
            // tabInventario
            // 
            this.tabInventario.Controls.Add(this.dgvInventario);
            this.tabInventario.Location = new System.Drawing.Point(4, 30);
            this.tabInventario.Name = "tabInventario";
            this.tabInventario.Padding = new System.Windows.Forms.Padding(10);
            this.tabInventario.Size = new System.Drawing.Size(1012, 446);
            this.tabInventario.TabIndex = 0;
            this.tabInventario.Text = "📦 Inventario por Tipo";
            this.tabInventario.UseVisualStyleBackColor = true;
            // 
            // dgvInventario
            // 
            this.dgvInventario.AllowUserToAddRows = false;
            this.dgvInventario.AllowUserToDeleteRows = false;
            this.dgvInventario.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvInventario.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvInventario.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvInventario.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInventario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInventario.EnableHeadersVisualStyles = false;
            this.dgvInventario.Location = new System.Drawing.Point(10, 10);
            this.dgvInventario.Name = "dgvInventario";
            this.dgvInventario.ReadOnly = true;
            this.dgvInventario.RowHeadersVisible = false;
            this.dgvInventario.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInventario.Size = new System.Drawing.Size(992, 426);
            this.dgvInventario.TabIndex = 0;
            // 
            // tabSolicitudes
            // 
            this.tabSolicitudes.Controls.Add(this.dgvSolicitudes);
            this.tabSolicitudes.Location = new System.Drawing.Point(4, 30);
            this.tabSolicitudes.Name = "tabSolicitudes";
            this.tabSolicitudes.Padding = new System.Windows.Forms.Padding(10);
            this.tabSolicitudes.Size = new System.Drawing.Size(1012, 446);
            this.tabSolicitudes.TabIndex = 1;
            this.tabSolicitudes.Text = "📋 Solicitudes Pendientes";
            this.tabSolicitudes.UseVisualStyleBackColor = true;
            // 
            // dgvSolicitudes
            // 
            this.dgvSolicitudes.AllowUserToAddRows = false;
            this.dgvSolicitudes.AllowUserToDeleteRows = false;
            this.dgvSolicitudes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSolicitudes.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSolicitudes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSolicitudes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSolicitudes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSolicitudes.EnableHeadersVisualStyles = false;
            this.dgvSolicitudes.Location = new System.Drawing.Point(10, 10);
            this.dgvSolicitudes.Name = "dgvSolicitudes";
            this.dgvSolicitudes.ReadOnly = true;
            this.dgvSolicitudes.RowHeadersVisible = false;
            this.dgvSolicitudes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSolicitudes.Size = new System.Drawing.Size(992, 426);
            this.dgvSolicitudes.TabIndex = 0;
            // 
            // tabDonaciones
            // 
            this.tabDonaciones.Controls.Add(this.dgvDonacionesRecientes);
            this.tabDonaciones.Location = new System.Drawing.Point(4, 30);
            this.tabDonaciones.Name = "tabDonaciones";
            this.tabDonaciones.Padding = new System.Windows.Forms.Padding(10);
            this.tabDonaciones.Size = new System.Drawing.Size(1012, 446);
            this.tabDonaciones.TabIndex = 2;
            this.tabDonaciones.Text = "💉 Donaciones Recientes";
            this.tabDonaciones.UseVisualStyleBackColor = true;
            // 
            // dgvDonacionesRecientes
            // 
            this.dgvDonacionesRecientes.AllowUserToAddRows = false;
            this.dgvDonacionesRecientes.AllowUserToDeleteRows = false;
            this.dgvDonacionesRecientes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDonacionesRecientes.BackgroundColor = System.Drawing.Color.White;
            this.dgvDonacionesRecientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDonacionesRecientes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDonacionesRecientes.Location = new System.Drawing.Point(10, 10);
            this.dgvDonacionesRecientes.Name = "dgvDonacionesRecientes";
            this.dgvDonacionesRecientes.ReadOnly = true;
            this.dgvDonacionesRecientes.RowHeadersVisible = false;
            this.dgvDonacionesRecientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDonacionesRecientes.Size = new System.Drawing.Size(992, 426);
            this.dgvDonacionesRecientes.TabIndex = 0;
            // 
            // tabAlertas
            // 
            this.tabAlertas.Controls.Add(this.lblSinAlertas);
            this.tabAlertas.Controls.Add(this.dgvAlertas);
            this.tabAlertas.Location = new System.Drawing.Point(4, 30);
            this.tabAlertas.Name = "tabAlertas";
            this.tabAlertas.Padding = new System.Windows.Forms.Padding(10);
            this.tabAlertas.Size = new System.Drawing.Size(1012, 446);
            this.tabAlertas.TabIndex = 3;
            this.tabAlertas.Text = "⚠️ Alertas";
            this.tabAlertas.UseVisualStyleBackColor = true;
            // 
            // lblSinAlertas
            // 
            this.lblSinAlertas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSinAlertas.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblSinAlertas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.lblSinAlertas.Location = new System.Drawing.Point(10, 10);
            this.lblSinAlertas.Name = "lblSinAlertas";
            this.lblSinAlertas.Size = new System.Drawing.Size(992, 426);
            this.lblSinAlertas.TabIndex = 1;
            this.lblSinAlertas.Text = "✓ No hay alertas de inventario";
            this.lblSinAlertas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSinAlertas.Visible = false;
            // 
            // dgvAlertas
            // 
            this.dgvAlertas.AllowUserToAddRows = false;
            this.dgvAlertas.AllowUserToDeleteRows = false;
            this.dgvAlertas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAlertas.BackgroundColor = System.Drawing.Color.White;
            this.dgvAlertas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAlertas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAlertas.Location = new System.Drawing.Point(10, 10);
            this.dgvAlertas.Name = "dgvAlertas";
            this.dgvAlertas.ReadOnly = true;
            this.dgvAlertas.RowHeadersVisible = false;
            this.dgvAlertas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAlertas.Size = new System.Drawing.Size(992, 426);
            this.dgvAlertas.TabIndex = 0;
            // 
            // tabEstadisticas
            // 
            this.tabEstadisticas.Controls.Add(this.dgvDonantesPorTipo);
            this.tabEstadisticas.Location = new System.Drawing.Point(4, 30);
            this.tabEstadisticas.Name = "tabEstadisticas";
            this.tabEstadisticas.Padding = new System.Windows.Forms.Padding(10);
            this.tabEstadisticas.Size = new System.Drawing.Size(1012, 446);
            this.tabEstadisticas.TabIndex = 4;
            this.tabEstadisticas.Text = "📊 Estadísticas de Donantes";
            this.tabEstadisticas.UseVisualStyleBackColor = true;
            // 
            // dgvDonantesPorTipo
            // 
            this.dgvDonantesPorTipo.AllowUserToAddRows = false;
            this.dgvDonantesPorTipo.AllowUserToDeleteRows = false;
            this.dgvDonantesPorTipo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDonantesPorTipo.BackgroundColor = System.Drawing.Color.White;
            this.dgvDonantesPorTipo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDonantesPorTipo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDonantesPorTipo.Location = new System.Drawing.Point(10, 10);
            this.dgvDonantesPorTipo.Name = "dgvDonantesPorTipo";
            this.dgvDonantesPorTipo.ReadOnly = true;
            this.dgvDonantesPorTipo.RowHeadersVisible = false;
            this.dgvDonantesPorTipo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDonantesPorTipo.Size = new System.Drawing.Size(992, 426);
            this.dgvDonantesPorTipo.TabIndex = 0;
            // 
            // FrmDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 700);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.panelEstadisticas);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmDashboard";
            this.Text = "Dashboard - Banco de Sangre";
            this.Load += new System.EventHandler(this.FrmDashboard_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelEstadisticas.ResumeLayout(false);
            this.tableLayoutEstadisticas.ResumeLayout(false);
            this.panelDonantes.ResumeLayout(false);
            this.panelDonaciones.ResumeLayout(false);
            this.panelSolicitudes.ResumeLayout(false);
            this.panelPorVencer.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabInventario.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventario)).EndInit();
            this.tabSolicitudes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSolicitudes)).EndInit();
            this.tabDonaciones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDonacionesRecientes)).EndInit();
            this.tabAlertas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlertas)).EndInit();
            this.tabEstadisticas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDonantesPorTipo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblUltimaActualizacion;
        private System.Windows.Forms.Button btnRefrescar;
        private System.Windows.Forms.Panel panelEstadisticas;
        private System.Windows.Forms.TableLayoutPanel tableLayoutEstadisticas;
        private System.Windows.Forms.Panel panelDonantes;
        private System.Windows.Forms.Label lblTotalDonantes;
        private System.Windows.Forms.Label lblTituloDonantes;
        private System.Windows.Forms.Panel panelDonaciones;
        private System.Windows.Forms.Label lblTotalDonaciones;
        private System.Windows.Forms.Label lblTituloDonaciones;
        private System.Windows.Forms.Panel panelSolicitudes;
        private System.Windows.Forms.Label lblSolicitudesPendientes;
        private System.Windows.Forms.Label lblTituloSolicitudes;
        private System.Windows.Forms.Panel panelPorVencer;
        private System.Windows.Forms.Label lblUnidadesPorVencer;
        private System.Windows.Forms.Label lblTituloPorVencer;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabInventario;
        private System.Windows.Forms.DataGridView dgvInventario;
        private System.Windows.Forms.TabPage tabSolicitudes;
        private System.Windows.Forms.DataGridView dgvSolicitudes;
        private System.Windows.Forms.TabPage tabDonaciones;
        private System.Windows.Forms.DataGridView dgvDonacionesRecientes;
        private System.Windows.Forms.TabPage tabAlertas;
        private System.Windows.Forms.DataGridView dgvAlertas;
        private System.Windows.Forms.Label lblSinAlertas;
        private System.Windows.Forms.TabPage tabEstadisticas;
        private System.Windows.Forms.DataGridView dgvDonantesPorTipo;
    }
}