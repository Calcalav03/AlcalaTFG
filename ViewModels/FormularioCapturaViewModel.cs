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
        private DateTime fecha=DateTime.Today;
        [ObservableProperty]
        private string clima;
        [ObservableProperty]
        private string equipamiento;
        [ObservableProperty]
        private string cebo;
        [ObservableProperty]
        private string lloviendo;
        [ObservableProperty]
        private string metodo;


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

                // Si no hay errores, creamos el objeto
                var capturaDto = new CapturaDTO(
                    usuario: new CapturaDTO.UsuarioDto(1), // Aquí creamos un nuevo objeto UsuarioDto directamente en el constructor
                    especie: Nombre,
                    peso: Peso,
                    tamano: Tamano,
                    ubicacion: Ubicacion,
                    fecha: Fecha.ToUniversalTime().AddDays(1),
                    imagenUrl: Convert.ToBase64String(imagenBytes),
                    cebos: new HashSet<CapturaDTO.CeboDto1> { new CapturaDTO.CeboDto1(1) }, // Nuevos objetos de CeboDto1
                    equipamientos: new HashSet<CapturaDTO.EquipamientoDto1> { new CapturaDTO.EquipamientoDto1(1) }, // Nuevos objetos de EquipamientoDto1
                    climas: new HashSet<CapturaDTO.ClimaDto>
                    {
                        new CapturaDTO.ClimaDto(Temperatura, Clima, true) // Nuevo objeto de ClimaDto
                    },
                    metodosPescas: new HashSet<CapturaDTO.MetodosPescaDto> { new CapturaDTO.MetodosPescaDto(Metodo) } // Nuevo objeto de MetodosPescaDto
                );

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
