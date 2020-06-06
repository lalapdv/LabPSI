using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PUC.LDSI.MVC.Models
{
    public class OpcaoAvaliacaoViewModel
    {
        public int Id { get; set; }
        public int QuestaoId { get; set; }
        [DisplayName("Descrição")]
        [Required(ErrorMessage = "O campo Descrição é obrigatório.")]
        [MaxLength(100, ErrorMessage = "Informe no máximo {0} caracteres.")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "O campo Verdadeira é obrigatório.")]
        public bool Verdadeira { get; set; }

        public QuestaoAvaliacaoViewModel Questao { get; set; }
    }
}
