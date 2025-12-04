using BancoDeSangreApp.Data;
using BancoDeSangreApp.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace BancoDeSangreApp.Business
{
    public class EntidadSaludBLL
    {
        private readonly ConexionDB _db;

        public EntidadSaludBLL()
        {
            _db = ConexionDB.Instancia;
        }

        public DataTable ObtenerEntidades()
        {
            try
            {
                string query = @"
                    SELECT 
                        IdEntidad,
                        Nombre,
                        Codigo,
                        Direccion,
                        Telefono,
                        Correo,
                        Activo,
                        FechaCreacion,
                        (SELECT COUNT(*) FROM Usuarios WHERE IdEntidad = e.IdEntidad) AS CantidadUsuarios
                    FROM EntidadesSalud e
                    ORDER BY Nombre";

                return _db.EjecutarConsulta(query);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener entidades de salud: {ex.Message}", ex);
            }
        }

        public EntidadSalud ObtenerEntidadPorId(int idEntidad)
        {
            try
            {
                string query = @"
                    SELECT IdEntidad, Nombre, Codigo, Direccion, Telefono, Correo, Activo, FechaCreacion
                    FROM EntidadesSalud
                    WHERE IdEntidad = @IdEntidad";

                SqlParameter[] parametros = {
                    new SqlParameter("@IdEntidad", idEntidad)
                };

                DataTable dt = _db.EjecutarConsulta(query, parametros);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    return new EntidadSalud
                    {
                        IdEntidad = Convert.ToInt32(row["IdEntidad"]),
                        Nombre = row["Nombre"].ToString(),
                        Codigo = row["Codigo"].ToString(),
                        Direccion = row["Direccion"]?.ToString(),
                        Telefono = row["Telefono"]?.ToString(),
                        Correo = row["Correo"]?.ToString(),
                        Activo = Convert.ToBoolean(row["Activo"]),
                        FechaCreacion = Convert.ToDateTime(row["FechaCreacion"])
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener entidad: {ex.Message}", ex);
            }
        }

        public (bool exito, string mensaje) CrearEntidad(EntidadSalud entidad)
        {
            try
            {
                // Validaciones
                if (string.IsNullOrWhiteSpace(entidad.Nombre))
                    return (false, "El nombre de la entidad es requerido.");

                if (entidad.Nombre.Length > 150)
                    return (false, "El nombre no puede exceder 150 caracteres.");

                if (string.IsNullOrWhiteSpace(entidad.Codigo))
                    return (false, "El código de la entidad es requerido.");

                if (entidad.Codigo.Length > 20)
                    return (false, "El código no puede exceder 20 caracteres.");

                if (!ValidarCodigo(entidad.Codigo))
                    return (false, "El código solo puede contener letras, números y guiones.");

                if (!string.IsNullOrWhiteSpace(entidad.Correo) && !ValidarCorreo(entidad.Correo))
                    return (false, "El formato del correo electrónico no es válido.");

                if (!string.IsNullOrWhiteSpace(entidad.Telefono) && !ValidarTelefono(entidad.Telefono))
                    return (false, "El formato del teléfono no es válido.");

                if (ExisteCodigo(entidad.Codigo))
                    return (false, "Ya existe una entidad con ese código.");

                string query = @"
                    INSERT INTO EntidadesSalud (Nombre, Codigo, Direccion, Telefono, Correo, Activo, FechaCreacion)
                    VALUES (@Nombre, @Codigo, @Direccion, @Telefono, @Correo, @Activo, SYSDATETIME());
                    SELECT CAST(SCOPE_IDENTITY() AS INT);";

                SqlParameter[] parametros = {
                    new SqlParameter("@Nombre", entidad.Nombre.Trim()),
                    new SqlParameter("@Codigo", entidad.Codigo.Trim().ToUpper()),
                    new SqlParameter("@Direccion", (object)entidad.Direccion?.Trim() ?? DBNull.Value),
                    new SqlParameter("@Telefono", (object)entidad.Telefono?.Trim() ?? DBNull.Value),
                    new SqlParameter("@Correo", (object)entidad.Correo?.Trim() ?? DBNull.Value),
                    new SqlParameter("@Activo", entidad.Activo)
                };

                object resultado = _db.EjecutarEscalar(query, parametros);
                int idEntidadNueva = Convert.ToInt32(resultado);

                RegistrarAuditoria("EntidadesSalud", "INSERT", idEntidadNueva.ToString(), null,
                    $"Entidad '{entidad.Nombre}' (Código: {entidad.Codigo}) creada");

                return (true, "Entidad de salud creada exitosamente.");
            }
            catch (Exception ex)
            {
                return (false, $"Error al crear entidad: {ex.Message}");
            }
        }

        public (bool exito, string mensaje) ActualizarEntidad(EntidadSalud entidad)
        {
            try
            {
                // Validaciones
                if (string.IsNullOrWhiteSpace(entidad.Nombre))
                    return (false, "El nombre de la entidad es requerido.");

                if (entidad.Nombre.Length > 150)
                    return (false, "El nombre no puede exceder 150 caracteres.");

                if (!string.IsNullOrWhiteSpace(entidad.Correo) && !ValidarCorreo(entidad.Correo))
                    return (false, "El formato del correo electrónico no es válido.");

                if (!string.IsNullOrWhiteSpace(entidad.Telefono) && !ValidarTelefono(entidad.Telefono))
                    return (false, "El formato del teléfono no es válido.");

                // Nota: El código NO se puede cambiar después de crear la entidad
                string query = @"
                    UPDATE EntidadesSalud 
                    SET Nombre = @Nombre,
                        Direccion = @Direccion,
                        Telefono = @Telefono,
                        Correo = @Correo
                    WHERE IdEntidad = @IdEntidad";

                SqlParameter[] parametros = {
                    new SqlParameter("@IdEntidad", entidad.IdEntidad),
                    new SqlParameter("@Nombre", entidad.Nombre.Trim()),
                    new SqlParameter("@Direccion", (object)entidad.Direccion?.Trim() ?? DBNull.Value),
                    new SqlParameter("@Telefono", (object)entidad.Telefono?.Trim() ?? DBNull.Value),
                    new SqlParameter("@Correo", (object)entidad.Correo?.Trim() ?? DBNull.Value)
                };

                int filasAfectadas = _db.EjecutarComando(query, parametros);

                if (filasAfectadas > 0)
                {
                    RegistrarAuditoria("EntidadesSalud", "UPDATE", entidad.IdEntidad.ToString(), null,
                        $"Entidad '{entidad.Nombre}' actualizada");
                    return (true, "Entidad de salud actualizada exitosamente.");
                }
                else
                {
                    return (false, "No se pudo actualizar la entidad.");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error al actualizar entidad: {ex.Message}");
            }
        }

        public (bool exito, string mensaje) CambiarEstadoEntidad(int idEntidad, bool nuevoEstado)
        {
            try
            {
                // Verificar si tiene usuarios activos
                if (!nuevoEstado) // Si va a desactivar
                {
                    if (TieneUsuariosActivos(idEntidad))
                    {
                        return (false, "No se puede desactivar la entidad porque tiene usuarios activos. Primero desactive o reasigne los usuarios.");
                    }
                }

                string query = @"
                    UPDATE EntidadesSalud 
                    SET Activo = @Activo
                    WHERE IdEntidad = @IdEntidad";

                SqlParameter[] parametros = {
                    new SqlParameter("@IdEntidad", idEntidad),
                    new SqlParameter("@Activo", nuevoEstado)
                };

                int filasAfectadas = _db.EjecutarComando(query, parametros);

                if (filasAfectadas > 0)
                {
                    string accion = nuevoEstado ? "activada" : "desactivada";
                    RegistrarAuditoria("EntidadesSalud", "UPDATE", idEntidad.ToString(), null,
                        $"Entidad {accion}");
                    return (true, $"Entidad {accion} exitosamente.");
                }
                else
                {
                    return (false, "No se pudo cambiar el estado de la entidad.");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error al cambiar estado de la entidad: {ex.Message}");
            }
        }

        public (bool exito, string mensaje) EliminarEntidad(int idEntidad)
        {
            try
            {
                // Verificar si tiene usuarios
                if (TieneUsuarios(idEntidad))
                {
                    return (false, "No se puede eliminar la entidad porque tiene usuarios asignados. Primero reasigne o elimine los usuarios.");
                }

                string query = "DELETE FROM EntidadesSalud WHERE IdEntidad = @IdEntidad";

                SqlParameter[] parametros = {
                    new SqlParameter("@IdEntidad", idEntidad)
                };

                int filasAfectadas = _db.EjecutarComando(query, parametros);

                if (filasAfectadas > 0)
                {
                    RegistrarAuditoria("EntidadesSalud", "DELETE", idEntidad.ToString(), null,
                        "Entidad eliminada");
                    return (true, "Entidad eliminada exitosamente.");
                }
                else
                {
                    return (false, "No se pudo eliminar la entidad.");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error al eliminar entidad: {ex.Message}");
            }
        }

        private bool ExisteCodigo(string codigo)
        {
            string query = "SELECT COUNT(*) FROM EntidadesSalud WHERE Codigo = @Codigo";
            SqlParameter[] parametros = { new SqlParameter("@Codigo", codigo.Trim().ToUpper()) };
            int count = Convert.ToInt32(_db.EjecutarEscalar(query, parametros));
            return count > 0;
        }

        private bool TieneUsuarios(int idEntidad)
        {
            string query = "SELECT COUNT(*) FROM Usuarios WHERE IdEntidad = @IdEntidad";
            SqlParameter[] parametros = { new SqlParameter("@IdEntidad", idEntidad) };
            int count = Convert.ToInt32(_db.EjecutarEscalar(query, parametros));
            return count > 0;
        }

        private bool TieneUsuariosActivos(int idEntidad)
        {
            string query = "SELECT COUNT(*) FROM Usuarios WHERE IdEntidad = @IdEntidad AND Activo = 1";
            SqlParameter[] parametros = { new SqlParameter("@IdEntidad", idEntidad) };
            int count = Convert.ToInt32(_db.EjecutarEscalar(query, parametros));
            return count > 0;
        }

        private bool ValidarCodigo(string codigo)
        {
            // Solo letras, números y guiones
            return Regex.IsMatch(codigo, @"^[A-Za-z0-9\-]+$");
        }

        private bool ValidarCorreo(string correo)
        {
            return Regex.IsMatch(correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        private bool ValidarTelefono(string telefono)
        {
            // Permite números, espacios, guiones y paréntesis
            return Regex.IsMatch(telefono, @"^[\d\s\-\(\)]+$");
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