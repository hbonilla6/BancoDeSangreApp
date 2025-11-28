using BancoDeSangreApp.Data;
using BancoDeSangreApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BancoDeSangreApp.Business
{
    /// <summary>
    /// Lógica de negocio para consulta de bitácora de auditoría
    /// </summary>
    public class AuditoriaBLL
    {
        private readonly ConexionDB _db;

        public AuditoriaBLL()
        {
            _db = ConexionDB.Instancia;
        }

        /// <summary>
        /// Obtiene registros de auditoría con filtros opcionales
        /// </summary>
        public DataTable ObtenerAuditoria(DateTime? fechaInicio = null, DateTime? fechaFin = null,
            int? idUsuario = null, string entidad = null, string operacion = null, int limite = 1000)
        {
            try
            {
                string query = @"
                    SELECT TOP (@Limite)
                        b.IdAuditoria, b.IdUsuario, b.Entidad, b.Operacion, 
                        b.ClavePrimaria, b.ValorAnterior, b.ValorNuevo, b.FechaAccion,
                        ISNULL(u.NombreCompleto, 'Sistema') AS NombreUsuario,
                        u.Usuario AS NombreUsuarioLogin
                    FROM BitacoraAuditoria b
                    LEFT JOIN Usuarios u ON b.IdUsuario = u.IdUsuario
                    WHERE 1=1";

                List<SqlParameter> parametros = new List<SqlParameter>
                {
                    new SqlParameter("@Limite", limite)
                };

                if (fechaInicio.HasValue)
                {
                    query += " AND b.FechaAccion >= @FechaInicio";
                    parametros.Add(new SqlParameter("@FechaInicio", fechaInicio.Value.Date));
                }

                if (fechaFin.HasValue)
                {
                    query += " AND b.FechaAccion <= @FechaFin";
                    parametros.Add(new SqlParameter("@FechaFin", fechaFin.Value.Date.AddDays(1).AddSeconds(-1)));
                }

                if (idUsuario.HasValue)
                {
                    query += " AND b.IdUsuario = @IdUsuario";
                    parametros.Add(new SqlParameter("@IdUsuario", idUsuario.Value));
                }

                if (!string.IsNullOrEmpty(entidad))
                {
                    query += " AND b.Entidad = @Entidad";
                    parametros.Add(new SqlParameter("@Entidad", entidad));
                }

                if (!string.IsNullOrEmpty(operacion))
                {
                    query += " AND b.Operacion = @Operacion";
                    parametros.Add(new SqlParameter("@Operacion", operacion));
                }

                query += " ORDER BY b.FechaAccion DESC";

                return _db.EjecutarConsulta(query, parametros.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener auditoría: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Obtiene las entidades únicas registradas en auditoría
        /// </summary>
        public List<string> ObtenerEntidades()
        {
            List<string> entidades = new List<string>();

            try
            {
                string query = "SELECT DISTINCT Entidad FROM BitacoraAuditoria ORDER BY Entidad";
                DataTable dt = _db.EjecutarConsulta(query);

                foreach (DataRow row in dt.Rows)
                {
                    entidades.Add(row["Entidad"].ToString());
                }

                return entidades;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al obtener entidades: {ex.Message}");
                return entidades;
            }
        }

        /// <summary>
        /// Obtiene las operaciones únicas registradas en auditoría
        /// </summary>
        public List<string> ObtenerOperaciones()
        {
            List<string> operaciones = new List<string>();

            try
            {
                string query = "SELECT DISTINCT Operacion FROM BitacoraAuditoria ORDER BY Operacion";
                DataTable dt = _db.EjecutarConsulta(query);

                foreach (DataRow row in dt.Rows)
                {
                    operaciones.Add(row["Operacion"].ToString());
                }

                return operaciones;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al obtener operaciones: {ex.Message}");
                return operaciones;
            }
        }

        /// <summary>
        /// Obtiene estadísticas de auditoría por período
        /// </summary>
        public Dictionary<string, int> ObtenerEstadisticasAuditoria(DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            var stats = new Dictionary<string, int>();

            try
            {
                string query = @"
                    SELECT 
                        COUNT(*) AS TotalRegistros,
                        COUNT(DISTINCT IdUsuario) AS UsuariosActivos,
                        SUM(CASE WHEN Operacion = 'INSERT' THEN 1 ELSE 0 END) AS TotalInserts,
                        SUM(CASE WHEN Operacion = 'UPDATE' THEN 1 ELSE 0 END) AS TotalUpdates,
                        SUM(CASE WHEN Operacion = 'DELETE' THEN 1 ELSE 0 END) AS TotalDeletes,
                        SUM(CASE WHEN Operacion = 'LOGIN' THEN 1 ELSE 0 END) AS TotalLogins
                    FROM BitacoraAuditoria
                    WHERE 1=1";

                List<SqlParameter> parametros = new List<SqlParameter>();

                if (fechaInicio.HasValue)
                {
                    query += " AND FechaAccion >= @FechaInicio";
                    parametros.Add(new SqlParameter("@FechaInicio", fechaInicio.Value.Date));
                }

                if (fechaFin.HasValue)
                {
                    query += " AND FechaAccion <= @FechaFin";
                    parametros.Add(new SqlParameter("@FechaFin", fechaFin.Value.Date.AddDays(1).AddSeconds(-1)));
                }

                DataTable dt = _db.EjecutarConsulta(query, parametros.ToArray());

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    stats["TotalRegistros"] = Convert.ToInt32(row["TotalRegistros"]);
                    stats["UsuariosActivos"] = Convert.ToInt32(row["UsuariosActivos"]);
                    stats["TotalInserts"] = Convert.ToInt32(row["TotalInserts"]);
                    stats["TotalUpdates"] = Convert.ToInt32(row["TotalUpdates"]);
                    stats["TotalDeletes"] = Convert.ToInt32(row["TotalDeletes"]);
                    stats["TotalLogins"] = Convert.ToInt32(row["TotalLogins"]);
                }

                return stats;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al obtener estadísticas: {ex.Message}");
                return stats;
            }
        }

        /// <summary>
        /// Obtiene actividad reciente de un usuario específico
        /// </summary>
        public DataTable ObtenerActividadUsuario(int idUsuario, int limite = 50)
        {
            try
            {
                string query = @"
                    SELECT TOP (@Limite)
                        IdAuditoria, Entidad, Operacion, ClavePrimaria, 
                        ValorNuevo, FechaAccion
                    FROM BitacoraAuditoria
                    WHERE IdUsuario = @IdUsuario
                    ORDER BY FechaAccion DESC";

                SqlParameter[] parametros = {
                    new SqlParameter("@IdUsuario", idUsuario),
                    new SqlParameter("@Limite", limite)
                };

                return _db.EjecutarConsulta(query, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener actividad del usuario: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Obtiene el historial de cambios de un registro específico
        /// </summary>
        public DataTable ObtenerHistorialRegistro(string entidad, string clavePrimaria)
        {
            try
            {
                string query = @"
                    SELECT 
                        b.IdAuditoria, b.Operacion, b.ValorAnterior, b.ValorNuevo, 
                        b.FechaAccion, ISNULL(u.NombreCompleto, 'Sistema') AS Usuario
                    FROM BitacoraAuditoria b
                    LEFT JOIN Usuarios u ON b.IdUsuario = u.IdUsuario
                    WHERE b.Entidad = @Entidad AND b.ClavePrimaria = @ClavePrimaria
                    ORDER BY b.FechaAccion DESC";

                SqlParameter[] parametros = {
                    new SqlParameter("@Entidad", entidad),
                    new SqlParameter("@ClavePrimaria", clavePrimaria)
                };

                return _db.EjecutarConsulta(query, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener historial: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Exporta auditoría a texto plano para reportes
        /// </summary>
        public string ExportarAuditoriaTexto(DataTable dtAuditoria)
        {
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.AppendLine("=== REPORTE DE AUDITORÍA ===");
                sb.AppendLine($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                sb.AppendLine($"Total de registros: {dtAuditoria.Rows.Count}");
                sb.AppendLine(new string('=', 80));
                sb.AppendLine();

                foreach (DataRow row in dtAuditoria.Rows)
                {
                    sb.AppendLine($"[{row["FechaAccion"]}] {row["NombreUsuario"]} - {row["Operacion"]}");
                    sb.AppendLine($"  Entidad: {row["Entidad"]} | Clave: {row["ClavePrimaria"]}");

                    if (row["ValorNuevo"] != DBNull.Value)
                        sb.AppendLine($"  Detalle: {row["ValorNuevo"]}");

                    sb.AppendLine();
                }

                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al exportar auditoría: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Limpia registros de auditoría antiguos (mantenimiento)
        /// </summary>
        public (bool exito, string mensaje) LimpiarAuditoriaAntigua(int diasAntiguedad, int? idUsuario = null)
        {
            try
            {
                string query = @"
                    DELETE FROM BitacoraAuditoria 
                    WHERE FechaAccion < DATEADD(DAY, -@DiasAntiguedad, GETDATE())";

                SqlParameter[] parametros = {
                    new SqlParameter("@DiasAntiguedad", diasAntiguedad)
                };

                int registrosEliminados = _db.EjecutarComando(query, parametros);

                // Registrar la limpieza
                RegistrarAuditoria(idUsuario, "BitacoraAuditoria", "DELETE",
                    "LIMPIEZA", null, $"Eliminados {registrosEliminados} registros con más de {diasAntiguedad} días");

                return (true, $"Se eliminaron {registrosEliminados} registros antiguos.");
            }
            catch (Exception ex)
            {
                return (false, $"Error al limpiar auditoría: {ex.Message}");
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

                _db.EjecutarComando(query, parametros);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en auditoría: {ex.Message}");
            }
        }
    }
}