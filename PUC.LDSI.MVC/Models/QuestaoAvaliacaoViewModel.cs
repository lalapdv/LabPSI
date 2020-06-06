using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PUC.LDSI.MVC.Models
{
    public class QuestaoAvaliacaoViewModel
    {
        public int Id { get; set; }
        public int AvaliacaoId { get; set; }
        [Required(ErrorMessage = "O campo Tipo é obrigatório.")]
        public int Tipo { get; set; }
        [Required(ErrorMessage = "O campo Enunciado é obrigatório.")]
        [MaxLength(255, ErrorMessage = "Informe no máximo {0} caracteres.")]
        public string Enunciado { get; set; }

        public AvaliacaoViewModel Avaliacao { get; set; }
        public List<OpcaoAvaliacaoViewModel> Opcoes { get; set; }

        public string Erro
        {
            get
            {
                if (Opcoes == null || Opcoes.Count < 4)
                    return "A questão deve ter pelo menos 4 (quatro) opções.";
                else if (Tipo == 1 && !Opcoes.Where(x => x.Verdadeira).Any())
                    return "A questão deve ter pelo menos 1 (uma) opção verdadeira.";
                else
                    return string.Empty;
            }
        }
    }
}
