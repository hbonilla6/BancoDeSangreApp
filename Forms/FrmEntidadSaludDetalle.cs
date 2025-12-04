using BancoDeSangreApp.Business;
using BancoDeSangreApp.Models;
using BancoDeSangreApp.Components;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BancoDeSangreApp.Forms
{
    public partial class FrmEntidadSaludDetalle : Form
    {
        private EntidadSaludBLL _entidadBLL;
        private int? _idEntidad;
        private bool _esEdicion;

        public FrmEntidadSaludDetalle(int? idEntidad = null)
        {
            InitializeComponent();
            _entidadBLL = new EntidadSaludBLL();
            _idEntidad = idEntidad;
            _esEdicion = idEntidad.HasValue;
        }

        private async void FrmEntidadSaludDetalle_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowLoading("Cargando información...");

                await Task.Run(() => System.Threading.Thread.Sleep(100));

                if (_esEdicion)
                {
                    lblTitulo.Text = "✏️ Editar Entidad de Salud";
                    txtCodigo.Enabled = false; // El código no se puede cambiar
                    lblCodigo.Text = "Código (no editable)";
                    await CargarDatosEntidad();
                }
                else
                {
                    lblTitulo.Text = "📝 Nueva Entidad de Salud";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar formulario:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.HideLoading();
            }
        }

        private async Task CargarDatosEntidad()
        {
            try
            {
                var entidad = await Task.Run(() => _entidadBLL.ObtenerEntidadPorId(_idEntidad.Value));

                if (entidad != null)
                {
                    txtCodigo.Text = entidad.Codigo;
                    txtNombre.Text = entidad.Nombre;
                    txtDireccion.Text = entidad.Direccion;
                    txtTelefono.Text = entidad.Telefono;
                    txtCorreo.Text = entidad.Correo;
                }
                else
                {
                    MessageBox.Show("No se pudo cargar la información de la entidad.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos de la entidad:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validaciones
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show("El nombre de la entidad es requerido.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombre.Focus();
                    return;
                }

                if (!_esEdicion && string.IsNullOrWhiteSpace(txtCodigo.Text))
                {
                    MessageBox.Show("El código de la entidad es requerido.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCodigo.Focus();
                    return;
                }

                this.ShowLoading(_esEdicion ? "Actualizando entidad..." : "Creando entidad...");

                await Task.Run(() => System.Threading.Thread.Sleep(200));

                EntidadSalud entidad = new EntidadSalud
                {
                    IdEntidad = _idEntidad ?? 0,
                    Codigo = txtCodigo.Text.Trim().ToUpper(),
                    Nombre = txtNombre.Text.Trim(),
                    Direccion = txtDireccion.Text.Trim(),
                    Telefono = txtTelefono.Text.Trim(),
                    Correo = txtCorreo.Text.Trim(),
                    Activo = true
                };

                bool resultado;
                string mensaje;

                if (_esEdicion)
                {
                    var resultadoUpdate = await Task.Run(() => _entidadBLL.ActualizarEntidad(entidad));
                    resultado = resultadoUpdate.exito;
                    mensaje = resultadoUpdate.mensaje;
                }
                else
                {
                    var resultadoCreate = await Task.Run(() => _entidadBLL.CrearEntidad(entidad));
                    resultado = resultadoCreate.exito;
                    mensaje = resultadoCreate.mensaje;
                }

                this.HideLoading();

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
                this.HideLoading();
                MessageBox.Show($"Error al guardar entidad:\n{ex.Message}", "Error",
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

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            lblContadorNombre.Text = $"{txtNombre.Text.Length}/150";
            lblContadorNombre.ForeColor = txtNombre.Text.Length > 150
                ? Color.FromArgb(231, 76, 60)
                : Color.Gray;
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            lblContadorCodigo.Text = $"{txtCodigo.Text.Length}/20";
            lblContadorCodigo.ForeColor = txtCodigo.Text.Length > 20
                ? Color.FromArgb(231, 76, 60)
                : Color.Gray;

            // Convertir a mayúsculas automáticamente
            if (!_esEdicion)
            {
                int pos = txtCodigo.SelectionStart;
                txtCodigo.Text = txtCodigo.Text.ToUpper();
                txtCodigo.SelectionStart = pos;
            }
        }

        private void txtDireccion_TextChanged(object sender, EventArgs e)
        {
            lblContadorDireccion.Text = $"{txtDireccion.Text.Length}/250";
            lblContadorDireccion.ForeColor = txtDireccion.Text.Length > 250
                ? Color.FromArgb(231, 76, 60)
                : Color.Gray;
        }

        private void txtTelefono_TextChanged(object sender, EventArgs e)
        {
            lblContadorTelefono.Text = $"{txtTelefono.Text.Length}/20";
            lblContadorTelefono.ForeColor = txtTelefono.Text.Length > 20
                ? Color.FromArgb(231, 76, 60)
                : Color.Gray;
        }

        private void txtCorreo_TextChanged(object sender, EventArgs e)
        {
            lblContadorCorreo.Text = $"{txtCorreo.Text.Length}/100";
            lblContadorCorreo.ForeColor = txtCorreo.Text.Length > 100
                ? Color.FromArgb(231, 76, 60)
                : Color.Gray;
        }
    }
}