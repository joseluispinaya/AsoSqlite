using AsoSqlite.Mobile.ViewModels;
using AsoSqlite.Mobile.Views;

namespace AsoSqlite.Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            BindingContext = new AppShellViewModel();
            Routing.RegisterRoute(nameof(InicioView), typeof(InicioView));
        }
    }
}
