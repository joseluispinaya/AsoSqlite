using AsoSqlite.Mobile.ViewModels;

namespace AsoSqlite.Mobile.Views;

public partial class SincronisarView : ContentPage
{
	public SincronisarView(SincronisarViewModel mv)
	{
		InitializeComponent();
        BindingContext = mv;
    }
}