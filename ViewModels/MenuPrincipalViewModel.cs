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
        private async Task CambiarVistaFC()
        {
            await Shell.Current.GoToAsync("///FormularioCaptura");
        }

        [RelayCommand]
        private async Task CambiarVistaFEC()
        {
            await Shell.Current.GoToAsync("///FormularioCeboEquipamiento");
        }

        [RelayCommand]
        private async Task CambiarVistaGV()
        {
            await Shell.Current.GoToAsync("///CapturaGlobalView");
        }

        [RelayCommand]
        private async Task CambiarVistaCU()
        {
            await Shell.Current.GoToAsync("///CapturaUsuView");
        }

        [RelayCommand]
        private async Task CambiarVistaL()
        {
            bool cerrarSesion = await Shell.Current.DisplayAlert(
                "Cerrar sesión",
                "¿Estás seguro de que quieres cerrar sesión?",
                "Sí",
                "No"
            );

            if (cerrarSesion)
            {
                AuthService.Instance.ClearCredentials();
                SecureStorage.Remove("auth_token");
                SecureStorage.Remove("user");
                await Shell.Current.GoToAsync("///Login");
            }
        }

        [RelayCommand]
        private async Task Apagar()
        {
            bool salir = await Shell.Current.DisplayAlert(
                "Salir de la aplicación",
                "¿Estás seguro de que quieres salir?",
                "Sí",
                "No"
            );

            if (salir)
            {
                Application.Current.Quit();
            }
        }


    }
}
