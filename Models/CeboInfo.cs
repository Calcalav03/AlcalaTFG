using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlcalaTFG.Models
{
    public class CeboInfo
    {
       

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("descripcion")]
        public string Descripcion { get; set; }

        [JsonProperty("tipoCebo")]
        public string TipoCebo { get; set; }

      

        public CeboInfo(int id, string? tipoCebo, string? descripcion)
        {
            this.Id = id;
            TipoCebo = tipoCebo;
            Descripcion = descripcion;
        }

        public CeboInfo() { }
    }

}
