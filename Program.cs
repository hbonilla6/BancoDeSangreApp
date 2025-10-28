using System;
using System.Windows.Forms;
using BancoDeSangreApp.Forms;
using BancoDeSangreApp.Models;

namespace BancoDeSangreApp
{
    /// <summary>
    /// Clase principal que inicia la aplicación
    /// </summary>
    static class Program
    {
        // Variable global para almacenar el usuario autenticado
        public static Usuario UsuarioActual { get; set; }

        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Configura el manejador de excepciones
            Application.ThreadException += Application_ThreadException;
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            // Muestra el formulario de login
            FrmLogin loginForm = new FrmLogin();

            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                // Si el login fue exitoso, guarda el usuario y abre el formulario principal
                UsuarioActual = loginForm.UsuarioAutenticado;

                // Verifica que el usuario no sea nulo
                if (UsuarioActual != null)
                {
                    // Abre el formulario principal (FrmMain) que contiene el menú y el dashboard
                    Application.Run(new FrmMain());
                }
                else
                {
                    MessageBox.Show(
                        "Error al obtener información del usuario.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            else
            {
                // El usuario canceló o cerró el login
                Application.Exit();
            }
        }

        /// <summary>
        /// Maneja excepciones no controladas en el hilo principal
        /// </summary>
        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            MessageBox.Show(
                $"Error no controlado:\n{e.Exception.Message}\n\nLa aplicación se cerrará.",
                "Error Crítico",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );

            // Log del error para debugging
            System.Diagnostics.Debug.WriteLine($"Error no controlado: {e.Exception}");

            Application.Exit();
        }

        /// <summary>
        /// Cierra sesión del usuario actual y reinicia la aplicación
        /// </summary>
        public static void CerrarSesion()
        {
            if (UsuarioActual != null)
            {
                // Aquí podrías registrar el cierre de sesión en la bitácora
                UsuarioActual = null;
            }

            // Reinicia la aplicación mostrando el login
            Application.Restart();
        }
    }
}