using PUC.LDSI.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PUC.LDSI.Domain.Interfaces.Repository
{
    public interface IPublicacaoRepository : IRepository<Publicacao> 
    {
        Task<List<Publicacao>> ListarPublicacoesDoProfessorAsync(int professorId);
        Task<List<Publicacao>> ListarPublicacoesDoAlunoAsync(int alunoId);
    }
}
