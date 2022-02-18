using Insurance.Common.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;

namespace Insurance.Data.Repositories
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext>, IDisposable
        where TContext : DbContext, new()
    {
        private bool _isDisposed;
        private readonly TContext _context;
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private readonly IDictionary<string, object> _repositories;
        
        public UnitOfWork()
        {
            _context = new TContext();
            _repositories = new Dictionary<string,  object>();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                _isDisposed = true;
            }
        }

        //// TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        //~UnitOfWork()
        //{
        //    // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //    Dispose(disposing: false);
        //}

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public TContext Context
        {
            get { return _context; }
        }

        public IDbConnection CreateOpenConnection()
        {
            _connection = _context.Database.GetDbConnection();
            if (_connection != null && _connection.State == ConnectionState.Closed)
            {
                _connection.ConnectionString = _context.Database.GetConnectionString();
                _connection.Open();
            }
            return _connection;
        }

        public IDbTransaction BeginTransaction()
        {
            _transaction = _connection?.BeginTransaction();
            return _transaction;
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            var type = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<TContext, TEntity>);
                var repositoryInstance = Activator.CreateInstance(repositoryType, _context);
                _repositories.Add(type, repositoryInstance);
            }
            return (Repository<TContext, TEntity>)_repositories[type];
        }

        public void Commit()
        {
            _transaction?.Commit();
            Save();
            _transaction.Dispose();
        }

        public void Rollback()
        {
            _transaction?.Rollback();
            _transaction?.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}