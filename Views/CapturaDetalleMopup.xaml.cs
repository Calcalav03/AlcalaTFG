using AlcalaTFG.Models;
using Mopups.Pages;
using Mopups.Services;

namespace AlcalaTFG.Views;

public partial class CapturaDetalleMopup : PopupPage
{
    public CapturaDetalleMopup(CapturaInfo captura)
    {
        InitializeComponent();
        BindingContext = captura;
    }

    private async void OnCerrarClicked(object sender, EventArgs e)
    {
        await MopupService.Instance.PopAsync();
    }
}