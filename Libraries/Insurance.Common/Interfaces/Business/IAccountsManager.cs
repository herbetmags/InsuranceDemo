using Insurance.Common.Models.Bases;
using Insurance.Common.Models.DTOs.User;
using System;
using System.Threading.Tasks;

namespace Insurance.Common.Interfaces.Business
{
    public  interface IAccountsManager
    {
        Task<ProcessResult<Guid>> RegisterUserAsync(CreateUserRequest request);
        Task<ProcessResult<bool>> SignInUserAsync(UserSessionRequest request);
        Task<ProcessResult<bool>> SignOutUserAsync(UserSessionRequest request);
    }
}