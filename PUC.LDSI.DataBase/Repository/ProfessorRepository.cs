using PUC.LDSI.Domain.Entities;
using PUC.LDSI.Domain.Interfaces.Repository;

namespace PUC.LDSI.DataBase.Repository
{
    public class ProfessorRepository : Repository<Professor>, IProfessorRepository
    {
        public ProfessorRepository(AppDbContext context) : base(context) { }
    }
}