using Insurance.Common.Interfaces.Business;
using Insurance.Common.Interfaces.Services;
using Insurance.Common.Models.Bases;
using Insurance.Common.Models.DTOs.Role;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Services
{
    public class RolesService : IRolesService
    {
        private readonly IRolesDataManager _roleDataManager;
        public RolesService(IRolesDataManager roleDataManager)
        {
            _roleDataManager = roleDataManager;
        }

        public async Task<CreateOrUpdateResponse> CreateRoleAsync(CreateRoleRequest request)
        {
            var response = new CreateOrUpdateResponse();
            try
            {
                var result = await _roleDataManager.CreateRoleAsync(request);
                if (result.IsSuccess)
                {
                    response.Id = result.ResultObject;
                }
                else
                {
                    response.ErrorMessage = result.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public async Task<SearchResponse<ICollection<RoleDTO>>> GetAllRolesAsync(SearchRequest request)
        {
            var response = new SearchResponse<ICollection<RoleDTO>>();
            try
            {
                var result = await _roleDataManager.GetAllRolesAsync(request);
                if (result.IsSuccess)
                {
                    response = result.ResultObject;
                }
                else
                {
                    response.ErrorMessage = result.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public async Task<SearchResponse<RoleDTO>> GetRoleByIdAsync(SearchByIdRequest request)
        {
            var response = new SearchResponse<RoleDTO>();
            try
            {
                var result = await _roleDataManager.GetRoleByIdAsync(request);
                if (result.IsSuccess)
                {
                    response.Records = result.ResultObject;
                    response.TotalRecords = result.ResultObject == null ? 0 : 1;
                }
                else
                {
                    response.ErrorMessage = result.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public async Task<CreateOrUpdateResponse> UpdateRoleAsync(UpdateRoleRequest request)
        {
            var response = new CreateOrUpdateResponse();
            try
            {
                var result = await _roleDataManager.UpdateRoleAsync(request);
                if (result.IsSuccess)
                {
                    response.Id = result.ResultObject;
                }
                else
                {
                    response.ErrorMessage = result.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public async Task<DeleteResponse> DeleteRoleAsync(DeleteRequest request)
        {
            var response = new DeleteResponse();
            try
            {
                var result = await _roleDataManager.DeleteRoleAsync(request);
                if (result.IsSuccess)
                {
                    response.TotalRecords = result.ResultObject;
                }
                else
                {
                    response.ErrorMessage = result.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
            }
            return response;
        }
    }
}
