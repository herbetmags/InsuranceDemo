using Insurance.Common.Models.Bases;
using Insurance.Common.Models.DTOs.Role;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Common.Interfaces.Services
{
    public interface IRolesService
    {
        Task<CreateOrUpdateResponse> CreateRoleAsync(CreateRoleRequest request);
        Task<SearchResponse<ICollection<RoleDTO>>> GetAllRolesAsync(SearchRequest request);
        Task<SearchResponse<RoleDTO>> GetRoleByIdAsync(SearchByIdRequest request);
        Task<CreateOrUpdateResponse> UpdateRoleAsync(UpdateRoleRequest request);
        Task<DeleteResponse> DeleteRoleAsync(DeleteRequest request);
    }
}