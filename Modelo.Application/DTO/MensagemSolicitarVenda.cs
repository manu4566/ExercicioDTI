using Modelo.Application.Enum;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;


namespace Modelo.Application.DTO
{
    public class MensagemSolicitarVenda
    {
        [JsonProperty(PropertyName = "acao")]
        [Required]

        public AcaoVenda acao { get; set; }

        
    }
}
