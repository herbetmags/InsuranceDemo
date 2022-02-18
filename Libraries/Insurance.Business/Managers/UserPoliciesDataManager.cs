using Insurance.Common.Interfaces.Business;
using Insurance.Common.Interfaces.Repositories;
using Insurance.Common.Models.Bases;
using Insurance.Common.Models.DTOs.UserPolicy;
using Insurance.Common.Models.Enums;
using Insurance.Data.Context;
using Insurance.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Insurance.Business.Managers
{
    public class UserPoliciesDataManager : DataManagerBase<InsuranceDbContext>, IUserPoliciesDataManager
    {
        private readonly IRepository<UserPolicy> _userPoliciesRepository;
        private readonly IRepository<Status> _statusRepository;
        public UserPoliciesDataManager(IUnitOfWork<InsuranceDbContext> unitOfWork)
            : base(unitOfWork)
        {
            _userPoliciesRepository = UnitOfWork.GetRepository<UserPolicy>();
            _statusRepository = UnitOfWork.GetRepository<Status>();
        }

        public async Task<ProcessResult<Guid>> CreateUserPolicyAsync(CreateUserPolicyRequest request)
        {
            var processResult = new ProcessResult<Guid>(string.Empty);
            try
            {
                using (var conn = UnitOfWork.CreateOpenConnection())
                {
                    var entity = new UserPolicy
                    {
                        Id = request.Id.Equals(Guid.Empty) ? Guid.NewGuid() : request.Id,
                        PolicyId = request.PolicyId,
                        StatusId = request.StatusId,
                        UserId = request.UserId
                    };
                    using (var trans = UnitOfWork.BeginTransaction())
                    {
                        await Task.FromResult(_userPoliciesRepository.Insert(entity));
                        UnitOfWork.Commit();
                        processResult = new ProcessResult<Guid>(entity.Id);
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

        public async Task<ProcessResult<SearchResponse<ICollection<UserPolicyDTO>>>> GetAllUserPoliciesAsync(SearchRequest request)
        {
            var processResult = new ProcessResult<SearchResponse<ICollection<UserPolicyDTO>>>(string.Empty);
            try
            {
                using (var conn = UnitOfWork.CreateOpenConnection())
                {
                    var result = new SearchResponse<ICollection<UserPolicyDTO>>();
                    var entities = await Task.FromResult(_userPoliciesRepository.GetAll(false));
                    List<UserPolicyDTO> dtos = null;
                    if (entities.Any())
                    {
                        dtos = entities
                                .Skip(request.PageSize * (request.PageIndex - 1))
                                .Take(request.PageSize)
                                .Select(entity => new UserPolicyDTO
                                {
                                    Id = entity.Id,
                                    PolicyId = entity.PolicyId,
                                    StatusId = entity.StatusId,
                                    UserId = entity.UserId
                                })
                                .ToList();
                        result.TotalRecords = entities.Count();
                        result.Records = dtos;
                    }
                    processResult = new ProcessResult<SearchResponse<ICollection<UserPolicyDTO>>>(result);
                }
            }
            catch (Exception ex)
            {
                processResult = new ProcessResult<SearchResponse<ICollection<UserPolicyDTO>>>(ex.Message);
            }
            return processResult;
        }

        public async Task<ProcessResult<UserPolicyDTO>> GetUserPolicyByIdAsync(SearchByIdRequest request)
        {
            var processResult = new ProcessResult<UserPolicyDTO>(string.Empty);
            try
            {
                using (var conn = UnitOfWork.CreateOpenConnection())
                {
                    var entity = await Task.FromResult(_userPoliciesRepository.GetById(request.Id));
                    UserPolicyDTO dto = null;
                    if (entity != null)
                    {
                        dto = new UserPolicyDTO
                        {
                            Id = entity.Id,
                            PolicyId = entity.PolicyId,
                            StatusId = entity.StatusId,
                            UserId = entity.UserId
                        };
                    }
                    processResult = new ProcessResult<UserPolicyDTO>(dto);
                }
            }
            catch (Exception ex)
            {
                processResult = new ProcessResult<UserPolicyDTO>(ex.Message);
            }
            return processResult;
        }

        public async Task<ProcessResult<Guid>> UpdateUserPolicyAsync(UpdateUserPolicyRequest request)
        {
            var processResult = new ProcessResult<Guid>(string.Empty);
            try
            {
                using (var conn = UnitOfWork.CreateOpenConnection())
                {
                    var entity = await Task.FromResult(_userPoliciesRepository.GetById(request.Id));
                    if (entity != null)
                    {
                        entity.PolicyId = request.PolicyId;
                        entity.StatusId = request.StatusId;
                        entity.UserId = request.UserId;

                        using (var trans = UnitOfWork.BeginTransaction())
                        {
                            await Task.FromResult(_userPoliciesRepository.Update(entity));
                            UnitOfWork.Commit();
                            processResult = new ProcessResult<Guid>(entity.Id);
                        }
                    }
                    else
                    {
                        throw new Exception("The user policy you're trying to update no longer exists.");
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

        public async Task<ProcessResult<int>> DeleteUserPolicyAsync(DeleteRequest request)
        {
            var processResult = new ProcessResult<int>(string.Empty);
            try
            {
                using (var conn = UnitOfWork.CreateOpenConnection())
                {
                    var statuses = await Task.FromResult(_statusRepository.GetAll(false));
                    var status = statuses.First(entity => entity.Code == nameof(StatusEnum.TERMINATED));
                    var entity = await Task.FromResult(_userPoliciesRepository.GetById(request.Id));
                    if (entity != null)
                    {
                        entity.StatusId = status.Id;
                        using (var trans = UnitOfWork.BeginTransaction())
                        {
                            await Task.FromResult(_userPoliciesRepository.Update(entity));
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
    }
}