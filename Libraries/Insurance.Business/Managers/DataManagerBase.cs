using Insurance.Common.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Insurance.Business.Managers
{
    public class DataManagerBase<TContext> where TContext : DbContext, new()
    {
        protected IUnitOfWork<TContext> UnitOfWork { get; }

        public DataManagerBase(IUnitOfWork<TContext> unitOfWork)
        {
            UnitOfWork = unitOfWork;
            //TODO Logging
        }
    }
}