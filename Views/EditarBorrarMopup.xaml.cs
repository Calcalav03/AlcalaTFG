using AlcalaTFG.Models;
using Mopups.Pages;
using Mopups.Services;

namespace AlcalaTFG.Views;

public partial class EditarBorrarMopup : PopupPage
{
	public EditarBorrarMopup()
	{
        InitializeComponent();
        
    }

    private async void OnCerrarClicked(object sender, EventArgs e)
    {
        await MopupService.Instance.PopAsync();
    }
}