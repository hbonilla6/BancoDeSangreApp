using System;

namespace BancoDeSangreApp.Models
{
    public class EntidadSalud
    {
        public int IdEntidad { get; set; }

        public string Nombre { get; set; }

        public string Codigo { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public string Correo { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaCreacion { get; set; }
    }

}
