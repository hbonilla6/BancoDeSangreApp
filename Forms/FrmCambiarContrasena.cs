using BancoDeSangreApp.Business;
using BancoDeSangreApp.Data;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BancoDeSangreApp.Forms
{
    public partial class FrmCambiarContrasena : Form
    {
        private readonly int _idUsuario;

        public FrmCambiarContrasena(int idUsuario)
        {
            InitializeComponent();
            _idUsuario = idUsuario;
        }

        private void FrmCambiarContrasena_Load(object sender, EventArgs e)
        {
            CargarDatosUsuario();
        }

        private void CargarDatosUsuario()
        {
            try
            {
                string query = @"
                    SELECT Usuario, NombreCompleto 
                    FROM Usuarios 
                    WHERE IdUsuario = @IdUsuario";

                SqlParameter[] parametros = {
                    new SqlParameter("@IdUsuario", _idUsuario)
                };

                var dt = ConexionDB.Instancia.EjecutarConsulta(query, parametros);

                if (dt.Rows.Count > 0)
                {
                    lblUsuarioInfo.Text = $"Usuario: {dt.Rows[0]["Usuario"]} - {dt.Rows[0]["NombreCompleto"]}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar usuario: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validaciones
                if (string.IsNullOrWhiteSpace(txtNuevaContrasena.Text))
                {
                    MessageBox.Show("Ingrese la nueva contraseña.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNuevaContrasena.Focus();
                    return;
                }

                if (txtNuevaContrasena.Text != txtConfirmarContrasena.Text)
                {
                    MessageBox.Show("Las contraseñas no coinciden.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtConfirmarContrasena.Focus();
                    return;
                }

                // Validar fortaleza de contraseña
                string errorFortaleza = SeguridadBLL.ValidarFortalezaClave(txtNuevaContrasena.Text);
                if (!string.IsNullOrEmpty(errorFortaleza))
                {
                    MessageBox.Show(errorFortaleza,
                        "Contraseña Débil", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNuevaContrasena.Focus();
                    return;
                }

                // Confirmar cambio
                var confirmacion = MessageBox.Show(
                    "¿Está seguro de cambiar la contraseña de este usuario?",
                    "Confirmar Cambio",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmacion == DialogResult.Yes)
                {
                    // Generar hash de nueva contraseña
                    string hash = SeguridadBLL.GenerarHash(txtNuevaContrasena.Text);

                    // Actualizar en base de datos
                    string query = @"
                        UPDATE Usuarios 
                        SET HashContrasena = @Hash,
                            FechaModificacion = SYSDATETIME()
                        WHERE IdUsuario = @IdUsuario";

                    SqlParameter[] parametros = {
                        new SqlParameter("@Hash", hash),
                        new SqlParameter("@IdUsuario", _idUsuario)
                    };

                    ConexionDB.Instancia.EjecutarComando(query, parametros);

                    // Registrar en auditoría
                    try
                    {
                        RegistrarAuditoria(
                            Program.UsuarioActual.IdUsuario,
                            "Usuarios",
                            "Cambio de Contraseña",
                            _idUsuario.ToString(),
                            null,
                            $"Se cambió la contraseña del usuario ID {_idUsuario}"
                        );
                    }
                    catch { } // No fallar si la auditoría falla

                    MessageBox.Show("Contraseña actualizada correctamente.",
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cambiar contraseña: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RegistrarAuditoria(int? idUsuario, string entidad, string operacion,
            string clavePrimaria, string valorAnterior, string valorNuevo)
        {
            try
            {
                string query = @"
                    INSERT INTO BitacoraAuditoria 
                    (IdUsuario, Entidad, Operacion, ClavePrimaria, ValorAnterior, ValorNuevo, FechaAccion)
                    VALUES (@IdUsuario, @Entidad, @Operacion, @ClavePrimaria, @ValorAnterior, @ValorNuevo, SYSDATETIME())";

                SqlParameter[] parametros = {
                    new SqlParameter("@IdUsuario", (object)idUsuario ?? DBNull.Value),
                    new SqlParameter("@Entidad", entidad),
                    new SqlParameter("@Operacion", operacion),
                    new SqlParameter("@ClavePrimaria", clavePrimaria),
                    new SqlParameter("@ValorAnterior", (object)valorAnterior ?? DBNull.Value),
                    new SqlParameter("@ValorNuevo", (object)valorNuevo ?? DBNull.Value)
                };

                ConexionDB.Instancia.EjecutarComando(query, parametros);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en auditoría: {ex.Message}");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void chkMostrarContrasenas_CheckedChanged(object sender, EventArgs e)
        {
            txtNuevaContrasena.UseSystemPasswordChar = !chkMostrarContrasenas.Checked;
            txtConfirmarContrasena.UseSystemPasswordChar = !chkMostrarContrasenas.Checked;
        }

        private void txtNuevaContrasena_TextChanged(object sender, EventArgs e)
        {
            ActualizarIndicadorFortaleza();
        }

        private void ActualizarIndicadorFortaleza()
        {
            string contrasena = txtNuevaContrasena.Text;

            if (string.IsNullOrWhiteSpace(contrasena))
            {
                lblFortaleza.Text = "";
                lblFortaleza.ForeColor = System.Drawing.Color.Gray;
                return;
            }

            int puntos = 0;

            // Longitud
            if (contrasena.Length >= 8) puntos++;
            if (contrasena.Length >= 12) puntos++;

            // Mayúsculas
            if (System.Text.RegularExpressions.Regex.IsMatch(contrasena, @"[A-Z]")) puntos++;

            // Minúsculas
            if (System.Text.RegularExpressions.Regex.IsMatch(contrasena, @"[a-z]")) puntos++;

            // Números
            if (System.Text.RegularExpressions.Regex.IsMatch(contrasena, @"[0-9]")) puntos++;

            // Caracteres especiales
            if (System.Text.RegularExpressions.Regex.IsMatch(contrasena, @"[!@#$%^&*(),.?""':{}|<>]")) puntos++;

            // Determinar fortaleza
            if (puntos <= 2)
            {
                lblFortaleza.Text = "Fortaleza: Débil ❌";
                lblFortaleza.ForeColor = System.Drawing.Color.Red;
            }
            else if (puntos <= 4)
            {
                lblFortaleza.Text = "Fortaleza: Media ⚠️";
                lblFortaleza.ForeColor = System.Drawing.Color.Orange;
            }
            else
            {
                lblFortaleza.Text = "Fortaleza: Fuerte ✅";
                lblFortaleza.ForeColor = System.Drawing.Color.Green;
            }
        }
    }
}