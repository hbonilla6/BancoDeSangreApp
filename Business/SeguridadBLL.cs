using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace BancoDeSangreApp.Business
{
    /// <summary>
    /// Clase que maneja la seguridad y encriptación del sistema
    /// </summary>
    public class SeguridadBLL
    {
        private static readonly string _saltKey = ConfigurationManager.AppSettings["SaltKey"] ?? "DefaultSalt2025";

        /// <summary>
        /// Genera un hash SHA256 de una contraseña
        /// </summary>
        /// <param name="clave">Contraseña en texto plano</param>
        /// <returns>Hash de la contraseña</returns>
        public static string GenerarHash(string clave)
        {
            if (string.IsNullOrEmpty(clave))
                throw new ArgumentException("La contraseña no puede estar vacía");

            // Concatena la clave con el salt para mayor seguridad
            string claveConSalt = clave + _saltKey;

            using (SHA256 sha256 = SHA256.Create())
            {
                // Convierte la cadena a bytes
                byte[] bytes = Encoding.UTF8.GetBytes(claveConSalt);

                // Calcula el hash
                byte[] hash = sha256.ComputeHash(bytes);

                // Convierte el hash a string hexadecimal
                StringBuilder builder = new StringBuilder();
                foreach (byte b in hash)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }
        }

        /// <summary>
        /// Verifica si una contraseña coincide con su hash
        /// </summary>
        /// <param name="clave">Contraseña en texto plano</param>
        /// <param name="hashAlmacenado">Hash almacenado en la base de datos</param>
        /// <returns>True si la contraseña es correcta</returns>
        public static bool VerificarHash(string clave, string hashAlmacenado)
        {
            if (string.IsNullOrEmpty(clave) || string.IsNullOrEmpty(hashAlmacenado))
                return false;

            string hashCalculado = GenerarHash(clave);
            return hashCalculado.Equals(hashAlmacenado, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Valida la fortaleza de una contraseña
        /// </summary>
        /// <param name="clave">Contraseña a validar</param>
        /// <returns>Mensaje de error o cadena vacía si es válida</returns>
        public static string ValidarFortalezaClave(string clave)
        {
            if (string.IsNullOrWhiteSpace(clave))
                return "La contraseña no puede estar vacía.";

            if (clave.Length < 6)
                return "La contraseña debe tener al menos 6 caracteres.";

            if (clave.Length > 50)
                return "La contraseña no puede exceder 50 caracteres.";

            // Opcional: Validar complejidad
            bool tieneMayuscula = false;
            bool tieneMinuscula = false;
            bool tieneNumero = false;

            foreach (char c in clave)
            {
                if (char.IsUpper(c)) tieneMayuscula = true;
                if (char.IsLower(c)) tieneMinuscula = true;
                if (char.IsDigit(c)) tieneNumero = true;
            }

            if (!tieneMayuscula)
                return "La contraseña debe contener al menos una letra mayúscula.";

            if (!tieneMinuscula)
                return "La contraseña debe contener al menos una letra minúscula.";

            if (!tieneNumero)
                return "La contraseña debe contener al menos un número.";

            return string.Empty; // Contraseña válida
        }

        /// <summary>
        /// Valida el formato de un correo electrónico
        /// </summary>
        public static bool ValidarCorreo(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo))
                return false;

            try
            {
                var addr = new System.Net.Mail.MailAddress(correo);
                return addr.Address == correo;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Valida el formato de un nombre de usuario
        /// </summary>
        public static string ValidarNombreUsuario(string usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario))
                return "El nombre de usuario no puede estar vacío.";

            if (usuario.Length < 4)
                return "El nombre de usuario debe tener al menos 4 caracteres.";

            if (usuario.Length > 50)
                return "El nombre de usuario no puede exceder 50 caracteres.";

            // No permitir espacios
            if (usuario.Contains(" "))
                return "El nombre de usuario no puede contener espacios.";

            // Solo permitir letras, números y guiones
            foreach (char c in usuario)
            {
                if (!char.IsLetterOrDigit(c) && c != '_' && c != '-')
                    return "El nombre de usuario solo puede contener letras, números, guiones y guiones bajos.";
            }

            return string.Empty; // Usuario válido
        }

        /// <summary>
        /// Genera un código de recuperación aleatorio
        /// </summary>
        public static string GenerarCodigoRecuperacion()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        /// <summary>
        /// Sanitiza una cadena para prevenir SQL Injection
        /// </summary>
        public static string SanitizarTexto(string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return texto;

            // Elimina caracteres peligrosos
            return texto.Replace("'", "''")
                       .Replace("--", "")
                       .Replace(";", "")
                       .Replace("/*", "")
                       .Replace("*/", "")
                       .Replace("xp_", "")
                       .Replace("sp_", "");
        }
    }
}
