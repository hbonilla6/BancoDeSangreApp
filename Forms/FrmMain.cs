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
            // TODO: Cambiar por el formulario real cuando esté disponible
            MessageBox.Show(
                "Módulo de Donantes\n\n" +
                "Este módulo permite gestionar:\n" +
                "• Registro de nuevos donantes\n" +
                "• Búsqueda y edición de donantes\n" +
                "• Historial de donaciones\n" +
                "• Estado de elegibilidad",
                "Información",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
            // Descomentar cuando tengas el formulario:
            // await AbrirFormularioAsync<FrmDonantes>("Cargando Donantes...");
        }

        private async void btnDonaciones_Click(object sender, EventArgs e)
        {
            ActivarBoton(btnDonaciones);
            //FrmDonaciones frmDonaciones = new FrmDonaciones();
            //frmDonaciones.Show();
            await AbrirFormularioAsync<FrmDonaciones>("Cargando Donaciones...");

        }

        private async void btnInventario_Click(object sender, EventArgs e)
        {
            ActivarBoton(btnInventario);
            // TODO: Cambiar por el formulario real cuando esté disponible
            MessageBox.Show(
                "Módulo de Inventario\n\n" +
                "Este módulo permite gestionar:\n" +
                "• Stock por tipo de sangre\n" +
                "• Alertas de inventario bajo\n" +
                "• Unidades próximas a vencer\n" +
                "• Movimientos de entrada/salida",
                "Información",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
            // Descomentar cuando tengas el formulario:
            // await AbrirFormularioAsync<FrmInventario>("Cargando Inventario...");
        }

        private async void btnSolicitudes_Click(object sender, EventArgs e)
        {
            ActivarBoton(btnSolicitudes);
            // TODO: Cambiar por el formulario real cuando esté disponible
            MessageBox.Show(
                "Módulo de Solicitudes Médicas\n\n" +
                "Este módulo permite gestionar:\n" +
                "• Crear nuevas solicitudes\n" +
                "• Asignar prioridades\n" +
                "• Aprobar o rechazar solicitudes\n" +
                "• Historial de solicitudes",
                "Información",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
            // Descomentar cuando tengas el formulario:
            // await AbrirFormularioAsync<FrmSolicitudes>("Cargando Solicitudes...");
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