using Insurance.Common.Interfaces.Business;
using Insurance.Common.Interfaces.Services;
using Insurance.Common.Models.Bases;
using Insurance.Common.Models.DTOs.User;
using System;
using System.Threading.Tasks;

namespace Insurance.Services
{
    public class AccountsService : IAccountsService
    {
        private readonly IAccountsManager _accountsManager;

        public AccountsService(IAccountsManager accountsManager)
        {
            _accountsManager = accountsManager;
        }

        public async Task<CreateOrUpdateResponse> RegisterUserAsync(CreateUserRequest request)
        {
            var response = new CreateOrUpdateResponse();
            try
            {
                var result = await _accountsManager.RegisterUserAsync(request);
                if (result.IsSuccess)
                {
                    response.Id = result.ResultObject;
                }
                else
                {
                    response.ErrorMessage = result.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public async Task<ResponseBase> SignInUserAsync(UserSessionRequest request)
        {
            await Task.CompletedTask;
            throw new NotImplementedException();
        }

        public async Task<ResponseBase> SignOutUserAsync(UserSessionRequest request)
        {
            await Task.CompletedTask;
            throw new NotImplementedException();
        }
    }
}