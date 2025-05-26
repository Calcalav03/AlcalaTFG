using AlcalaTFG.Services;
using AlcalaTFG.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlcalaTFG.ViewModels
{
    public partial class MenuPrincipalViewModel: ObservableObject
    {


        [RelayCommand]
        private async void CambiarVistaFC()
        {
            // Aquí navegas a otra vista (por ejemplo, a "NuevaVistaPage")
            await Shell.Current.GoToAsync("///FormularioCaptura");
        }
        [RelayCommand]
        private async void CambiarVistaFEC()
        {
            // Aquí navegas a otra vista (por ejemplo, a "NuevaVistaPage")
            await Shell.Current.GoToAsync("///FormularioCeboEquipamiento");
        }
        [RelayCommand]
        private async void CambiarVistaL()
        {
            // Borrar los datos del AuthService 
            AuthService.Instance.ClearCredentials();

            // Eliminar las credenciales almacenadas
            SecureStorage.Remove("auth_token");
            SecureStorage.Remove("user");

            // Navegar a la vista de Login
            await Shell.Current.GoToAsync("///Login");
        }

        [RelayCommand]
        private async void CambiarVistaGV()
        {
            // Aquí navegas a otra vista (por ejemplo, a "NuevaVistaPage")
            await Shell.Current.GoToAsync("///CapturaGlobalView");
        }

        [RelayCommand]
        private async void CambiarVistaCU()
        {
            // Aquí navegas a otra vista (por ejemplo, a "NuevaVistaPage")
            await Shell.Current.GoToAsync("///CapturaUsuView");
        }
    }
}
