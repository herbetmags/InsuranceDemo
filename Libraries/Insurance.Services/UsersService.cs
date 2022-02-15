using Insurance.Common.Interfaces.Business;
using Insurance.Common.Interfaces.Services;
using Insurance.Common.Models.Bases;
using Insurance.Common.Models.DTOs.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersDataManager _usersDataManager;

        public UsersService(IUsersDataManager usersDataManager)
        {
            _usersDataManager = usersDataManager;
        }

        public async Task<CreateOrUpdateResponse> CreateUserAsync(CreateUserRequest request)
        {
            var response = new CreateOrUpdateResponse();
            try
            {
                var result = await _usersDataManager.CreateUserAsync(request);
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

        public async Task<SearchResponse<ICollection<UserDTO>>> GetAllUsersAsync(SearchRequest request)
        {
            var response = new SearchResponse<ICollection<UserDTO>>();
            try
            {
                var result = await _usersDataManager.GetAllUsersAsync(request);
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

        public async Task<SearchResponse<UserDTO>> GetUserByIdAsync(SearchByIdRequest request)
        {
            var response = new SearchResponse<UserDTO>();
            try
            {
                var result = await _usersDataManager.GetUserByIdAsync(request);
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

        public async Task<CreateOrUpdateResponse> UpdateUserAsync(UpdateUserRequest request)
        {
            var response = new CreateOrUpdateResponse();
            try
            {
                var result = await _usersDataManager.UpdateUserAsync(request);
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

        public async Task<DeleteResponse> DeleteUserAsync(DeleteRequest request)
        {
            var response = new DeleteResponse();
            try
            {
                var result = await _usersDataManager.DeleteUserAsync(request);
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
