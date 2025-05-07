using AlcalaTFG.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlcalaTFG.ViewModels
{
    public partial class FormularioCapturaViewModel:ObservableObject    
    {
        [ObservableProperty]
        private string imagenUrl;


        //METODO PARA SELECCIONAR LA IMAGEN
        [RelayCommand]
        public async void SeleciconarImagen()
        {
            var file = await FileSelector.SelectImageAsync();
            if (file != null)
            {
                ImagenUrl = file.FullPath;
            }
        }
    }
}
