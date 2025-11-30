using BancoDeSangreApp.Business;
using BancoDeSangreApp.Components;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BancoDeSangreApp.Forms
{
    public partial class FrmMain : Form
    {
        private Button btnActivo;
        private Button btnAcercaDe; // Nuevo botón para equipo

        public FrmMain()
        {
            InitializeComponent();
            ConfigurarFormulario();
            ConfigurarPermisos();
        }

        private void ConfigurarFormulario()
        {
            lblUsuario.Text = $"👤 {Program.UsuarioActual.NombreCompleto}";
            lblEntidad.Text = $"🏥 {Program.UsuarioActual.NombreEntidad}";

            // Agregar label de rol
            Label lblRol = new Label
            {
                Text = $"🔐 {Program.UsuarioActual.RolesTexto}",
                Dock = DockStyle.Bottom,
                Font = new Font("Segoe UI", 7.5F),
                ForeColor = Color.FromArgb(189, 195, 199),
                Padding = new Padding(15, 0, 15, 5),
                AutoSize = false,
                Height = 20
            };
            panelUsuario.Controls.Add(lblRol);

            ConfigurarEventosMenu();
            AgregarBotonAcercaDe();
        }

        private void AgregarBotonAcercaDe()
        {
            btnAcercaDe = new Button
            {
                Text = "ℹ️  Acerca de",
                Dock = DockStyle.Bottom,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.White,
                Height = 50,
                Padding = new Padding(15, 0, 0, 0),
                TextAlign = ContentAlignment.MiddleLeft
            };

            btnAcercaDe.FlatAppearance.BorderSize = 0;
            btnAcercaDe.Click += btnAcercaDe_Click;

            ConfigurarHoverBoton(btnAcercaDe);

            // Insertar antes del botón cerrar sesión
            panelMenu.Controls.Add(btnAcercaDe);
            btnAcercaDe.BringToFront();
        }

        private void ConfigurarPermisos()
        {
            var usuario = Program.UsuarioActual;

            // Ocultar/mostrar botones según permisos
            btnDashboard.Visible = PermisosBLL.TienePermiso(usuario, "Dashboard.Ver");
            btnDonantes.Visible = PermisosBLL.TienePermiso(usuario, "Donantes.Ver");
            btnDonaciones.Visible = PermisosBLL.TienePermiso(usuario, "Donaciones.Ver");
            btnInventario.Visible = PermisosBLL.TienePermiso(usuario, "Inventario.Ver");
            btnSolicitudes.Visible = PermisosBLL.TienePermiso(usuario, "Solicitudes.Ver");
            btnReportes.Visible = PermisosBLL.TienePermiso(usuario, "Reportes.Ver");

            // Agregar botón de auditoría si tiene permiso
            if (PermisosBLL.TienePermiso(usuario, "Auditoria.Ver"))
            {
                AgregarBotonAuditoria();
            }

            // Reorganizar posiciones de botones visibles
            ReorganizarBotonesMenu();
        }

        private void AgregarBotonAuditoria()
        {
            Button btnAuditoria = new Button
            {
                Name = "btnAuditoria",
                Text = "🔒  Auditoría",
                Dock = DockStyle.Top,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.White,
                Height = 45,
                Padding = new Padding(15, 0, 0, 0),
                TextAlign = ContentAlignment.MiddleLeft
            };

            btnAuditoria.FlatAppearance.BorderSize = 0;
            btnAuditoria.Click += async (s, e) =>
            {
                ActivarBoton(btnAuditoria);
                await AbrirFormularioAsync<FrmAuditoria>("Cargando Auditoría...");
            };

            ConfigurarHoverBoton(btnAuditoria);

            // Insertar después de reportes
            panelMenu.Controls.Add(btnAuditoria);
            btnAuditoria.BringToFront();
        }

        private void ReorganizarBotonesMenu()
        {
            // Obtener todos los botones visibles del menú
            var botonesMenu = new System.Collections.Generic.List<Button>();

            foreach (Control control in panelMenu.Controls)
            {
                if (control is Button btn && btn.Visible &&
                    btn != btnCerrarSesion && btn != btnAcercaDe)
                {
                    botonesMenu.Add(btn);
                }
            }

            // Reordenar de arriba hacia abajo
            int topPosition = panelUsuario.Bottom;
            foreach (var btn in botonesMenu)
            {
                btn.Top = topPosition;
                topPosition = btn.Bottom;
            }
        }

        private void ConfigurarEventosMenu()
        {
            ConfigurarHoverBoton(btnDashboard);
            ConfigurarHoverBoton(btnDonantes);
            ConfigurarHoverBoton(btnDonaciones);
            ConfigurarHoverBoton(btnInventario);
            ConfigurarHoverBoton(btnSolicitudes);
            ConfigurarHoverBoton(btnReportes);

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
            // Cargar el primer formulario disponible
            if (btnDashboard.Visible)
            {
                await AbrirFormularioAsync<FrmDashboard>("Cargando Dashboard...");
                btnActivo = btnDashboard;
            }
            else if (btnInventario.Visible)
            {
                await AbrirFormularioAsync<FrmInventario>("Cargando Inventario...");
                btnActivo = btnInventario;
            }
            else if (btnSolicitudes.Visible)
            {
                await AbrirFormularioAsync<FrmSolicitudesMedicas>("Cargando Solicitudes...");
                btnActivo = btnSolicitudes;
            }
        }

        private async Task AbrirFormularioAsync<T>(string mensajeCarga = "Cargando...")
            where T : Form, new()
        {
            try
            {
                panelContenedor.ShowLoading(mensajeCarga);

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

                foreach (Control control in panelContenedor.Controls)
                {
                    if (control is Form f)
                    {
                        f.Close();
                        f.Dispose();
                    }
                }
                panelContenedor.Controls.Clear();

                panelContenedor.Controls.Add(formulario);
                formulario.Show();

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

        private void ActivarBoton(Button boton)
        {
            if (btnActivo != null)
            {
                btnActivo.BackColor = Color.FromArgb(37, 46, 59);
            }

            btnActivo = boton;
            boton.BackColor = Color.FromArgb(51, 122, 183);
        }

        // ==================== EVENTOS DE BOTONES ====================

        private async void btnDashboard_Click(object sender, EventArgs e)
        {
            if (!PermisosBLL.TienePermiso(Program.UsuarioActual, "Dashboard.Ver"))
            {
                MostrarAccesoDenegado();
                return;
            }

            ActivarBoton(btnDashboard);
            await AbrirFormularioAsync<FrmDashboard>("Cargando Dashboard...");
        }

        private async void btnDonantes_Click(object sender, EventArgs e)
        {
            if (!PermisosBLL.TienePermiso(Program.UsuarioActual, "Donantes.Ver"))
            {
                MostrarAccesoDenegado();
                return;
            }

            ActivarBoton(btnDonantes);
            await AbrirFormularioAsync<FrmDonantes>("Cargando Donantes...");
        }
        private async void btnCentros_Click(object sender, EventArgs e)
        {
            ActivarBoton(btnCentros);
            await AbrirFormularioAsync<FrmCentrosRecoleccion>("Cargando Centros...");
        }

        private async void btnDonaciones_Click(object sender, EventArgs e)
        {
            if (!PermisosBLL.TienePermiso(Program.UsuarioActual, "Donaciones.Ver"))
            {
                MostrarAccesoDenegado();
                return;
            }

            ActivarBoton(btnDonaciones);
            await AbrirFormularioAsync<FrmDonaciones>("Cargando Centros...");
        }

        private async void btnInventario_Click(object sender, EventArgs e)
        {
            if (!PermisosBLL.TienePermiso(Program.UsuarioActual, "Inventario.Ver"))
            {
                MostrarAccesoDenegado();
                return;
            }

            ActivarBoton(btnInventario);
            await AbrirFormularioAsync<FrmInventario>("Cargando Inventario...");
        }

        private async void btnSolicitudes_Click(object sender, EventArgs e)
        {
            if (!PermisosBLL.TienePermiso(Program.UsuarioActual, "Solicitudes.Ver"))
            {
                MostrarAccesoDenegado();
                return;
            }

            ActivarBoton(btnSolicitudes);
            await AbrirFormularioAsync<FrmSolicitudesMedicas>("Cargando Solicitudes...");
        }

        private async void btnReportes_Click(object sender, EventArgs e)
        {
            if (!PermisosBLL.TienePermiso(Program.UsuarioActual, "Reportes.Ver"))
            {
                MostrarAccesoDenegado();
                return;
            }

            ActivarBoton(btnReportes);
            await AbrirFormularioAsync<FrmReportes>("Generando Reportes...");
        }

        private void btnAcercaDe_Click(object sender, EventArgs e)
        {
            FrmEquipo frmAcerca = new FrmEquipo();
            frmAcerca.ShowDialog();
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
                // Registrar cierre de sesión en auditoría
                try
                {
                    var auditoriaBLL = new AuditoriaBLL();
                    // Aquí podrías llamar a un método de registro
                }
                catch { }

                Program.UsuarioActual = null;
                this.Close();

                FrmLogin login = new FrmLogin();
                if (login.ShowDialog() == DialogResult.OK)
                {
                    Program.UsuarioActual = login.UsuarioAutenticado;
                    FrmMain main = new FrmMain();
                    main.Show();
                }
                else
                {
                    Application.Exit();
                }
            }
        }

        private void MostrarAccesoDenegado()
        {
            MessageBox.Show(
                "No tiene permisos para acceder a esta sección.\n\n" +
                $"Su rol actual: {Program.UsuarioActual.RolesTexto}",
                "Acceso Denegado",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
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

        private void panelContenedor_Paint(object sender, PaintEventArgs e)
        {
        }

        private void FrmMain_Load_1(object sender, EventArgs e)
        {

        }
    }
}