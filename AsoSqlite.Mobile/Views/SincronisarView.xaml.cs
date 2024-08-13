using AsoSqlite.Mobile.ViewModels;

namespace AsoSqlite.Mobile.Views;

public partial class SincronisarView : ContentPage
{
    private readonly SincronisarViewModel _viewModel;
    public SincronisarView(SincronisarViewModel mv)
	{
		InitializeComponent();
        BindingContext = mv;
        _viewModel = mv;
    }

    protected async override void OnAppearing()
    {
        //base.OnAppearing();
        await _viewModel.ObtenerAfiliadosAsync();
    }
}