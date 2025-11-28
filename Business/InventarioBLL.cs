using BancoDeSangreApp.Data;
using BancoDeSangreApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BancoDeSangreApp.Business
{
    /// <summary>
    /// Lógica de negocio para control de inventario de sangre
    /// </summary>
    public class InventarioBLL
    {
        private readonly ConexionDB _db;

        public InventarioBLL()
        {
            _db = ConexionDB.Instancia;
        }

        /// <summary>
        /// Obtiene el inventario completo o filtrado por entidad
        /// </summary>
        public DataTable ObtenerInventario(int? idEntidad = null)
        {
            try
            {
                string query = @"
                    SELECT 
                        i.IdEntidad, i.IdTipoSangre, i.CantidadUnidades, i.UltimaActualizacion,
                        e.Nombre AS NombreEntidad,
                        ts.Codigo AS TipoSangreCodigo,
                        ts.Descripcion AS TipoSangreDescripcion,
                        CASE 
                            WHEN i.CantidadUnidades < 2 THEN 'Crítico'
                            WHEN i.CantidadUnidades < 5 THEN 'Bajo'
                            ELSE 'Normal'
                        END AS EstadoStock
                    FROM InventarioSangre i
                    INNER JOIN EntidadesSalud e ON i.IdEntidad = e.IdEntidad
                    INNER JOIN TiposSangre ts ON i.IdTipoSangre = ts.IdTipoSangre";

                List<SqlParameter> parametros = new List<SqlParameter>();

                if (idEntidad.HasValue)
                {
                    query += " WHERE i.IdEntidad = @IdEntidad";
                    parametros.Add(new SqlParameter("@IdEntidad", idEntidad.Value));
                }

                query += " ORDER BY ts.Codigo";

                return _db.EjecutarConsulta(query, parametros.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener inventario: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Actualiza el inventario basado en donaciones disponibles
        /// </summary>
        public (bool exito, string mensaje) ActualizarInventario(int? idUsuario = null)
        {
            try
            {
                // Recalcula el inventario por entidad y tipo de sangre
                string query = @"
                    MERGE InventarioSangre AS target
                    USING (
                        SELECT 
                            d.IdTipoSangre,
                            don.IdEntidad,
                            COUNT(*) AS CantidadUnidades
                        FROM Donaciones d
                        INNER JOIN Donantes don ON d.IdDonante = don.IdDonante
                        WHERE d.Estado = 'Disponible' 
                        AND d.FechaCaducidad > GETDATE()
                        GROUP BY d.IdTipoSangre, don.IdEntidad
                    ) AS source
                    ON target.IdEntidad = source.IdEntidad 
                    AND target.IdTipoSangre = source.IdTipoSangre
                    WHEN MATCHED THEN
                        UPDATE SET 
                            CantidadUnidades = source.CantidadUnidades,
                            UltimaActualizacion = SYSDATETIME()
                    WHEN NOT MATCHED BY TARGET THEN
                        INSERT (IdEntidad, IdTipoSangre, CantidadUnidades, UltimaActualizacion)
                        VALUES (source.IdEntidad, source.IdTipoSangre, source.CantidadUnidades, SYSDATETIME())
                    WHEN NOT MATCHED BY SOURCE THEN
                        UPDATE SET 
                            CantidadUnidades = 0,
                            UltimaActualizacion = SYSDATETIME();";

                _db.EjecutarComando(query);

                RegistrarAuditoria(idUsuario, "InventarioSangre", "UPDATE",
                    "ALL", null, "Inventario actualizado automáticamente");

                return (true, "Inventario actualizado exitosamente.");
            }
            catch (Exception ex)
            {
                return (false, $"Error al actualizar inventario: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene alertas de stock bajo
        /// </summary>
        public DataTable ObtenerAlertasStockBajo(int? idEntidad = null, int umbral = 5)
        {
            try
            {
                string query = @"
                    SELECT 
                        i.IdEntidad, i.IdTipoSangre, i.CantidadUnidades,
                        e.Nombre AS NombreEntidad,
                        ts.Codigo AS TipoSangreCodigo,
                        CASE 
                            WHEN i.CantidadUnidades < 2 THEN 'CRÍTICO'
                            WHEN i.CantidadUnidades < 5 THEN 'BAJO'
                            ELSE 'ADVERTENCIA'
                        END AS NivelAlerta
                    FROM InventarioSangre i
                    INNER JOIN EntidadesSalud e ON i.IdEntidad = e.IdEntidad
                    INNER JOIN TiposSangre ts ON i.IdTipoSangre = ts.IdTipoSangre
                    WHERE i.CantidadUnidades < @Umbral";

                List<SqlParameter> parametros = new List<SqlParameter>
                {
                    new SqlParameter("@Umbral", umbral)
                };

                if (idEntidad.HasValue)
                {
                    query += " AND i.IdEntidad = @IdEntidad";
                    parametros.Add(new SqlParameter("@IdEntidad", idEntidad.Value));
                }

                query += " ORDER BY i.CantidadUnidades ASC, ts.Codigo";

                return _db.EjecutarConsulta(query, parametros.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener alertas: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Obtiene donaciones próximas a caducar
        /// </summary>
        public DataTable ObtenerDonacionesProximasVencer(int diasAnticipacion = 30)
        {
            try
            {
                string query = @"
                    SELECT 
                        d.IdDonacion, d.CantidadML, d.FechaRecoleccion, d.FechaCaducidad,
                        DATEDIFF(DAY, GETDATE(), d.FechaCaducidad) AS DiasRestantes,
                        ts.Codigo AS TipoSangre,
                        don.Nombre AS NombreDonante,
                        e.Nombre AS NombreEntidad
                    FROM Donaciones d
                    INNER JOIN TiposSangre ts ON d.IdTipoSangre = ts.IdTipoSangre
                    INNER JOIN Donantes don ON d.IdDonante = don.IdDonante
                    INNER JOIN EntidadesSalud e ON don.IdEntidad = e.IdEntidad
                    WHERE d.Estado = 'Disponible'
                    AND d.FechaCaducidad BETWEEN GETDATE() AND DATEADD(DAY, @DiasAnticipacion, GETDATE())
                    ORDER BY d.FechaCaducidad ASC";

                SqlParameter[] parametros = {
                    new SqlParameter("@DiasAnticipacion", diasAnticipacion)
                };

                return _db.EjecutarConsulta(query, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener donaciones próximas a vencer: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Genera reporte de inventario consolidado
        /// </summary>
        public DataTable GenerarReporteInventario(int? idEntidad = null)
        {
            try
            {
                string query = @"
                    SELECT 
                        ts.Codigo AS TipoSangre,
                        ISNULL(SUM(CASE WHEN d.Estado = 'Disponible' THEN 1 ELSE 0 END), 0) AS UnidadesDisponibles,
                        ISNULL(SUM(CASE WHEN d.Estado = 'Reservada' THEN 1 ELSE 0 END), 0) AS UnidadesReservadas,
                        ISNULL(SUM(CASE WHEN d.Estado = 'Disponible' 
                            AND d.FechaCaducidad BETWEEN GETDATE() AND DATEADD(DAY, 7, GETDATE()) 
                            THEN 1 ELSE 0 END), 0) AS UnidadesProximasVencer,
                        MAX(i.UltimaActualizacion) AS UltimaActualizacion
                    FROM TiposSangre ts
                    LEFT JOIN Donaciones d ON ts.IdTipoSangre = d.IdTipoSangre
                    LEFT JOIN Donantes don ON d.IdDonante = don.IdDonante
                    LEFT JOIN InventarioSangre i ON ts.IdTipoSangre = i.IdTipoSangre";

                List<SqlParameter> parametros = new List<SqlParameter>();

                if (idEntidad.HasValue)
                {
                    query += " AND don.IdEntidad = @IdEntidad";
                    parametros.Add(new SqlParameter("@IdEntidad", idEntidad.Value));
                }

                query += " GROUP BY ts.Codigo ORDER BY ts.Codigo";

                return _db.EjecutarConsulta(query, parametros.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al generar reporte: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Obtiene estadísticas rápidas del inventario
        /// </summary>
        public Dictionary<string, int> ObtenerEstadisticasInventario(int? idEntidad = null)
        {
            var stats = new Dictionary<string, int>();

            try
            {
                string query = @"
                    SELECT 
                        COUNT(DISTINCT i.IdTipoSangre) AS TiposDiferentes,
                        SUM(i.CantidadUnidades) AS TotalUnidades,
                        SUM(CASE WHEN i.CantidadUnidades < 2 THEN 1 ELSE 0 END) AS TiposCriticos,
                        SUM(CASE WHEN i.CantidadUnidades < 5 THEN 1 ELSE 0 END) AS TiposBajos
                    FROM InventarioSangre i";

                List<SqlParameter> parametros = new List<SqlParameter>();

                if (idEntidad.HasValue)
                {
                    query += " WHERE i.IdEntidad = @IdEntidad";
                    parametros.Add(new SqlParameter("@IdEntidad", idEntidad.Value));
                }

                DataTable dt = _db.EjecutarConsulta(query, parametros.ToArray());

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    stats["TiposDiferentes"] = row["TiposDiferentes"] != DBNull.Value ? Convert.ToInt32(row["TiposDiferentes"]) : 0;
                    stats["TotalUnidades"] = row["TotalUnidades"] != DBNull.Value ? Convert.ToInt32(row["TotalUnidades"]) : 0;
                    stats["TiposCriticos"] = row["TiposCriticos"] != DBNull.Value ? Convert.ToInt32(row["TiposCriticos"]) : 0;
                    stats["TiposBajos"] = row["TiposBajos"] != DBNull.Value ? Convert.ToInt32(row["TiposBajos"]) : 0;
                }

                return stats;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al obtener estadísticas: {ex.Message}");
                return stats;
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