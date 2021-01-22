using Microsoft.EntityFrameworkCore;
using PUC.LDSI.Domain.Entities;
using PUC.LDSI.Domain.Interfaces.Repository;
using System.Linq;
using System.Threading.Tasks;

namespace PUC.LDSI.DataBase.Repository
{
    public class IQuestaoProvaRepository : Repository<QuestaoProva>
    {
        private readonly AppDbContext _context;

        public IQuestaoProvaRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<QuestaoProva> ObterAsync(int id)
        {
            var questaoProva = await _context.QuestaoProva
                .Include(a => a.Prova)
                .Include(q => q.QuestaoId)
                .Include(o => o.QuestaoAvaliacao)
                .Where(x => x.Id == id).FirstOrDefaultAsync();

            return questaoProva;
        }
    }
}