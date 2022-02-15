using Insurance.Common.Interfaces.Business;
using Insurance.Common.Interfaces.Repositories;
using Insurance.Common.Models.Bases;
using Insurance.Common.Models.DTOs.Status;
using Insurance.Data.Context;
using Insurance.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Insurance.Business.Managers
{
    public class StatusDataManager : DataManagerBase<InsuranceDbContext>, IStatusDataManager
    {
        private readonly IRepository<Status> _statusRepository;
        public StatusDataManager(IUnitOfWork<InsuranceDbContext> unitOfWork)
            : base (unitOfWork)
        {
            _statusRepository = UnitOfWork.GetRepository<Status>();
        }

        public async Task<ProcessResult<SearchResponse<ICollection<StatusDTO>>>> GetAllStatusAsync(SearchRequest request)
        {
            var processResult = new ProcessResult<SearchResponse<ICollection<StatusDTO>>>(string.Empty);
            try
            {
                using (var conn = UnitOfWork.CreateOpenConnection())
                {
                    var result = new SearchResponse<ICollection<StatusDTO>>();
                    var entities = await Task.FromResult(_statusRepository.GetAll(false));
                    List<StatusDTO> dtos = null;
                    if (entities.Any())
                    {
                        dtos = entities
                                .Skip(request.PageSize * (request.PageIndex - 1))
                                .Take(request.PageSize)
                                .Select(entity => new StatusDTO
                                {
                                    Code = entity.Code,
                                    Id = entity.Id,
                                    Name = entity.Name
                                })
                                .ToList();
                        result.TotalRecords = entities.Count();
                        result.Records = dtos;
                    }
                    processResult = new ProcessResult<SearchResponse<ICollection<StatusDTO>>>(result);
                }
            }
            catch (Exception ex)
            {
                processResult = new ProcessResult<SearchResponse<ICollection<StatusDTO>>>(ex.Message);
            }
            return processResult;
        }

        public async Task<ProcessResult<StatusDTO>> GetStatusByIdAsync(SearchByIdRequest request)
        {
            var processResult = new ProcessResult<StatusDTO>(string.Empty);
            try
            {
                using (var conn = UnitOfWork.CreateOpenConnection())
                {
                    var entity = await Task.FromResult(_statusRepository.GetById(request.Id));
                    StatusDTO dto = null;
                    if (entity != null)
                    {
                        dto = new StatusDTO
                        {
                            Code = entity.Code,
                            Id = entity.Id,
                            Name = entity.Name
                        };
                    }
                    processResult = new ProcessResult<StatusDTO>(dto);
                }
            }
            catch (Exception ex)
            {
                processResult = new ProcessResult<StatusDTO>(ex.Message);
            }
            return processResult;
        }
    }
}
