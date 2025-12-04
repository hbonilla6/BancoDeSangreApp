using BancoDeSangreApp.Business;
using BancoDeSangreApp.Components;
using FontAwesome.Sharp;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;

namespace BancoDeSangreApp.Forms
{
    public partial class FrmMain : Form
    {
        private IconButton btnActivo;
        private IconButton btnAcercaDe;

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
        }

        private void ConfigurarPermisos()
        {
            var usuario = Program.UsuarioActual;

            // Limpiar botones y controles existentes del panelMenuScroll
            var controlesAEliminar = panelMenuScroll.Controls
                .Cast<Control>()
                .ToList();

            foreach (var control in controlesAEliminar)
            {
                panelMenuScroll.Controls.Remove(control);
                control.Dispose();
            }

            // Obtener categorías disponibles
            var categorias = MenuConfig.ObtenerCategoriasDisponibles(usuario);

            int posicionY = 10;

            foreach (var categoria in categorias)
            {
                // Crear panel contenedor para la categoría
                var panelCategoria = new Panel
                {
                    Name = $"panelCategoria_{categoria.Nombre}",
                    Location = new Point(0, posicionY),
                    Width = panelMenuScroll.Width - 20,
                    Height = 40,
                    Tag = false
                };

                // Botón de categoría (header colapsable) - AHORA ES IconButton
                var btnCategoria = new IconButton
                {
                    Text = $"  {categoria.Nombre.ToUpper()}",
                    IconChar = IconChar.ChevronRight,
                    IconColor = Color.FromArgb(189, 195, 199),
                    IconSize = 20,
                    ImageAlign = ContentAlignment.MiddleLeft,
                    Location = new Point(0, 0),
                    Width = panelMenuScroll.Width - 20,
                    Height = 40,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(189, 195, 199),
                    BackColor = Color.FromArgb(44, 62, 80),
                    TextAlign = ContentAlignment.MiddleLeft,
                    Padding = new Padding(35, 0, 0, 0),
                    TextImageRelation = TextImageRelation.ImageBeforeText,
                    Tag = categoria
                };
                btnCategoria.FlatAppearance.BorderSize = 0;

                // Efecto hover para categoría
                btnCategoria.MouseEnter += (s, e) =>
                {
                    btnCategoria.BackColor = Color.FromArgb(52, 73, 94);
                };
                btnCategoria.MouseLeave += (s, e) =>
                {
                    btnCategoria.BackColor = Color.FromArgb(44, 62, 80);
                };

                // Panel de contenido de formularios
                var panelFormularios = new Panel
                {
                    Name = $"panelFormularios_{categoria.Nombre}",
                    Location = new Point(0, 40),
                    Width = panelMenuScroll.Width - 20,
                    Height = 0,
                    Visible = false
                };

                // Agregar formularios al panel
                int alturaFormularios = 0;
                int posY = 0;
                foreach (var formulario in categoria.Formularios.OrderBy(f => f.Orden))
                {
                    // AHORA USAMOS IconButton
                    var btnFormulario = new IconButton
                    {
                        Name = $"btn{formulario.Nombre}",
                        Text = $"  {formulario.Texto}",
                        IconChar = ObtenerIconChar(formulario.Icono),
                        IconColor = Color.White,
                        IconSize = 24,
                        ImageAlign = ContentAlignment.MiddleLeft,
                        Location = new Point(0, posY),
                        Width = panelMenuScroll.Width - 20,
                        Height = 45,
                        FlatStyle = FlatStyle.Flat,
                        Font = new Font("Segoe UI", 10F),
                        ForeColor = Color.White,
                        BackColor = Color.FromArgb(37, 46, 59),
                        Padding = new Padding(45, 0, 0, 0),
                        TextAlign = ContentAlignment.MiddleLeft,
                        TextImageRelation = TextImageRelation.ImageBeforeText,
                        Tag = formulario.Nombre
                    };

                    btnFormulario.FlatAppearance.BorderSize = 0;
                    btnFormulario.Click += BtnFormulario_Click;
                    ConfigurarHoverBoton(btnFormulario);

                    panelFormularios.Controls.Add(btnFormulario);
                    posY += btnFormulario.Height;
                    alturaFormularios += btnFormulario.Height;
                }

                // Evento click para expandir/colapsar
                btnCategoria.Click += (s, e) =>
                {
                    bool estaExpandido = (bool)panelCategoria.Tag;

                    if (estaExpandido)
                    {
                        ColapsarCategoria(btnCategoria, panelFormularios, panelCategoria);
                    }
                    else
                    {
                        ExpandirCategoria(btnCategoria, panelFormularios, panelCategoria, alturaFormularios);
                    }
                };

                // Agregar controles al panel de categoría
                panelCategoria.Controls.Add(btnCategoria);
                panelCategoria.Controls.Add(panelFormularios);

                panelMenuScroll.Controls.Add(panelCategoria);
                panelCategoria.BringToFront();

                posicionY += panelCategoria.Height + 2;
            }

            AgregarBotonAcercaDe();
        }

        /// <summary>
        /// Convierte el nombre del icono FontAwesome a IconChar
        /// </summary>
        private IconChar ObtenerIconChar(string nombreIcono)
        {
            switch (nombreIcono)
            {
                case "fa-home": return IconChar.Home;
                case "fa-users": return IconChar.Users;
                case "fa-hand-holding-medical": return IconChar.HandHoldingMedical;
                case "fa-syringe": return IconChar.Syringe;
                case "fa-hospital": return IconChar.Hospital;
                case "fa-boxes": return IconChar.Boxes;
                case "fa-clipboard-list": return IconChar.ClipboardList;
                case "fa-chart-bar": return IconChar.ChartBar;
                case "fa-shield-alt": return IconChar.ShieldAlt;
                case "fa-lock": return IconChar.Lock;
                case "fa-user-cog": return IconChar.UserCog;
                case "fa-user-tag": return IconChar.UserTag;
                case "fa-cog": return IconChar.Cog;
                case "fa-info-circle": return IconChar.InfoCircle;
                case "fa-door-open": return IconChar.DoorOpen;
                default: return IconChar.Circle;
            }
        }

        private void ExpandirCategoria(IconButton btnCategoria, Panel panelFormularios, Panel panelCategoria, int alturaTotal)
        {
            // Cambiar icono a ChevronDown
            btnCategoria.IconChar = IconChar.ChevronDown;

            // Marcar como expandido
            panelCategoria.Tag = true;

            // Mostrar panel de formularios
            panelFormularios.Visible = true;
            panelFormularios.Height = 0;

            // Animación suave de expansión
            Timer timer = new Timer { Interval = 10 };
            int alturaObjetivo = alturaTotal;
            int paso = Math.Max(alturaObjetivo / 15, 3);

            timer.Tick += (s, e) =>
            {
                if (panelFormularios.Height < alturaObjetivo)
                {
                    panelFormularios.Height += paso;
                    if (panelFormularios.Height > alturaObjetivo)
                        panelFormularios.Height = alturaObjetivo;

                    panelCategoria.Height = btnCategoria.Height + panelFormularios.Height;
                    AjustarPosicionCategorias();
                }
                else
                {
                    panelFormularios.Height = alturaObjetivo;
                    panelCategoria.Height = btnCategoria.Height + alturaObjetivo;
                    timer.Stop();
                    timer.Dispose();
                    AjustarPosicionCategorias();
                }
            };

            timer.Start();
        }

        private void ColapsarCategoria(IconButton btnCategoria, Panel panelFormularios, Panel panelCategoria)
        {
            // Cambiar icono a ChevronRight
            btnCategoria.IconChar = IconChar.ChevronRight;

            // Marcar como colapsado
            panelCategoria.Tag = false;

            // Animación suave de colapso
            int alturaInicial = panelFormularios.Height;
            Timer timer = new Timer { Interval = 10 };
            int paso = Math.Max(alturaInicial / 15, 3);

            timer.Tick += (s, e) =>
            {
                if (panelFormularios.Height > paso)
                {
                    panelFormularios.Height -= paso;
                    panelCategoria.Height = btnCategoria.Height + panelFormularios.Height;
                    AjustarPosicionCategorias();
                }
                else
                {
                    panelFormularios.Height = 0;
                    panelFormularios.Visible = false;
                    panelCategoria.Height = btnCategoria.Height;
                    timer.Stop();
                    timer.Dispose();
                    AjustarPosicionCategorias();
                }
            };

            timer.Start();
        }

        private void AjustarPosicionCategorias()
        {
            int posicionY = 10;

            var panelesCategorias = panelMenuScroll.Controls
                .OfType<Panel>()
                .Where(p => p.Name != null && p.Name.StartsWith("panelCategoria_"))
                .ToList();

            panelesCategorias.Sort((a, b) =>
            {
                int indexA = panelMenuScroll.Controls.IndexOf(a);
                int indexB = panelMenuScroll.Controls.IndexOf(b);
                return indexB.CompareTo(indexA);
            });

            foreach (var panel in panelesCategorias)
            {
                panel.Location = new Point(0, posicionY);
                posicionY += panel.Height + 2;
            }
        }

        private void AgregarBotonAcercaDe()
        {
            var btnExistente = panelMenu.Controls
                .OfType<IconButton>()
                .FirstOrDefault(b => b.Name == "btnAcercaDe");

            if (btnExistente != null)
                return;

            btnAcercaDe = new IconButton
            {
                Name = "btnAcercaDe",
                Text = "  Acerca de",
                IconChar = IconChar.InfoCircle,
                IconColor = Color.White,
                IconSize = 24,
                ImageAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Bottom,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.White,
                Height = 50,
                Padding = new Padding(15, 0, 0, 0),
                TextAlign = ContentAlignment.MiddleLeft,
                TextImageRelation = TextImageRelation.ImageBeforeText
            };

            btnAcercaDe.FlatAppearance.BorderSize = 0;
            btnAcercaDe.Click += btnAcercaDe_Click;
            ConfigurarHoverBoton(btnAcercaDe);

            panelMenu.Controls.Add(btnAcercaDe);
            btnAcercaDe.BringToFront();
        }

        private async void BtnFormulario_Click(object sender, EventArgs e)
        {
            if (sender is IconButton btn && btn.Tag is string nombreFormulario)
            {
                if (!MenuConfig.TieneAcceso(Program.UsuarioActual, nombreFormulario))
                {
                    MostrarAccesoDenegado();
                    return;
                }

                ActivarBoton(btn);
                await AbrirFormularioPorNombre(nombreFormulario);
            }
        }

        private async Task AbrirFormularioPorNombre(string nombreFormulario)
        {
            try
            {
                panelContenedor.ShowLoading($"Cargando {nombreFormulario}...");
                await Task.Delay(100);

                var tipo = Type.GetType($"BancoDeSangreApp.Forms.{nombreFormulario}");

                if (tipo == null)
                {
                    throw new Exception($"No se encontró el formulario: {nombreFormulario}");
                }

                Form formulario = (Form)Activator.CreateInstance(tipo);
                formulario.TopLevel = false;
                formulario.FormBorderStyle = FormBorderStyle.None;
                formulario.Dock = DockStyle.Fill;

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
                    $"Error al cargar el formulario:\n{ex.Message}\n\nStack Trace:\n{ex.StackTrace}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void ConfigurarEventosMenu()
        {
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

        private void ConfigurarHoverBoton(IconButton btn)
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
            var categorias = MenuConfig.ObtenerCategoriasDisponibles(Program.UsuarioActual);

            if (categorias.Any() && categorias[0].Formularios.Any())
            {
                await Task.Delay(100);

                var primerPanelCategoria = panelMenuScroll.Controls
                    .OfType<Panel>()
                    .Where(p => p.Name != null && p.Name.StartsWith("panelCategoria_"))
                    .FirstOrDefault();

                if (primerPanelCategoria != null)
                {
                    var btnCategoria = primerPanelCategoria.Controls.OfType<IconButton>().FirstOrDefault();
                    if (btnCategoria != null && btnCategoria.IconChar == IconChar.ChevronRight)
                    {
                        btnCategoria.PerformClick();
                    }
                }

                var primerFormulario = categorias[0].Formularios[0].Nombre;
                await AbrirFormularioPorNombre(primerFormulario);

                await Task.Delay(200);

                var primerBoton = panelMenuScroll.Controls
                    .OfType<Panel>()
                    .Where(p => p.Name != null && p.Name.StartsWith("panelCategoria_"))
                    .SelectMany(p => p.Controls.OfType<Panel>())
                    .SelectMany(p => p.Controls.OfType<IconButton>())
                    .FirstOrDefault(b => b.Tag is string tag && tag == primerFormulario);

                if (primerBoton != null)
                {
                    btnActivo = primerBoton;
                    primerBoton.BackColor = Color.FromArgb(51, 122, 183);
                }
            }
        }

        private void ActivarBoton(IconButton boton)
        {
            if (btnActivo != null)
            {
                btnActivo.BackColor = Color.FromArgb(37, 46, 59);
            }

            btnActivo = boton;
            boton.BackColor = Color.FromArgb(51, 122, 183);
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
                try
                {
                    var auditoriaBLL = new AuditoriaBLL();
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
    }
}