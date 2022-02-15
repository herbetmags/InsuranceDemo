using Insurance.Common.Models.Bases;
using Insurance.Common.Models.DTOs.Role;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Common.Interfaces.Business
{
    public interface IRolesDataManager
    {
        Task<ProcessResult<Guid>> CreateRoleAsync(CreateRoleRequest request);
        Task<ProcessResult<SearchResponse<ICollection<RoleDTO>>>> GetAllRolesAsync(SearchRequest request);
        Task<ProcessResult<RoleDTO>> GetRoleByIdAsync(SearchByIdRequest request);
        Task<ProcessResult<Guid>> UpdateRoleAsync(UpdateRoleRequest request);
        Task<ProcessResult<int>> DeleteRoleAsync(DeleteRequest request);
        Task<ProcessResult<RoleDTO>> GetRoleByNameAsync(string name);
    }
}