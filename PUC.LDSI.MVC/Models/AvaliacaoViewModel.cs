using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PUC.LDSI.MVC.Models
{
    public class AvaliacaoViewModel
    {
        public int Id { get; set; }

        [DisplayName("Professor")]
        public string Professor { get; set; }

        [Required(ErrorMessage = "O campo Disciplina é obrigatório.")]
        [MaxLength(100, ErrorMessage = "Informe no máximo {0} caracteres.")]
        [DisplayName("Disciplina")]
        public string Disciplina { get; set; }

        [Required(ErrorMessage = "O campo Matéria é obrigatório.")]
        [MaxLength(100, ErrorMessage = "Informe no máximo {0} caracteres.")]
        [DisplayName("Matéria")]
        public string Materia { get; set; }

        [Required(ErrorMessage = "O campo Descrição é obrigatório.")]
        [MaxLength(100, ErrorMessage = "Informe no máximo {0} caracteres.")]
        [DisplayName("Descrição")]
        public string Descricao { get; set; }

        public List<QuestaoAvaliacaoViewModel> Questoes { get; set; }

        public bool EhValida
        {
            get
            {
                return Questoes != null && Questoes.Count > 0 && !Questoes.Where(x => x.Erro != string.Empty).Any();
            }
        }
    }
}
