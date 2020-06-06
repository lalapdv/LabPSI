namespace PUC.LDSI.Domain.Exception
{
    public class DomainException : System.Exception
    {
        private const string MENSAGEMPADRAO = "Violação de regra do sistema!";

        private readonly string[] _erros;

        public string[] Erros
        {
            get
            {
                return _erros;
            }
        }

        public DomainException(string[] erros) : base(MENSAGEMPADRAO)
        {
            _erros = erros;
        }

        public DomainException(string erro) : base(MENSAGEMPADRAO) 
        {
            _erros = new string[] { erro };
        }

        public DomainException(string message, string erro) : base(message)
        {
            _erros = new string[] { erro };
        }
    }
}
