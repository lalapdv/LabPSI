using System.Collections.Generic;

namespace PUC.LDSI.Domain.Entities
{
    public class OpcaoAvaliacao : Entity
    {
        public int QuestaoId { get; set; }
        public string Descricao { get; set; }
        public bool Verdadeira { get; set; }
        public QuestaoAvaliacao Questao { get; set; }
        public List<OpcaoProva> OpcoesProva { get; set; }

        public override string[] Validate()
        {
            var erros = new List<string>();

            if (string.IsNullOrEmpty(Descricao))
                erros.Add("A descrição precisa ser informada!");

            if (QuestaoId == 0)
                erros.Add("A questão precisa ser informada!");

            return erros.ToArray();
        }
    }
}
