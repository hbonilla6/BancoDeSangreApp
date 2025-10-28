using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

namespace BancoDeSangreApp.Data
{
    /// <summary>
    /// Clase que gestiona la conexión a la base de datos SQL Server
    /// Implementa el patrón Singleton para una única instancia de conexión
    /// </summary>
    public class ConexionDB
    {
        private static ConexionDB _instancia;
        private readonly string _connectionString;
        private readonly int _commandTimeout;
        private readonly int _maxRetries;
        private readonly int _retryDelaySeconds;

        /// <summary>
        /// Constructor privado para implementar Singleton
        /// </summary>
        private ConexionDB()
        {
            // Obtiene la cadena de conexión desde App.config
            _connectionString = ConfigurationManager.ConnectionStrings["BancoDeSangreDB"]?.ConnectionString;

            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException("No se encontró la cadena de conexión 'BancoDeSangreDB' en App.config");
            }

            // Configuración de timeouts y reintentos
            _commandTimeout = int.TryParse(ConfigurationManager.AppSettings["CommandTimeout"], out int timeout) ? timeout : 60;
            _maxRetries = int.TryParse(ConfigurationManager.AppSettings["MaxRetries"], out int retries) ? retries : 3;
            _retryDelaySeconds = int.TryParse(ConfigurationManager.AppSettings["RetryDelaySeconds"], out int delay) ? delay : 2;
        }

        /// <summary>
        /// Obtiene la única instancia de ConexionDB
        /// </summary>
        public static ConexionDB Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new ConexionDB();
                }
                return _instancia;
            }
        }

        /// <summary>
        /// Obtiene una nueva conexión a la base de datos
        /// </summary>
        public SqlConnection ObtenerConexion()
        {
            return new SqlConnection(_connectionString);
        }

        /// <summary>
        /// Prueba la conexión a la base de datos con reintentos
        /// </summary>
        /// <returns>True si la conexión es exitosa</returns>
        public bool ProbarConexion()
        {
            int intentos = 0;

            while (intentos < _maxRetries)
            {
                try
                {
                    using (SqlConnection conn = ObtenerConexion())
                    {
                        conn.Open();
                        if (conn.State == ConnectionState.Open)
                        {
                            return true;
                        }
                    }
                }
                catch (SqlException ex)
                {
                    intentos++;
                    System.Diagnostics.Debug.WriteLine($"Intento {intentos} de {_maxRetries} - Error: {ex.Message}");

                    if (intentos >= _maxRetries)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error al probar conexión después de {_maxRetries} intentos: {ex.Message}");
                        return false;
                    }

                    // Esperar antes de reintentar
                    Thread.Sleep(_retryDelaySeconds * 1000);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error al probar conexión: {ex.Message}");
                    return false;
                }
            }

            return false;
        }

        /// <summary>
        /// Verifica si el error es recuperable y vale la pena reintentar
        /// </summary>
        private bool EsErrorRecuperable(SqlException ex)
        {
            // Códigos de error de SQL Server que son recuperables
            int[] codigosRecuperables = {
                -2,    // Timeout
                -1,    // Error de conexión
                2,     // Error de red
                53,    // Error de conexión de red
                64,    // Error de comunicación
                233,   // Conexión no inicializada
                10053, // Error de conexión de red
                10054, // Conexión forzada a cerrarse
                10060, // Timeout de conexión
                40197, // Error de servicio
                40501, // Servicio ocupado
                40613  // Base de datos no disponible
            };

            foreach (SqlError error in ex.Errors)
            {
                if (Array.IndexOf(codigosRecuperables, error.Number) >= 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Ejecuta un comando SQL que no devuelve resultados (INSERT, UPDATE, DELETE)
        /// </summary>
        public int EjecutarComando(string query, params SqlParameter[] parametros)
        {
            int intentos = 0;

            while (intentos < _maxRetries)
            {
                try
                {
                    using (SqlConnection conn = ObtenerConexion())
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.CommandTimeout = _commandTimeout;

                            if (parametros != null)
                            {
                                cmd.Parameters.AddRange(parametros);
                            }

                            return cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (SqlException ex)
                {
                    intentos++;

                    if (!EsErrorRecuperable(ex) || intentos >= _maxRetries)
                    {
                        throw new Exception($"Error al ejecutar comando SQL: {ex.Message}", ex);
                    }

                    Thread.Sleep(_retryDelaySeconds * 1000);
                }
            }

            throw new Exception("No se pudo ejecutar el comando después de varios intentos");
        }

        /// <summary>
        /// Ejecuta una consulta y devuelve un DataTable con los resultados
        /// </summary>
        public DataTable EjecutarConsulta(string query, params SqlParameter[] parametros)
        {
            int intentos = 0;

            while (intentos < _maxRetries)
            {
                DataTable dt = new DataTable();

                try
                {
                    using (SqlConnection conn = ObtenerConexion())
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.CommandTimeout = _commandTimeout;

                            if (parametros != null)
                            {
                                cmd.Parameters.AddRange(parametros);
                            }

                            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                            {
                                adapter.Fill(dt);
                            }
                        }
                    }

                    return dt;
                }
                catch (SqlException ex)
                {
                    intentos++;

                    if (!EsErrorRecuperable(ex) || intentos >= _maxRetries)
                    {
                        throw new Exception($"Error al ejecutar consulta SQL: {ex.Message}", ex);
                    }

                    Thread.Sleep(_retryDelaySeconds * 1000);
                }
            }

            throw new Exception("No se pudo ejecutar la consulta después de varios intentos");
        }

        /// <summary>
        /// Ejecuta una consulta que devuelve un único valor (COUNT, MAX, etc.)
        /// </summary>
        public object EjecutarEscalar(string query, params SqlParameter[] parametros)
        {
            int intentos = 0;

            while (intentos < _maxRetries)
            {
                try
                {
                    using (SqlConnection conn = ObtenerConexion())
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.CommandTimeout = _commandTimeout;

                            if (parametros != null)
                            {
                                cmd.Parameters.AddRange(parametros);
                            }

                            return cmd.ExecuteScalar();
                        }
                    }
                }
                catch (SqlException ex)
                {
                    intentos++;

                    if (!EsErrorRecuperable(ex) || intentos >= _maxRetries)
                    {
                        throw new Exception($"Error al ejecutar consulta escalar: {ex.Message}", ex);
                    }

                    Thread.Sleep(_retryDelaySeconds * 1000);
                }
            }

            throw new Exception("No se pudo ejecutar la consulta después de varios intentos");
        }

        /// <summary>
        /// Obtiene la cadena de conexión actual
        /// </summary>
        public string ObtenerCadenaConexion()
        {
            return _connectionString;
        }

        /// <summary>
        /// Limpia el pool de conexiones (útil para problemas de conexión)
        /// </summary>
        public void LimpiarPoolConexiones()
        {
            SqlConnection.ClearAllPools();
        }
    }
}