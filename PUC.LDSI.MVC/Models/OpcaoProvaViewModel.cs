using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PUC.LDSI.MVC.Models
{
    public class OpcaoProvaViewModel
    {
        public int Id { get; set; }
        public bool Resposta { get; set; }
        public string Descricao { get; set; }

    }
}
