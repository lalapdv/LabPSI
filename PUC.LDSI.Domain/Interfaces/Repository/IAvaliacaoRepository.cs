using PUC.LDSI.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PUC.LDSI.Domain.Interfaces.Repository
{
    public interface IAvaliacaoRepository : IRepository<Avaliacao> 
    {
        Task<List<Avaliacao>> ListarAvaliacoesDoProfessorAsync(int professorId);
    }
}
