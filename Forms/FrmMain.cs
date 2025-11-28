using BancoDeSangreApp.Components;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BancoDeSangreApp.Forms
{
    public partial class FrmMain : Form
    {
        private Button btnActivo; // Rastrea el botón actualmente activo

        public FrmMain()
        {
            InitializeComponent();
            ConfigurarFormulario();
        }

        private void ConfigurarFormulario()
        {
            // Configurar información del usuario
            lblUsuario.Text = $"👤 {Program.UsuarioActual.NombreCompleto}";
            lblEntidad.Text = $"🏥 {Program.UsuarioActual.NombreEntidad}";

            // Configurar estilos de botones al pasar el mouse
            ConfigurarEventosMenu();
        }

        private void ConfigurarEventosMenu()
        {
            // Configurar todos los botones del menú
            ConfigurarHoverBoton(btnDashboard);
            ConfigurarHoverBoton(btnDonantes);
            ConfigurarHoverBoton(btnDonaciones);
            ConfigurarHoverBoton(btnInventario);
            ConfigurarHoverBoton(btnSolicitudes);
            ConfigurarHoverBoton(btnReportes);

            // Configurar botón de cerrar sesión
            if (btnCerrarSesion != null)
            {
                btnCerrarSesion.MouseEnter += (s, e) =>
                {
                    btnCerrarSesion.BackColor = Color.FromArgb(192, 57, 43);
                };

                btnCerrarSesion.MouseLeave += (s, e) =>
                {
                    btnCerrarSesion.BackColor = Color.FromArgb(37, 46, 59);
                };
            }
        }

        /// <summary>
        /// Configura los eventos hover para un botón del menú
        /// </summary>
        private void ConfigurarHoverBoton(Button btn)
        {
            if (btn == null) return;

            btn.MouseEnter += (s, e) =>
            {
                if (btn != btnActivo)
                {
                    btn.BackColor = Color.FromArgb(70, 130, 180);
                }
            };

            btn.MouseLeave += (s, e) =>
            {
                if (btn != btnActivo)
                {
                    btn.BackColor = Color.FromArgb(37, 46, 59);
                }
            };
        }

        private async void FrmMain_Load(object sender, EventArgs e)
        {
            // Cargar el dashboard por defecto
            await AbrirFormularioAsync<FrmDashboard>("Cargando Dashboard...");
            btnActivo = btnDashboard;
        }

        /// <summary>
        /// Método genérico para abrir formularios con loading
        /// </summary>
        private async Task AbrirFormularioAsync<T>(string mensajeCarga = "Cargando...")
            where T : Form, new()
        {
            try
            {
                // Mostrar loading
                panelContenedor.ShowLoading(mensajeCarga);

                // Crear formulario en segundo plano
                Form formulario = await Task.Run(() =>
                {
                    T frm = new T
                    {
                        TopLevel = false,
                        FormBorderStyle = FormBorderStyle.None,
                        Dock = DockStyle.Fill
                    };
                    return frm;
                });

                // Limpiar formularios anteriores
                foreach (Control control in panelContenedor.Controls)
                {
                    if (control is Form f)
                    {
                        f.Close();
                        f.Dispose();
                    }
                }
                panelContenedor.Controls.Clear();

                // Agregar y mostrar nuevo formulario
                panelContenedor.Controls.Add(formulario);
                formulario.Show();

                // Ocultar loading
                panelContenedor.HideLoading();
            }
            catch (Exception ex)
            {
                panelContenedor.HideLoading();
                MessageBox.Show(
                    $"Error al cargar el formulario:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Cambia el color del botón activo
        /// </summary>
        private void ActivarBoton(Button boton)
        {
            // Resetear botón anterior
            if (btnActivo != null)
            {
                btnActivo.BackColor = Color.FromArgb(37, 46, 59);
            }

            // Activar nuevo botón
            btnActivo = boton;
            boton.BackColor = Color.FromArgb(51, 122, 183);
        }

        // ==================== EVENTOS DE BOTONES ====================

        private async void btnDashboard_Click(object sender, EventArgs e)
        {
            ActivarBoton(btnDashboard);
            await AbrirFormularioAsync<FrmDashboard>("Cargando Dashboard...");
        }

        private async void btnDonantes_Click(object sender, EventArgs e)
        {
            ActivarBoton(btnDonantes);
            FrmDonantes frmDonantes = new FrmDonantes();
            frmDonantes.Show();

        }

        private async void btnDonaciones_Click(object sender, EventArgs e)
        {
            ActivarBoton(btnDonaciones);
            FrmDonaciones frmDonaciones = new FrmDonaciones();
            frmDonaciones.Show();

        }

        private async void btnInventario_Click(object sender, EventArgs e)
        {
            ActivarBoton(btnInventario);
            await AbrirFormularioAsync<FrmInventario>("Cargando Inventario...");
        }

        private async void btnSolicitudes_Click(object sender, EventArgs e)
        {
            ActivarBoton(btnSolicitudes);
            await AbrirFormularioAsync<FrmSolicitudesMedicas>("Cargando Solicitudes...");
        }

        private async void btnReportes_Click(object sender, EventArgs e)
        {
            ActivarBoton(btnReportes);
            // TODO: Cambiar por el formulario real cuando esté disponible
            MessageBox.Show(
                "Módulo de Reportes\n\n" +
                "Este módulo permite generar:\n" +
                "• Reportes de donaciones\n" +
                "• Estadísticas de inventario\n" +
                "• Gráficos de tendencias\n" +
                "• Exportación a PDF/Excel",
                "Información",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
            // Descomentar cuando tengas el formulario:
            // await AbrirFormularioAsync<FrmReportes>("Generando Reportes...");
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
                "¿Está seguro que desea cerrar sesión?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resultado == DialogResult.Yes)
            {
                // Limpiar usuario actual
                Program.UsuarioActual = null;

                // Cerrar formulario principal
                this.Close();

                // Mostrar login nuevamente
                FrmLogin login = new FrmLogin();
                if (login.ShowDialog() == DialogResult.OK)
                {
                    Program.UsuarioActual = login.UsuarioAutenticado;

                    // Reabrir formulario principal
                    FrmMain main = new FrmMain();
                    main.Show();
                }
                else
                {
                    Application.Exit();
                }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Limpiar recursos
            foreach (Control control in panelContenedor.Controls)
            {
                if (control is Form f)
                {
                    f.Close();
                    f.Dispose();
                }
            }
            base.OnFormClosing(e);
        }
    }
}