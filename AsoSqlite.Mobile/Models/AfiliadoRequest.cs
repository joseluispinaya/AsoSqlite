using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsoSqlite.Mobile.Models
{
    public class AfiliadoRequest
    {
        public int Idasoci { get; set; }
        public string? NroCI { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Direccion { get; set; }
        public string? Celular { get; set; }
    }
}
