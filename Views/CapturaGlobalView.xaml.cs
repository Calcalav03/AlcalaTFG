using AlcalaTFG.ViewModels;

namespace AlcalaTFG.Views;

public partial class CapturaGlobalView : ContentPage
{
	public CapturaGlobalView()
	{
		InitializeComponent();
        BindingContext = new CapturaGlobalViewModel();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Aquí llamas al método de tu ViewModel
        if (BindingContext is CapturaGlobalViewModel viewModel)
        {
            viewModel.RequestCapturas();
        }
    }
}