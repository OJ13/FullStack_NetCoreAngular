using System.ComponentModel.DataAnnotations;

namespace ProAgil.WebApi.Dto
{
    public class RedeSocialDto
    {
        public int Id { get;set; }
        [Required (ErrorMessage="O Campo {0} é Obrigatório!")]
        public string Nome { get; set; }
        public string URL { get; set; }
    }
}