using AsoSqlite.Mobile.ViewModels;

namespace AsoSqlite.Mobile.Views;

public partial class AsociacionesView : ContentPage
{
	public AsociacionesView(AsociacionesViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}