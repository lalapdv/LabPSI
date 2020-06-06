using System;
using System.Collections.Generic;

namespace PUC.LDSI.Domain.Entities
{
    public class Publicacao : Entity
    {
        public int AvaliacaoId { get; set; }
        public int TurmaId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public int ValorProva { get; set; }
        public Avaliacao Avaliacao { get; set; }
        public Turma Turma { get; set; }

        public override string[] Validate()
        {
            var erros = new List<string>();

            if (AvaliacaoId == 0)
                erros.Add("A avaliação precisa ser informada!");

            if (TurmaId == 0)
                erros.Add("A turma precisa ser informada!");

            if (DataInicio == DateTime.MinValue)
                erros.Add("A data de início precisa ser informada!");

            if (DataFim == DateTime.MinValue)
                erros.Add("A data de término precisa ser informada!");

            if (DataFim < DataInicio)
                erros.Add("A data de fim não pode ser anterior à data de início!");

            if (ValorProva <= 0)
                erros.Add("A prova precisa ter um valor!");

            return erros.ToArray();
        }
    }
}
