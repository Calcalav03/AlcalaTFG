using AlcalaTFG.models;
using AlcalaTFG.Models;
using AlcalaTFG.services;
using AlcalaTFG.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlcalaTFG.ViewModels
{
    public partial class FormularioCapturaViewModel:ObservableObject    
    {
        [ObservableProperty]
        private string imagenUrl="defecto.png";
        // Ruta de imagen por defecto
        private const string ImagenPorDefecto = "resource://AlcalaTFG.Resources.Images.defecto.png";

       


        [ObservableProperty]
        private byte[] imagenBytes;


        [ObservableProperty]
        private string peso;
        [ObservableProperty]
        private string tamano;
        [ObservableProperty]
        private string temperatura;
        [ObservableProperty]
        private string nombre;
        [ObservableProperty]
        private string ubicacion;
        [ObservableProperty]
        private string fecha;
        [ObservableProperty]
        private string clima;
        [ObservableProperty]
        private string equipamiento;
        [ObservableProperty]
        private string cebo;
        [ObservableProperty]
        private string lloviendo;


        [RelayCommand]
        public async Task CrearCaptura()
        {
            string message = "Ocurrió un error al crear la captura: ";
            string bien = "captura creada correctamente!";

            try
            {
                //if (string.IsNullOrWhiteSpace(TipoCebo) || string.IsNullOrWhiteSpace(Descripcion))
                //{
                //    await App.Current.MainPage.DisplayAlert("Error", "El tipo de cebo y la descripción son obligatorios", "Aceptar");
                //    return;
                //}

                var capturaDto = new CapturaDTO(
                usuario: new CapturaDTO.UsuarioDto(1), // Aquí creamos un nuevo objeto UsuarioDto directamente en el constructor
                especie: "Carpa",
                peso: 54,
                tamano: 80,
                ubicacion: "Embalse de Entrepeñas",
                fecha: DateTime.UtcNow,
                imagenUrl: Convert.ToBase64String(imagenBytes),
                cebos: new HashSet<CapturaDTO.CeboDto1> { new CapturaDTO.CeboDto1(1), new CapturaDTO.CeboDto1(2) }, // Nuevos objetos de CeboDto1
                equipamientos: new HashSet<CapturaDTO.EquipamientoDto1> { new CapturaDTO.EquipamientoDto1(1) }, // Nuevos objetos de EquipamientoDto1
                climas: new HashSet<CapturaDTO.ClimaDto>
                {
                    new CapturaDTO.ClimaDto("22°C", "Despejado", true) // Nuevo objeto de ClimaDto
                },
                metodosPescas: new HashSet<CapturaDTO.MetodosPescaDto> { new CapturaDTO.MetodosPescaDto("Spinning") } // Nuevo objeto de MetodosPescaDto
            );

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



                LimpiarFormularioCebo();


            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", message + ex.Message, "Aceptar");
                Debug.WriteLine(message + ex.Message);
            }
        }
        [RelayCommand]
        public void LimpiarFormularioCebo()
        {
            //TipoCebo = string.Empty;
            //Descripcion = string.Empty;
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

       
    }
}
