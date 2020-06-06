using System.Collections.Generic;

namespace PUC.LDSI.Domain.Entities
{
    public class OpcaoProva : Entity
    {
        public int OpcaoAvaliacaoId { get; set; }
        public int QuestaoProvaId { get; set; }
        public bool Resposta { get; set; }
        public OpcaoAvaliacao OpcaoAvaliacao { get; set; }
        public QuestaoProva QuestaoProva { get; set; }

        public override string[] Validate()
        {
            var erros = new List<string>();

            if (OpcaoAvaliacaoId == 0)
                erros.Add("A opção da avaliação precisa ser informada!");

            if (QuestaoProvaId == 0)
                erros.Add("A questão da prova precisa ser informada!");

            return erros.ToArray();
        }
    }
}
