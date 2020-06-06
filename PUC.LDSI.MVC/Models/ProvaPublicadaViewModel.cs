using System;
using System.ComponentModel;

namespace PUC.LDSI.MVC.Models
{
    public class ProvaPublicadaViewModel
    {
        [DisplayName("Avaliação")]
        public int AvaliacaoId { get; set; }

        [DisplayName("Disciplina")]
        public string Disciplina { get; set; }
        [DisplayName("Matéria")]
        public string Materia { get; set; }
        [DisplayName("Descrição")]
        public string Descricao { get; set; }
        [DisplayName("Publicada Em")]
        public DateTime DataCriacao { get; set; }
        [DisplayName("Início")]
        public DateTime DataInicio { get; set; }
        [DisplayName("Fim")]
        public DateTime DataFim { get; set; }
        [DisplayName("Valor da Prova")]
        public int ValorProva { get; set; }
        [DisplayName("Nota")]
        public decimal NotaObtida { get; set; }
        [DisplayName("Situação")]
        public string Status { get; set; }
    }
}
