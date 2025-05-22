using AlcalaTFG.models;
using AlcalaTFG.Models;
using AlcalaTFG.services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AlcalaTFG.ViewModels
{
    public partial class CapturaGlobalViewModel:ObservableObject
    {

        [ObservableProperty]
        private ObservableCollection<CapturaInfo> capturas;
        public CapturaGlobalViewModel()
        {
            RequestCapturas();
        }

        [RelayCommand]
        public async void RequestCapturas()
        {
            RequestModel requestModel = new RequestModel
            {
                Method = "GET",
                Route = "http://localhost:8089/jpa/capturas", // Ajusta la ruta si es necesario
                Data = string.Empty
            };

            ResponseModel response = await APIService.ExecuteRequestJPA(requestModel);

            if (response.Success.Equals(0))
            {
                try
                {
                    Capturas = JsonConvert.DeserializeObject<ObservableCollection<CapturaInfo>>(response.Data.ToString());
                }
                catch (Exception ex)
                {
                    // Opcional: puedes registrar el error o mostrar un mensaje
                }
            }
        }


    }
}
