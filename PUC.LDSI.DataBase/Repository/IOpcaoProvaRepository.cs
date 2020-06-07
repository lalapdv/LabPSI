using Microsoft.EntityFrameworkCore;
using PUC.LDSI.Domain.Entities;
using PUC.LDSI.Domain.Interfaces.Repository;
using System.Linq;
using System.Threading.Tasks;

namespace PUC.LDSI.DataBase.Repository
{
    public class IOpcaoProvaRepository : Repository<OpcaoProva>
    {
        private readonly AppDbContext _context;

        public IOpcaoProvaRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<OpcaoProva> ObterAsync(int id)
        {
            var OpcaoProva = await _context.OpcaoProva
                .Include(a => a.OpcaoAvaliacao)
                .Include(a => a.OpcaoAvaliacaoId)
                .Where(x => x.Id == id).FirstOrDefaultAsync();

            return OpcaoProva;
        }
    }
}
