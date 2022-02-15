using Insurance.Common.Interfaces.Business;
using Insurance.Common.Interfaces.Services;
using Insurance.Common.Models.Bases;
using Insurance.Common.Models.DTOs.UserPolicy;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Services
{
    public class UserPoliciesService : IUserPoliciesService
    {
        private readonly IUserPoliciesDataManager _userPoliciesDataManager;
        public UserPoliciesService(IUserPoliciesDataManager userPoliciesDataManager)
        {
            _userPoliciesDataManager = userPoliciesDataManager;
        }

        public async Task<CreateOrUpdateResponse> CreateUserPolicyAsync(CreateUserPolicyRequest request)
        {
            var response = new CreateOrUpdateResponse();
            try
            {
                var result = await _userPoliciesDataManager.CreateUserPolicyAsync(request);
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

        public async Task<SearchResponse<ICollection<UserPolicyDTO>>> GetAllUserPoliciesAsync(SearchRequest request)
        {
            var response = new SearchResponse<ICollection<UserPolicyDTO>>();
            try
            {
                var result = await _userPoliciesDataManager.GetAllUserPoliciesAsync(request);
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

        public async Task<SearchResponse<UserPolicyDTO>> GetUserPolicyByIdAsync(SearchByIdRequest request)
        {
            var response = new SearchResponse<UserPolicyDTO>();
            try
            {
                var result = await _userPoliciesDataManager.GetUserPolicyByIdAsync(request);
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

        public async Task<CreateOrUpdateResponse> UpdateUserPolicyAsync(UpdateUserPolicyRequest request)
        {
            var response = new CreateOrUpdateResponse();
            try
            {
                var result = await _userPoliciesDataManager.UpdateUserPolicyAsync(request);
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

        public async Task<DeleteResponse> DeleteUserPolicyAsync(DeleteRequest request)
        {
            var response = new DeleteResponse();
            try
            {
                var result = await _userPoliciesDataManager.DeleteUserPolicyAsync(request);
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