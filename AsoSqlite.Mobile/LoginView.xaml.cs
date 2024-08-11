using AsoSqlite.Mobile.ViewModels;

namespace AsoSqlite.Mobile;

public partial class LoginView : ContentPage
{
    public LoginView(LoginViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;

    }
}