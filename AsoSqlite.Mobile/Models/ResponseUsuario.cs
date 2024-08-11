using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsoSqlite.Mobile.Models
{
    public class ResponseUsuario
    {
        public int IdUsuario { get; set; }
        public string? NroCI { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Correo { get; set; }
        public string? Clave { get; set; }
        public string? Foto { get; set; }
        public int IdRol { get; set; }
        public bool Activo { get; set; }
        public string? Descripcionrol { get; set; }

        public string PictureFullPath => string.IsNullOrEmpty(Foto)
            ? "https://asociacion-001-site1.ktempurl.com/Imagenes/sinimagen.png"
            : $"https://asociacion-001-site1.ktempurl.com/{Foto}";

        public string FullName => $"{Nombres} {Apellidos}";
    }
}
