using Microsoft.EntityFrameworkCore;
using PUC.LDSI.Domain.Entities;
using PUC.LDSI.Domain.Interfaces.Repository;
using System.Linq;
using System.Threading.Tasks;

namespace PUC.LDSI.DataBase.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected AppDbContext DbContext;
        protected DbSet<TEntity> DbEntity;

        protected Repository(AppDbContext context)
        {
            DbContext = context;

            DbEntity = DbContext.Set<TEntity>();
        }

        public virtual async Task AdicionarAsync(TEntity obj)
        {
            await DbEntity.AddAsync(obj);
        }

        public virtual void Modificar(TEntity obj)
        {
            DbEntity.Update(obj);
        }

        public virtual async Task<TEntity> ObterAsync(int id)
        {
            return await DbEntity.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        }

        public virtual IQueryable<TEntity> ObterTodos()
        {
            return DbEntity.AsNoTracking().AsQueryable();
        }

        public virtual void Excluir(int id)
        {
            var entity = DbEntity.Find(id);

            DbEntity.Remove(entity);
        }

        public virtual int SaveChanges()
        {
            return DbContext.SaveChanges();
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
