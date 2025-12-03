using BancoDeSangreApp.Business;
using BancoDeSangreApp.Data;
using BancoDeSangreApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BancoDeSangreApp.Forms
{
    public partial class FrmUsuarios : Form
    {
        private readonly UsuarioBLL _usuarioBLL;
        private int? _idUsuarioSeleccionado = null;
        private int _paginaActual = 1;
        private int _registrosPorPagina = 20;
        private int _totalRegistros = 0;
        private int _totalPaginas = 0;

        public FrmUsuarios()
        {
            InitializeComponent();
            _usuarioBLL = new UsuarioBLL();
        }

        private void Frmusuarios_Load(object sender, EventArgs e)
        {
            // Validar permisos
            if (!PermisosBLL.TienePermiso(Program.UsuarioActual, "Usuarios.Ver"))
            {
                MessageBox.Show("No tiene permisos para acceder a este módulo.",
                    "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            ConfigurarDataGridView();
            CargarEntidades();
            CargarRoles();
            CargarUsuarios();
        }

        private void ConfigurarDataGridView()
        {
            dgvUsuarios.AutoGenerateColumns = false;
            dgvUsuarios.Columns.Clear();

            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdUsuario",
                DataPropertyName = "IdUsuario",
                HeaderText = "ID",
                Width = 50
            });

            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Usuario",
                DataPropertyName = "Usuario",
                HeaderText = "Usuario",
                Width = 120
            });

            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NombreCompleto",
                DataPropertyName = "NombreCompleto",
                HeaderText = "Nombre Completo",
                Width = 200
            });

            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Correo",
                DataPropertyName = "Correo",
                HeaderText = "Correo",
                Width = 180
            });

            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Entidad",
                DataPropertyName = "NombreEntidad",
                HeaderText = "Entidad",
                Width = 150
            });

            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Roles",
                DataPropertyName = "RolesTexto",
                HeaderText = "Roles",
                Width = 150
            });

            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "UltimoAcceso",
                DataPropertyName = "UltimoAcceso",
                HeaderText = "Último Acceso",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" }
            });

            dgvUsuarios.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "Activo",
                DataPropertyName = "Activo",
                HeaderText = "Activo",
                Width = 60
            });

            dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsuarios.MultiSelect = false;
            dgvUsuarios.ReadOnly = true;
            dgvUsuarios.AllowUserToAddRows = false;
            dgvUsuarios.RowHeadersVisible = false;
        }

        private void CargarEntidades()
        {
            try
            {
                DataTable dt = _usuarioBLL.ObtenerEntidadesSalud();
                cmbEntidad.DataSource = dt;
                cmbEntidad.DisplayMember = "Nombre";
                cmbEntidad.ValueMember = "IdEntidad";
                cmbEntidad.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar entidades: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarRoles()
        {
            try
            {
                var roles = _usuarioBLL.ObtenerTodosLosRoles();

                clbRoles.Items.Clear();
                foreach (var rol in roles)
                {
                    clbRoles.Items.Add(rol, false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar roles: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarUsuarios()
        {
            try
            {
                string busqueda = txtBuscar.Text.Trim();

                // Calcular offset para paginación
                int offset = (_paginaActual - 1) * _registrosPorPagina;

                string query = @"
                    WITH UsuariosConRoles AS (
                        SELECT 
                            u.IdUsuario,
                            u.Usuario,
                            u.NombreCompleto,
                            u.Correo,
                            u.Telefono,
                            u.Activo,
                            u.UltimoAcceso,
                            e.Nombre AS NombreEntidad,
                            STUFF((
                                SELECT ', ' + r.Nombre
                                FROM UsuarioRoles ur
                                INNER JOIN Roles r ON ur.IdRol = r.IdRol
                                WHERE ur.IdUsuario = u.IdUsuario
                                FOR XML PATH('')
                            ), 1, 2, '') AS RolesTexto
                        FROM Usuarios u
                        INNER JOIN EntidadesSalud e ON u.IdEntidad = e.IdEntidad
                        WHERE 1=1";

                var parametros = new System.Collections.Generic.List<SqlParameter>();

                if (!string.IsNullOrWhiteSpace(busqueda))
                {
                    query += @" AND (u.Usuario LIKE @Busqueda 
                               OR u.NombreCompleto LIKE @Busqueda 
                               OR u.Correo LIKE @Busqueda)";
                    parametros.Add(new SqlParameter("@Busqueda", $"%{busqueda}%"));
                }

                // Solo mostrar usuarios de la misma entidad si no es admin global
                if (!Program.UsuarioActual.EsAdministrador())
                {
                    query += " AND u.IdEntidad = @IdEntidad";
                    parametros.Add(new SqlParameter("@IdEntidad", Program.UsuarioActual.IdEntidad));
                }

                query += @"
                    )
                    SELECT 
                        IdUsuario, Usuario, NombreCompleto, Correo, Telefono,
                        NombreEntidad, RolesTexto, UltimoAcceso, Activo
                    FROM UsuariosConRoles
                    ORDER BY Usuario
                    OFFSET @Offset ROWS
                    FETCH NEXT @Limit ROWS ONLY";

                parametros.Add(new SqlParameter("@Offset", offset));
                parametros.Add(new SqlParameter("@Limit", _registrosPorPagina));

                DataTable dt = ConexionDB.Instancia.EjecutarConsulta(query, parametros.ToArray());
                dgvUsuarios.DataSource = dt;

                // Obtener total de registros para paginación
                ObtenerTotalRegistros(busqueda);
                ActualizarControlesPaginacion();

                lblTotal.Text = $"Total: {_totalRegistros} usuarios | Página {_paginaActual} de {_totalPaginas}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar usuarios: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ObtenerTotalRegistros(string busqueda)
        {
            try
            {
                string query = @"
                    SELECT COUNT(*) 
                    FROM Usuarios u
                    WHERE 1=1";

                var parametros = new System.Collections.Generic.List<SqlParameter>();

                if (!string.IsNullOrWhiteSpace(busqueda))
                {
                    query += @" AND (u.Usuario LIKE @Busqueda 
                               OR u.NombreCompleto LIKE @Busqueda 
                               OR u.Correo LIKE @Busqueda)";
                    parametros.Add(new SqlParameter("@Busqueda", $"%{busqueda}%"));
                }

                if (!Program.UsuarioActual.EsAdministrador())
                {
                    query += " AND u.IdEntidad = @IdEntidad";
                    parametros.Add(new SqlParameter("@IdEntidad", Program.UsuarioActual.IdEntidad));
                }

                _totalRegistros = Convert.ToInt32(
                    ConexionDB.Instancia.EjecutarEscalar(query, parametros.ToArray()));

                _totalPaginas = (int)Math.Ceiling((double)_totalRegistros / _registrosPorPagina);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al obtener total: {ex.Message}");
            }
        }

        private void ActualizarControlesPaginacion()
        {
            btnPaginaAnterior.Enabled = _paginaActual > 1;
            btnPaginaSiguiente.Enabled = _paginaActual < _totalPaginas;

            lblPaginacion.Text = $"Página {_paginaActual} de {_totalPaginas}";
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (!PermisosBLL.TienePermiso(Program.UsuarioActual, "Usuarios.Crear"))
            {
                MessageBox.Show("No tiene permisos para crear usuarios.",
                    "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LimpiarFormulario();
            HabilitarControles(true);
            _idUsuarioSeleccionado = null;
            txtUsuario.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (_idUsuarioSeleccionado == null) return;

            if (!PermisosBLL.TienePermiso(Program.UsuarioActual, "Usuarios.Editar"))
            {
                MessageBox.Show("No tiene permisos para editar usuarios.",
                    "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            HabilitarControles(true);
            txtUsuario.Enabled = false; // No permitir cambiar nombre de usuario
            txtContrasena.Enabled = false; // Cambio de contraseña por otro botón
            txtConfirmarContrasena.Enabled = false;
            txtNombreCompleto.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validaciones
                if (cmbEntidad.SelectedIndex == -1)
                {
                    MessageBox.Show("Seleccione una entidad.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtNombreCompleto.Text))
                {
                    MessageBox.Show("Ingrese el nombre completo.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (clbRoles.CheckedItems.Count == 0)
                {
                    MessageBox.Show("Seleccione al menos un rol.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Nuevo usuario
                if (_idUsuarioSeleccionado == null)
                {
                    string errorUsuario = SeguridadBLL.ValidarNombreUsuario(txtUsuario.Text);
                    if (!string.IsNullOrEmpty(errorUsuario))
                    {
                        MessageBox.Show(errorUsuario, "Validación",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string errorClave = SeguridadBLL.ValidarFortalezaClave(txtContrasena.Text);
                    if (!string.IsNullOrEmpty(errorClave))
                    {
                        MessageBox.Show(errorClave, "Validación",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (txtContrasena.Text != txtConfirmarContrasena.Text)
                    {
                        MessageBox.Show("Las contraseñas no coinciden.", "Validación",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Crear usuario
                    Usuario nuevoUsuario = new Usuario
                    {
                        IdEntidad = Convert.ToInt32(cmbEntidad.SelectedValue),
                        NombreUsuario = txtUsuario.Text.Trim(),
                        NombreCompleto = txtNombreCompleto.Text.Trim(),
                        Correo = string.IsNullOrWhiteSpace(txtCorreo.Text) ? null : txtCorreo.Text.Trim(),
                        Telefono = string.IsNullOrWhiteSpace(txtTelefono.Text) ? null : txtTelefono.Text.Trim(),
                        Activo = chkActivo.Checked
                    };

                    int[] rolesIds = ObtenerRolesSeleccionados();
                    var resultado = _usuarioBLL.RegistrarUsuario(nuevoUsuario, txtContrasena.Text, rolesIds);

                    if (resultado.exito)
                    {
                        MessageBox.Show(resultado.mensaje, "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarFormulario();
                        HabilitarControles(false);
                        CargarUsuarios();
                    }
                    else
                    {
                        MessageBox.Show(resultado.mensaje, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else // Editar usuario existente
                {
                    string query = @"
                        UPDATE Usuarios 
                        SET NombreCompleto = @NombreCompleto,
                            Correo = @Correo,
                            Telefono = @Telefono,
                            Activo = @Activo,
                            IdEntidad = @IdEntidad,
                            FechaModificacion = SYSDATETIME()
                        WHERE IdUsuario = @IdUsuario";

                    SqlParameter[] parametros = {
                        new SqlParameter("@IdUsuario", _idUsuarioSeleccionado.Value),
                        new SqlParameter("@IdEntidad", cmbEntidad.SelectedValue),
                        new SqlParameter("@NombreCompleto", txtNombreCompleto.Text.Trim()),
                        new SqlParameter("@Correo", string.IsNullOrWhiteSpace(txtCorreo.Text) ?
                            DBNull.Value : (object)txtCorreo.Text.Trim()),
                        new SqlParameter("@Telefono", string.IsNullOrWhiteSpace(txtTelefono.Text) ?
                            DBNull.Value : (object)txtTelefono.Text.Trim()),
                        new SqlParameter("@Activo", chkActivo.Checked)
                    };

                    ConexionDB.Instancia.EjecutarComando(query, parametros);

                    // Actualizar roles
                    ActualizarRolesUsuario(_idUsuarioSeleccionado.Value);

                    MessageBox.Show("Usuario actualizado correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LimpiarFormulario();
                    HabilitarControles(false);
                    CargarUsuarios();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarRolesUsuario(int idUsuario)
        {
            try
            {
                // Eliminar roles actuales
                string queryDelete = "DELETE FROM UsuarioRoles WHERE IdUsuario = @IdUsuario";
                SqlParameter[] paramDelete = { new SqlParameter("@IdUsuario", idUsuario) };
                ConexionDB.Instancia.EjecutarComando(queryDelete, paramDelete);

                // Insertar nuevos roles
                int[] rolesIds = ObtenerRolesSeleccionados();
                foreach (int idRol in rolesIds)
                {
                    string queryInsert = "INSERT INTO UsuarioRoles (IdUsuario, IdRol) VALUES (@IdUsuario, @IdRol)";
                    SqlParameter[] paramInsert = {
                        new SqlParameter("@IdUsuario", idUsuario),
                        new SqlParameter("@IdRol", idRol)
                    };
                    ConexionDB.Instancia.EjecutarComando(queryInsert, paramInsert);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar roles: {ex.Message}");
            }
        }

        private int[] ObtenerRolesSeleccionados()
        {
            int[] roles = new int[clbRoles.CheckedItems.Count];
            for (int i = 0; i < clbRoles.CheckedItems.Count; i++)
            {
                Rol rol = (Rol)clbRoles.CheckedItems[i];
                roles[i] = rol.IdRol;
            }
            return roles;
        }

        private void btnCambiarContrasena_Click(object sender, EventArgs e)
        {
            if (_idUsuarioSeleccionado == null)
            {
                MessageBox.Show("Seleccione un usuario primero.",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            FrmCambiarContrasena frmCambiar = new FrmCambiarContrasena(_idUsuarioSeleccionado.Value);
            if (frmCambiar.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Contraseña actualizada correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_idUsuarioSeleccionado == null) return;

            if (!PermisosBLL.TienePermiso(Program.UsuarioActual, "Usuarios.Eliminar"))
            {
                MessageBox.Show("No tiene permisos para eliminar usuarios.",
                    "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // No permitir eliminar al usuario actual
            if (_idUsuarioSeleccionado.Value == Program.UsuarioActual.IdUsuario)
            {
                MessageBox.Show("No puede eliminar su propio usuario.",
                    "Operación no Permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmacion = MessageBox.Show(
                "¿Está seguro de desactivar este usuario?\n\n" +
                "El usuario no podrá iniciar sesión pero sus registros se mantendrán.",
                "Confirmar Desactivación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    string query = "UPDATE Usuarios SET Activo = 0 WHERE IdUsuario = @IdUsuario";
                    SqlParameter[] parametros = {
                        new SqlParameter("@IdUsuario", _idUsuarioSeleccionado.Value)
                    };

                    ConexionDB.Instancia.EjecutarComando(query, parametros);

                    MessageBox.Show("Usuario desactivado exitosamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LimpiarFormulario();
                    HabilitarControles(false);
                    CargarUsuarios();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar: {ex.Message}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            HabilitarControles(false);
            dgvUsuarios.ClearSelection();
            _idUsuarioSeleccionado = null;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            _paginaActual = 1;
            CargarUsuarios();
        }

        private void btnPaginaAnterior_Click(object sender, EventArgs e)
        {
            if (_paginaActual > 1)
            {
                _paginaActual--;
                CargarUsuarios();
            }
        }

        private void btnPaginaSiguiente_Click(object sender, EventArgs e)
        {
            if (_paginaActual < _totalPaginas)
            {
                _paginaActual++;
                CargarUsuarios();
            }
        }

        private void dgvUsuarios_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvUsuarios.SelectedRows[0];
                _idUsuarioSeleccionado = Convert.ToInt32(row.Cells["IdUsuario"].Value);

                if (!txtNombreCompleto.Enabled) // Solo si está en modo visualización
                {
                    txtUsuario.Text = row.Cells["Usuario"].Value?.ToString();
                    txtNombreCompleto.Text = row.Cells["NombreCompleto"].Value?.ToString();
                    txtCorreo.Text = row.Cells["Correo"].Value?.ToString();
                    txtTelefono.Text = row.Cells["Telefono"].Value?.ToString();
                    chkActivo.Checked = Convert.ToBoolean(row.Cells["Activo"].Value);

                    // Cargar entidad
                    CargarEntidadUsuario(_idUsuarioSeleccionado.Value);

                    // Cargar roles
                    CargarRolesUsuario(_idUsuarioSeleccionado.Value);
                }

                btnEditar.Enabled = true;
                btnEliminar.Enabled = _idUsuarioSeleccionado.Value != Program.UsuarioActual.IdUsuario;
                btnCambiarContrasena.Enabled = true;
            }
            else
            {
                _idUsuarioSeleccionado = null;
                btnEditar.Enabled = false;
                btnEliminar.Enabled = false;
                btnCambiarContrasena.Enabled = false;
            }
        }

        private void CargarEntidadUsuario(int idUsuario)
        {
            try
            {
                string query = "SELECT IdEntidad FROM Usuarios WHERE IdUsuario = @IdUsuario";
                SqlParameter[] parametros = { new SqlParameter("@IdUsuario", idUsuario) };

                object resultado = ConexionDB.Instancia.EjecutarEscalar(query, parametros);
                if (resultado != null)
                {
                    cmbEntidad.SelectedValue = resultado;
                }
            }
            catch { }
        }

        private void CargarRolesUsuario(int idUsuario)
        {
            try
            {
                // Desmarcar todos
                for (int i = 0; i < clbRoles.Items.Count; i++)
                {
                    clbRoles.SetItemChecked(i, false);
                }

                // Obtener roles del usuario
                string query = @"
                    SELECT IdRol 
                    FROM UsuarioRoles 
                    WHERE IdUsuario = @IdUsuario";

                SqlParameter[] parametros = { new SqlParameter("@IdUsuario", idUsuario) };
                DataTable dt = ConexionDB.Instancia.EjecutarConsulta(query, parametros);

                // Marcar roles correspondientes
                foreach (DataRow row in dt.Rows)
                {
                    int idRol = Convert.ToInt32(row["IdRol"]);

                    for (int i = 0; i < clbRoles.Items.Count; i++)
                    {
                        Rol rol = (Rol)clbRoles.Items[i];
                        if (rol.IdRol == idRol)
                        {
                            clbRoles.SetItemChecked(i, true);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al cargar roles: {ex.Message}");
            }
        }

        private void LimpiarFormulario()
        {
            txtUsuario.Clear();
            txtNombreCompleto.Clear();
            txtCorreo.Clear();
            txtTelefono.Clear();
            txtContrasena.Clear();
            txtConfirmarContrasena.Clear();
            cmbEntidad.SelectedIndex = -1;
            chkActivo.Checked = true;

            for (int i = 0; i < clbRoles.Items.Count; i++)
            {
                clbRoles.SetItemChecked(i, false);
            }
        }

        private void HabilitarControles(bool habilitar)
        {
            txtUsuario.Enabled = habilitar && _idUsuarioSeleccionado == null;
            txtNombreCompleto.Enabled = habilitar;
            txtCorreo.Enabled = habilitar;
            txtTelefono.Enabled = habilitar;
            txtContrasena.Enabled = habilitar && _idUsuarioSeleccionado == null;
            txtConfirmarContrasena.Enabled = habilitar && _idUsuarioSeleccionado == null;
            cmbEntidad.Enabled = habilitar;
            chkActivo.Enabled = habilitar;
            clbRoles.Enabled = habilitar;

            btnGuardar.Enabled = habilitar;
            btnCancelar.Enabled = habilitar;

            btnNuevo.Enabled = !habilitar;
            btnEditar.Enabled = !habilitar && _idUsuarioSeleccionado != null;
            btnEliminar.Enabled = !habilitar && _idUsuarioSeleccionado != null;
            btnCambiarContrasena.Enabled = !habilitar && _idUsuarioSeleccionado != null;
            dgvUsuarios.Enabled = !habilitar;
        }

        private void chkMostrarContrasena_CheckedChanged(object sender, EventArgs e)
        {
            txtContrasena.UseSystemPasswordChar = !chkMostrarContrasena.Checked;
            txtConfirmarContrasena.UseSystemPasswordChar = !chkMostrarContrasena.Checked;
        }
    }
}
