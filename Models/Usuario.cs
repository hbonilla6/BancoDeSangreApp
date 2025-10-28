using System;
using System.Collections.Generic;

namespace BancoDeSangreApp.Models
{
    /// <summary>
    /// Modelo que representa un usuario del sistema
    /// </summary>
    public class Usuario
    {
        // Propiedades principales
        public int IdUsuario { get; set; }
        public int IdEntidad { get; set; }
        public string NombreUsuario { get; set; }
        public string ClaveHash { get; set; }
        public string NombreCompleto { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public bool Activo { get; set; }
        public DateTime? UltimoAcceso { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        // Propiedades de navegación
        public string NombreEntidad { get; set; }
        public List<Rol> Roles { get; set; }

        // Constructor
        public Usuario()
        {
            Roles = new List<Rol>();
            Activo = true;
            FechaCreacion = DateTime.Now;
        }

        /// <summary>
        /// Obtiene los nombres de los roles concatenados
        /// </summary>
        public string RolesTexto
        {
            get
            {
                if (Roles == null || Roles.Count == 0)
                    return "Sin roles asignados";

                return string.Join(", ", Roles.ConvertAll(r => r.Nombre));
            }
        }

        /// <summary>
        /// Verifica si el usuario tiene un rol específico
        /// </summary>
        public bool TieneRol(string nombreRol)
        {
            return Roles.Exists(r => r.Nombre.Equals(nombreRol, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Verifica si el usuario es administrador
        /// </summary>
        public bool EsAdministrador()
        {
            return TieneRol("Administrador");
        }
    }

    /// <summary>
    /// Modelo que representa un rol del sistema
    /// </summary>
    public class Rol
    {
        public int IdRol { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int NivelAcceso { get; set; }
        public DateTime FechaCreacion { get; set; }

        public Rol()
        {
            FechaCreacion = DateTime.Now;
            NivelAcceso = 1;
        }

        public override string ToString()
        {
            return Nombre;
        }
    }

    /// <summary>
    /// Modelo para el proceso de login
    /// </summary>
    public class LoginRequest
    {
        public string Usuario { get; set; }
        public string Clave { get; set; }
    }

    /// <summary>
    /// Resultado del proceso de autenticación
    /// </summary>
    public class LoginResult
    {
        public bool Exitoso { get; set; }
        public string Mensaje { get; set; }
        public Usuario Usuario { get; set; }
        public int IntentosRestantes { get; set; }

        public LoginResult()
        {
            Exitoso = false;
            IntentosRestantes = 3;
        }
    }
}
