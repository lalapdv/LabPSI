using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PUC.LDSI.MVC.Models
{
    public class PublicacaoViewModel
    {
        public int Id { get; set; }
        [DisplayName("Turma")]
        public int TurmaId { get; set; }
        [DisplayName("Avaliação")]
        public int AvaliacaoId { get; set; }
        [DisplayName("Publicado Em")]
        public DateTime DataCriacao { get; set; }
        [Required(ErrorMessage = "O campo Início é obrigatório.")]
        [DisplayName("Início")]
        [DataType(DataType.Date)]
        public DateTime DataInicio { get; set; }
        [Required(ErrorMessage = "O campo Fim é obrigatório.")]
        [DisplayName("Fim")]
        [DataType(DataType.Date)]
        public DateTime DataFim { get; set; }
        [Required(ErrorMessage = "O campo Valor é obrigatório.")]
        [DisplayName("Valor")]
        public int ValorProva { get; set; }
        public AvaliacaoViewModel Avaliacao { get; set; }
        public TurmaViewModel Turma { get; set; }
    }
}
