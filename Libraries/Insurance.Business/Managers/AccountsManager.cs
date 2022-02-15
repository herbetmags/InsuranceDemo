using Insurance.Common.Interfaces.Business;
using Insurance.Common.Interfaces.Repositories;
using Insurance.Common.Models.Bases;
using Insurance.Common.Models.DTOs.User;
using Insurance.Common.Models.Enums;
using Insurance.Data.Context;
using Insurance.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Insurance.Business.Managers
{
    public class AccountsManager : DataManagerBase<InsuranceDbContext>, IAccountsManager
    {
        private readonly IUsersDataManager _usersDataManager;
        public AccountsManager(IUsersDataManager usersDataManager,
            IUnitOfWork<InsuranceDbContext> unitofWork)
            : base (unitofWork)
        {
            _usersDataManager = usersDataManager;
        }

        public async Task<ProcessResult<Guid>> RegisterUserAsync(CreateUserRequest request)
        {
            var processResult = new ProcessResult<Guid>(string.Empty);
            try
            {
                processResult = await _usersDataManager.CreateUserAsync(request);
            }
            catch (Exception ex)
            {
                processResult = new ProcessResult<Guid>(ex.Message);
            }
            return processResult;
        }

        public async Task<ProcessResult<bool>> SignInUserAsync(UserSessionRequest request)
        {
            await Task.CompletedTask;
            throw new NotImplementedException();
        }

        public async Task<ProcessResult<bool>> SignOutUserAsync(UserSessionRequest request)
        {
            await Task.CompletedTask;
            throw new NotImplementedException();
        }
    }
}
