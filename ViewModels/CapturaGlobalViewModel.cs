using AlcalaTFG.models;
using AlcalaTFG.Models;
using AlcalaTFG.services;
using AlcalaTFG.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        [ObservableProperty]
        private bool isLoading;

        public bool TieneCapturas => Capturas != null && Capturas.Count > 0;

        partial void OnCapturasChanged(ObservableCollection<CapturaInfo> value)
        {
            OnPropertyChanged(nameof(TieneCapturas));
        }

        [RelayCommand]
        private async Task MostrarCapturaDetalle(CapturaInfo captura)
        {
            if (captura == null)
                return;



            //var popup = new CapturaDetalleMopup(captura);
            //popup.BindingContext = this;

            await MopupService.Instance.PushAsync(new CapturaDetalleMopup(captura));
        }

       


        public CapturaGlobalViewModel()
        {
            RequestCapturas();
        }

        [RelayCommand]
        public async Task RequestCapturas()
        {
            IsLoading = true;

            var requestModel = new RequestModel
            {
                Method = "GET",
                Route = "http://localhost:8089/jpa/capturas",
                Data = string.Empty
            };

            var response = await APIService.ExecuteRequestJPA(requestModel);

            try
            {
                if (response.Success == 0 && response.Data != null)
                {
                    var capturas = JsonConvert.DeserializeObject<ObservableCollection<CapturaInfo>>(response.Data.ToString());
                    Capturas = capturas ?? new ObservableCollection<CapturaInfo>();
                }
                else
                {
                    Capturas = new ObservableCollection<CapturaInfo>();
                }
            }
            catch (Exception ex)
            {
                Capturas = new ObservableCollection<CapturaInfo>();
                Debug.WriteLine($"Error al deserializar capturas: {ex.Message}");
            }

            IsLoading = false;
        }

        [RelayCommand]
        public async Task AMenu()
        {
            await Shell.Current.GoToAsync("//MenuPrincipal");
        }




    }
}
