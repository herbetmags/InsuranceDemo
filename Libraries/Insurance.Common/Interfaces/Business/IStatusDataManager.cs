using Insurance.Common.Models.Bases;
using Insurance.Common.Models.DTOs.Status;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Common.Interfaces.Business
{
    public interface IStatusDataManager
    {
        Task<ProcessResult<SearchResponse<ICollection<StatusDTO>>>> GetAllStatusAsync(SearchRequest request);
        Task<ProcessResult<StatusDTO>> GetStatusByIdAsync(SearchByIdRequest request);
    }
}