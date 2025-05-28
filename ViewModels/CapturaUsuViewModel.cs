using AlcalaTFG.models;
using AlcalaTFG.Models;
using AlcalaTFG.services;
using AlcalaTFG.Services;
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
    public partial class CapturaUsuViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<CapturaInfo> capturasUsu;
        [ObservableProperty]
        private int id;
        [ObservableProperty]
        private bool isLoading;
        [ObservableProperty]
        private CapturaInfo capturaSelected;
        public bool TieneCapturas => CapturasUsu != null && CapturasUsu.Count > 0;

        partial void OnCapturasUsuChanged(ObservableCollection<CapturaInfo> value)
        {
            OnPropertyChanged(nameof(TieneCapturas));
        }

        [RelayCommand]
        private async Task MostrarCapturaDetalle(CapturaInfo captura)
        {
            if (captura == null)
                return;

            await MopupService.Instance.PushAsync(new CapturaDetalleMopup(captura));
        }

        [RelayCommand]
        private async Task MostrarEditarBorrar(CapturaInfo _captura)
        {
            if (_captura == null)
                return;
            CapturaSelected = _captura;
            var popup = new EditarBorrarMopup();
            popup.BindingContext = this;

            await MopupService.Instance.PushAsync(popup);
        }

        [RelayCommand]
        public async Task IrMopupEditar(CapturaInfo _captura)
        {
            if (_captura == null) return;

            

            var editarViewModel = new FormularioCapturaViewModel(_captura, this);

            var editarPopup = new EditarCapturaMopup()
            {
                BindingContext = editarViewModel
            };

            await MopupService.Instance.PopAllAsync();

            await MopupService.Instance.PushAsync(editarPopup);
        }

        [RelayCommand]
        public async void DeleteCaptura()
        {
           
            string ruta = "http://localhost:8089/jpa/capturas/Borrar/" + CapturaSelected.Id;

            RequestModel requestModel = new RequestModel
            {
                Method = "DELETE",
                Route = ruta,
                Data = string.Empty
            };


            ResponseModel response = await APIService.ExecuteRequestJPA(requestModel);
            await App.Current.MainPage.DisplayAlert("Mensaje", response.Message, "Aceptar");

            if (response.Success.Equals(0))
            {
                try
                {
                    InitializeAsync();

                }
                catch (Exception ex) { }

                await MopupService.Instance.PopAllAsync();


            }
        }

        public CapturaUsuViewModel()
        {

            RequestIdUsu();
            RequestCapturas();
        }
        public async Task InitializeAsync()
        {
            IsLoading = true;
            await RequestIdUsu();
            await RequestCapturas();
            IsLoading = false;
        }

        public async Task RequestIdUsu()
        {
            string userLogin = AuthService.Instance.UserLogin;

            if (string.IsNullOrEmpty(userLogin))
            {
                Debug.WriteLine("UserLogin no está establecido.");
                return;
            }

            var requestModel = new RequestModel
            {
                Method = "GET",
                Route = $"http://localhost:8089/jpa/usuarioId/{userLogin}",
                Data = string.Empty
            };

            var response = await APIService.ExecuteRequestJPA(requestModel);

            if (response.Success.Equals(0))
            {
                try
                {
                    dynamic jsonResponse = JsonConvert.DeserializeObject<dynamic>(response.Data.ToString());
                    Id = (int)jsonResponse;
                    Debug.WriteLine($"ID del usuario obtenido: {Id}");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error al deserializar el ID del usuario: {ex.Message}");
                }
            }
        }

        [RelayCommand]
        public async Task RequestCapturas()
        {
            if (Id <= 0)
            {
                Debug.WriteLine("ID no válido, no se pueden obtener las capturas.");
                return;
            }

            string ruta = $"http://localhost:8089/jpa/capturas/{Id}";
            var requestModel = new RequestModel
            {
                Method = "GET",
                Route = ruta,
                Data = string.Empty
            };

            var response = await APIService.ExecuteRequestJPA(requestModel);

            Debug.WriteLine($"Respuesta desde la API: {response.Data}");

            try
            {
                if (response.Success == 0 && response.Data != null)
                {
                    var capturas = JsonConvert.DeserializeObject<ObservableCollection<CapturaInfo>>(response.Data.ToString());
                    CapturasUsu = capturas ?? new ObservableCollection<CapturaInfo>();
                }
                else
                {
                    // Si la API indica error o no hay datos, vaciamos la colección
                    CapturasUsu = new ObservableCollection<CapturaInfo>();
                    Debug.WriteLine("No se pudieron obtener las capturas o la respuesta fue vacía.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al deserializar las capturas: {ex.Message}");
                CapturasUsu = new ObservableCollection<CapturaInfo>(); // Asegura que se limpia en caso de error
            }
        }

    }
}
