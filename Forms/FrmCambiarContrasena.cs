using BancoDeSangreApp.Business;
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BancoDeSangreApp.Forms
{
    public partial class FrmCambiarContrasena : Form
    {
        public FrmCambiarContrasena()
        {
            InitializeComponent();
        }

        private void FrmCambiarContrasena_Load(object sender, EventArgs e)
        {
            lblUsuarioInfo.Text = $"Usuario: {Program.UsuarioActual.NombreCompleto}";
        }

        private void chkMostrarContrasenas_CheckedChanged(object sender, EventArgs e)
        {
            txtNuevaContrasena.UseSystemPasswordChar = !chkMostrarContrasenas.Checked;
            txtConfirmarContrasena.UseSystemPasswordChar = !chkMostrarContrasenas.Checked;
        }

        private void txtNuevaContrasena_TextChanged(object sender, EventArgs e)
        {
            string password = txtNuevaContrasena.Text;

            if (string.IsNullOrEmpty(password))
            {
                lblFortaleza.Text = "";
                lblFortaleza.ForeColor = Color.Gray;
                return;
            }

            int fortaleza = EvaluarFortalezaContrasena(password);

            if (fortaleza < 2)
            {
                lblFortaleza.Text = "⚠️ Débil";
                lblFortaleza.ForeColor = Color.FromArgb(192, 57, 43);
            }
            else if (fortaleza < 4)
            {
                lblFortaleza.Text = "⚡ Media";
                lblFortaleza.ForeColor = Color.FromArgb(243, 156, 18);
            }
            else
            {
                lblFortaleza.Text = "✅ Fuerte";
                lblFortaleza.ForeColor = Color.FromArgb(39, 174, 96);
            }
        }

        private int EvaluarFortalezaContrasena(string password)
        {
            int puntos = 0;
            if (password.Length >= 8) puntos++;
            if (password.Length >= 12) puntos++;
            if (Regex.IsMatch(password, @"[a-z]")) puntos++;
            if (Regex.IsMatch(password, @"[A-Z]")) puntos++;
            if (Regex.IsMatch(password, @"[0-9]")) puntos++;
            if (Regex.IsMatch(password, @"[!@#$%^&*(),.?""':{}|<>]")) puntos++;
            return puntos;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNuevaContrasena.Text))
                {
                    MessageBox.Show("Debe ingresar una nueva contraseña.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNuevaContrasena.Focus();
                    return;
                }

                if (txtNuevaContrasena.Text.Length < 8)
                {
                    MessageBox.Show("La contraseña debe tener al menos 8 caracteres.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNuevaContrasena.Focus();
                    return;
                }

                if (!ValidarComplejidadContrasena(txtNuevaContrasena.Text))
                {
                    MessageBox.Show(
                        "La contraseña debe incluir:\n" +
                        "- Al menos una letra mayúscula\n" +
                        "- Al menos una letra minúscula\n" +
                        "- Al menos un número\n" +
                        "- Al menos un carácter especial (!@#$%^&*)",
                        "Contraseña débil", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNuevaContrasena.Focus();
                    return;
                }

                if (txtNuevaContrasena.Text != txtConfirmarContrasena.Text)
                {
                    MessageBox.Show("Las contraseñas no coinciden.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtConfirmarContrasena.Focus();
                    return;
                }

                DialogResult confirmacion = MessageBox.Show(
                    "¿Está seguro que desea cambiar su contraseña?",
                    "Confirmar Cambio", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirmacion != DialogResult.Yes)
                    return;

                var usuariosBLL = new UsuarioBLL();
                bool resultado = usuariosBLL.CambiarContrasena(
                    Program.UsuarioActual.IdUsuario,
                    txtNuevaContrasena.Text);

                if (resultado)
                {
                    MessageBox.Show(
                        "✅ Contraseña cambiada exitosamente.\n\nPor seguridad, se cerrará su sesión.",
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(
                        "❌ No se pudo cambiar la contraseña.\nIntente nuevamente o contacte al administrador.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cambiar la contraseña:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarComplejidadContrasena(string password)
        {
            if (!Regex.IsMatch(password, @"[A-Z]")) return false;
            if (!Regex.IsMatch(password, @"[a-z]")) return false;
            if (!Regex.IsMatch(password, @"[0-9]")) return false;
            if (!Regex.IsMatch(password, @"[!@#$%^&*(),.?""':{}|<>]")) return false;
            return true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
                "¿Desea cancelar el cambio de contraseña?",
                "Cancelar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
    }
}