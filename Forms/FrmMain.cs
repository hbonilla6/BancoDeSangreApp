using BancoDeSangreApp.Business;
using BancoDeSangreApp.Components;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;

namespace BancoDeSangreApp.Forms
{
    public partial class FrmMain : Form
    {
        private Button btnActivo;
        private Button btnAcercaDe;

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

            int posicionY = 10; // Empieza desde arriba del panel scroll

            foreach (var categoria in categorias)
            {
                // Crear panel contenedor para la categoría
                var panelCategoria = new Panel
                {
                    Name = $"panelCategoria_{categoria.Nombre}",
                    Location = new Point(0, posicionY),
                    Width = panelMenuScroll.Width - 20, // Restar espacio del scroll
                    Height = 40,
                    Tag = false
                };

                // Botón de categoría (header colapsable)
                var btnCategoria = new Button
                {
                    Text = $"▶  {categoria.Nombre.ToUpper()}",
                    Location = new Point(0, 0),
                    Width = panelMenuScroll.Width - 20,
                    Height = 40,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(189, 195, 199),
                    BackColor = Color.FromArgb(44, 62, 80),
                    TextAlign = ContentAlignment.MiddleLeft,
                    Padding = new Padding(15, 0, 0, 0),
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
                    var btnFormulario = new Button
                    {
                        Name = $"btn{formulario.Nombre}",
                        Text = $"{formulario.Icono}  {formulario.Texto}",
                        Location = new Point(0, posY),
                        Width = panelMenuScroll.Width - 20,
                        Height = 45,
                        FlatStyle = FlatStyle.Flat,
                        Font = new Font("Segoe UI", 10F),
                        ForeColor = Color.White,
                        BackColor = Color.FromArgb(37, 46, 59),
                        Padding = new Padding(35, 0, 0, 0),
                        TextAlign = ContentAlignment.MiddleLeft,
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

        private void ExpandirCategoria(Button btnCategoria, Panel panelFormularios, Panel panelCategoria, int alturaTotal)
        {
            // Cambiar icono
            btnCategoria.Text = btnCategoria.Text.Replace("▶", "▼");

            // Marcar como expandido ANTES de la animación
            panelCategoria.Tag = true;

            // Mostrar panel de formularios
            panelFormularios.Visible = true;
            panelFormularios.Height = 0;

            // Animación suave de expansión
            Timer timer = new Timer { Interval = 10 };
            int alturaObjetivo = alturaTotal;
            int paso = Math.Max(alturaObjetivo / 15, 3); // Mínimo 3px por paso

            timer.Tick += (s, e) =>
            {
                if (panelFormularios.Height < alturaObjetivo)
                {
                    panelFormularios.Height += paso;
                    if (panelFormularios.Height > alturaObjetivo)
                        panelFormularios.Height = alturaObjetivo;

                    // Actualizar altura del panel contenedor
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

        private void ColapsarCategoria(Button btnCategoria, Panel panelFormularios, Panel panelCategoria)
        {
            // Cambiar icono
            btnCategoria.Text = btnCategoria.Text.Replace("▼", "▶");

            // Marcar como colapsado ANTES de la animación
            panelCategoria.Tag = false;

            // Animación suave de colapso
            int alturaInicial = panelFormularios.Height;
            Timer timer = new Timer { Interval = 10 };
            int paso = Math.Max(alturaInicial / 15, 3); // Mínimo 3px por paso

            timer.Tick += (s, e) =>
            {
                if (panelFormularios.Height > paso)
                {
                    panelFormularios.Height -= paso;

                    // Actualizar altura del panel contenedor
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
            // Reposicionar todas las categorías después de expansión/colapso
            int posicionY = 10; // CAMBIO: Empieza desde el inicio del panelMenuScroll

            var panelesCategorias = panelMenuScroll.Controls // CAMBIO: panelMenuScroll
                .OfType<Panel>()
                .Where(p => p.Name != null && p.Name.StartsWith("panelCategoria_"))
                .ToList();

            // Ordenar por el orden original
            panelesCategorias.Sort((a, b) =>
            {
                int indexA = panelMenuScroll.Controls.IndexOf(a); // CAMBIO: panelMenuScroll
                int indexB = panelMenuScroll.Controls.IndexOf(b); // CAMBIO: panelMenuScroll
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
            // Buscar si ya existe
            var btnExistente = panelMenu.Controls // CAMBIO: Ahora en panelMenu, no panelMenuScroll
                .OfType<Button>()
                .FirstOrDefault(b => b.Name == "btnAcercaDe");

            if (btnExistente != null)
                return;

            btnAcercaDe = new Button
            {
                Name = "btnAcercaDe",
                Text = "ℹ️  Acerca de",
                Dock = DockStyle.Bottom, // FIJO en la parte inferior
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

            panelMenu.Controls.Add(btnAcercaDe); // CAMBIO: En panelMenu
            btnAcercaDe.BringToFront(); // Lo coloca antes de btnCerrarSesion
        }

        private async void BtnFormulario_Click(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is string nombreFormulario)
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

                var tipo = Type.GetType($"BancoDeSangreApp.Forms.{nombreFormulario}");

                if (tipo == null)
                {
                    throw new Exception($"No se encontró el formulario: {nombreFormulario}");
                }

                Form formulario = await Task.Run(() =>
                {
                    Form frm = (Form)Activator.CreateInstance(tipo);
                    frm.TopLevel = false;
                    frm.FormBorderStyle = FormBorderStyle.None;
                    frm.Dock = DockStyle.Fill;
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
            // Obtener el primer formulario disponible
            var categorias = MenuConfig.ObtenerCategoriasDisponibles(Program.UsuarioActual);

            if (categorias.Any() && categorias[0].Formularios.Any())
            {
                // Esperar un poco para que los controles se rendericen
                await Task.Delay(100);

                // Expandir automáticamente la primera categoría
                var primerPanelCategoria = panelMenuScroll.Controls // CAMBIO: panelMenuScroll
                    .OfType<Panel>()
                    .Where(p => p.Name != null && p.Name.StartsWith("panelCategoria_"))
                    .FirstOrDefault();

                if (primerPanelCategoria != null)
                {
                    var btnCategoria = primerPanelCategoria.Controls.OfType<Button>().FirstOrDefault();
                    if (btnCategoria != null && btnCategoria.Text.Contains("▶"))
                    {
                        // Simular click para expandir
                        btnCategoria.PerformClick();
                    }
                }

                // Cargar el primer formulario
                var primerFormulario = categorias[0].Formularios[0].Nombre;
                await AbrirFormularioPorNombre(primerFormulario);

                // Activar el primer botón
                await Task.Delay(200);

                var primerBoton = panelMenuScroll.Controls // CAMBIO: panelMenuScroll
                    .OfType<Panel>()
                    .Where(p => p.Name != null && p.Name.StartsWith("panelCategoria_"))
                    .SelectMany(p => p.Controls.OfType<Panel>())
                    .SelectMany(p => p.Controls.OfType<Button>())
                    .FirstOrDefault(b => b.Tag is string tag && tag == primerFormulario);

                if (primerBoton != null)
                {
                    btnActivo = primerBoton;
                    primerBoton.BackColor = Color.FromArgb(51, 122, 183);
                }
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
    }
}