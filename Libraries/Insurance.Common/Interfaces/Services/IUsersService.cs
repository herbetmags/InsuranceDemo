using Insurance.Common.Models.Bases;
using Insurance.Common.Models.DTOs.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Common.Interfaces.Services
{
    public interface IUsersService
    {
        Task<CreateOrUpdateResponse> CreateUserAsync(CreateUserRequest request);
        Task<SearchResponse<ICollection<UserDTO>>> GetAllUsersAsync(SearchRequest request);
        Task<SearchResponse<UserDTO>> GetUserByIdAsync(SearchByIdRequest request);
        Task<CreateOrUpdateResponse> UpdateUserAsync(UpdateUserRequest request);
        Task<DeleteResponse> DeleteUserAsync(DeleteRequest request);
    }
}