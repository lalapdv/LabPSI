using Microsoft.EntityFrameworkCore;
using PUC.LDSI.Domain.Entities;
using PUC.LDSI.Domain.Interfaces.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PUC.LDSI.DataBase.Repository
{
    public class AvaliacaoRepository : Repository<Avaliacao>, IAvaliacaoRepository
    {
        private readonly AppDbContext _context;

        public AvaliacaoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Avaliacao>> ListarAvaliacoesDoProfessorAsync(int professorId)
        {
            var query = _context.Avaliacao
                .Include(x => x.Professor)
                .Include(x => x.Questoes).ThenInclude(y => y.Opcoes)
                .Where(x => x.Professor.Id == professorId);

            return await query.ToListAsync();
        }

        public override async Task<Avaliacao> ObterAsync(int id)
        {
            var avaliacao = await _context.Avaliacao
                .Include(x => x.Professor)
                .Include(x => x.Questoes).ThenInclude(y => y.Opcoes)
                .Include(x => x.Publicacoes)
                .FirstOrDefaultAsync(m => m.Id == id);

            return avaliacao;
        }
    }
}
