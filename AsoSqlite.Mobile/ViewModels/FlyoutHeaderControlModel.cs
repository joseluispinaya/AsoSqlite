using CommunityToolkit.Mvvm.ComponentModel;
using AsoSqlite.Mobile.Models;
using Newtonsoft.Json;

namespace AsoSqlite.Mobile.ViewModels
{
    public partial class FlyoutHeaderControlModel : ObservableObject
    {
        [ObservableProperty]
        private string? email;

        [ObservableProperty]
        private string? nombre;

        [ObservableProperty]
        private string? foto;

        public FlyoutHeaderControlModel()
        {
            LoadPersonalAsync();
        }
        private async void LoadPersonalAsync()
        {
            await InicioAsync();
        }
        private async Task InicioAsync()
        {
            var use = await SecureStorage.Default.GetAsync(SettingsConst.Userl);
            if (!string.IsNullOrEmpty(use))
            {
                ResponseUsuario? responsePCD = JsonConvert.DeserializeObject<ResponseUsuario>(use);
                Nombre = responsePCD?.Apellidos;
                Foto = responsePCD?.PictureFullPath;
                Email = responsePCD?.Correo;
            }
        }
    }
}
