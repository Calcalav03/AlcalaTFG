using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlcalaTFG.Models
{
    public class CapturaInfo
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("especie")]
        public string Especie { get; set; }

        [JsonProperty("peso")]
        public decimal Peso { get; set; }

        [JsonProperty("tamano")]
        public decimal Tamano { get; set; }

        [JsonProperty("ubicacion")]
        public string Ubicacion { get; set; }

        [JsonProperty("fecha")]
        public DateTime Fecha { get; set; }

        [JsonProperty("imagenUrl")]
        public string ImagenUrl { get; set; }

        [JsonProperty("usuario")]
        public UsuarioInfo Usuario { get; set; }

        [JsonProperty("cebos")]
        public HashSet<CeboInfo1> Cebos { get; set; }

        [JsonProperty("equipamientos")]
        public HashSet<EquipamientoInfo1> Equipamientos { get; set; }

        [JsonProperty("climas")]
        public HashSet<ClimaInfo> Climas { get; set; }

        [JsonProperty("metodosPescas")]
        public HashSet<MetodosPescaInfo> MetodosPescas { get; set; }

        public class UsuarioInfo
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("nombre")]
            public string Nombre { get; set; }

            [JsonProperty("email")]
            public string Email { get; set; }
        }

        public class CeboInfo1
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("tipoCebo")]
            public string TipoCebo { get; set; }

            [JsonProperty("descripcion")]
            public string Descripcion { get; set; }
        }

        public class EquipamientoInfo1
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("tipoEquipo")]
            public string TipoEquipo { get; set; }

            [JsonProperty("marca")]
            public string Marca { get; set; }

            [JsonProperty("modelo")]
            public string Modelo { get; set; }
        }

        public class ClimaInfo
        {
            [JsonProperty("temperatura")]
            public string Temperatura { get; set; }

            [JsonProperty("nubosidad")]
            public string Nubosidad { get; set; }

            [JsonProperty("lluvia")]
            public bool Lluvia { get; set; }
        }

        public class MetodosPescaInfo
        {
            [JsonProperty("metodo")]
            public string Metodo { get; set; }
        }
    }
}
