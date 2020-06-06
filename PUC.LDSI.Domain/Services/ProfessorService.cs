using PUC.LDSI.Domain.Entities;
using PUC.LDSI.Domain.Exception;
using PUC.LDSI.Domain.Interfaces.Repository;
using PUC.LDSI.Domain.Interfaces.Services;
using System.Threading.Tasks;

namespace PUC.LDSI.Domain.Services
{
    public class ProfessorService : IProfessorService
    {
        private readonly IProfessorRepository _professorRepository;

        public ProfessorService(IProfessorRepository professorRepository)
        {
            _professorRepository = professorRepository;
        }

        public async Task<int> IncluirProfessorAsync(string nome)
        {
            var professor = new Professor() { Nome = nome };

            var erros = professor.Validate();

            if (erros.Length == 0)
            {
                await _professorRepository.AdicionarAsync(professor);

                _professorRepository.SaveChanges();

                return professor.Id;
            }
            else throw new DomainException(erros);
        }
    }
}
