using BancoDeSangreApp.Business;
using BancoDeSangreApp.Models;
using BancoDeSangreApp.Components;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BancoDeSangreApp.Forms
{
    public partial class FrmRolDetalle : Form
    {
        private RolBLL _rolBLL;
        private int? _idRol;
        private bool _esEdicion;

        public FrmRolDetalle(int? idRol = null)
        {
            InitializeComponent();
            _rolBLL = new RolBLL();
            _idRol = idRol;
            _esEdicion = idRol.HasValue;
        }

        private async void FrmRolDetalle_Load(object sender, EventArgs e)
        {
            try
            {
                this.ShowLoading("Cargando información...");

                await Task.Run(() => System.Threading.Thread.Sleep(100));

                ConfigurarNivelesAcceso();

                if (_esEdicion)
                {
                    lblTitulo.Text = "✏️ Editar Rol";
                    await CargarDatosRol();
                }
                else
                {
                    lblTitulo.Text = "📝 Nuevo Rol";
                    cmbNivelAcceso.SelectedIndex = 0; // Nivel 1 por defecto
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

        private void ConfigurarNivelesAcceso()
        {
            cmbNivelAcceso.Items.Clear();
            cmbNivelAcceso.Items.Add("1 - Básico");
            cmbNivelAcceso.Items.Add("2 - Operador");
            cmbNivelAcceso.Items.Add("3 - Supervisor");
            cmbNivelAcceso.Items.Add("4 - Gerente");
            cmbNivelAcceso.Items.Add("5 - Administrador");
        }

        private async Task CargarDatosRol()
        {
            try
            {
                var rol = await Task.Run(() => _rolBLL.ObtenerRolPorId(_idRol.Value));

                if (rol != null)
                {
                    txtNombre.Text = rol.Nombre;
                    txtDescripcion.Text = rol.Descripcion;
                    cmbNivelAcceso.SelectedIndex = rol.NivelAcceso - 1; // Índice basado en 0
                }
                else
                {
                    MessageBox.Show("No se pudo cargar la información del rol.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos del rol:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show("El nombre del rol es requerido.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombre.Focus();
                    return;
                }

                if (txtNombre.Text.Length > 50)
                {
                    MessageBox.Show("El nombre del rol no puede exceder 50 caracteres.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombre.Focus();
                    return;
                }

                if (cmbNivelAcceso.SelectedIndex < 0)
                {
                    MessageBox.Show("Debe seleccionar un nivel de acceso.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbNivelAcceso.Focus();
                    return;
                }

                if (!string.IsNullOrWhiteSpace(txtDescripcion.Text) && txtDescripcion.Text.Length > 200)
                {
                    MessageBox.Show("La descripción no puede exceder 200 caracteres.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDescripcion.Focus();
                    return;
                }

                this.ShowLoading(_esEdicion ? "Actualizando rol..." : "Creando rol...");

                await Task.Run(() => System.Threading.Thread.Sleep(200));

                Rol rol = new Rol
                {
                    IdRol = _idRol ?? 0,
                    Nombre = txtNombre.Text.Trim(),
                    Descripcion = txtDescripcion.Text.Trim(),
                    NivelAcceso = cmbNivelAcceso.SelectedIndex + 1 // Convertir de índice 0 a valor 1-5
                };

                bool resultado;
                string mensaje;

                if (_esEdicion)
                {
                    var resultadoUpdate = await Task.Run(() => _rolBLL.ActualizarRol(rol));
                    resultado = resultadoUpdate.exito;
                    mensaje = resultadoUpdate.mensaje;
                }
                else
                {
                    var resultadoCreate = await Task.Run(() => _rolBLL.CrearRol(rol));
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
                MessageBox.Show($"Error al guardar rol:\n{ex.Message}", "Error",
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
            lblContador.Text = $"{txtNombre.Text.Length}/50";
            lblContador.ForeColor = txtNombre.Text.Length > 50
                ? Color.FromArgb(231, 76, 60)
                : Color.Gray;
        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {
            lblContadorDesc.Text = $"{txtDescripcion.Text.Length}/200";
            lblContadorDesc.ForeColor = txtDescripcion.Text.Length > 200
                ? Color.FromArgb(231, 76, 60)
                : Color.Gray;
        }

        private void cmbNivelAcceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Actualizar la descripción del nivel de acceso seleccionado
            switch (cmbNivelAcceso.SelectedIndex)
            {
                case 0:
                    lblInfoNivel.Text = "ℹ️ Nivel Básico: Acceso limitado, solo operaciones básicas.";
                    lblInfoNivel.ForeColor = Color.FromArgb(149, 165, 166);
                    break;
                case 1:
                    lblInfoNivel.Text = "ℹ️ Operador: Puede realizar operaciones del día a día.";
                    lblInfoNivel.ForeColor = Color.FromArgb(52, 152, 219);
                    break;
                case 2:
                    lblInfoNivel.Text = "ℹ️ Supervisor: Supervisión de operaciones y reportes.";
                    lblInfoNivel.ForeColor = Color.FromArgb(46, 204, 113);
                    break;
                case 3:
                    lblInfoNivel.Text = "ℹ️ Gerente: Gestión completa y toma de decisiones.";
                    lblInfoNivel.ForeColor = Color.FromArgb(241, 196, 15);
                    break;
                case 4:
                    lblInfoNivel.Text = "ℹ️ Administrador: Control total del sistema.";
                    lblInfoNivel.ForeColor = Color.FromArgb(231, 76, 60);
                    break;
                default:
                    lblInfoNivel.Text = "";
                    break;
            }
        }
    }
}