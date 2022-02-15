using Insurance.Common.Models.Bases;
using Insurance.Common.Models.DTOs.User;
using System.Threading.Tasks;

namespace Insurance.Common.Interfaces.Services
{
    public interface IAccountsService
    {
        Task<CreateOrUpdateResponse> RegisterUserAsync(CreateUserRequest request);
        Task<ResponseBase> SignInUserAsync(UserSessionRequest request);
        Task<ResponseBase> SignOutUserAsync(UserSessionRequest request);
    }
}