using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AlcalaTFG.Models
{
    public class CapturaDTO
    {
        private CeboDto1 cebos;
        private EquipamientoDto1 equipamientos;
        private ClimaDto climas;
        private MetodosPescaDto metodosPescas;

        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("usuario")]
        public UsuarioDto Usuario { get; set; }

        [JsonProperty("especie")]
        public string Especie { get; set; }

        [JsonProperty("peso")]
        public decimal Peso { get; set; }

        [JsonProperty("tamano")]
        public decimal Tamano { get; set; }

        [JsonProperty("ubicacion")]
        public string Ubicacion { get; set; }

        [JsonProperty("fecha")]
        public DateTimeOffset Fecha { get; set; } = DateTimeOffset.UtcNow;

        [JsonProperty("imagenUrl")]
        public string ImagenUrl { get; set; }

        [JsonProperty("cebos")]
        public HashSet<CeboDto1> Cebos { get; set; }

        [JsonProperty("equipamientos")]
        public HashSet<EquipamientoDto1> Equipamientos { get; set; }

        [JsonProperty("climas")]
        public HashSet<ClimaDto> Climas { get; set; }

        [JsonProperty("metodosPescas")]
        public HashSet<MetodosPescaDto> MetodosPescas { get; set; }

        public CapturaDTO( UsuarioDto usuario, string especie, decimal peso, decimal tamano, string ubicacion, DateTime fecha, string imagenUrl, HashSet<CeboDto1> cebos, HashSet<EquipamientoDto1> equipamientos, HashSet<ClimaDto> climas, HashSet<MetodosPescaDto> metodosPescas)
        {
            Usuario = usuario;
            Especie = especie;
            Peso = peso;
            Tamano = tamano;
            Ubicacion = ubicacion;
            Fecha = fecha;
            ImagenUrl = imagenUrl;
            Cebos = cebos;
            Equipamientos = equipamientos;
            Climas = climas;
            MetodosPescas = metodosPescas;
        }

        public CapturaDTO(UsuarioDto usuario, string especie, int peso, int tamano, string ubicacion, DateTime fecha, string imagenUrl, CeboDto1 cebos, EquipamientoDto1 equipamientos, ClimaDto climas, MetodosPescaDto metodosPescas)
        {
            Usuario = usuario;
            Especie = especie;
            Peso = peso;
            Tamano = tamano;
            Ubicacion = ubicacion;
            Fecha = fecha;
            ImagenUrl = imagenUrl;
            this.cebos = cebos;
            this.equipamientos = equipamientos;
            this.climas = climas;
            this.metodosPescas = metodosPescas;
        }

        public override bool Equals(object obj)
        {
            return obj is CapturaDTO dto &&
                   
                   EqualityComparer<UsuarioDto>.Default.Equals(Usuario, dto.Usuario) &&
                   Especie == dto.Especie &&
                   Peso == dto.Peso &&
                   Tamano == dto.Tamano &&
                   Ubicacion == dto.Ubicacion &&
                   Fecha == dto.Fecha &&
                   ImagenUrl == dto.ImagenUrl &&
                   EqualityComparer<HashSet<CeboDto1>>.Default.Equals(Cebos, dto.Cebos) &&
                   EqualityComparer<HashSet<EquipamientoDto1>>.Default.Equals(Equipamientos, dto.Equipamientos) &&
                   EqualityComparer<HashSet<ClimaDto>>.Default.Equals(Climas, dto.Climas) &&
                   EqualityComparer<HashSet<MetodosPescaDto>>.Default.Equals(MetodosPescas, dto.MetodosPescas);
        }

        //public override int GetHashCode()
        //{
        //    return HashCode.Combine(Usuario, Especie, Peso, Tamano, Ubicacion, Fecha, ImagenUrl, Cebos, Equipamientos, Climas, MetodosPescas);
        //}

        public override string ToString()
        {
            return $"{nameof(CapturaDTO)}( Usuario = {Usuario}, Especie = {Especie}, Peso = {Peso}, Tamano = {Tamano}, Ubicacion = {Ubicacion}, Fecha = {Fecha}, ImagenUrl = {ImagenUrl}, Cebos = {Cebos}, Equipamientos = {Equipamientos}, Climas = {Climas}, MetodosPescas = {MetodosPescas})";
        }
        public class UsuarioDto
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            public UsuarioDto(int id)
            {
                Id = id;
            }

            public override bool Equals(object obj)
            {
                return obj is UsuarioDto dto &&
                       Id == dto.Id;
            }

            public override int GetHashCode()
            {
                return Id.GetHashCode();
            }

            public override string ToString()
            {
                return $"{nameof(UsuarioDto)}(Id = {Id})";
            }
        }

        public class CeboDto1
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            public CeboDto1(int id)
            {
                Id = id;
            }

            public override bool Equals(object obj)
            {
                return obj is CeboDto1 dto &&
                       Id == dto.Id;
            }

            public override int GetHashCode()
            {
                return Id.GetHashCode();
            }

            public override string ToString()
            {
                return $"{nameof(CeboDto1)}(Id = {Id})";
            }
        }

        public class EquipamientoDto1
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            public EquipamientoDto1(int id)
            {
                Id = id;
            }

            public override bool Equals(object obj)
            {
                return obj is EquipamientoDto1 dto &&
                       Id == dto.Id;
            }

            public override int GetHashCode()
            {
                return Id.GetHashCode();
            }

            public override string ToString()
            {
                return $"{nameof(EquipamientoDto1)}(Id = {Id})";
            }
        }

        public class ClimaDto
        {
            [JsonProperty("temperatura")]
            public string Temperatura { get; set; }

            [JsonProperty("nubosidad")]
            public string Nubosidad { get; set; }

            [JsonProperty("lluvia")]
            public bool Lluvia { get; set; }

            public ClimaDto(string temperatura, string nubosidad, bool lluvia)
            {
                Temperatura = temperatura;
                Nubosidad = nubosidad;
                Lluvia = lluvia;
            }

            public override bool Equals(object obj)
            {
                return obj is ClimaDto dto &&
                       Temperatura == dto.Temperatura &&
                       Nubosidad == dto.Nubosidad &&
                       Lluvia == dto.Lluvia;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Temperatura, Nubosidad, Lluvia);
            }

            public override string ToString()
            {
                return $"{nameof(ClimaDto)}(Temperatura = {Temperatura}, Nubosidad = {Nubosidad}, Lluvia = {Lluvia})";
            }
        }

        public class MetodosPescaDto
        {
            [JsonProperty("metodo")]
            public string Metodo { get; set; }

            public MetodosPescaDto(string metodo)
            {
                Metodo = metodo;
            }

            public override bool Equals(object obj)
            {
                return obj is MetodosPescaDto dto &&
                       Metodo == dto.Metodo;
            }

            public override int GetHashCode()
            {
                return Metodo.GetHashCode();
            }

            public override string ToString()
            {
                return $"{nameof(MetodosPescaDto)}(Metodo = {Metodo})";
            }
        }
    }

   
}
