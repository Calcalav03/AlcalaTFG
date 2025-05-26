using AlcalaTFG.ViewModels;

namespace AlcalaTFG.Views;

public partial class CapturaUsuView : ContentPage
{
    private readonly CapturaUsuViewModel viewModel;

    public CapturaUsuView()
    {
        InitializeComponent();
        viewModel = new CapturaUsuViewModel();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await viewModel.InitializeAsync();
    }
}
