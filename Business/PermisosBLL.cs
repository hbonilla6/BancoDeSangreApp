using BancoDeSangreApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BancoDeSangreApp.Business
{
    /// <summary>
    /// Lógica de negocio para gestión de permisos y roles
    /// </summary>
    public class PermisosBLL
    {
        // Diccionario de permisos por rol
        private static readonly Dictionary<string, HashSet<string>> PermisosPorRol = new Dictionary<string, HashSet<string>>
        {
            {
                "Administrador", new HashSet<string>
                {
                    "Dashboard.Ver",
                    "Donantes.Ver", "Donantes.Crear", "Donantes.Editar", "Donantes.Eliminar",
                    "Donaciones.Ver", "Donaciones.Crear", "Donaciones.Editar", "Donaciones.Eliminar",
                    "Inventario.Ver", "Inventario.Actualizar", "Inventario.Reportes",
                    "Solicitudes.Ver", "Solicitudes.Crear", "Solicitudes.Aprobar", "Solicitudes.Rechazar", "Solicitudes.Atender",
                    "Reportes.Ver", "Reportes.Exportar",
                    "Auditoria.Ver", "Auditoria.Exportar",
                    "Centros.Ver", "Centros.Crear", "Centros.Editar", "Centros.Eliminar",
                    "Usuarios.Ver", "Usuarios.Crear", "Usuarios.Editar", "Usuarios.Eliminar"
                }
            },
            {
                "Operador", new HashSet<string>
                {
                    "Dashboard.Ver",
                    "Donantes.Ver", "Donantes.Crear", "Donantes.Editar",
                    "Donaciones.Ver", "Donaciones.Crear", "Donaciones.Editar",
                    "Inventario.Ver", "Inventario.Actualizar",
                    "Solicitudes.Ver", "Solicitudes.Crear",
                    "Reportes.Ver",
                    "Centros.Ver"
                }
            },
            {
                "TecnicoLaboratorio", new HashSet<string>
                {
                    "Dashboard.Ver",
                    "Donaciones.Ver", "Donaciones.Crear",
                    "Inventario.Ver", "Inventario.Actualizar",
                    "Solicitudes.Ver",
                    "Centros.Ver"
                }
            },
            {
                "Medico", new HashSet<string>
                {
                    "Dashboard.Ver",
                    "Inventario.Ver",
                    "Solicitudes.Ver", "Solicitudes.Crear",
                    "Reportes.Ver"
                }
            }
        };

        /// <summary>
        /// Verifica si un usuario tiene un permiso específico
        /// </summary>
        public static bool TienePermiso(Usuario usuario, string permiso)
        {
            if (usuario == null || usuario.Roles == null || usuario.Roles.Count == 0)
                return false;

            // El administrador tiene todos los permisos
            if (usuario.TieneRol("Administrador"))
                return true;

            // Verificar permiso en los roles del usuario
            foreach (var rol in usuario.Roles)
            {
                if (PermisosPorRol.ContainsKey(rol.Nombre))
                {
                    if (PermisosPorRol[rol.Nombre].Contains(permiso))
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Obtiene todos los permisos de un usuario
        /// </summary>
        public static HashSet<string> ObtenerPermisos(Usuario usuario)
        {
            var permisos = new HashSet<string>();

            if (usuario == null || usuario.Roles == null)
                return permisos;

            foreach (var rol in usuario.Roles)
            {
                if (PermisosPorRol.ContainsKey(rol.Nombre))
                {
                    permisos.UnionWith(PermisosPorRol[rol.Nombre]);
                }
            }

            return permisos;
        }

        /// <summary>
        /// Verifica si un usuario puede acceder a un formulario
        /// </summary>
        public static bool PuedeAccederAFormulario(Usuario usuario, string nombreFormulario)
        {
            var mapaPermisos = new Dictionary<string, string>
            {
                { "FrmDashboard", "Dashboard.Ver" },
                { "FrmDonantes", "Donantes.Ver" },
                { "FrmDonaciones", "Donaciones.Ver" },
                { "FrmInventario", "Inventario.Ver" },
                { "FrmSolicitudesMedicas", "Solicitudes.Ver" },
                { "FrmReportes", "Reportes.Ver" },
                { "FrmAuditoria", "Auditoria.Ver" },
                { "FrmCentrosRecoleccion", "Centros.Ver" }
            };

            if (mapaPermisos.ContainsKey(nombreFormulario))
            {
                return TienePermiso(usuario, mapaPermisos[nombreFormulario]);
            }

            return false;
        }

        /// <summary>
        /// Obtiene descripción legible de un permiso
        /// </summary>
        public static string ObtenerDescripcionPermiso(string permiso)
        {
            var descripciones = new Dictionary<string, string>
            {
                { "Dashboard.Ver", "Ver Dashboard" },
                { "Donantes.Ver", "Ver Donantes" },
                { "Donantes.Crear", "Crear Donantes" },
                { "Donantes.Editar", "Editar Donantes" },
                { "Donantes.Eliminar", "Eliminar Donantes" },
                { "Donaciones.Ver", "Ver Donaciones" },
                { "Donaciones.Crear", "Registrar Donaciones" },
                { "Donaciones.Editar", "Modificar Donaciones" },
                { "Donaciones.Eliminar", "Eliminar Donaciones" },
                { "Inventario.Ver", "Ver Inventario" },
                { "Inventario.Actualizar", "Actualizar Inventario" },
                { "Inventario.Reportes", "Generar Reportes de Inventario" },
                { "Solicitudes.Ver", "Ver Solicitudes Médicas" },
                { "Solicitudes.Crear", "Crear Solicitudes" },
                { "Solicitudes.Aprobar", "Aprobar Solicitudes" },
                { "Solicitudes.Rechazar", "Rechazar Solicitudes" },
                { "Solicitudes.Atender", "Atender Solicitudes" },
                { "Reportes.Ver", "Ver Reportes" },
                { "Reportes.Exportar", "Exportar Reportes" },
                { "Auditoria.Ver", "Ver Auditoría" },
                { "Auditoria.Exportar", "Exportar Auditoría" },
                { "Centros.Ver", "Ver Centros de Recolección" },
                { "Centros.Crear", "Crear Centros" },
                { "Centros.Editar", "Editar Centros" },
                { "Centros.Eliminar", "Eliminar Centros" }
            };

            return descripciones.ContainsKey(permiso) ? descripciones[permiso] : permiso;
        }
    }
}