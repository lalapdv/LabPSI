using System.Collections.Generic;

namespace PUC.LDSI.Domain.Entities
{
    public class Avaliacao : Entity
    {
        public int ProfessorId { get; set; }
        public string Disciplina { get; set; }
        public string Materia { get; set; }
        public string Descricao { get; set; }
        public Professor Professor { get; set; }
        public List<QuestaoAvaliacao> Questoes { get; set; }
        public List<Prova> Provas { get; set; }
        public List<Publicacao> Publicacoes { get; set; }

        public override string[] Validate()
        {
            var erros = new List<string>();

            if (string.IsNullOrEmpty(Disciplina))
                erros.Add("A disciplina precisa ser informada!");

            if (string.IsNullOrEmpty(Materia))
                erros.Add("A matéria precisa ser informada!");

            if (string.IsNullOrEmpty(Descricao))
                erros.Add("A descrição da avaliacao precisa ser informada!");

            if (ProfessorId == 0)
                erros.Add("O professor precisa ser informado!");

            return erros.ToArray();
        }
    }
}
