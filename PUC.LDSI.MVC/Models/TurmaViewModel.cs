using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PUC.LDSI.MVC.Models
{
    public class TurmaViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da turma é requerido.")]
        [MaxLength(100, ErrorMessage = "Informe no máximo {0} caracteres.")]
        [DisplayName("Nome da Turma")]
        public string Nome { get; set; }
    }
}
