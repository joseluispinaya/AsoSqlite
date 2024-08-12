using System.ComponentModel.DataAnnotations;

namespace AsoSqlite.Mobile.Modelos
{
    public class EAsociacion
    {
        [Key]
        public int Idasoci { get; set; }
        public string? Nombre { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
        public bool Activo { get; set; }
        public ICollection<EAfiliado>? Afiliados { get; set; }
    }
}
