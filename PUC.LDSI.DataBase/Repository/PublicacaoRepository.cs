using Microsoft.EntityFrameworkCore;
using PUC.LDSI.Domain.Entities;
using PUC.LDSI.Domain.Interfaces.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PUC.LDSI.DataBase.Repository
{
    public class PublicacaoRepository : Repository<Publicacao>, IPublicacaoRepository
    {
        private readonly AppDbContext _context;

        public PublicacaoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Publicacao>> ListarPublicacoesDoAlunoAsync(int alunoId)
        {
            var query = _context.Publicacao
                .Include(x => x.Avaliacao).ThenInclude(x => x.Provas)
                .Include(x => x.Turma)
                .Where(x => x.Turma.Alunos.Any(y => y.Id == alunoId));

            return await query.ToListAsync();
        }

        public async Task<List<Publicacao>> ListarPublicacoesDoProfessorAsync(int professorId)
        {
            var query = _context.Publicacao
                .Include(x => x.Avaliacao)
                .Include(x => x.Turma)
                .Where(x => x.Avaliacao.ProfessorId == professorId);

            return await query.ToListAsync();
        }

        public override async Task<Publicacao> ObterAsync(int id)
        {
            var result = await _context.Publicacao
                .Include(x => x.Avaliacao).ThenInclude(x => x.Provas)
                .Include(x => x.Turma)
                .FirstOrDefaultAsync(m => m.Id == id);

            return result;
        }
    }
}
