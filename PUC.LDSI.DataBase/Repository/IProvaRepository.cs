
using Microsoft.EntityFrameworkCore;
using PUC.LDSI.Domain.Entities;
using PUC.LDSI.Domain.Interfaces.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PUC.LDSI.DataBase.Repository
{
    public class ProvaRepository : Repository<Prova>
    {
        private readonly AppDbContext _context;

        public ProvaRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Prova>> ObterPorAvaliacaoAlunoAsync(int idAvaliacao, int idAluno)
        {
            var query = _context.Prova
                .Where(x => x.AvaliacaoId == idAvaliacao).Where(x => x.AlunoId == idAluno);

            return await query.ToListAsync();
        }
        public override async Task<Prova> ObterAsync(int id)
        {
            var prova = await _context.Prova
                .Include(x => x.QuestoesProva)
                .ThenInclude(y => y.QuestaoAvaliacao)
                .ThenInclude(z => z.Opcoes)
                .ThenInclude(t => t.OpcoesProva)
                .FirstOrDefaultAsync(m => m.Id == id);
            return prova;
        }

    }
}