using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlcalaTFG.Models
{
    public class EquipamientoInfo
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("modelo")]
        public string Modelo { get; set; }

        [JsonProperty("marca")]
        public string Marca { get; set; }

        [JsonProperty("tipoEquipo")]
        public string TipoEquipo { get; set; }
    }
}
