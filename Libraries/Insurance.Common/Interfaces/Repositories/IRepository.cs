using System.Linq;

namespace Insurance.Common.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll(bool asTracking);
        TEntity GetById(object id);
        bool Delete(object id);
        bool Insert(TEntity entity);
        bool Update(TEntity entity);
    }
}