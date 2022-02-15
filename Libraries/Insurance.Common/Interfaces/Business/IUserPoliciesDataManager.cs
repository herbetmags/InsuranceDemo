using Insurance.Common.Models.Bases;
using Insurance.Common.Models.DTOs.UserPolicy;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Common.Interfaces.Business
{
    public interface IUserPoliciesDataManager
    {
        Task<ProcessResult<Guid>> CreateUserPolicyAsync(CreateUserPolicyRequest request);
        Task<ProcessResult<SearchResponse<ICollection<UserPolicyDTO>>>> GetAllUserPoliciesAsync(SearchRequest request);
        Task<ProcessResult<UserPolicyDTO>> GetUserPolicyByIdAsync(SearchByIdRequest request);
        Task<ProcessResult<Guid>> UpdateUserPolicyAsync(UpdateUserPolicyRequest request);
        Task<ProcessResult<int>> DeleteUserPolicyAsync(DeleteRequest request);
    }
}
