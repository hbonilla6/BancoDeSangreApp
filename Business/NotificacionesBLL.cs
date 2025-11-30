using BancoDeSangreApp.Data;
using BancoDeSangreApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace BancoDeSangreApp.Business
{
    /// <summary>
    /// Lógica de negocio para el sistema de notificaciones
    /// </summary>
    public class NotificacionesBLL
    {
        private readonly ConexionDB _db;
        private readonly InventarioBLL _inventarioBLL;
        private readonly SolicitudMedicaBLL _solicitudBLL;

        public NotificacionesBLL()
        {
            _db = ConexionDB.Instancia;
            _inventarioBLL = new InventarioBLL();
            _solicitudBLL = new SolicitudMedicaBLL();
        }

        /// <summary>
        /// Modelo para notificaciones
        /// </summary>
        public class Notificacion
        {
            public string Tipo { get; set; } // Crítico, Advertencia, Info
            public string Titulo { get; set; }
            public string Mensaje { get; set; }
            public DateTime Fecha { get; set; }
            public string Icono { get; set; }
            public System.Drawing.Color Color { get; set; }

            public Notificacion()
            {
                Fecha = DateTime.Now;
            }
        }

        /// <summary>
        /// Obtiene todas las notificaciones activas para una entidad
        /// </summary>
        public List<Notificacion> ObtenerNotificaciones(int idEntidad)
        {
            var notificaciones = new List<Notificacion>();

            try
            {
                // 1. Alertas de stock bajo/crítico
                var alertasStock = ObtenerAlertasStockBajo(idEntidad);
                notificaciones.AddRange(alertasStock);

                // 2. Solicitudes urgentes pendientes
                var solicitudesUrgentes = ObtenerSolicitudesUrgentes(idEntidad);
                notificaciones.AddRange(solicitudesUrgentes);

                // 3. Donaciones próximas a vencer
                var donacionesVencer = ObtenerDonacionesProximasVencer(idEntidad);
                notificaciones.AddRange(donacionesVencer);

                // 4. Donaciones recientes (últimas 24 horas)
                var donacionesRecientes = ObtenerDonacionesRecientes(idEntidad);
                notificaciones.AddRange(donacionesRecientes);

                // Ordenar por tipo (Crítico > Advertencia > Info) y luego por fecha
                notificaciones = notificaciones
                    .OrderBy(n => n.Tipo == "Crítico" ? 0 : n.Tipo == "Advertencia" ? 1 : 2)
                    .ThenByDescending(n => n.Fecha)
                    .ToList();

                return notificaciones;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al obtener notificaciones: {ex.Message}");
                return notificaciones;
            }
        }

        private List<Notificacion> ObtenerAlertasStockBajo(int idEntidad)
        {
            var notificaciones = new List<Notificacion>();

            try
            {
                DataTable alertas = _inventarioBLL.ObtenerAlertasStockBajo(idEntidad, 5);

                foreach (DataRow row in alertas.Rows)
                {
                    int cantidad = Convert.ToInt32(row["CantidadUnidades"]);
                    string tipoSangre = row["TipoSangreCodigo"].ToString();

                    Notificacion notif = new Notificacion
                    {
                        Titulo = $"Stock {(cantidad == 0 ? "Agotado" : "Bajo")} - {tipoSangre}",
                        Mensaje = cantidad == 0
                            ? $"⚠️ NO HAY unidades de tipo {tipoSangre} disponibles"
                            : $"Solo quedan {cantidad} unidad(es) de tipo {tipoSangre}",
                        Tipo = cantidad == 0 ? "Crítico" : cantidad < 3 ? "Crítico" : "Advertencia",
                        Icono = "📦",
                        Color = cantidad == 0
                            ? System.Drawing.Color.FromArgb(220, 53, 69)
                            : System.Drawing.Color.FromArgb(255, 193, 7)
                    };

                    notificaciones.Add(notif);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en alertas de stock: {ex.Message}");
            }

            return notificaciones;
        }

        private List<Notificacion> ObtenerSolicitudesUrgentes(int idEntidad)
        {
            var notificaciones = new List<Notificacion>();

            try
            {
                string query = @"
                    SELECT COUNT(*) AS Total, 
                           SUM(CASE WHEN Prioridad = 'Emergencia' THEN 1 ELSE 0 END) AS Emergencias
                    FROM SolicitudesMedicas
                    WHERE IdEntidad = @IdEntidad 
                    AND Estado = 'Pendiente'";

                SqlParameter[] parametros = {
                    new SqlParameter("@IdEntidad", idEntidad)
                };

                DataTable dt = _db.EjecutarConsulta(query, parametros);

                if (dt.Rows.Count > 0)
                {
                    int total = Convert.ToInt32(dt.Rows[0]["Total"]);
                    int emergencias = Convert.ToInt32(dt.Rows[0]["Emergencias"]);

                    if (emergencias > 0)
                    {
                        notificaciones.Add(new Notificacion
                        {
                            Titulo = "Solicitudes de Emergencia",
                            Mensaje = $"🚨 {emergencias} solicitud(es) de EMERGENCIA pendientes de atención",
                            Tipo = "Crítico",
                            Icono = "🚨",
                            Color = System.Drawing.Color.FromArgb(220, 53, 69)
                        });
                    }

                    if (total > emergencias)
                    {
                        int normales = total - emergencias;
                        notificaciones.Add(new Notificacion
                        {
                            Titulo = "Solicitudes Pendientes",
                            Mensaje = $"{normales} solicitud(es) médicas pendientes de revisión",
                            Tipo = "Info",
                            Icono = "📋",
                            Color = System.Drawing.Color.FromArgb(0, 123, 255)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en solicitudes urgentes: {ex.Message}");
            }

            return notificaciones;
        }

        private List<Notificacion> ObtenerDonacionesProximasVencer(int idEntidad)
        {
            var notificaciones = new List<Notificacion>();

            try
            {
                DataTable dt = _inventarioBLL.ObtenerDonacionesProximasVencer(7);

                // Filtrar por entidad
                var donacionesEntidad = dt.AsEnumerable()
                    .Where(row => row.Table.Columns.Contains("NombreEntidad"))
                    .Count();

                if (dt.Rows.Count > 0)
                {
                    // Contar donaciones por urgencia
                    int criticas = 0; // <= 3 días
                    int advertencia = 0; // 4-7 días

                    foreach (DataRow row in dt.Rows)
                    {
                        int dias = Convert.ToInt32(row["DiasRestantes"]);
                        if (dias <= 3) criticas++;
                        else if (dias <= 7) advertencia++;
                    }

                    if (criticas > 0)
                    {
                        notificaciones.Add(new Notificacion
                        {
                            Titulo = "Donaciones por Vencer (Crítico)",
                            Mensaje = $"⏰ {criticas} unidad(es) vencen en 3 días o menos",
                            Tipo = "Crítico",
                            Icono = "⏰",
                            Color = System.Drawing.Color.FromArgb(220, 53, 69)
                        });
                    }

                    if (advertencia > 0)
                    {
                        notificaciones.Add(new Notificacion
                        {
                            Titulo = "Donaciones Próximas a Vencer",
                            Mensaje = $"{advertencia} unidad(es) vencen en los próximos 7 días",
                            Tipo = "Advertencia",
                            Icono = "📅",
                            Color = System.Drawing.Color.FromArgb(255, 193, 7)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en donaciones por vencer: {ex.Message}");
            }

            return notificaciones;
        }

        private List<Notificacion> ObtenerDonacionesRecientes(int idEntidad)
        {
            var notificaciones = new List<Notificacion>();

            try
            {
                string query = @"
                    SELECT COUNT(*) AS Total
                    FROM Donaciones d
                    INNER JOIN Donantes dn ON d.IdDonante = dn.IdDonante
                    WHERE dn.IdEntidad = @IdEntidad
                    AND d.FechaRecoleccion >= DATEADD(HOUR, -24, GETDATE())";

                SqlParameter[] parametros = {
                    new SqlParameter("@IdEntidad", idEntidad)
                };

                DataTable dt = _db.EjecutarConsulta(query, parametros);

                if (dt.Rows.Count > 0)
                {
                    int total = Convert.ToInt32(dt.Rows[0]["Total"]);

                    if (total > 0)
                    {
                        notificaciones.Add(new Notificacion
                        {
                            Titulo = "Donaciones Recientes",
                            Mensaje = $"✅ {total} nueva(s) donación(es) registradas en las últimas 24 horas",
                            Tipo = "Info",
                            Icono = "💉",
                            Color = System.Drawing.Color.FromArgb(40, 167, 69)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en donaciones recientes: {ex.Message}");
            }

            return notificaciones;
        }

        /// <summary>
        /// Obtiene el número total de notificaciones críticas
        /// </summary>
        public int ContarNotificacionesCriticas(int idEntidad)
        {
            var notificaciones = ObtenerNotificaciones(idEntidad);
            return notificaciones.Count(n => n.Tipo == "Crítico");
        }

        /// <summary>
        /// Genera resumen de notificaciones para mostrar en el dashboard
        /// </summary>
        public string GenerarResumenNotificaciones(int idEntidad)
        {
            var notificaciones = ObtenerNotificaciones(idEntidad);

            int criticas = notificaciones.Count(n => n.Tipo == "Crítico");
            int advertencias = notificaciones.Count(n => n.Tipo == "Advertencia");
            int informativas = notificaciones.Count(n => n.Tipo == "Info");

            return $"🔔 {criticas} Críticas | {advertencias} Advertencias | {informativas} Informativas";
        }
    }
}