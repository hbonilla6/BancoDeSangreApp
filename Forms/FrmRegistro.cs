using BancoDeSangreApp.Business;
using BancoDeSangreApp.Components;
using BancoDeSangreApp.Models;
using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BancoDeSangreApp.Forms
{
    public partial class FrmRegistro : Form
    {
        private UsuarioBLL _usuarioBLL;
        private bool _datosInicializados = false;
        public string UsuarioRegistrado { get; private set; }

        public FrmRegistro()
        {
            InitializeComponent();
            _usuarioBLL = new UsuarioBLL();
            ConfigurarFormulario();
        }

        private void ConfigurarFormulario()
        {
            this.Text = "Registro de Usuario - Banco de Sangre";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Size = new Size(500, 620);
            this.BackColor = Color.FromArgb(240, 244, 248);
        }

        /// <summary>
        /// Evento Load - CAMBIAR DE Shown A Load
        /// </summary>
        private async void FrmRegistro_Load(object sender, EventArgs e)
        {
            if (!_datosInicializados)
            {
                await CargarDatosInicialesAsync();
                _datosInicializados = true;
            }
        }

        private async Task CargarDatosInicialesAsync()
        {
            try
            {
                DeshabilitarControles();
                this.ShowLoading("Cargando datos iniciales...");

                // Cargar datos de BD en segundo plano
                DataTable dtEntidades = null;
                System.Collections.Generic.List<Rol> roles = null;

                await Task.Run(() =>
                {
                    dtEntidades = _usuarioBLL.ObtenerEntidadesSalud();
                    roles = _usuarioBLL.ObtenerTodosLosRoles();
                });

                // Asignar datos a los controles (en el hilo UI)
                if (dtEntidades != null && dtEntidades.Rows.Count > 0)
                {
                    cmbEntidad.DataSource = dtEntidades;
                    cmbEntidad.DisplayMember = "Nombre";
                    cmbEntidad.ValueMember = "IdEntidad";
                    cmbEntidad.SelectedIndex = -1;
                }

                if (roles != null && roles.Count > 0)
                {
                    clbRoles.Items.Clear();
                    foreach (var rol in roles)
                    {
                        clbRoles.Items.Add(rol, false);
                    }

                    // Por defecto, marca el rol "Operador" si existe
                    for (int i = 0; i < clbRoles.Items.Count; i++)
                    {
                        Rol rol = (Rol)clbRoles.Items[i];
                        if (rol.Nombre.Equals("Operador", StringComparison.OrdinalIgnoreCase))
                        {
                            clbRoles.SetItemChecked(i, true);
                            break;
                        }
                    }
                }

                this.HideLoading();
                HabilitarControles();
            }
            catch (Exception ex)
            {
                this.HideLoading();
                DeshabilitarControles();

                MessageBox.Show(
                    $"Error al cargar datos iniciales:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void DeshabilitarControles()
        {
            cmbEntidad.Enabled = false;
            txtNombreCompleto.Enabled = false;
            txtUsuario.Enabled = false;
            txtContrasena.Enabled = false;
            txtConfirmarContrasena.Enabled = false;
            txtCorreo.Enabled = false;
            txtTelefono.Enabled = false;
            clbRoles.Enabled = false;
            chkMostrarContrasena.Enabled = false;
            btnRegistrar.Enabled = false;
        }

        private void HabilitarControles()
        {
            cmbEntidad.Enabled = true;
            txtNombreCompleto.Enabled = true;
            txtUsuario.Enabled = true;
            txtContrasena.Enabled = true;
            txtConfirmarContrasena.Enabled = true;
            txtCorreo.Enabled = true;
            txtTelefono.Enabled = true;
            clbRoles.Enabled = true;
            chkMostrarContrasena.Enabled = true;
            btnRegistrar.Enabled = true;
        }

        private bool ValidarCampos()
        {
            if (cmbEntidad.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione una entidad de salud.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbEntidad.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNombreCompleto.Text))
            {
                MessageBox.Show("Ingrese el nombre completo.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreCompleto.Focus();
                return false;
            }

            string errorUsuario = SeguridadBLL.ValidarNombreUsuario(txtUsuario.Text);
            if (!string.IsNullOrEmpty(errorUsuario))
            {
                MessageBox.Show(errorUsuario, "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsuario.Focus();
                return false;
            }

            string errorClave = SeguridadBLL.ValidarFortalezaClave(txtContrasena.Text);
            if (!string.IsNullOrEmpty(errorClave))
            {
                MessageBox.Show(errorClave, "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContrasena.Focus();
                return false;
            }

            if (txtContrasena.Text != txtConfirmarContrasena.Text)
            {
                MessageBox.Show("Las contraseñas no coinciden.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmarContrasena.Focus();
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtCorreo.Text) &&
                !SeguridadBLL.ValidarCorreo(txtCorreo.Text))
            {
                MessageBox.Show("El formato del correo electrónico no es válido.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCorreo.Focus();
                return false;
            }

            if (clbRoles.CheckedItems.Count == 0)
            {
                MessageBox.Show("Seleccione al menos un rol para el usuario.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private async void btnRegistrar_Click(object sender, EventArgs e)
        {
            await RegistrarUsuarioAsync();
        }

        private async Task RegistrarUsuarioAsync()
        {
            try
            {
                if (!ValidarCampos())
                    return;

                if (MessageBox.Show(
                    "¿Está seguro de que desea registrar este usuario?",
                    "Confirmar Registro",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }

                btnRegistrar.Enabled = false;
                btnCancelar.Enabled = false;
                this.ShowLoading("Registrando usuario...");

                Usuario nuevoUsuario = new Usuario
                {
                    IdEntidad = Convert.ToInt32(cmbEntidad.SelectedValue),
                    NombreUsuario = txtUsuario.Text.Trim(),
                    NombreCompleto = txtNombreCompleto.Text.Trim(),
                    Correo = string.IsNullOrWhiteSpace(txtCorreo.Text) ? null : txtCorreo.Text.Trim(),
                    Telefono = string.IsNullOrWhiteSpace(txtTelefono.Text) ? null : txtTelefono.Text.Trim(),
                    Activo = true
                };

                int[] rolesIds = new int[clbRoles.CheckedItems.Count];
                for (int i = 0; i < clbRoles.CheckedItems.Count; i++)
                {
                    Rol rol = (Rol)clbRoles.CheckedItems[i];
                    rolesIds[i] = rol.IdRol;
                }

                string contrasena = txtContrasena.Text;

                var resultado = await Task.Run(() =>
                    _usuarioBLL.RegistrarUsuario(nuevoUsuario, contrasena, rolesIds));

                this.HideLoading();
                btnRegistrar.Enabled = true;
                btnCancelar.Enabled = true;

                if (resultado.exito)
                {
                    UsuarioRegistrado = nuevoUsuario.NombreUsuario;

                    MessageBox.Show(
                        resultado.mensaje,
                        "Registro Exitoso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(
                        resultado.mensaje,
                        "Error en Registro",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                this.HideLoading();
                btnRegistrar.Enabled = true;
                btnCancelar.Enabled = true;

                MessageBox.Show(
                    $"Error al registrar usuario:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (HayCambiosPendientes())
            {
                if (MessageBox.Show(
                    "¿Está seguro de que desea cancelar? Se perderán los datos ingresados.",
                    "Confirmar Cancelación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }
            }

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private bool HayCambiosPendientes()
        {
            return !string.IsNullOrWhiteSpace(txtNombreCompleto.Text) ||
                   !string.IsNullOrWhiteSpace(txtUsuario.Text) ||
                   !string.IsNullOrWhiteSpace(txtContrasena.Text) ||
                   !string.IsNullOrWhiteSpace(txtCorreo.Text) ||
                   !string.IsNullOrWhiteSpace(txtTelefono.Text) ||
                   cmbEntidad.SelectedIndex != -1;
        }

        private void chkMostrarContrasena_CheckedChanged(object sender, EventArgs e)
        {
            txtContrasena.UseSystemPasswordChar = !chkMostrarContrasena.Checked;
            txtConfirmarContrasena.UseSystemPasswordChar = !chkMostrarContrasena.Checked;
        }

        private void txtContrasena_TextChanged(object sender, EventArgs e)
        {
            ActualizarIndicadorFortaleza();
        }

        private void ActualizarIndicadorFortaleza()
        {
            string clave = txtContrasena.Text;

            if (string.IsNullOrEmpty(clave))
            {
                lblFortaleza.Text = "";
                lblFortaleza.ForeColor = Color.Gray;
                return;
            }

            int puntos = 0;

            if (clave.Length >= 6) puntos++;
            if (clave.Length >= 8) puntos++;
            if (System.Text.RegularExpressions.Regex.IsMatch(clave, @"[A-Z]")) puntos++;
            if (System.Text.RegularExpressions.Regex.IsMatch(clave, @"[a-z]")) puntos++;
            if (System.Text.RegularExpressions.Regex.IsMatch(clave, @"\d")) puntos++;
            if (System.Text.RegularExpressions.Regex.IsMatch(clave, @"[!@#$%^&*(),.?""':{}|<>]")) puntos++;

            if (puntos <= 2)
            {
                lblFortaleza.Text = "Fortaleza: Débil";
                lblFortaleza.ForeColor = Color.Red;
            }
            else if (puntos <= 4)
            {
                lblFortaleza.Text = "Fortaleza: Media";
                lblFortaleza.ForeColor = Color.Orange;
            }
            else
            {
                lblFortaleza.Text = "Fortaleza: Fuerte";
                lblFortaleza.ForeColor = Color.Green;
            }
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }
    }
}