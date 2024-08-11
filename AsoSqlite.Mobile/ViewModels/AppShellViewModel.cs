using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AsoSqlite.Mobile.ViewModels
{
    public partial class AppShellViewModel : ObservableObject
    {
        [RelayCommand]
        private async Task SignOut()
        {
            SecureStorage.Default.Remove(SettingsConst.Logi);
            SecureStorage.Default.Remove(SettingsConst.Userl);

            await Shell.Current.GoToAsync($"//{nameof(LoginView)}");
            //await Shell.Current.GoToAsync("..");
        }
    }
}
