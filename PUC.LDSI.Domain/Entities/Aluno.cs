using System.Collections.Generic;

namespace PUC.LDSI.Domain.Entities
{
    public class Aluno : Entity
    {
        public int TurmaId { get; set; }
        public string Nome { get; set; }
        public Turma Turma { get; set; }
        public List<Prova> Provas { get; set; }

        public override string[] Validate()
        {
            var erros = new List<string>();

            if (TurmaId == 0)
                erros.Add("A turma precisa ser informada!");

            if (string.IsNullOrEmpty(Nome))
                erros.Add("O nome precisa ser informado!");

            return erros.ToArray();
        }
    }
}
