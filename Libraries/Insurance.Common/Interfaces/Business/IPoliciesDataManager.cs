using Insurance.Common.Models.Bases;
using Insurance.Common.Models.DTOs.Policy;
using Insurance.Common.Models.Policy;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Common.Interfaces.Business
{
    public interface IPoliciesDataManager
    {
        Task<ProcessResult<Guid>> CreatePolicyAsync(CreatePolicyRequest request);
        Task<ProcessResult<SearchResponse<ICollection<PolicyDTO>>>> GetAllPoliciesAsync(SearchRequest request);
        Task<ProcessResult<PolicyDTO>> GetPolicyByIdAsync(SearchByIdRequest request);
        Task<ProcessResult<Guid>> UpdatePolicyAsync(UpdatePolicyRequest request);
        Task<ProcessResult<int>> DeletePolicyAsync(DeleteRequest request);
        Task<ProcessResult<PolicyDTO>> GetPolicyByNameAsync(string name);
    }
}