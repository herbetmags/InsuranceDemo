using Insurance.Common.Interfaces.Business;
using Insurance.Common.Interfaces.Services;
using Insurance.Common.Models.Bases;
using Insurance.Common.Models.DTOs.UserRole;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Services
{
    public class UserRolesService : IUserRolesService
    {
        private readonly IUserRolesDataManager _userRolesDataManager;
        public UserRolesService(IUserRolesDataManager userRolesDataManager)
        {
            _userRolesDataManager = userRolesDataManager;
        }

        public async Task<CreateOrUpdateResponse> CreateUserRoleAsync(CreateUserRoleRequest request)
        {
            var response = new CreateOrUpdateResponse();
            try
            {
                var result = await _userRolesDataManager.CreateUserRoleAsync(request);
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

        public async Task<SearchResponse<ICollection<UserRoleDTO>>> GetAllUserRoleesAsync(SearchRequest request)
        {
            var response = new SearchResponse<ICollection<UserRoleDTO>>();
            try
            {
                var result = await _userRolesDataManager.GetAllUserRolesAsync(request);
                if (result.IsSuccess)
                {
                    response.Records = result.ResultObject;
                    response.TotalRecords = result.ResultObject?.Count ?? 0;
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

        public async Task<SearchResponse<UserRoleDTO>> GetUserRoleByIdAsync(SearchByIdRequest request)
        {
            var response = new SearchResponse<UserRoleDTO>();
            try
            {
                var result = await _userRolesDataManager.GetUserRoleByIdAsync(request);
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

        public async Task<CreateOrUpdateResponse> UpdateUserRoleAsync(UpdateUserRoleRequest request)
        {
            var response = new CreateOrUpdateResponse();
            try
            {
                var result = await _userRolesDataManager.UpdateUserRoleAsync(request);
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

        public async Task<DeleteResponse> DeleteUserRoleAsync(DeleteRequest request)
        {
            var response = new DeleteResponse();
            try
            {
                var result = await _userRolesDataManager.DeleteUserRoleAsync(request);
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
