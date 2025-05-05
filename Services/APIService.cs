
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
    

}
}
