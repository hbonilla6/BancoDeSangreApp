using BancoDeSangreApp.Data;
using BancoDeSangreApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BancoDeSangreApp.Business
{
    /// <summary>
    /// Lógica de negocio para gestión de solicitudes médicas de sangre
    /// </summary>
    public class SolicitudMedicaBLL
    {
        private readonly ConexionDB _db;

        public SolicitudMedicaBLL()
        {
            _db = ConexionDB.Instancia;
        }

        /// <summary>
        /// Obtiene las solicitudes médicas con filtros opcionales
        /// </summary>
        public DataTable ObtenerSolicitudes(int? idEntidad = null, string estado = null)
        {
            try
            {
                string query = @"
                    SELECT 
                        s.IdSolicitud, s.IdEntidad, s.Solicitante, s.IdTipoSangre,
                        s.CantidadSolicitada, s.Prioridad, s.Estado, s.Observaciones,
                        s.FechaSolicitud, s.FechaAtencion, s.CreadoPor,
                        e.Nombre AS NombreEntidad,
                        ts.Codigo AS TipoSangreCodigo,
                        ts.Descripcion AS TipoSangreDescripcion,
                        ISNULL(u.NombreCompleto, 'Sistema') AS NombreUsuarioCreador
                    FROM SolicitudesMedicas s
                    INNER JOIN EntidadesSalud e ON s.IdEntidad = e.IdEntidad
                    INNER JOIN TiposSangre ts ON s.IdTipoSangre = ts.IdTipoSangre
                    LEFT JOIN Usuarios u ON s.CreadoPor = u.IdUsuario
                    WHERE 1=1";

                List<SqlParameter> parametros = new List<SqlParameter>();

                if (idEntidad.HasValue)
                {
                    query += " AND s.IdEntidad = @IdEntidad";
                    parametros.Add(new SqlParameter("@IdEntidad", idEntidad.Value));
                }

                if (!string.IsNullOrEmpty(estado))
                {
                    query += " AND s.Estado = @Estado";
                    parametros.Add(new SqlParameter("@Estado", estado));
                }

                query += " ORDER BY s.FechaSolicitud DESC";

                return _db.EjecutarConsulta(query, parametros.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener solicitudes: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Registra una nueva solicitud médica
        /// </summary>
        public (bool exito, string mensaje) RegistrarSolicitud(SolicitudMedica solicitud)
        {
            try
            {
                // Validaciones básicas
                if (string.IsNullOrWhiteSpace(solicitud.Solicitante))
                    return (false, "El nombre del solicitante es requerido.");

                if (solicitud.CantidadSolicitada <= 0)
                    return (false, "La cantidad solicitada debe ser mayor a cero.");

                if (string.IsNullOrEmpty(solicitud.Prioridad))
                    solicitud.Prioridad = "Normal";

                if (string.IsNullOrEmpty(solicitud.Estado))
                    solicitud.Estado = "Pendiente";

                // Insertar solicitud
                string query = @"
                    INSERT INTO SolicitudesMedicas 
                    (IdEntidad, Solicitante, IdTipoSangre, CantidadSolicitada, 
                     Prioridad, Estado, Observaciones, FechaSolicitud, CreadoPor)
                    VALUES 
                    (@IdEntidad, @Solicitante, @IdTipoSangre, @CantidadSolicitada,
                     @Prioridad, @Estado, @Observaciones, SYSDATETIME(), @CreadoPor);
                    SELECT CAST(SCOPE_IDENTITY() AS INT);";

                SqlParameter[] parametros = {
                    new SqlParameter("@IdEntidad", solicitud.IdEntidad),
                    new SqlParameter("@Solicitante", solicitud.Solicitante),
                    new SqlParameter("@IdTipoSangre", solicitud.IdTipoSangre),
                    new SqlParameter("@CantidadSolicitada", solicitud.CantidadSolicitada),
                    new SqlParameter("@Prioridad", solicitud.Prioridad),
                    new SqlParameter("@Estado", solicitud.Estado),
                    new SqlParameter("@Observaciones", (object)solicitud.Observaciones ?? DBNull.Value),
                    new SqlParameter("@CreadoPor", (object)solicitud.CreadoPor ?? DBNull.Value)
                };

                object resultado = _db.EjecutarEscalar(query, parametros);
                int idSolicitud = Convert.ToInt32(resultado);

                // Registrar en auditoría
                RegistrarAuditoria(solicitud.CreadoPor, "SolicitudesMedicas", "INSERT",
                    idSolicitud.ToString(), null,
                    $"Solicitud creada: {solicitud.Solicitante} - {solicitud.CantidadSolicitada} unidades");

                return (true, $"Solicitud registrada exitosamente con ID: {idSolicitud}");
            }
            catch (Exception ex)
            {
                return (false, $"Error al registrar solicitud: {ex.Message}");
            }
        }

        /// <summary>
        /// Actualiza el estado de una solicitud médica
        /// </summary>
        public (bool exito, string mensaje) ActualizarEstadoSolicitud(int idSolicitud, string nuevoEstado, int? idUsuario = null)
        {
            try
            {
                // Validar estado
                List<string> estadosValidos = new List<string> { "Pendiente", "Aprobada", "Rechazada", "Atendida" };
                if (!estadosValidos.Contains(nuevoEstado))
                    return (false, "Estado no válido.");

                // Obtener estado anterior
                string queryEstado = "SELECT Estado FROM SolicitudesMedicas WHERE IdSolicitud = @IdSolicitud";
                SqlParameter[] paramEstado = { new SqlParameter("@IdSolicitud", idSolicitud) };
                DataTable dtEstado = _db.EjecutarConsulta(queryEstado, paramEstado);

                if (dtEstado.Rows.Count == 0)
                    return (false, "Solicitud no encontrada.");

                string estadoAnterior = dtEstado.Rows[0]["Estado"].ToString();

                // Actualizar estado
                string query = @"
                    UPDATE SolicitudesMedicas 
                    SET Estado = @Estado,
                        FechaAtencion = CASE WHEN @Estado = 'Atendida' THEN SYSDATETIME() ELSE FechaAtencion END
                    WHERE IdSolicitud = @IdSolicitud";

                SqlParameter[] parametros = {
                    new SqlParameter("@IdSolicitud", idSolicitud),
                    new SqlParameter("@Estado", nuevoEstado)
                };

                int filasAfectadas = _db.EjecutarComando(query, parametros);

                if (filasAfectadas > 0)
                {
                    RegistrarAuditoria(idUsuario, "SolicitudesMedicas", "UPDATE",
                        idSolicitud.ToString(), estadoAnterior, nuevoEstado);

                    return (true, $"Estado actualizado a '{nuevoEstado}' exitosamente.");
                }
                else
                {
                    return (false, "No se pudo actualizar el estado.");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error al actualizar estado: {ex.Message}");
            }
        }

        /// <summary>
        /// Asigna donaciones a una solicitud médica
        /// </summary>
        public (bool exito, string mensaje) AsignarDonaciones(int idSolicitud, List<int> idsDonaciones, int? idUsuario = null)
        {
            try
            {
                if (idsDonaciones == null || idsDonaciones.Count == 0)
                    return (false, "Debe seleccionar al menos una donación.");

                // Verificar que la solicitud existe y está aprobada
                string queryVerificar = @"
                    SELECT Estado, CantidadSolicitada 
                    FROM SolicitudesMedicas 
                    WHERE IdSolicitud = @IdSolicitud";

                SqlParameter[] paramVerificar = { new SqlParameter("@IdSolicitud", idSolicitud) };
                DataTable dtVerificar = _db.EjecutarConsulta(queryVerificar, paramVerificar);

                if (dtVerificar.Rows.Count == 0)
                    return (false, "Solicitud no encontrada.");

                string estado = dtVerificar.Rows[0]["Estado"].ToString();
                if (estado != "Aprobada")
                    return (false, "Solo se pueden asignar donaciones a solicitudes aprobadas.");

                // Asignar cada donación
                foreach (int idDonacion in idsDonaciones)
                {
                    // Verificar que la donación no esté ya asignada
                    string queryVerifDonacion = @"
                        SELECT COUNT(*) 
                        FROM SolicitudesDonaciones 
                        WHERE IdDonacion = @IdDonacion";

                    SqlParameter[] paramVerifDonacion = { new SqlParameter("@IdDonacion", idDonacion) };
                    int yaAsignada = Convert.ToInt32(_db.EjecutarEscalar(queryVerifDonacion, paramVerifDonacion));

                    if (yaAsignada > 0)
                        continue; // Saltar esta donación si ya está asignada

                    // Insertar asignación
                    string queryAsignar = @"
                        INSERT INTO SolicitudesDonaciones (IdSolicitud, IdDonacion, FechaAsignacion)
                        VALUES (@IdSolicitud, @IdDonacion, SYSDATETIME())";

                    SqlParameter[] paramAsignar = {
                        new SqlParameter("@IdSolicitud", idSolicitud),
                        new SqlParameter("@IdDonacion", idDonacion)
                    };

                    _db.EjecutarComando(queryAsignar, paramAsignar);

                    // Actualizar estado de la donación a 'Asignada'
                    string queryActualizarDonacion = @"
                        UPDATE Donaciones 
                        SET Estado = 'Asignada' 
                        WHERE IdDonacion = @IdDonacion";

                    SqlParameter[] paramActualizar = { new SqlParameter("@IdDonacion", idDonacion) };
                    _db.EjecutarComando(queryActualizarDonacion, paramActualizar);
                }

                // Actualizar estado de solicitud a 'Atendida'
                ActualizarEstadoSolicitud(idSolicitud, "Atendida", idUsuario);

                RegistrarAuditoria(idUsuario, "SolicitudesDonaciones", "INSERT",
                    idSolicitud.ToString(), null,
                    $"Asignadas {idsDonaciones.Count} donaciones a solicitud {idSolicitud}");

                return (true, "Donaciones asignadas exitosamente.");
            }
            catch (Exception ex)
            {
                return (false, $"Error al asignar donaciones: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene donaciones disponibles para una solicitud específica
        /// </summary>
        public DataTable ObtenerDonacionesDisponibles(int idSolicitud)
        {
            try
            {
                string query = @"
                    SELECT 
                        d.IdDonacion, d.CantidadML, d.FechaRecoleccion, d.FechaCaducidad,
                        DATEDIFF(DAY, GETDATE(), d.FechaCaducidad) AS DiasRestantes,
                        ts.Codigo AS TipoSangre,
                        don.Nombre AS NombreDonante
                    FROM Donaciones d
                    INNER JOIN TiposSangre ts ON d.IdTipoSangre = ts.IdTipoSangre
                    INNER JOIN Donantes don ON d.IdDonante = don.IdDonante
                    INNER JOIN SolicitudesMedicas s ON s.IdTipoSangre = d.IdTipoSangre
                    WHERE s.IdSolicitud = @IdSolicitud
                    AND d.Estado = 'Disponible'
                    AND d.FechaCaducidad > GETDATE()
                    AND d.IdDonacion NOT IN (
                        SELECT IdDonacion FROM SolicitudesDonaciones
                    )
                    ORDER BY d.FechaCaducidad ASC";

                SqlParameter[] parametros = { new SqlParameter("@IdSolicitud", idSolicitud) };
                return _db.EjecutarConsulta(query, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener donaciones disponibles: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Obtiene estadísticas de solicitudes
        /// </summary>
        public Dictionary<string, int> ObtenerEstadisticasSolicitudes(int? idEntidad = null)
        {
            var stats = new Dictionary<string, int>();

            try
            {
                string query = @"
                    SELECT 
                        COUNT(*) AS Total,
                        SUM(CASE WHEN Estado = 'Pendiente' THEN 1 ELSE 0 END) AS Pendientes,
                        SUM(CASE WHEN Estado = 'Aprobada' THEN 1 ELSE 0 END) AS Aprobadas,
                        SUM(CASE WHEN Estado = 'Rechazada' THEN 1 ELSE 0 END) AS Rechazadas,
                        SUM(CASE WHEN Estado = 'Atendida' THEN 1 ELSE 0 END) AS Atendidas,
                        SUM(CASE WHEN Prioridad = 'Emergencia' THEN 1 ELSE 0 END) AS Emergencias
                    FROM SolicitudesMedicas
                    WHERE 1=1";

                List<SqlParameter> parametros = new List<SqlParameter>();

                if (idEntidad.HasValue)
                {
                    query += " AND IdEntidad = @IdEntidad";
                    parametros.Add(new SqlParameter("@IdEntidad", idEntidad.Value));
                }

                DataTable dt = _db.EjecutarConsulta(query, parametros.ToArray());

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    stats["Total"] = Convert.ToInt32(row["Total"]);
                    stats["Pendientes"] = Convert.ToInt32(row["Pendientes"]);
                    stats["Aprobadas"] = Convert.ToInt32(row["Aprobadas"]);
                    stats["Rechazadas"] = Convert.ToInt32(row["Rechazadas"]);
                    stats["Atendidas"] = Convert.ToInt32(row["Atendidas"]);
                    stats["Emergencias"] = Convert.ToInt32(row["Emergencias"]);
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
        /// Obtiene el detalle completo de una solicitud
        /// </summary>
        public SolicitudMedica ObtenerSolicitudPorId(int idSolicitud)
        {
            try
            {
                string query = @"
                    SELECT 
                        s.IdSolicitud, s.IdEntidad, s.Solicitante, s.IdTipoSangre,
                        s.CantidadSolicitada, s.Prioridad, s.Estado, s.Observaciones,
                        s.FechaSolicitud, s.FechaAtencion, s.CreadoPor,
                        e.Nombre AS NombreEntidad,
                        ts.Codigo AS TipoSangreCodigo
                    FROM SolicitudesMedicas s
                    INNER JOIN EntidadesSalud e ON s.IdEntidad = e.IdEntidad
                    INNER JOIN TiposSangre ts ON s.IdTipoSangre = ts.IdTipoSangre
                    WHERE s.IdSolicitud = @IdSolicitud";

                SqlParameter[] parametros = { new SqlParameter("@IdSolicitud", idSolicitud) };
                DataTable dt = _db.EjecutarConsulta(query, parametros);

                if (dt.Rows.Count == 0)
                    return null;

                DataRow row = dt.Rows[0];
                return new SolicitudMedica
                {
                    IdSolicitud = Convert.ToInt32(row["IdSolicitud"]),
                    IdEntidad = Convert.ToInt32(row["IdEntidad"]),
                    Solicitante = row["Solicitante"].ToString(),
                    IdTipoSangre = Convert.ToInt32(row["IdTipoSangre"]),
                    CantidadSolicitada = Convert.ToInt32(row["CantidadSolicitada"]),
                    Prioridad = row["Prioridad"].ToString(),
                    Estado = row["Estado"].ToString(),
                    Observaciones = row["Observaciones"]?.ToString(),
                    FechaSolicitud = Convert.ToDateTime(row["FechaSolicitud"]),
                    FechaAtencion = row["FechaAtencion"] != DBNull.Value
                        ? Convert.ToDateTime(row["FechaAtencion"])
                        : (DateTime?)null,
                    CreadoPor = row["CreadoPor"] != DBNull.Value
                        ? Convert.ToInt32(row["CreadoPor"])
                        : (int?)null,
                    NombreEntidad = row["NombreEntidad"].ToString(),
                    TipoSangreCodigo = row["TipoSangreCodigo"].ToString()
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener solicitud: {ex.Message}", ex);
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