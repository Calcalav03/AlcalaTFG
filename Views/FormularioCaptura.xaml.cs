using AlcalaTFG.ViewModels;

namespace AlcalaTFG.Views;

public partial class FormularioCaptura : ContentPage
{
	public FormularioCaptura()
	{
		InitializeComponent();
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Aquí llamas al método de tu ViewModel
        if (BindingContext is FormularioCapturaViewModel viewModel)
        {
            viewModel.CargarDatos();
        }
    }
}