using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsoSqlite.Mobile.Models
{
    public class AfiliadoResponse
    {
        public int IdAfiliado { get; set; }
        public int Idasoci { get; set; }
        public string? NroCI { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Direccion { get; set; }
        public string? Celular { get; set; }
        public bool Estado { get; set; }
        public string? AsociacionNom { get; set; }
    }
}
