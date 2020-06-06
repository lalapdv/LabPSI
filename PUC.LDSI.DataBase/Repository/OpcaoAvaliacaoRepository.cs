using Microsoft.EntityFrameworkCore;
using PUC.LDSI.Domain.Entities;
using PUC.LDSI.Domain.Interfaces.Repository;
using System.Linq;
using System.Threading.Tasks;

namespace PUC.LDSI.DataBase.Repository
{
    public class OpcaoAvaliacaoRepository : Repository<OpcaoAvaliacao>, IOpcaoAvaliacaoRepository
    {
        private readonly AppDbContext _context;

        public OpcaoAvaliacaoRepository(AppDbContext context) : base(context) 
        {
            _context = context;
        }

        public override async Task<OpcaoAvaliacao> ObterAsync(int id)
        {
            var questao = await _context.OpcaoAvaliacao
                .Include(a => a.OpcoesProva)
                .Include(a => a.Questao).ThenInclude(a => a.Avaliacao)
                .Where(x => x.Id == id).FirstOrDefaultAsync();

            return questao;
        }
    }
}
