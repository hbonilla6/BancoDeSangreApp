using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoDeSangreApp.Models
{
    /// <summary>
    /// Modelo que representa una solicitud médica de sangre
    /// </summary>
    public class SolicitudMedica
    {
        public int IdSolicitud { get; set; }
        public int IdEntidad { get; set; }
        public string Solicitante { get; set; }
        public int IdTipoSangre { get; set; }
        public int CantidadSolicitada { get; set; }
        public string Prioridad { get; set; } // Emergencia, Programada, Normal
        public string Estado { get; set; } // Pendiente, Aprobada, Rechazada, Atendida
        public string Observaciones { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public DateTime? FechaAtencion { get; set; }
        public int? CreadoPor { get; set; }

        // Propiedades de navegación
        public string NombreEntidad { get; set; }
        public string TipoSangreCodigo { get; set; }
        public string NombreUsuarioCreador { get; set; }

        public SolicitudMedica()
        {
            FechaSolicitud = DateTime.Now;
            Estado = "Pendiente";
            Prioridad = "Normal";
        }

        /// <summary>
        /// Verifica si la solicitud puede ser atendida
        /// </summary>
        public bool PuedeSerAtendida()
        {
            return Estado == "Aprobada";
        }

        /// <summary>
        /// Verifica si es una solicitud de emergencia
        /// </summary>
        public bool EsEmergencia()
        {
            return Prioridad.Equals("Emergencia", StringComparison.OrdinalIgnoreCase);
        }
    }

    /// <summary>
    /// Modelo para la asignación de donaciones a solicitudes
    /// </summary>
    public class SolicitudDonacion
    {
        public int IdSolicitud { get; set; }
        public int IdDonacion { get; set; }
        public DateTime FechaAsignacion { get; set; }

        // Propiedades adicionales para vista
        public string CodigoDonacion { get; set; }
        public int CantidadML { get; set; }
        public DateTime FechaCaducidad { get; set; }

        public SolicitudDonacion()
        {
            FechaAsignacion = DateTime.Now;
        }
    }

    /// <summary>
    /// Modelo para el inventario de sangre
    /// </summary>
    public class InventarioSangre
    {
        public int IdEntidad { get; set; }
        public int IdTipoSangre { get; set; }
        public int CantidadUnidades { get; set; }
        public DateTime UltimaActualizacion { get; set; }

        // Propiedades de navegación
        public string NombreEntidad { get; set; }
        public string TipoSangreCodigo { get; set; }
        public string TipoSangreDescripcion { get; set; }

        public InventarioSangre()
        {
            CantidadUnidades = 0;
            UltimaActualizacion = DateTime.Now;
        }

        /// <summary>
        /// Verifica si el stock está bajo (menos de 5 unidades)
        /// </summary>
        public bool StockBajo()
        {
            return CantidadUnidades < 5;
        }

        /// <summary>
        /// Verifica si hay stock crítico (menos de 2 unidades)
        /// </summary>
        public bool StockCritico()
        {
            return CantidadUnidades < 2;
        }
    }

    /// <summary>
    /// Modelo para la auditoría del sistema
    /// </summary>
    public class BitacoraAuditoria
    {
        public long IdAuditoria { get; set; }
        public int? IdUsuario { get; set; }
        public string Entidad { get; set; }
        public string Operacion { get; set; }
        public string ClavePrimaria { get; set; }
        public string ValorAnterior { get; set; }
        public string ValorNuevo { get; set; }
        public DateTime FechaAccion { get; set; }

        // Propiedades de navegación
        public string NombreUsuario { get; set; }

        public BitacoraAuditoria()
        {
            FechaAccion = DateTime.Now;
        }
    }

    /// <summary>
    /// Modelo para reportes de inventario
    /// </summary>
    public class ReporteInventario
    {
        public string TipoSangre { get; set; }
        public int UnidadesDisponibles { get; set; }
        public int UnidadesReservadas { get; set; }
        public int UnidadesProximasVencer { get; set; }
        public DateTime UltimaActualizacion { get; set; }
        public string EstadoStock { get; set; } // Normal, Bajo, Crítico
    }

    /// <summary>
    /// Modelo para reportes de solicitudes
    /// </summary>
    public class ReporteSolicitudes
    {
        public DateTime Fecha { get; set; }
        public int TotalPendientes { get; set; }
        public int TotalAprobadas { get; set; }
        public int TotalRechazadas { get; set; }
        public int TotalAtendidas { get; set; }
        public int TotalEmergencias { get; set; }
    }

    /// <summary>
    /// Modelo para donaciones próximas a caducar
    /// </summary>
    public class DonacionCaducidad
    {
        public int IdDonacion { get; set; }
        public string TipoSangre { get; set; }
        public int CantidadML { get; set; }
        public DateTime FechaRecoleccion { get; set; }
        public DateTime FechaCaducidad { get; set; }
        public int DiasRestantes { get; set; }
        public string NombreDonante { get; set; }

        public bool EsCritico()
        {
            return DiasRestantes <= 7;
        }
    }
}