using AsoSqlite.Mobile.ViewModels;

namespace AsoSqlite.Mobile.Views;

public partial class AfiliadoView : ContentPage
{
	public AfiliadoView(AfiliadoViewModel mv)
	{
		InitializeComponent();
        BindingContext = mv;
    }
}