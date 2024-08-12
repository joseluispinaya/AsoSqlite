using CommunityToolkit.Mvvm.ComponentModel;

namespace AsoSqlite.Mobile.DTOs
{
    public partial class EAsociacionDTO : ObservableObject
    {
        [ObservableProperty]
        public int idasoci;
        [ObservableProperty]
        public string? nombre;
        [ObservableProperty]
        public string? direccion;
        [ObservableProperty]
        public string? telefono;
        [ObservableProperty]
        public string? correo;
        [ObservableProperty]
        public bool activo;
    }
}
