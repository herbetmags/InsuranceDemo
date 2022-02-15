using Insurance.Common.Models.Bases;
using Insurance.Common.Models.DTOs.UserPolicy;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Common.Interfaces.Services
{
    public interface IUserPoliciesService
    {
        Task<CreateOrUpdateResponse> CreateUserPolicyAsync(CreateUserPolicyRequest request);
        Task<SearchResponse<ICollection<UserPolicyDTO>>> GetAllUserPoliciesAsync(SearchRequest request);
        Task<SearchResponse<UserPolicyDTO>> GetUserPolicyByIdAsync(SearchByIdRequest request);
        Task<CreateOrUpdateResponse> UpdateUserPolicyAsync(UpdateUserPolicyRequest request);
        Task<DeleteResponse> DeleteUserPolicyAsync(DeleteRequest request);
    }
}
