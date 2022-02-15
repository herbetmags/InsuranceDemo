using Insurance.Common.Models.Bases;
using Insurance.Common.Models.DTOs.Status;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Common.Interfaces.Services
{
    public interface IStatusService
    {
        Task<SearchResponse<ICollection<StatusDTO>>> GetAllStatusAsync(SearchRequest request);
        Task<SearchResponse<StatusDTO>> GetStatusByIdAsync(SearchByIdRequest request);
    }
}
