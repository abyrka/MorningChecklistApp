using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace MorningChecklist.Infrastructure.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetById(int id);

        IEnumerable<TEntity> GetAll();

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Remove(TEntity entity);

        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression);

        IIncludableQueryable<TEntity, TProperty> Include<TProperty>(Expression<Func<TEntity, TProperty>> entities);
    }
}
