using System.Collections.Generic;

namespace PUC.LDSI.Domain.Entities
{
    public class Professor : Entity
    {
        public string Nome { get; set; }
        public List<Avaliacao> Avaliacoes { get; set; }

        public override string[] Validate()
        {
            var erros = new List<string>();

            if (string.IsNullOrEmpty(Nome))
                erros.Add("O nome precisa ser informado!");

            return erros.ToArray();
        }
    }
}
