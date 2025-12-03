using BancoDeSangreApp.Business;
using BancoDeSangreApp.Components;
using BancoDeSangreApp.Data;
using BancoDeSangreApp.Models;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BancoDeSangreApp.Forms
{
    public partial class FrmLogin : Form
    {
        private readonly UsuarioBLL _usuarioBLL;
        private int _intentosFallidos = 0;
        private const int MAX_INTENTOS = 3;

        public Usuario UsuarioAutenticado { get; private set; }

        public FrmLogin()
        {
            InitializeComponent();
            _usuarioBLL = new UsuarioBLL();
            ConfigurarFormulario();
        }

        private void ConfigurarFormulario()
        {
            this.Text = "Banco de Sangre - Inicio de Sesión";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Size = new Size(450, 400);
            this.BackColor = Color.FromArgb(240, 244, 248);
        }

        private async void FrmLogin_Load(object sender, EventArgs e)
        {
            await VerificarConexionAsync();
        }

        /// <summary>
        /// Verifica la conexión a la base de datos de forma asíncrona
        /// </summary>
        private async Task VerificarConexionAsync()
        {
            try
            {
                this.ShowLoading("Verificando conexión al servidor...");

                bool conexionExitosa = await Task.Run(() =>
                {
                    ConexionDB db = ConexionDB.Instancia;
                    return db.ProbarConexion();
                });

                this.HideLoading();

                if (!conexionExitosa)
                {
                    var result = MessageBox.Show(
                        "No se pudo conectar a la base de datos.\n\n" +
                        "Posibles causas:\n" +
                        "• Sin conexión a Internet\n" +
                        "• Servidor remoto no disponible\n" +
                        "• Firewall bloqueando la conexión\n\n" +
                        "¿Desea reintentar?",
                        "Error de Conexión",
                        MessageBoxButtons.RetryCancel,
                        MessageBoxIcon.Error
                    );

                    if (result == DialogResult.Retry)
                    {
                        // Limpiar pool y reintentar
                        ConexionDB.Instancia.LimpiarPoolConexiones();
                        await VerificarConexionAsync();
                    }
                    else
                    {
                        Application.Exit();
                    }
                }
            }
            catch (Exception ex)
            {
                this.HideLoading();
                MessageBox.Show(
                    $"Error al inicializar la conexión:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                Application.Exit();
            }
        }

        private async void BtnIniciarSesion_Click(object sender, EventArgs e)
        {
            await IniciarSesionAsync();
        }

        /// <summary>
        /// Proceso de autenticación del usuario (versión asíncrona)
        /// </summary>
        private async Task IniciarSesionAsync()
        {
            try
            {
                // Validaciones básicas
                if (string.IsNullOrWhiteSpace(txtUsuario.Text))
                {
                    MessageBox.Show("Ingrese su nombre de usuario.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUsuario.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtContrasena.Text))
                {
                    MessageBox.Show("Ingrese su contraseña.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtContrasena.Focus();
                    return;
                }

                // Deshabilitar controles
                btnIniciarSesion.Enabled = false;
                txtUsuario.Enabled = false;
                txtContrasena.Enabled = false;

                // Mostrar loading
                this.ShowLoading("Autenticando usuario...");

                // Capturar valores antes de la tarea asíncrona
                string usuario = txtUsuario.Text.Trim();
                string contrasena = txtContrasena.Text;

                // Ejecutar autenticación en segundo plano
                LoginResult resultado = await Task.Run(() =>
                    _usuarioBLL.IniciarSesion(usuario, contrasena));

                // Ocultar loading
                this.HideLoading();

                if (resultado.Exitoso)
                {
                    // Autenticación exitosa
                    UsuarioAutenticado = resultado.Usuario;

                    //MessageBox.Show(
                    //    $"¡Bienvenido {resultado.Usuario.NombreCompleto}!\n\n" +
                    //    $"Entidad: {resultado.Usuario.NombreEntidad}\n" +
                    //    $"Roles: {resultado.Usuario.RolesTexto}",
                    //    "Inicio de Sesión Exitoso",
                    //    MessageBoxButtons.OK,
                    //    MessageBoxIcon.Information
                    //);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    // Autenticación fallida
                    _intentosFallidos++;

                    if (_intentosFallidos >= MAX_INTENTOS)
                    {
                        MessageBox.Show(
                            "Ha excedido el número máximo de intentos.\nLa aplicación se cerrará.",
                            "Acceso Denegado",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        Application.Exit();
                    }
                    else
                    {
                        int intentosRestantes = MAX_INTENTOS - _intentosFallidos;
                        MessageBox.Show(
                            $"{resultado.Mensaje}\n\nIntentos restantes: {intentosRestantes}",
                            "Error de Autenticación",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );

                        txtContrasena.Clear();
                        txtContrasena.Focus();
                    }

                    // Re-habilitar controles
                    btnIniciarSesion.Enabled = true;
                    txtUsuario.Enabled = true;
                    txtContrasena.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                this.HideLoading();

                // Re-habilitar controles
                btnIniciarSesion.Enabled = true;
                txtUsuario.Enabled = true;
                txtContrasena.Enabled = true;

                MessageBox.Show(
                    $"Error al iniciar sesión:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private async void Txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                await IniciarSesionAsync();
            }
        }

        private void ChkMostrarContrasena_CheckedChanged(object sender, EventArgs e)
        {
            txtContrasena.UseSystemPasswordChar = !chkMostrarContrasena.Checked;
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "¿Está seguro de que desea salir?",
                "Confirmar Salida",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void LnkRecuperarContrasena_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show(
                "Para recuperar su contraseña, contacte al administrador del sistema.\n\n" +
                "Email: danielesau06@gmail.com\n" +
                "Teléfono: (503) 7959-3780",
                "Recuperar Contraseña",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
    }
}