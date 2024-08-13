using AsoSqlite.Mobile.ViewModels;

namespace AsoSqlite.Mobile.Views;

public partial class AfiliadoView : ContentPage
{
    private readonly AfiliadoViewModel _viewModel;
    public AfiliadoView(AfiliadoViewModel mv)
	{
		InitializeComponent();
        BindingContext = mv;
        _viewModel = mv;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.Obtener();
    }
}