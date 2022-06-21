using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Modelo.Application.DTO
{
    public class MensagemAdcUsuario
    {
        [JsonProperty(PropertyName = "cpf")]
        [Required]
        public string Cpf { get; set; }

        [JsonProperty(PropertyName = "nome")]
        [Required]
        public string Nome { get; set; }

        [JsonProperty(PropertyName = "email")]
        [Required]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "senha")]
        [Required]
        public string senha { get; set; }

        [JsonProperty(PropertyName = "admin")]
        [Required]
        public bool Admin { get; set; }

    }
}
