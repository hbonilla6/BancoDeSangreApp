using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace BancoDeSangreApp.Business
{
    /// <summary>
    /// Configuración de menú basada en categorías con FontAwesome
    /// </summary>
    public static class MenuConfig
    {
        // ============================================
        // AQUÍ DEFINES TODO EL MENÚ CON FONTAWESOME
        // ============================================
        public static readonly Dictionary<string, CategoriaMenu> Categorias = new Dictionary<string, CategoriaMenu>
        {
            {
                "Principal", new CategoriaMenu
                {
                    Nombre = "Principal",
                    Orden = 1,
                    Formularios = new List<FormularioMenu>
                    {
                        new FormularioMenu { Nombre = "FrmDashboard", Icono = "fa-home", Texto = "Dashboard", Orden = 1 }
                    }
                }
            },
            {
                "Operaciones", new CategoriaMenu
                {
                    Nombre = "Operaciones",
                    Orden = 2,
                    Formularios = new List<FormularioMenu>
                    {
                        new FormularioMenu { Nombre = "FrmDonantes", Icono = "fa-users", Texto = "Donantes", Orden = 1 },
                        new FormularioMenu { Nombre = "FrmDonaciones", Icono = "fa-hand-holding-medical", Texto = "Donaciones", Orden = 2 },
                        new FormularioMenu { Nombre = "FrmCentrosRecoleccion", Icono = "fa-hospital", Texto = "Centros", Orden = 3 },
                        new FormularioMenu { Nombre = "FrmInventario", Icono = "fa-boxes", Texto = "Inventario", Orden = 4 },
                        new FormularioMenu { Nombre = "FrmSolicitudesMedicas", Icono = "fa-clipboard-list", Texto = "Solicitudes", Orden = 5 }
                    }
                }
            },
            {
                "Reportes", new CategoriaMenu
                {
                    Nombre = "Reportes",
                    Orden = 3,
                    Formularios = new List<FormularioMenu>
                    {
                        new FormularioMenu { Nombre = "FrmReportes", Icono = "fa-chart-bar", Texto = "Reportes", Orden = 1 }
                    }
                }
            },
            {
                "Administracion", new CategoriaMenu
                {
                    Nombre = "Administración",
                    Orden = 4,
                    RolRequerido = "Administrador",
                    Formularios = new List<FormularioMenu>
                    {
                        new FormularioMenu { Nombre = "FrmAuditoria", Icono = "fa-shield-alt", Texto = "Auditoría", Orden = 1 },
                        new FormularioMenu { Nombre = "FrmUsuarios", Icono = "fa-user-cog", Texto = "Usuarios", Orden = 2 },
                        new FormularioMenu { Nombre = "FrmRoles", Icono = "fa-user-tag", Texto = "Roles", Orden = 3 },
                        new FormularioMenu { Nombre = "FrmEntidadesSalud", Icono = "fa-hospital", Texto = "Entidades de Salud", Orden = 4 }
                    }
                }
            }
        };

        // ============================================
        // PERMISOS POR ROL
        // ============================================
        public static readonly Dictionary<string, HashSet<string>> AccesoPorRol = new Dictionary<string, HashSet<string>>
        {
            {
                "Administrador", new HashSet<string>
                {
                    "*" // Acceso a TODO
                }
            },
            {
                "Operador", new HashSet<string>
                {
                    "FrmDashboard",
                    "FrmDonantes",
                    "FrmDonaciones",
                    "FrmCentrosRecoleccion",
                    "FrmInventario",
                    "FrmSolicitudesMedicas",
                    "FrmReportes"
                }
            },
            {
                "TecnicoLaboratorio", new HashSet<string>
                {
                    "FrmDashboard",
                    "FrmDonaciones",
                    "FrmInventario",
                    "FrmSolicitudesMedicas",
                    "FrmCentrosRecoleccion"
                }
            },
            {
                "Medico", new HashSet<string>
                {
                    "FrmDashboard",
                    "FrmInventario",
                    "FrmSolicitudesMedicas",
                    "FrmReportes"
                }
            }
        };

        /// <summary>
        /// Verifica si el usuario tiene acceso a un formulario
        /// </summary>
        public static bool TieneAcceso(Models.Usuario usuario, string nombreFormulario)
        {
            if (usuario == null || usuario.Roles == null || usuario.Roles.Count == 0)
                return false;

            foreach (var rol in usuario.Roles)
            {
                if (AccesoPorRol.ContainsKey(rol.Nombre))
                {
                    var permisos = AccesoPorRol[rol.Nombre];

                    if (permisos.Contains("*") || permisos.Contains(nombreFormulario))
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Obtiene las categorías disponibles para el usuario
        /// </summary>
        public static List<CategoriaMenu> ObtenerCategoriasDisponibles(Models.Usuario usuario)
        {
            var categoriasDisponibles = new List<CategoriaMenu>();

            foreach (var categoria in Categorias.Values.OrderBy(c => c.Orden))
            {
                if (!string.IsNullOrEmpty(categoria.RolRequerido))
                {
                    if (!usuario.TieneRol(categoria.RolRequerido))
                        continue;
                }

                var formulariosAccesibles = categoria.Formularios
                    .Where(f => TieneAcceso(usuario, f.Nombre))
                    .OrderBy(f => f.Orden)
                    .ToList();

                if (formulariosAccesibles.Count > 0)
                {
                    categoriasDisponibles.Add(new CategoriaMenu
                    {
                        Nombre = categoria.Nombre,
                        Orden = categoria.Orden,
                        RolRequerido = categoria.RolRequerido,
                        Formularios = formulariosAccesibles
                    });
                }
            }

            return categoriasDisponibles;
        }
    }

    public class CategoriaMenu
    {
        public string Nombre { get; set; }
        public int Orden { get; set; }
        public string RolRequerido { get; set; }
        public List<FormularioMenu> Formularios { get; set; } = new List<FormularioMenu>();
    }

    public class FormularioMenu
    {
        public string Nombre { get; set; }
        public string Icono { get; set; } // Ahora contiene clases de FontAwesome
        public string Texto { get; set; }
        public int Orden { get; set; }
    }
}