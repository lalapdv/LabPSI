using System.Threading.Tasks;

namespace PUC.LDSI.Domain.Interfaces.Services
{
    public interface IProfessorService
    {
        Task<int> IncluirProfessorAsync(string nome);
    }
}
