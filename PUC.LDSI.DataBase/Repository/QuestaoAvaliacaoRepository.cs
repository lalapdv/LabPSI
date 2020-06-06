using Microsoft.EntityFrameworkCore;
using PUC.LDSI.Domain.Entities;
using PUC.LDSI.Domain.Interfaces.Repository;
using System.Linq;
using System.Threading.Tasks;

namespace PUC.LDSI.DataBase.Repository
{
    public class QuestaoAvaliacaoRepository : Repository<QuestaoAvaliacao>, IQuestaoAvaliacaoRepository
    {
        private readonly AppDbContext _context;

        public QuestaoAvaliacaoRepository(AppDbContext context) : base(context) 
        {
            _context = context;
        }

        public override async Task<QuestaoAvaliacao> ObterAsync(int id) 
        {
            var questao = await _context.QuestaoAvaliacao
                .Include(a => a.Avaliacao)
                .Include(q => q.QuestoesProva)
                .Include(o => o.Opcoes)
                .Where(x => x.Id == id).FirstOrDefaultAsync();

            return questao;
        }
    }
}
