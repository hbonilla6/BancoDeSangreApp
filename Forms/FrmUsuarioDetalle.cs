using BancoDeSangreApp.Business;
using BancoDeSangreApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BancoDeSangreApp.Forms
{
    public partial class FrmUsuarioDetalle : Form
    {
        private UsuarioBLL _usuariosBLL;
        private int? _idUsuario;
        private bool _esEdicion;

        public FrmUsuarioDetalle(int? idUsuario = null)
        {
            InitializeComponent();
            _usuariosBLL = new UsuarioBLL();
            _idUsuario = idUsuario;
            _esEdicion = idUsuario.HasValue;
        }

        private void FrmUsuarioDetalle_Load(object sender, EventArgs e)
        {
            CargarRoles();

            if (_esEdicion)
            {
                lblTitulo.Text = "✏️ Editar Usuario";
                lblContrasena.Text = "Contraseña (dejar vacío para mantener actual)";
                lblConfirmarContrasena.Text = "Confirmar Contraseña";
                CargarDatosUsuario();
            }
            else
            {
                lblTitulo.Text = "📝 Nuevo Usuario";
            }
        }

        private void CargarRoles()
        {
            try
            {
                List<Rol> roles = _usuariosBLL.ObtenerTodosLosRoles();

                chkListRoles.Items.Clear();
                foreach (var rol in roles)
                {
                    chkListRoles.Items.Add(rol, false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar roles:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarDatosUsuario()
        {
            try
            {
                var dtUsuarios = _usuariosBLL.ObtenerUsuarios(Program.UsuarioActual.IdEntidad);
                var row = dtUsuarios.AsEnumerable().FirstOrDefault(r => r.Field<int>("IdUsuario") == _idUsuario.Value);

                if (row != null)
                {
                    txtUsuario.Text = row.Field<string>("Usuario");
                    txtUsuario.Enabled = false;
                    txtNombreCompleto.Text = row.Field<string>("NombreCompleto");
                    txtCorreo.Text = row.Field<string>("Correo");
                    txtTelefono.Text = row.Field<string>("Telefono");
                    chkActivo.Checked = row.Field<bool>("Activo");

                    var rolesUsuario = _usuariosBLL.ObtenerRolesUsuario(_idUsuario.Value);
                    for (int i = 0; i < chkListRoles.Items.Count; i++)
                    {
                        Rol rol = (Rol)chkListRoles.Items[i];
                        if (rolesUsuario.Any(r => r.IdRol == rol.IdRol))
                        {
                            chkListRoles.SetItemChecked(i, true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos del usuario:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtContrasena_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtContrasena.Text))
            {
                lblFortaleza.Text = "";
                return;
            }

            int fortaleza = EvaluarFortalezaContrasena(txtContrasena.Text);

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

        private void chkMostrarContrasenas_CheckedChanged(object sender, EventArgs e)
        {
            txtContrasena.UseSystemPasswordChar = !chkMostrarContrasenas.Checked;
            txtConfirmarContrasena.UseSystemPasswordChar = !chkMostrarContrasenas.Checked;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombreCompleto.Text))
                {
                    MessageBox.Show("El nombre completo es requerido.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombreCompleto.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtUsuario.Text))
                {
                    MessageBox.Show("El nombre de usuario es requerido.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUsuario.Focus();
                    return;
                }

                if (!_esEdicion || !string.IsNullOrWhiteSpace(txtContrasena.Text))
                {
                    if (string.IsNullOrWhiteSpace(txtContrasena.Text))
                    {
                        MessageBox.Show("La contraseña es requerida.", "Validación",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtContrasena.Focus();
                        return;
                    }

                    if (txtContrasena.Text.Length < 8)
                    {
                        MessageBox.Show("La contraseña debe tener al menos 8 caracteres.", "Validación",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtContrasena.Focus();
                        return;
                    }

                    if (txtContrasena.Text != txtConfirmarContrasena.Text)
                    {
                        MessageBox.Show("Las contraseñas no coinciden.", "Validación",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtConfirmarContrasena.Focus();
                        return;
                    }
                }

                if (chkListRoles.CheckedItems.Count == 0)
                {
                    MessageBox.Show("Debe seleccionar al menos un rol.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Usuario usuario = new Usuario
                {
                    IdUsuario = _idUsuario ?? 0,
                    IdEntidad = Program.UsuarioActual.IdEntidad,
                    NombreUsuario = txtUsuario.Text.Trim(),
                    NombreCompleto = txtNombreCompleto.Text.Trim(),
                    Correo = txtCorreo.Text.Trim(),
                    Telefono = txtTelefono.Text.Trim(),
                    Activo = chkActivo.Checked
                };

                int[] rolesIds = chkListRoles.CheckedItems.Cast<Rol>()
                    .Select(r => r.IdRol)
                    .ToArray();

                bool resultado;
                string mensaje;

                if (_esEdicion)
                {
                    (resultado, mensaje) = _usuariosBLL.ActualizarUsuario(usuario);

                    if (resultado && !string.IsNullOrWhiteSpace(txtContrasena.Text))
                    {
                        _usuariosBLL.CambiarContrasena(_idUsuario.Value, txtContrasena.Text);
                    }
                }
                else
                {
                    (resultado, mensaje) = _usuariosBLL.RegistrarUsuario(usuario, txtContrasena.Text, rolesIds);
                }

                if (resultado)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar usuario:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
                "¿Desea cancelar? Se perderán los cambios no guardados.",
                "Cancelar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
    }
}