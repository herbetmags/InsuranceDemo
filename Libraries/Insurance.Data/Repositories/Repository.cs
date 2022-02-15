using Insurance.Common.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Insurance.Data.Repositories
{
    public class Repository<TContext, TEntity> : IRepository<TEntity>, IDisposable
        where TContext : DbContext, new()
        where TEntity : class
    {
        private bool _isDisposed;
        private DbSet<TEntity> _entities;

        public Repository(IUnitOfWork<TContext> unitOfWork)
            : this(unitOfWork.Context)
        {
        }

        public Repository(TContext context)
        {
            _isDisposed = false;
            Context = context;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    Context?.Dispose();
                }

                _isDisposed = true;
            }
        }

        //// TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        //~Repository()
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

        public TContext Context { get; private set; }

        protected virtual DbSet<TEntity> Entities
        {
            get { return _entities ??= Context.Set<TEntity>(); }
        }

        public IQueryable<TEntity> GetAll(bool asTracking)
        {
            return asTracking ? Entities.AsTracking() : Entities.AsNoTracking();
        }

        public TEntity GetById(object id)
        {
            return Entities.Find(id);
        }

        public bool Delete(object id)
        {
            if (Context == null || _isDisposed)
                Context = new TContext();
            var entity = GetById(id);
            if (entity != null)
            Context.Entry(entity).State = EntityState.Deleted;
            return true;
        }

        public bool Insert(TEntity entity)
        {
            if (Context == null || _isDisposed)
                Context = new TContext();
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            Context.Entry(entity).State = EntityState.Added;
            return true;
        }

        public bool Update(TEntity entity)
        {
            if (Context == null || _isDisposed)
                Context = new TContext();
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            Context.Entry(entity).State = EntityState.Modified;
            return true;
        }
    }
}