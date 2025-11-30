using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BancoDeSangreApp.Forms
{
    public partial class FrmEquipo : Form
    {
        public FrmEquipo()
        {
            InitializeComponent();
            ConfigurarFormulario();
        }

        private void ConfigurarFormulario()
        {
            this.Text = "Equipo de Desarrollo - Banco de Sangre";
            this.Size = new Size(700, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;

            CrearInterfaz();
        }

        private void CrearInterfaz()
        {
            // Panel superior con logo y título
            Panel panelHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 120,
                BackColor = Color.FromArgb(51, 122, 183)
            };

            Label lblTitulo = new Label
            {
                Text = "BANCO DE SANGRE",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = false,
                Size = new Size(700, 50),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(0, 20)
            };

            Label lblSubtitulo = new Label
            {
                Text = "Sistema de Gestión Hospitalaria",
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.White,
                AutoSize = false,
                Size = new Size(700, 30),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(0, 70)
            };

            panelHeader.Controls.Add(lblTitulo);
            panelHeader.Controls.Add(lblSubtitulo);
            this.Controls.Add(panelHeader);

            // Panel de contenido
            Panel panelContenido = new Panel
            {
                Location = new Point(20, 140),
                Size = new Size(640, 350),
                AutoScroll = true
            };

            // Información del proyecto
            Label lblInfoProyecto = new Label
            {
                Text = "INFORMACIÓN DEL PROYECTO",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 122, 183),
                AutoSize = false,
                Size = new Size(620, 30),
                Location = new Point(0, 0)
            };

            Label lblDescripcion = new Label
            {
                Text = "Sistema desarrollado para la gestión integral de un Banco de Sangre,\n" +
                       "incluyendo registro de donantes, control de inventario, solicitudes médicas\n" +
                       "y generación de reportes.",
                Font = new Font("Segoe UI", 9),
                AutoSize = false,
                Size = new Size(620, 60),
                Location = new Point(10, 35)
            };

            Label lblVersion = new Label
            {
                Text = "Versión: 1.0.0\n" +
                       "Fecha: Noviembre 2024\n" +
                       "Tecnología: C# .NET Framework 4.7.2 | SQL Server",
                Font = new Font("Segoe UI", 9),
                AutoSize = false,
                Size = new Size(620, 60),
                Location = new Point(10, 100)
            };

            // Sección de equipo de desarrollo
            Label lblEquipo = new Label
            {
                Text = "EQUIPO DE DESARROLLO",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 122, 183),
                AutoSize = false,
                Size = new Size(620, 30),
                Location = new Point(0, 170)
            };

            // AQUÍ AGREGAR LOS INTEGRANTES REALES DEL EQUIPO
            var integrantes = new[]
            {
                new { Carnet = "BF100124", Nombre = "Hector Edenilson Bonilla Flores" },
                new { Carnet = "GG100924", Nombre = "Gerardo Rubén González González" },
                new { Carnet = "RC100223", Nombre = "Daniel Alonso Rodriguez Cornejo" },
            };

            int yPosition = 210;
            foreach (var integrante in integrantes)
            {
                Panel panelIntegrante = new Panel
                {
                    Location = new Point(10, yPosition),
                    Size = new Size(600, 35),
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = Color.FromArgb(240, 244, 248)
                };

                Label lblCarnet = new Label
                {
                    Text = $"📌 {integrante.Carnet}",
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    ForeColor = Color.FromArgb(51, 122, 183),
                    AutoSize = false,
                    Size = new Size(150, 30),
                    Location = new Point(10, 5),
                    TextAlign = ContentAlignment.MiddleLeft
                };

                Label lblNombre = new Label
                {
                    Text = integrante.Nombre,
                    Font = new Font("Segoe UI", 9),
                    AutoSize = false,
                    Size = new Size(420, 30),
                    Location = new Point(170, 5),
                    TextAlign = ContentAlignment.MiddleLeft
                };

                panelIntegrante.Controls.Add(lblCarnet);
                panelIntegrante.Controls.Add(lblNombre);
                panelContenido.Controls.Add(panelIntegrante);

                yPosition += 40;
            }

            panelContenido.Controls.Add(lblInfoProyecto);
            panelContenido.Controls.Add(lblDescripcion);
            panelContenido.Controls.Add(lblVersion);
            panelContenido.Controls.Add(lblEquipo);

            this.Controls.Add(panelContenido);

            // Botón cerrar
            Button btnCerrar = new Button
            {
                Text = "Cerrar",
                Size = new Size(100, 35),
                Location = new Point(570, 510),
                BackColor = Color.FromArgb(51, 122, 183),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };

            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.Click += (s, e) => this.Close();

            this.Controls.Add(btnCerrar);
        }

        private void FrmEquipo_Load(object sender, EventArgs e)
        {
            // Evento Load
        }

        private void FrmEquipo_Load_1(object sender, EventArgs e)
        {

        }
    }
}