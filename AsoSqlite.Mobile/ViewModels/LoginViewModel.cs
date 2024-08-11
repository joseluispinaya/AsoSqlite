
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AsoSqlite.Mobile.DTOs;
using AsoSqlite.Mobile.Models;
using Newtonsoft.Json;
using AsoSqlite.Mobile.Repositories;
using AsoSqlite.Mobile.Views;

namespace AsoSqlite.Mobile.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IRepository _repository;

        [ObservableProperty]
        private string? email;

        [ObservableProperty]
        private string? password;

        [ObservableProperty]
        private bool loadingEsVisible = false;

        public LoginViewModel(IRepository repository)
        {
            _repository = repository;
        }

        [RelayCommand]
        private async Task Login()
        {
            if (string.IsNullOrEmpty(Email))
            {
                await Shell.Current.DisplayAlert("Error", "Ingrese un Usuario", "Ok");
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await Shell.Current.DisplayAlert("Error", "Ingrese una Contraseña", "Ok");
                return;
            }

            LoadingEsVisible = true;

            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                LoadingEsVisible = false;
                await Shell.Current.DisplayAlert("Error", "Verifique la conexion a Internet", "Ok");
                return;
            }

            string url = "https://asociacion-001-site1.ktempurl.com/";

            LoginDTO loginDTO = new LoginDTO
            {
                Correo = Email,
                Clave = Password
            };
            var httpResponse = await _repository.GetUsuario<ResponseUsuario>(url, "api/usuarios/Login", loginDTO);

            if (httpResponse.Error)
            {
                LoadingEsVisible = false;
                var message = await httpResponse.GetErrorMessageAsync();
                await Shell.Current.DisplayAlert("Error", message, "Ok");
                return;
            }

            await SecureStorage.Default.SetAsync(SettingsConst.Logi, "si");
            ResponseUsuario responseUsu = httpResponse.Response!;
            string userDetail = JsonConvert.SerializeObject(responseUsu);
            await SecureStorage.Default.SetAsync(SettingsConst.Userl, userDetail);

            LoadingEsVisible = false;

            AppShell.Current.FlyoutHeader = new FlyoutHeaderControl();
            await Shell.Current.GoToAsync($"//{nameof(InicioView)}");
        }
    }
}
