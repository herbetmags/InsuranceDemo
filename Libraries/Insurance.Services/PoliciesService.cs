using Insurance.Common.Interfaces.Business;
using Insurance.Common.Interfaces.Services;
using Insurance.Common.Models.Bases;
using Insurance.Common.Models.DTOs.Policy;
using Insurance.Common.Models.Policy;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Services
{
    public class PoliciesService : IPoliciesService
    {
        private readonly IPoliciesDataManager _policyDataManager;

        public PoliciesService(IPoliciesDataManager policyDataManager)
        {
            _policyDataManager = policyDataManager;
        }

        public async Task<CreateOrUpdateResponse> CreatePolicyAsync(CreatePolicyRequest request)
        {
            var response = new CreateOrUpdateResponse();
            try
            {
                var result = await _policyDataManager.CreatePolicyAsync(request);
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

        public async Task<SearchResponse<ICollection<PolicyDTO>>> GetAllPoliciesAsync(SearchRequest request)
        {
            var response = new SearchResponse<ICollection<PolicyDTO>>();
            try
            {
                var result = await _policyDataManager.GetAllPoliciesAsync(request);
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

        public async Task<SearchResponse<PolicyDTO>> GetPolicyByIdAsync(SearchByIdRequest request)
        {
            var response = new SearchResponse<PolicyDTO>();
            try
            {
                var result = await _policyDataManager.GetPolicyByIdAsync(request);
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

        public async Task<CreateOrUpdateResponse> UpdatePolicyAsync(UpdatePolicyRequest request)
        {
            var response = new CreateOrUpdateResponse();
            try
            {
                var result = await _policyDataManager.UpdatePolicyAsync(request);
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

        public async Task<DeleteResponse> DeletePolicyAsync(DeleteRequest request)
        {
            var response = new DeleteResponse();
            try
            {
                var result = await _policyDataManager.DeletePolicyAsync(request);
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
