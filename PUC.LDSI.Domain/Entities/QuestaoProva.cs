using System.Collections.Generic;

namespace PUC.LDSI.Domain.Entities
{
    public class QuestaoProva : Entity
    {
        public int ProvaId { get; set; }
        public int QuestaoId { get; set; }
        public decimal Nota { get; set; }
        public Prova Prova { get; set; }
        public QuestaoAvaliacao QuestaoAvaliacao { get; set; }
        public List<OpcaoProva> OpcoesProva { get; set; }

        public override string[] Validate()
        {
            var erros = new List<string>();

            if (ProvaId == 0)
                erros.Add("A prova precisa ser informada!");

            if (QuestaoId == 0)
                erros.Add("A questão precisa ser informada!");

            return erros.ToArray();
        }
    }
}
