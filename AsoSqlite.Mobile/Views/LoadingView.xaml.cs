using AsoSqlite.Mobile.ViewModels;

namespace AsoSqlite.Mobile.Views;

public partial class LoadingView : ContentPage
{
	public LoadingView(LoadingViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}