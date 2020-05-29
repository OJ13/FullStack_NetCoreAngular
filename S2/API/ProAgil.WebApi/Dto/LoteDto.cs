using System.ComponentModel.DataAnnotations;

namespace ProAgil.WebApi.Dto
{
    public class LoteDto
    {
         public int Id { get; set; }
         [Required]
        public string Nome{ get; set; }
        [Required]
        public decimal Preco { get; set; }
        public string DataInicio { get; set; }    
        public string DataFim { get; set; }

        [Range(1, 5, ErrorMessage= "Lote tem que ser entre 1 e 5!")]
        public int Quantidade { get; set; }
        
    }
}