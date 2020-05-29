using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProAgil.WebApi.Dto
{
    public class EventoDto
    {
        public int? EventoId { get; set; }

        [Required (ErrorMessage="Campo Obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage= "Local entre 3 e 100 caracteres")]
        public string Local { get; set; }
        public string DataEvento { get; set; }
        
        [Required (ErrorMessage = "O Tema deve ser preenchido!")]
        public string Tema { get; set; }

        [Range(2, 120000, ErrorMessage = "Quantidade de Pessoas é entre 2 e 120 Mil!")]
        public int QtdPessoas { get; set; }
        public string ImagemURL { get; set; }

        [Phone]
        public string Telefone { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }
        public List<LoteDto> Lotes { get; set; }
        public List<RedeSocialDto> RedesSociais { get; set; }
        public List<PalestranteDto> Palestrantes { get; set; }
    }
}