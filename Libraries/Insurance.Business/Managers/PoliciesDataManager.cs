using Insurance.Common.Interfaces.Business;
using Insurance.Common.Interfaces.Repositories;
using Insurance.Common.Models.Bases;
using Insurance.Common.Models.DTOs.Policy;
using Insurance.Common.Models.Policy;
using Insurance.Data.Context;
using Insurance.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Insurance.Business.Managers
{
    public class PoliciesDataManager : DataManagerBase<InsuranceDbContext>, IPoliciesDataManager
    {
        private readonly IRepository<Policy> _policiesRepository;

        public PoliciesDataManager(IUnitOfWork<InsuranceDbContext> unitOfWork)
            : base(unitOfWork)
        {
            _policiesRepository = UnitOfWork.GetRepository<Policy>();
        }

        public async Task<ProcessResult<Guid>> CreatePolicyAsync(CreatePolicyRequest request)
        {
            var processResult = new ProcessResult<Guid>(string.Empty);
            try
            {
                var result = await GetPolicyByNameAsync(request.Name);
                using (var conn = UnitOfWork.CreateOpenConnection())
                {
                    var entity = new Policy
                    {
                        CreatedBy = request.CreatedBy,
                        CreatedDate = request.CreatedDate,
                        Description = request.Description,
                        IsDeleted = request.IsDeleted,
                        Id = request.Id.Equals(Guid.Empty) ? Guid.NewGuid() : request.Id,
                        Name = request.Name
                    };
                    var policy = result.ResultObject;
                    if (policy == null)
                    {
                        using (var trans = UnitOfWork.BeginTransaction())
                        {
                            await Task.FromResult(_policiesRepository.Insert(entity));
                            UnitOfWork.Commit();
                            processResult = new ProcessResult<Guid>(entity.Id);
                        }
                    }
                    else
                    {
                        processResult = new ProcessResult<Guid>("The policy name has already been existed.");
                    }
                }
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                processResult = new ProcessResult<Guid>(ex.Message);
            }
            return processResult;
        }

        public async Task<ProcessResult<SearchResponse<ICollection<PolicyDTO>>>> GetAllPoliciesAsync(SearchRequest request)
        {
            var processResult = new ProcessResult<SearchResponse<ICollection<PolicyDTO>>>(string.Empty);
            try
            {
                using (var conn = UnitOfWork.CreateOpenConnection())
                {
                    var result = new SearchResponse<ICollection<PolicyDTO>>();
                    var entities = await Task.FromResult(_policiesRepository.GetAll(false));
                    List<PolicyDTO> dtos = null;
                    if (entities.Any())
                    {
                        dtos = entities
                                .Skip(request.PageSize * (request.PageIndex - 1))
                                .Take(request.PageSize)
                                .Select(entity => new PolicyDTO
                                {
                                    CreatedBy = entity.CreatedBy,
                                    CreatedDate = entity.CreatedDate,
                                    Description = entity.Description,
                                    Id = entity.Id,
                                    IsDeleted = entity.IsDeleted,
                                    ModifiedBy = entity.ModifiedBy,
                                    ModifiedDate = entity.ModifiedDate,
                                    Name = entity.Name
                                })
                                .ToList();
                        result.TotalRecords = entities.Count();
                        result.Records = dtos;
                    }
                    processResult = new ProcessResult<SearchResponse<ICollection<PolicyDTO>>>(result);
                }
            }
            catch (Exception ex)
            {
                processResult = new ProcessResult<SearchResponse<ICollection<PolicyDTO>>>(ex.Message);
            }
            return processResult;
        }

        public async Task<ProcessResult<PolicyDTO>> GetPolicyByIdAsync(SearchByIdRequest request)
        {
            var processResult = new ProcessResult<PolicyDTO>(string.Empty);
            try
            {
                using (var conn = UnitOfWork.CreateOpenConnection())
                {
                    var entity = await Task.FromResult(_policiesRepository.GetById(request.Id));
                    PolicyDTO dto = null;
                    if (entity != null)
                    {
                        dto = new PolicyDTO
                        {
                            CreatedBy = entity.CreatedBy,
                            CreatedDate = entity.CreatedDate,
                            Description = entity.Description,
                            Id = entity.Id,
                            IsDeleted = entity.IsDeleted,
                            ModifiedBy = entity.ModifiedBy,
                            ModifiedDate = entity.ModifiedDate,
                            Name = entity.Name
                        };
                    }
                    processResult = new ProcessResult<PolicyDTO>(dto);
                }
            }
            catch (Exception ex)
            {
                processResult = new ProcessResult<PolicyDTO>(ex.Message);
            }
            return processResult;
        }

        public async Task<ProcessResult<Guid>> UpdatePolicyAsync(UpdatePolicyRequest request)
        {
            var processResult = new ProcessResult<Guid>(string.Empty);
            try
            {
                using (var conn = UnitOfWork.CreateOpenConnection())
                {
                    var entity = await Task.FromResult(_policiesRepository.GetById(request.Id));
                    if (entity != null)
                    {
                        entity.Description = request.Description;
                        entity.IsDeleted = request.IsDeleted;
                        entity.ModifiedBy = request.ModifiedBy;
                        entity.ModifiedDate = request.ModifiedDate;
                        entity.Name = request.Name;
                        using (var trans = UnitOfWork.BeginTransaction())
                        {
                            await Task.FromResult(_policiesRepository.Update(entity));
                            UnitOfWork.Commit();
                            processResult = new ProcessResult<Guid>(entity.Id);
                        }
                    }
                    else
                    {
                        var createRequest = new CreatePolicyRequest
                        {
                            CreatedBy = request.ModifiedBy.Value,
                            CreatedDate = request.ModifiedDate.Value,
                            Description = request.Description,
                            Id = request.Id,
                            IsDeleted = request.IsDeleted,
                            Name = request.Name
                        };
                        return CreatePolicyAsync(createRequest).Result;
                    }
                }
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                processResult = new ProcessResult<Guid>(ex.Message);
            }
            return processResult;
        }

        public async Task<ProcessResult<int>> DeletePolicyAsync(DeleteRequest request)
        {
            var processResult = new ProcessResult<int>(string.Empty);
            try
            {
                using (var conn = UnitOfWork.CreateOpenConnection())
                {
                    using (var trans = UnitOfWork.BeginTransaction())
                    {
                        var entity = await Task.FromResult(_policiesRepository.GetById(request.Id));
                        if (entity != null)
                        {
                            entity.IsDeleted = true;
                            await Task.FromResult(_policiesRepository.Update(entity));
                            UnitOfWork.Commit();
                            processResult = new ProcessResult<int>(1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                processResult = new ProcessResult<int>(ex.Message);
            }
            return processResult;
        }

        public async Task<ProcessResult<PolicyDTO>> GetPolicyByNameAsync(string name)
        {
            var processResult = new ProcessResult<PolicyDTO>(string.Empty);
            try
            {
                using (var conn = UnitOfWork.CreateOpenConnection())
                {
                    var entities = await Task.FromResult(_policiesRepository.GetAll(false));
                    var entity = entities.FirstOrDefault(entity => entity.Name == name);
                    PolicyDTO dto = null;
                    if (entity != null)
                    {
                        dto = new PolicyDTO
                        {
                            CreatedBy = entity.CreatedBy,
                            CreatedDate = entity.CreatedDate,
                            Description = entity.Description,
                            Id = entity.Id,
                            IsDeleted = entity.IsDeleted,
                            ModifiedBy = entity.ModifiedBy,
                            ModifiedDate = entity.ModifiedDate,
                            Name = entity.Name
                        };
                    }
                    processResult = new ProcessResult<PolicyDTO>(dto);
                }
            }
            catch (Exception ex)
            {
                processResult = new ProcessResult<PolicyDTO>(ex.Message);
            }
            return processResult;
        }
    }
}