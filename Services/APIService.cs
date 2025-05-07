
using AlcalaTFG.models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace AlcalaTFG.services
{
    internal class APIService
    {

        // Método para ejecutar una solicitud HTTP y obtener una respuesta asincrónica
        public static async Task<ResponseModel> ExecuteRequest()
        {
            string baseUrl = "http://api.weatherapi.com/v1/forecast.json";
            string key = "b6533ee18ead4cab984170649252104";
            string q = "39.9622466,-3.5096778";
            string days = "1";
            string aqi = "no";
            string alerts = "no";

            string url = $"{baseUrl}?key={key}&q={q}&days={days}&aqi={aqi}&alerts={alerts}";

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(content);
                dynamic jsonResponse = JsonConvert.DeserializeObject(content);
                // Ejemplos de acceso
                string ciudad = jsonResponse.location.name;
                double temperaturaActual = jsonResponse.current.temp_c;
                string condicionActual = jsonResponse.current.condition.text;
                string hora = jsonResponse.forecast.forecastday[0].hour[0].time;
                double tempPrimeraHora = jsonResponse.forecast.forecastday[0].hour[0].temp_c;
            }
            else
            {
                Debug.WriteLine($"Error: {response.StatusCode}");
            }
            return new ResponseModel();
        }





        // Método para ejecutar una solicitud HTTP y obtener una respuesta asincrónica
        public static async Task<ResponseModel> ExecuteRequestJPA(RequestModel requestModel)
        {
            // Se crea una instancia de la clase ResponseModel para almacenar la respuesta
            ResponseModel responseModel = new ResponseModel();

            // Se serializa el objeto RequestModel a formato JSON
            var data = JsonConvert.SerializeObject(requestModel.Data);
            Debug.WriteLine(data);

            // Se utilizan bloques 'using' para asegurar que los recursos se liberen correctamente
            using (var handler = new StandardSocketsHttpHandler())
            using (var client = new HttpClient(handler))
            {

                // Se crea una nueva solicitud HTTP con el método y la ruta especificados en el objeto RequestModel
                var request = new HttpRequestMessage(new HttpMethod(requestModel.Method), requestModel.Route);

                // Se establece el encabezado 'Accept' para indicar que se acepta JSON como tipo de respuesta
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Se incluye el contenido JSON en la solicitud
                request.Content = new StringContent(data, Encoding.UTF8, "application/json");

                // Recuperar el token y usuario desde SecureStorage
                string token = await SecureStorage.GetAsync("auth_token") ?? "";
                string user = await SecureStorage.GetAsync("user") ?? "";

                // Si el token está disponible, agregarlo al encabezado
                if (!string.IsNullOrEmpty(token))
                {
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    request.Headers.Add("User", user); // Puedes cambiar el nombre del encabezado si el backend espera otro.
                }
                try
                {
                    // Se envía la solicitud al servidor de manera asíncrona y se espera una respuesta
                    using (HttpResponseMessage response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
                    {
                        // Si la respuesta es exitosa
                        if (response.IsSuccessStatusCode)
                        {
                            // Se lee la respuesta como una cadena JSON
                            var stringResponse = await response.Content.ReadAsStringAsync();
                            if (stringResponse != null)
                            {
                                // Se deserializa la cadena JSON en un objeto ResponseModel
                                responseModel = JsonConvert.DeserializeObject<ResponseModel>(stringResponse) ?? new ResponseModel();

                                // Se imprime la respuesta en la consola de depuración
                                Debug.Write("Respuesta desde la API: ");
                                Debug.WriteLine(stringResponse);
                            }

                        }
                        else
                        {
                            // Si la respuesta no es exitosa, se imprime el código de estado en la consola de depuración
                            Debug.WriteLine(response.StatusCode);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Si se produce una excepción durante la solicitud, se imprime un mensaje de error en la consola de depuración
                    Debug.WriteLine($"Error al enviar la solicitud a la API: {ex.Message}");
                }
            }

            // Se devuelve el objeto ResponseModel, que contiene la respuesta de la API
            return responseModel;
        }
    }


}

