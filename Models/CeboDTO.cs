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
    public class CeboDTO
    {
        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("tipoCebo")]
        public string TipoCebo { get; set; }

        [JsonProperty("descripcion")]
        public string Descripcion { get; set; }

        //public CeboDTO(int id, string tipoCebo, string descripcion)
        //{
        //    //Id = id;
        //    TipoCebo = tipoCebo;
        //    Descripcion = descripcion;
        //}
        public CeboDTO( string tipoCebo, string descripcion)
        {
            TipoCebo = tipoCebo;
            Descripcion = descripcion;
        }

        public override bool Equals(object obj)
        {
            return obj is CeboDTO dto &&
                   //Id == dto.Id &&
                   TipoCebo == dto.TipoCebo &&
                   Descripcion == dto.Descripcion;
        }

        //public override int GetHashCode()
        //{
        //    return HashCode.Combine(Id, TipoCebo, Descripcion);
        //}

        //public override string ToString()
        //{
        //    return $"{nameof(CeboDTO)}(Id = {Id}, TipoCebo = {TipoCebo}, Descripcion = {Descripcion})";
        //}
    }
}
