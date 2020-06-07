using System;
using System.Collections.Generic;

namespace PUC.LDSI.Domain.Entities
{
    public class Prova : Entity
    {
        public int AlunoId { get; set; }
        public int AvaliacaoId { get; set; }
        public decimal NotaObtida { get; set; }
        public DateTime? DataProva { get; set; }
        public Aluno Aluno { get; set; }
        public Avaliacao Avaliacao { get; set; }

        public List<QuestaoProva> QuestoesProva { get; set; }

        public override string[] Validate()
        {
            var erros = new List<string>();

            if (AvaliacaoId == 0)
                erros.Add("A avaliação precisa ser informada!");

            if (AlunoId == 0)
                erros.Add("O aluno precisa ser informado!");

            if (DataProva == DateTime.MinValue)
                erros.Add("A data da prova precisa ser informada!");

            return erros.ToArray();
        }
    }
}
