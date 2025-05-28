using AlcalaTFG.models;
using AlcalaTFG.Models;
using AlcalaTFG.services;
using AlcalaTFG.Services;
using AlcalaTFG.Utils;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Storage;
using Mopups.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AlcalaTFG.ViewModels
{
    public partial class FormularioCapturaViewModel : ObservableObject
    {
        [ObservableProperty]
        private string imagenUrl = "defecto.png";
        // Ruta de imagen por defecto
        private const string ImagenPorDefecto = "resource://AlcalaTFG.Resources.Images.defecto.png";




        [ObservableProperty]
        private byte[] imagenBytes;


        [ObservableProperty]
        private decimal peso;
        [ObservableProperty]
        private decimal tamano;
        [ObservableProperty]
        private string temperatura;
        [ObservableProperty]
        private string nombre;
        [ObservableProperty]
        private string ubicacion;
        [ObservableProperty]
        private DateTime fecha = DateTime.Today;
        [ObservableProperty]
        private string clima;
        [ObservableProperty]
        private ObservableCollection<EquipamientoInfo> equipamientos;
        [ObservableProperty]
        private EquipamientoInfo equipamiento;

        [ObservableProperty]
        private ObservableCollection<CeboInfo> cebos;

        [ObservableProperty]
        private CeboInfo cebo;
        [ObservableProperty]
        private bool lloviendo;
        [ObservableProperty]
        private string metodo;
        [ObservableProperty]
        private int id;
        [ObservableProperty]
        private CapturaInfo capturaSelected;

        CapturaUsuViewModel capturaUsuViewModel;


        public FormularioCapturaViewModel()
        {
            CargarDatos();
        }
        public FormularioCapturaViewModel(CapturaInfo capturainfo, CapturaUsuViewModel capturaUsuVM)
        {
            capturaUsuViewModel = capturaUsuVM;
           
            CargarDatos();
            CapturaSelected = capturainfo;

            ImagenUrl = capturainfo.ImagenUrl;
            //ImagenBytes = ConvertirImagenABase64(ImagenUrl);
            Tamano = capturainfo.Tamano;
            Peso = capturainfo.Peso;
            Nombre = capturainfo.Especie;
            Fecha = capturainfo.Fecha;
            Ubicacion = capturainfo.Ubicacion;
            Metodo = capturainfo.MetodosPescas?.FirstOrDefault()?.Metodo;
            Temperatura = capturainfo.Climas?.FirstOrDefault()?.Temperatura;
            Clima = capturainfo.Climas?.FirstOrDefault()?.Nubosidad;
            Lloviendo = capturainfo.Climas?.FirstOrDefault()?.Lluvia ?? false;


        }
        [RelayCommand]
        public void CargarDatos()
        {
            RequestIdUsu();
            RequestCebos();
            RequestEquipamientos();
        }

        //METODO PARA OBTENER LOS CEBOS
        [RelayCommand]
        public async void RequestCebos()
        {
            RequestModel requestModel = new RequestModel
            {
                Method = "GET",
                Route = "http://localhost:8089/jpa/cebos",
                Data = string.Empty
            };


            ResponseModel response = await APIService.ExecuteRequestJPA(requestModel);

            if (response.Success.Equals(0))
            {
                try
                {
                    Cebos = JsonConvert.DeserializeObject<ObservableCollection<CeboInfo>>(response.Data.ToString());
                    try
                    {
                       if(CapturaSelected != null)
                        Cebo = Cebos.Where(c => c.Id == CapturaSelected.Cebos.FirstOrDefault().Id).FirstOrDefault();
                        

                    }
                    catch (Exception ex) { }

                }
                catch (Exception ex) { }
            }
        }

        public async void RequestIdUsu()
        {
            string userLogin = AuthService.Instance.UserLogin;

            if (string.IsNullOrEmpty(userLogin))
            {
                Debug.WriteLine("UserLogin no está establecido.");
                return;
            }

            RequestModel requestModel = new RequestModel
            {
                Method = "GET",
                Route = $"http://localhost:8089/jpa/usuarioId/{userLogin}",
                Data = string.Empty
            };

            ResponseModel response = await APIService.ExecuteRequestJPA(requestModel);

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



        //METODO PARA OBTENER LOS EQUIPAMIENTOS
        [RelayCommand]
        public async void RequestEquipamientos()
        {
            RequestModel requestModel = new RequestModel
            {
                Method = "GET",
                Route = "http://localhost:8089/jpa/equipamientos",
                Data = string.Empty
            };


            ResponseModel response = await APIService.ExecuteRequestJPA(requestModel);

            if (response.Success.Equals(0))
            {
                try
                {
                    Equipamientos = JsonConvert.DeserializeObject<ObservableCollection<EquipamientoInfo>>(response.Data.ToString());
                    try
                    {
                        if (CapturaSelected != null)
                            Equipamiento = Equipamientos.Where(e => e.Id == CapturaSelected.Equipamientos.FirstOrDefault().Id).FirstOrDefault();
                    }
                    catch (Exception ex) { }
                }
                catch (Exception ex) { }
            }
        }

        [RelayCommand]
        public async Task CrearCaptura()
        {
            string message = "Ocurrió un error al crear la captura: ";
            string bien = "captura creada correctamente!";


            try
            {
                // Lista para acumular errores
                List<string> errores = new List<string>();

                // Validación de cada campo
                if (string.IsNullOrWhiteSpace(Nombre))
                    errores.Add("El nombre de la especie es obligatorio.");

                if (!decimal.TryParse(Peso.ToString(), out _) || Peso <= 0)
                    errores.Add("El peso debe ser un valor numérico positivo.");

                if (!decimal.TryParse(Tamano.ToString(), out _) || Tamano <= 0)
                    errores.Add("El tamaño debe ser un valor numérico positivo.");

                if (string.IsNullOrWhiteSpace(Ubicacion))
                    errores.Add("La ubicación es obligatoria.");

                if (Fecha == default)
                    errores.Add("La fecha es obligatoria.");
                
                 if (imagenBytes == null || imagenBytes.Length == 0)
                        errores.Add("La imagen es obligatoria.");
                

                if (Temperatura == null)
                    errores.Add("La temperatura es obligatoria.");

                if (string.IsNullOrWhiteSpace(Clima))
                    errores.Add("El clima es obligatorio.");

                if (string.IsNullOrWhiteSpace(Metodo))
                    errores.Add("El método de pesca es obligatorio.");

                // Si hay errores, los mostramos y salimos
                if (errores.Count > 0)
                {
                    await App.Current.MainPage.DisplayAlert("Error", string.Join("\n", errores), "Aceptar");
                    return;
                }
                CapturaDTO capturaDto;
                if (CapturaSelected != null)
                {
                    capturaDto = new CapturaDTO(
                        usuario: new CapturaDTO.UsuarioDto(Id), // Aquí creamos un nuevo objeto UsuarioDto directamente en el constructor
                        especie: Nombre,
                        peso: Peso,
                        tamano: Tamano,
                        ubicacion: Ubicacion,
                        fecha: Fecha.ToUniversalTime().AddDays(1),
                        imagenUrl: Convert.ToBase64String(imagenBytes),
                        cebos: new HashSet<CapturaDTO.CeboDto1> { new CapturaDTO.CeboDto1(Cebo.Id) }, // Nuevos objetos de CeboDto1
                        equipamientos: new HashSet<CapturaDTO.EquipamientoDto1> { new CapturaDTO.EquipamientoDto1(Equipamiento.Id) }, // Nuevos objetos de EquipamientoDto1
                        climas: new HashSet<CapturaDTO.ClimaDto>
                        {
                        new CapturaDTO.ClimaDto(Temperatura, Clima, Lloviendo) // Nuevo objeto de ClimaDto
                        },
                        metodosPescas: new HashSet<CapturaDTO.MetodosPescaDto> { new CapturaDTO.MetodosPescaDto(Metodo) } // Nuevo objeto de MetodosPescaDto
                    )
                    {
                        Id = CapturaSelected.Id
                    };


                }
                else
                {
                    // Si no hay errores, creamos el objeto
                    capturaDto = new CapturaDTO(
                        usuario: new CapturaDTO.UsuarioDto(Id), // Aquí creamos un nuevo objeto UsuarioDto directamente en el constructor
                        especie: Nombre,
                        peso: Peso,
                        tamano: Tamano,
                        ubicacion: Ubicacion,
                        fecha: Fecha.ToUniversalTime().AddDays(1),
                        imagenUrl: Convert.ToBase64String(imagenBytes),
                        cebos: new HashSet<CapturaDTO.CeboDto1> { new CapturaDTO.CeboDto1(Cebo.Id) }, // Nuevos objetos de CeboDto1
                        equipamientos: new HashSet<CapturaDTO.EquipamientoDto1> { new CapturaDTO.EquipamientoDto1(Equipamiento.Id) }, // Nuevos objetos de EquipamientoDto1
                        climas: new HashSet<CapturaDTO.ClimaDto>
                        {
                        new CapturaDTO.ClimaDto(Temperatura, Clima, Lloviendo) // Nuevo objeto de ClimaDto
                        },
                        metodosPescas: new HashSet<CapturaDTO.MetodosPescaDto> { new CapturaDTO.MetodosPescaDto(Metodo) } // Nuevo objeto de MetodosPescaDto
                    );
                }

                // Aquí puedes continuar con el proceso de guardado o cualquier otra lógica que necesites.

                // Crear el RequestModel
                var request = new RequestModel
                {
                    Data = capturaDto,
                    Method = "POST",
                    Route = "http://localhost:8089/jpa/crearCaptura"
                };

                // Enviar la solicitud al servidor
                ResponseModel response = await APIService.ExecuteRequestJPA(request);

                // Mostrar el mensaje de respuesta
                await App.Current.MainPage.DisplayAlert("Mensaje", bien, "Aceptar");



                LimpiarFormulario();

                if (capturaSelected != null) {
                    
                    await capturaUsuViewModel.InitializeAsync();
                    await MopupService.Instance.PopAllAsync();
                }


            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", message + ex.Message, "Aceptar");
                Debug.WriteLine(message + ex.Message);
            }
        }
        // MÉTODO PARA SELECCIONAR LA IMAGEN
        [RelayCommand]
        public async void SeleccionarImagen()
        {
            var file = await FileSelector.SelectImageAsync();
            if (file != null)
            {
                ImagenUrl = file.FullPath;
                ImagenBytes = await ConvertirImagenABase64(ImagenUrl);
                if (CapturaSelected != null)
                {
                    ImagenUrl = Convert.ToBase64String(ImagenBytes);
                    
                }
            }
            else
            {
                //ImagenUrl = ImagenPorDefecto; // Asignar la imagen por defecto si no se selecciona ninguna
                //ImagenBytes = await ConvertirImagenABase64(ImagenUrl);
            }
        }

        // METODO PARA CONVERTIR LA IMAGEN A BYTE[]
        private async Task<byte[]> ConvertirImagenABase64(string rutaImagen)
        {
            try
            {
                using (var stream = File.OpenRead(rutaImagen))
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await stream.CopyToAsync(memoryStream);
                        return memoryStream.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al convertir la imagen: {ex.Message}");
                return null;
            }
        }

        [RelayCommand]
        public async Task LimpiarFormulario()
        {
            Peso = 0;
            Tamano = 0;
            //Modelo = string.Empty;
            Fecha = DateTime.Today;
            Temperatura = null;
            Nombre = string.Empty;
            Ubicacion = string.Empty;
            Clima = null;
            Equipamiento = null;
            Cebo = null;
            Lloviendo = false;
            Metodo = string.Empty;
            ImagenUrl = "defecto.png";
            ImagenBytes = null;
            if (CapturaSelected != null)
            {
                using Stream stream = await FileSystem.OpenAppPackageFileAsync("imagen.png");
                using var ms = new MemoryStream();
                stream.CopyTo(ms);

                ImagenUrl = Convert.ToBase64String(ms.ToArray());
            }

        }
    }
}
