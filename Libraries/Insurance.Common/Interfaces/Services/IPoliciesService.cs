using Insurance.Common.Models.Bases;
using Insurance.Common.Models.DTOs.Policy;
using Insurance.Common.Models.Policy;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Common.Interfaces.Services
{
    public interface IPoliciesService
    {
        Task<CreateOrUpdateResponse> CreatePolicyAsync(CreatePolicyRequest request);
        Task<SearchResponse<ICollection<PolicyDTO>>> GetAllPoliciesAsync(SearchRequest request);
        Task<SearchResponse<PolicyDTO>> GetPolicyByIdAsync(SearchByIdRequest request);
        Task<CreateOrUpdateResponse> UpdatePolicyAsync(UpdatePolicyRequest request);
        Task<DeleteResponse> DeletePolicyAsync(DeleteRequest request);
    }
}