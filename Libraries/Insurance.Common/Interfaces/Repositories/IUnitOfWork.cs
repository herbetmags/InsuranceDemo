using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace Insurance.Common.Interfaces.Repositories
{
    public interface IUnitOfWork<TContext> where TContext : DbContext, new()
    {
        TContext Context { get; }
        IDbConnection CreateOpenConnection();
        IDbTransaction BeginTransaction();
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        void Commit();
        void Rollback();
        void Save();
    }
}