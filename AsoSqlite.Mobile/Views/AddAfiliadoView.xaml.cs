using AsoSqlite.Mobile.ViewModels;

namespace AsoSqlite.Mobile.Views;

public partial class AddAfiliadoView : ContentPage
{
	public AddAfiliadoView(AddAfiliadoViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}