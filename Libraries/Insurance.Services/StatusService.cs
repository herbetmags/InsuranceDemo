using Insurance.Common.Interfaces.Business;
using Insurance.Common.Interfaces.Services;
using Insurance.Common.Models.Bases;
using Insurance.Common.Models.DTOs.Status;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Services
{
    public class StatusService : IStatusService
    {
        private readonly IStatusDataManager _statusDataManager;

        public StatusService(IStatusDataManager statusDataManager)
        {
            _statusDataManager = statusDataManager;
        }

        public async Task<SearchResponse<ICollection<StatusDTO>>> GetAllStatusAsync(SearchRequest request)
        {
            var response = new SearchResponse<ICollection<StatusDTO>>();
            try
            {
                var result = await _statusDataManager.GetAllStatusAsync(request);
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

        public async Task<SearchResponse<StatusDTO>> GetStatusByIdAsync(SearchByIdRequest request)
        {
            var response = new SearchResponse<StatusDTO>();
            try
            {
                var result = await _statusDataManager.GetStatusByIdAsync(request);
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
    }
}
