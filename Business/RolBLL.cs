using BancoDeSangreApp.Data;
using BancoDeSangreApp.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BancoDeSangreApp.Business
{
    public class RolBLL
    {
        private readonly ConexionDB _db;

        public RolBLL()
        {
            _db = ConexionDB.Instancia;
        }

        public DataTable ObtenerRoles()
        {
            try
            {
                string query = @"
                    SELECT 
                        IdRol,
                        Nombre,
                        Descripcion,
                        NivelAcceso,
                        FechaCreacion,
                        (SELECT COUNT(*) FROM UsuarioRoles WHERE IdRol = r.IdRol) AS CantidadUsuarios
                    FROM Roles r
                    ORDER BY NivelAcceso DESC, Nombre";

                return _db.EjecutarConsulta(query);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener roles: {ex.Message}", ex);
            }
        }

        public Rol ObtenerRolPorId(int idRol)
        {
            try
            {
                string query = @"
                    SELECT IdRol, Nombre, Descripcion, NivelAcceso, FechaCreacion
                    FROM Roles
                    WHERE IdRol = @IdRol";

                SqlParameter[] parametros = {
                    new SqlParameter("@IdRol", idRol)
                };

                DataTable dt = _db.EjecutarConsulta(query, parametros);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    return new Rol
                    {
                        IdRol = Convert.ToInt32(row["IdRol"]),
                        Nombre = row["Nombre"].ToString(),
                        Descripcion = row["Descripcion"].ToString(),
                        NivelAcceso = Convert.ToInt32(row["NivelAcceso"]),
                        FechaCreacion = Convert.ToDateTime(row["FechaCreacion"])
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener rol: {ex.Message}", ex);
            }
        }

        public (bool exito, string mensaje) CrearRol(Rol rol)
        {
            try
            {
                // Validaciones
                if (string.IsNullOrWhiteSpace(rol.Nombre))
                    return (false, "El nombre del rol es requerido.");

                if (rol.Nombre.Length > 50)
                    return (false, "El nombre del rol no puede exceder 50 caracteres.");

                if (rol.NivelAcceso < 1 || rol.NivelAcceso > 5)
                    return (false, "El nivel de acceso debe estar entre 1 y 5.");

                if (ExisteRol(rol.Nombre))
                    return (false, "Ya existe un rol con ese nombre.");

                string query = @"
                    INSERT INTO Roles (Nombre, Descripcion, NivelAcceso, FechaCreacion)
                    VALUES (@Nombre, @Descripcion, @NivelAcceso, SYSDATETIME());
                    SELECT CAST(SCOPE_IDENTITY() AS INT);";

                SqlParameter[] parametros = {
                    new SqlParameter("@Nombre", rol.Nombre.Trim()),
                    new SqlParameter("@Descripcion", (object)rol.Descripcion?.Trim() ?? DBNull.Value),
                    new SqlParameter("@NivelAcceso", rol.NivelAcceso)
                };

                object resultado = _db.EjecutarEscalar(query, parametros);
                int idRolNuevo = Convert.ToInt32(resultado);

                RegistrarAuditoria("Roles", "INSERT", idRolNuevo.ToString(), null,
                    $"Rol '{rol.Nombre}' creado con nivel {rol.NivelAcceso}");

                return (true, "Rol creado exitosamente.");
            }
            catch (Exception ex)
            {
                return (false, $"Error al crear rol: {ex.Message}");
            }
        }

        public (bool exito, string mensaje) ActualizarRol(Rol rol)
        {
            try
            {
                // Validaciones
                if (string.IsNullOrWhiteSpace(rol.Nombre))
                    return (false, "El nombre del rol es requerido.");

                if (rol.Nombre.Length > 50)
                    return (false, "El nombre del rol no puede exceder 50 caracteres.");

                if (rol.NivelAcceso < 1 || rol.NivelAcceso > 5)
                    return (false, "El nivel de acceso debe estar entre 1 y 5.");

                if (ExisteRolParaOtro(rol.Nombre, rol.IdRol))
                    return (false, "Ya existe otro rol con ese nombre.");

                string query = @"
                    UPDATE Roles 
                    SET Nombre = @Nombre,
                        Descripcion = @Descripcion,
                        NivelAcceso = @NivelAcceso
                    WHERE IdRol = @IdRol";

                SqlParameter[] parametros = {
                    new SqlParameter("@IdRol", rol.IdRol),
                    new SqlParameter("@Nombre", rol.Nombre.Trim()),
                    new SqlParameter("@Descripcion", (object)rol.Descripcion?.Trim() ?? DBNull.Value),
                    new SqlParameter("@NivelAcceso", rol.NivelAcceso)
                };

                int filasAfectadas = _db.EjecutarComando(query, parametros);

                if (filasAfectadas > 0)
                {
                    RegistrarAuditoria("Roles", "UPDATE", rol.IdRol.ToString(), null,
                        $"Rol '{rol.Nombre}' actualizado");
                    return (true, "Rol actualizado exitosamente.");
                }
                else
                {
                    return (false, "No se pudo actualizar el rol.");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error al actualizar rol: {ex.Message}");
            }
        }

        public (bool exito, string mensaje) EliminarRol(int idRol)
        {
            try
            {
                // Verificar si hay usuarios asignados a este rol
                if (TieneUsuariosAsignados(idRol))
                {
                    return (false, "No se puede eliminar el rol porque tiene usuarios asignados. Primero reasigne o elimine los usuarios.");
                }

                string query = "DELETE FROM Roles WHERE IdRol = @IdRol";

                SqlParameter[] parametros = {
                    new SqlParameter("@IdRol", idRol)
                };

                int filasAfectadas = _db.EjecutarComando(query, parametros);

                if (filasAfectadas > 0)
                {
                    RegistrarAuditoria("Roles", "DELETE", idRol.ToString(), null, "Rol eliminado");
                    return (true, "Rol eliminado exitosamente.");
                }
                else
                {
                    return (false, "No se pudo eliminar el rol.");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error al eliminar rol: {ex.Message}");
            }
        }

        private bool ExisteRol(string nombre)
        {
            string query = "SELECT COUNT(*) FROM Roles WHERE Nombre = @Nombre";
            SqlParameter[] parametros = { new SqlParameter("@Nombre", nombre.Trim()) };
            int count = Convert.ToInt32(_db.EjecutarEscalar(query, parametros));
            return count > 0;
        }

        private bool ExisteRolParaOtro(string nombre, int idRolActual)
        {
            string query = "SELECT COUNT(*) FROM Roles WHERE Nombre = @Nombre AND IdRol != @IdRol";
            SqlParameter[] parametros = {
                new SqlParameter("@Nombre", nombre.Trim()),
                new SqlParameter("@IdRol", idRolActual)
            };
            int count = Convert.ToInt32(_db.EjecutarEscalar(query, parametros));
            return count > 0;
        }

        private bool TieneUsuariosAsignados(int idRol)
        {
            string query = "SELECT COUNT(*) FROM UsuarioRoles WHERE IdRol = @IdRol";
            SqlParameter[] parametros = { new SqlParameter("@IdRol", idRol) };
            int count = Convert.ToInt32(_db.EjecutarEscalar(query, parametros));
            return count > 0;
        }

        private void RegistrarAuditoria(string entidad, string operacion, string clavePrimaria, string valorAnterior, string valorNuevo)
        {
            try
            {
                string query = @"
                    INSERT INTO BitacoraAuditoria (IdUsuario, Entidad, Operacion, ClavePrimaria, ValorAnterior, ValorNuevo, FechaAccion)
                    VALUES (@IdUsuario, @Entidad, @Operacion, @ClavePrimaria, @ValorAnterior, @ValorNuevo, SYSDATETIME())";

                SqlParameter[] parametros = {
                    new SqlParameter("@IdUsuario", (object)Program.UsuarioActual?.IdUsuario ?? DBNull.Value),
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
    }
}