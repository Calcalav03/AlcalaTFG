using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlcalaTFG.Models
{
    [Serializable]
    public class EquipamientoDTO
    {
        //[JsonProperty("id")]
        //public int Id { get; set; }

        [JsonProperty("tipoEquipo")]
        public string TipoEquipo { get; set; }

        [JsonProperty("marca")]
        public string Marca { get; set; }

        [JsonProperty("modelo")]
        public string Modelo { get; set; }


        // Constructor
        public EquipamientoDTO(string tipoEquipo, string marca, string modelo)
        {
            TipoEquipo = tipoEquipo;
            Marca = marca;
            Modelo = modelo;
        }

        // Equals (compara propiedades, útil para pruebas)
        public override bool Equals(object obj)
        {
            return obj is EquipamientoDTO dto &&
                   TipoEquipo == dto.TipoEquipo &&
                   Marca == dto.Marca &&
                   Modelo == dto.Modelo;
        }

        // GetHashCode (para colecciones)
        public override int GetHashCode()
        {
            return HashCode.Combine(TipoEquipo, Marca, Modelo);
        }

        // ToString (para depuración)
        public override string ToString()
        {
            return $"{nameof(EquipamientoDTO)}(TipoEquipo = {TipoEquipo}, Marca = {Marca}, Modelo = {Modelo})";
        }
    }

   
}
