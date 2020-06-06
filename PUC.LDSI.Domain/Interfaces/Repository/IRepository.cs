using PUC.LDSI.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PUC.LDSI.Domain.Interfaces.Repository
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task AdicionarAsync(TEntity obj);
        void Modificar(TEntity entity);
        Task<TEntity> ObterAsync(int id);
        IQueryable<TEntity> ObterTodos();
        void Excluir(int id);
        int SaveChanges();
    }
}
