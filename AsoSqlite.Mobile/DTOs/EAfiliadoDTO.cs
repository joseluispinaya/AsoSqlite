using CommunityToolkit.Mvvm.ComponentModel;

namespace AsoSqlite.Mobile.DTOs
{
    public partial class EAfiliadoDTO : ObservableObject
    {
        [ObservableProperty]
        public int idAfiliado;
        [ObservableProperty]
        public string? nroCI;
        [ObservableProperty]
        public string? nombres;
        [ObservableProperty]
        public string? apellidos;
        [ObservableProperty]
        public string? direccion;
        [ObservableProperty]
        public string? celular;
        [ObservableProperty]
        public bool estado;
        [ObservableProperty]
        public int eAsociacionId;
        [ObservableProperty]
        private string? eAsociacionNombre;
    }
}
