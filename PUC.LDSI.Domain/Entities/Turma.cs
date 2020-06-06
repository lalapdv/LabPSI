using System.Collections.Generic;

namespace PUC.LDSI.Domain.Entities
{
    public class Turma : Entity
    {
        public string Nome { get; set; }
        public List<Publicacao> Publicacoes { get; set; }
        public List<Aluno> Alunos { get; set; }

        public override string[] Validate()
        {
            var erros = new List<string>();

            if (string.IsNullOrEmpty(Nome))
                erros.Add("O nome da turma precisa ser informado!");

            return erros.ToArray();
        }
    }
}
