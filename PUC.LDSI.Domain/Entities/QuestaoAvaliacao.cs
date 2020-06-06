using System.Collections.Generic;
using System.Linq;

namespace PUC.LDSI.Domain.Entities
{
    public class QuestaoAvaliacao : Entity
    {
        public int AvaliacaoId { get; set; }
        public int Tipo { get; set; }
        public string Enunciado { get; set; }
        public Avaliacao Avaliacao { get; set; }
        public List<OpcaoAvaliacao> Opcoes { get; set; }
        public List<QuestaoProva> QuestoesProva { get; set; }

        public override string[] Validate()
        {
            var erros = new List<string>();

            if (AvaliacaoId == 0)
                erros.Add("A avaliação precisa ser informada!");

            if (Tipo != 1 && Tipo != 2)
                erros.Add("Tipo inválido! Os tipos válidos são: 1 - Multipla Escolha e 2 - Verdadeiro ou Falso");

            if (string.IsNullOrWhiteSpace(Enunciado))
                erros.Add("O enunciado precisa ser informado!");

            return erros.ToArray();
        }
    }
}
