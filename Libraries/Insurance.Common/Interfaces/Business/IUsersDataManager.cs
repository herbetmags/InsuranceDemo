using Insurance.Common.Models.Bases;
using Insurance.Common.Models.DTOs.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Common.Interfaces.Business
{
    public interface IUsersDataManager
    {
        Task<ProcessResult<Guid>> CreateUserAsync(CreateUserRequest request);
        Task<ProcessResult<SearchResponse<ICollection<UserDTO>>>> GetAllUsersAsync(SearchRequest request);
        Task<ProcessResult<UserDTO>> GetUserByIdAsync(SearchByIdRequest request);
        Task<ProcessResult<Guid>> UpdateUserAsync(UpdateUserRequest request);
        Task<ProcessResult<int>> DeleteUserAsync(DeleteRequest request);

        Task<ProcessResult<UserDTO>> GetUserByUsernameAsync(string username);
        Task<ProcessResult<UserDTO>> GetUserByUserCredentialsAsync(string username, string password);
    }
}
