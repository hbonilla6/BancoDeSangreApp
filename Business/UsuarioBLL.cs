using BancoDeSangreApp.Data;
using BancoDeSangreApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BancoDeSangreApp.Business
{
    public class UsuarioBLL
    {
        private readonly ConexionDB _db;

        public UsuarioBLL()
        {
            _db = ConexionDB.Instancia;
        }

        public void ActualizarRolesUsuario(int idUsuario, int[] rolesIds)
        {
            try
            {
                // Eliminar roles existentes
                string queryEliminar = "DELETE FROM UsuarioRoles WHERE IdUsuario = @IdUsuario";
                SqlParameter[] paramEliminar = { new SqlParameter("@IdUsuario", idUsuario) };
                _db.EjecutarComando(queryEliminar, paramEliminar);

                // Asignar nuevos roles
                if (rolesIds != null && rolesIds.Length > 0)
                {
                    AsignarRoles(idUsuario, rolesIds);
                }

                RegistrarAuditoria(idUsuario, "UsuarioRoles", "UPDATE",
                    idUsuario.ToString(), null, $"Roles actualizados: {string.Join(", ", rolesIds)}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar roles: {ex.Message}", ex);
            }
        }

        public bool CambiarContrasena(int idUsuario, string nuevaContrasena)
        {
            try
            {
                string claveHash = SeguridadBLL.GenerarHash(nuevaContrasena);

                string query = @"
                    UPDATE Usuarios 
                    SET ClaveHash = @ClaveHash,
                        FechaModificacion = SYSDATETIME()
                    WHERE IdUsuario = @IdUsuario";

                SqlParameter[] parametros = {
                    new SqlParameter("@IdUsuario", idUsuario),
                    new SqlParameter("@ClaveHash", claveHash)
                };

                int filasAfectadas = _db.EjecutarComando(query, parametros);

                if (filasAfectadas > 0)
                {
                    RegistrarAuditoria(idUsuario, "Usuarios", "CAMBIO_CONTRASEÑA",
                        idUsuario.ToString(), null, "Contraseña actualizada");
                }

                return filasAfectadas > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al cambiar contraseña: {ex.Message}", ex);
            }
        }

        public LoginResult IniciarSesion(string nombreUsuario, string clave)
        {
            LoginResult result = new LoginResult();

            try
            {
                if (string.IsNullOrWhiteSpace(nombreUsuario) || string.IsNullOrWhiteSpace(clave))
                {
                    result.Mensaje = "Usuario y contraseña son requeridos.";
                    return result;
                }

                string query = @"
                    SELECT u.IdUsuario, u.IdEntidad, u.Usuario, u.ClaveHash, u.NombreCompleto, 
                           u.Correo, u.Telefono, u.Activo, u.UltimoAcceso, 
                           e.Nombre AS NombreEntidad
                    FROM Usuarios u
                    INNER JOIN EntidadesSalud e ON u.IdEntidad = e.IdEntidad
                    WHERE u.Usuario = @Usuario";

                SqlParameter[] parametros = { new SqlParameter("@Usuario", nombreUsuario) };
                DataTable dt = _db.EjecutarConsulta(query, parametros);

                if (dt.Rows.Count == 0)
                {
                    result.Mensaje = "Usuario o contraseña incorrectos.";
                    return result;
                }

                DataRow row = dt.Rows[0];
                bool activo = Convert.ToBoolean(row["Activo"]);

                if (!activo)
                {
                    result.Mensaje = "Usuario inactivo. Contacte al administrador.";
                    return result;
                }

                string hashAlmacenado = row["ClaveHash"].ToString();
                if (!SeguridadBLL.VerificarHash(clave, hashAlmacenado))
                {
                    result.Mensaje = "Usuario o contraseña incorrectos.";
                    return result;
                }

                Usuario usuario = new Usuario
                {
                    IdUsuario = Convert.ToInt32(row["IdUsuario"]),
                    IdEntidad = Convert.ToInt32(row["IdEntidad"]),
                    NombreUsuario = row["Usuario"].ToString(),
                    NombreCompleto = row["NombreCompleto"].ToString(),
                    Correo = row["Correo"].ToString(),
                    Telefono = row["Telefono"].ToString(),
                    Activo = activo,
                    NombreEntidad = row["NombreEntidad"].ToString(),
                    UltimoAcceso = row["UltimoAcceso"] != DBNull.Value
                        ? Convert.ToDateTime(row["UltimoAcceso"])
                        : (DateTime?)null
                };

                usuario.Roles = ObtenerRolesUsuario(usuario.IdUsuario);
                ActualizarUltimoAcceso(usuario.IdUsuario);
                RegistrarAuditoria(usuario.IdUsuario, "Usuarios", "LOGIN", usuario.IdUsuario.ToString(), null, "Inicio de sesión exitoso");

                result.Exitoso = true;
                result.Usuario = usuario;
                result.Mensaje = "Inicio de sesión exitoso.";
                return result;
            }
            catch (Exception ex)
            {
                result.Mensaje = $"Error al iniciar sesión: {ex.Message}";
                return result;
            }
        }

        public (bool exito, string mensaje) RegistrarUsuario(Usuario usuario, string clave, int[] rolesIds)
        {
            try
            {
                string errorUsuario = SeguridadBLL.ValidarNombreUsuario(usuario.NombreUsuario);
                if (!string.IsNullOrEmpty(errorUsuario))
                    return (false, errorUsuario);

                string errorClave = SeguridadBLL.ValidarFortalezaClave(clave);
                if (!string.IsNullOrEmpty(errorClave))
                    return (false, errorClave);

                if (!string.IsNullOrEmpty(usuario.Correo) && !SeguridadBLL.ValidarCorreo(usuario.Correo))
                    return (false, "El formato del correo electrónico no es válido.");

                if (ExisteUsuario(usuario.NombreUsuario))
                    return (false, "El nombre de usuario ya está en uso.");

                string hashClave = SeguridadBLL.GenerarHash(clave);

                string query = @"
                    INSERT INTO Usuarios (IdEntidad, Usuario, ClaveHash, NombreCompleto, Correo, Telefono, Activo, FechaCreacion)
                    VALUES (@IdEntidad, @Usuario, @ClaveHash, @NombreCompleto, @Correo, @Telefono, @Activo, SYSDATETIME());
                    SELECT CAST(SCOPE_IDENTITY() AS INT);";

                SqlParameter[] parametros = {
                    new SqlParameter("@IdEntidad", usuario.IdEntidad),
                    new SqlParameter("@Usuario", usuario.NombreUsuario),
                    new SqlParameter("@ClaveHash", hashClave),
                    new SqlParameter("@NombreCompleto", usuario.NombreCompleto),
                    new SqlParameter("@Correo", (object)usuario.Correo ?? DBNull.Value),
                    new SqlParameter("@Telefono", (object)usuario.Telefono ?? DBNull.Value),
                    new SqlParameter("@Activo", usuario.Activo)
                };

                object resultado = _db.EjecutarEscalar(query, parametros);
                int idUsuarioNuevo = Convert.ToInt32(resultado);

                if (rolesIds != null && rolesIds.Length > 0)
                {
                    AsignarRoles(idUsuarioNuevo, rolesIds);
                }

                RegistrarAuditoria(idUsuarioNuevo, "Usuarios", "INSERT", idUsuarioNuevo.ToString(), null, $"Usuario {usuario.NombreUsuario} registrado");
                return (true, "Usuario registrado exitosamente.");
            }
            catch (Exception ex)
            {
                return (false, $"Error al registrar usuario: {ex.Message}");
            }
        }

        public DataTable ObtenerUsuarios(int idEntidad)
        {
            try
            {
                string query = @"
                    SELECT 
                        u.IdUsuario,
                        u.Usuario,
                        u.NombreCompleto,
                        u.Correo,
                        u.Telefono,
                        u.Activo,
                        u.UltimoAcceso,
                        u.FechaCreacion,
                        STRING_AGG(r.Nombre, ', ') AS Roles
                    FROM Usuarios u
                    LEFT JOIN UsuarioRoles ur ON u.IdUsuario = ur.IdUsuario
                    LEFT JOIN Roles r ON ur.IdRol = r.IdRol
                    WHERE u.IdEntidad = @IdEntidad
                    GROUP BY u.IdUsuario, u.Usuario, u.NombreCompleto, u.Correo, u.Telefono, u.Activo, u.UltimoAcceso, u.FechaCreacion
                    ORDER BY u.NombreCompleto";

                SqlParameter[] parametros = {
                    new SqlParameter("@IdEntidad", idEntidad)
                };

                return _db.EjecutarConsulta(query, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener usuarios: {ex.Message}", ex);
            }
        }

        public (bool exito, string mensaje) ActualizarUsuario(Usuario usuario)
        {
            try
            {
                // ✅ NOTA IMPORTANTE: Este método NO modifica el campo Activo
                // El estado activo/inactivo SOLO se cambia con el método CambiarEstadoUsuario o EliminarUsuario

                string query = @"
            UPDATE Usuarios 
            SET NombreCompleto = @NombreCompleto,
                Correo = @Correo,
                Telefono = @Telefono,
                FechaModificacion = SYSDATETIME()
            WHERE IdUsuario = @IdUsuario";

                SqlParameter[] parametros = {
            new SqlParameter("@IdUsuario", usuario.IdUsuario),
            new SqlParameter("@NombreCompleto", usuario.NombreCompleto),
            new SqlParameter("@Correo", (object)usuario.Correo ?? DBNull.Value),
            new SqlParameter("@Telefono", (object)usuario.Telefono ?? DBNull.Value)
            // ✅ ELIMINADO: @Activo - Ya no se actualiza aquí
        };

                int filasAfectadas = _db.EjecutarComando(query, parametros);

                if (filasAfectadas > 0)
                {
                    RegistrarAuditoria(usuario.IdUsuario, "Usuarios", "UPDATE",
                        usuario.IdUsuario.ToString(), null, $"Usuario actualizado");
                    return (true, "Usuario actualizado exitosamente.");
                }
                else
                {
                    return (false, "No se pudo actualizar el usuario.");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error al actualizar usuario: {ex.Message}");
            }
        }

        public (bool exito, string mensaje) EliminarUsuario(int idUsuario)
        {
            try
            {
                string query = @"
                    UPDATE Usuarios 
                    SET Activo = 0,
                        FechaModificacion = SYSDATETIME()
                    WHERE IdUsuario = @IdUsuario";

                SqlParameter[] parametros = {
                    new SqlParameter("@IdUsuario", idUsuario)
                };

                int filasAfectadas = _db.EjecutarComando(query, parametros);

                if (filasAfectadas > 0)
                {
                    RegistrarAuditoria(idUsuario, "Usuarios", "DELETE",
                        idUsuario.ToString(), null, "Usuario desactivado");
                    return (true, "Usuario eliminado exitosamente.");
                }
                else
                {
                    return (false, "No se pudo eliminar el usuario.");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error al eliminar usuario: {ex.Message}");
            }
        }

        private bool ExisteUsuario(string nombreUsuario)
        {
            string query = "SELECT COUNT(*) FROM Usuarios WHERE Usuario = @Usuario";
            SqlParameter[] parametros = { new SqlParameter("@Usuario", nombreUsuario) };
            int count = Convert.ToInt32(_db.EjecutarEscalar(query, parametros));
            return count > 0;
        }

        public List<Rol> ObtenerRolesUsuario(int idUsuario)
        {
            List<Rol> roles = new List<Rol>();
            string query = @"
                SELECT r.IdRol, r.Nombre, r.Descripcion, r.NivelAcceso
                FROM Roles r
                INNER JOIN UsuarioRoles ur ON r.IdRol = ur.IdRol
                WHERE ur.IdUsuario = @IdUsuario";

            SqlParameter[] parametros = { new SqlParameter("@IdUsuario", idUsuario) };
            DataTable dt = _db.EjecutarConsulta(query, parametros);

            foreach (DataRow row in dt.Rows)
            {
                roles.Add(new Rol
                {
                    IdRol = Convert.ToInt32(row["IdRol"]),
                    Nombre = row["Nombre"].ToString(),
                    Descripcion = row["Descripcion"].ToString(),
                    NivelAcceso = Convert.ToInt32(row["NivelAcceso"])
                });
            }
            return roles;
        }

        private void AsignarRoles(int idUsuario, int[] rolesIds)
        {
            foreach (int idRol in rolesIds)
            {
                string query = "INSERT INTO UsuarioRoles (IdUsuario, IdRol) VALUES (@IdUsuario, @IdRol)";
                SqlParameter[] parametros = {
                    new SqlParameter("@IdUsuario", idUsuario),
                    new SqlParameter("@IdRol", idRol)
                };
                _db.EjecutarComando(query, parametros);
            }
        }

        private void ActualizarUltimoAcceso(int idUsuario)
        {
            string query = "UPDATE Usuarios SET UltimoAcceso = SYSDATETIME() WHERE IdUsuario = @IdUsuario";
            SqlParameter[] parametros = { new SqlParameter("@IdUsuario", idUsuario) };
            _db.EjecutarComando(query, parametros);
        }

        public List<Rol> ObtenerTodosLosRoles()
        {
            List<Rol> roles = new List<Rol>();
            string query = "SELECT IdRol, Nombre, Descripcion, NivelAcceso FROM Roles ORDER BY NivelAcceso DESC";
            DataTable dt = _db.EjecutarConsulta(query);

            foreach (DataRow row in dt.Rows)
            {
                roles.Add(new Rol
                {
                    IdRol = Convert.ToInt32(row["IdRol"]),
                    Nombre = row["Nombre"].ToString(),
                    Descripcion = row["Descripcion"].ToString(),
                    NivelAcceso = Convert.ToInt32(row["NivelAcceso"])
                });
            }
            return roles;
        }

        public DataTable ObtenerEntidadesSalud()
        {
            string query = "SELECT IdEntidad, Nombre, Codigo FROM EntidadesSalud WHERE Activo = 1 ORDER BY Nombre";
            return _db.EjecutarConsulta(query);
        }

        private void RegistrarAuditoria(int? idUsuario, string entidad, string operacion, string clavePrimaria, string valorAnterior, string valorNuevo)
        {
            try
            {
                string query = @"
                    INSERT INTO BitacoraAuditoria (IdUsuario, Entidad, Operacion, ClavePrimaria, ValorAnterior, ValorNuevo, FechaAccion)
                    VALUES (@IdUsuario, @Entidad, @Operacion, @ClavePrimaria, @ValorAnterior, @ValorNuevo, SYSDATETIME())";

                SqlParameter[] parametros = {
                    new SqlParameter("@IdUsuario", (object)idUsuario ?? DBNull.Value),
                    new SqlParameter("@Entidad", entidad),
                    new SqlParameter("@Operacion", operacion),
                    new SqlParameter("@ClavePrimaria", clavePrimaria),
                    new SqlParameter("@ValorAnterior", (object)valorAnterior ?? DBNull.Value),
                    new SqlParameter("@ValorNuevo", (object)valorNuevo ?? DBNull.Value)
                };
                _db.EjecutarComando(query, parametros);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en auditoría: {ex.Message}");
            }
        }

        public (bool exito, string mensaje) CambiarEstadoUsuario(int idUsuario, bool nuevoEstado)
        {
            try
            {
                string query = @"
            UPDATE Usuarios 
            SET Activo = @Activo,
                FechaModificacion = SYSDATETIME()
            WHERE IdUsuario = @IdUsuario";

                SqlParameter[] parametros = {
            new SqlParameter("@IdUsuario", idUsuario),
            new SqlParameter("@Activo", nuevoEstado)
        };

                int filasAfectadas = _db.EjecutarComando(query, parametros);

                if (filasAfectadas > 0)
                {
                    string accion = nuevoEstado ? "activado" : "desactivado";
                    RegistrarAuditoria(idUsuario, "Usuarios", "UPDATE",
                        idUsuario.ToString(), null, $"Usuario {accion}");
                    return (true, $"Usuario {accion} exitosamente.");
                }
                else
                {
                    return (false, "No se pudo cambiar el estado del usuario.");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error al cambiar estado del usuario: {ex.Message}");
            }
        }

        public void CerrarSesion(int idUsuario)
        {
            RegistrarAuditoria(idUsuario, "Usuarios", "LOGOUT", idUsuario.ToString(), null, "Cierre de sesión");
        }
    }
}