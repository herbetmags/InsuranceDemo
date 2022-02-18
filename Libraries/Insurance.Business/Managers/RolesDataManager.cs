using Insurance.Common.Interfaces.Business;
using Insurance.Common.Interfaces.Repositories;
using Insurance.Common.Models.Bases;
using Insurance.Common.Models.DTOs.Role;
using Insurance.Data.Context;
using Insurance.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Insurance.Business.Managers
{
    public class RolesDataManager : DataManagerBase<InsuranceDbContext>, IRolesDataManager
    {
        private readonly IRepository<Role> _rolesRepository;
        public RolesDataManager(IUnitOfWork<InsuranceDbContext> unitOfWork)
            : base(unitOfWork)
        {
            _rolesRepository = UnitOfWork.GetRepository<Role>();
        }

        public async Task<ProcessResult<Guid>> CreateRoleAsync(CreateRoleRequest request)
        {
            var processResult = new ProcessResult<Guid>(string.Empty);
            try
            {
                var result = await GetRoleByNameAsync(request.Name);
                using (var conn = UnitOfWork.CreateOpenConnection())
                {
                    var entity = new Role
                    {
                        CreatedBy = request.CreatedBy,
                        CreatedDate = request.CreatedDate,
                        Description = request.Description,
                        Id = request.Id.Equals(Guid.Empty) ? Guid.NewGuid() : request.Id,
                        IsDeleted = request.IsDeleted,
                        Name = request.Name
                    };
                    var role = result.ResultObject;
                    if (role == null)
                    {
                        using (var trans = UnitOfWork.BeginTransaction())
                        {
                            await Task.FromResult(_rolesRepository.Insert(entity));
                            UnitOfWork.Commit();
                            processResult = new ProcessResult<Guid>(entity.Id);
                        }
                    }
                    else
                    {
                        processResult = new ProcessResult<Guid>("The role name has already been existed.");
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

        public async Task<ProcessResult<SearchResponse<ICollection<RoleDTO>>>> GetAllRolesAsync(SearchRequest request)
        {
            var processResult = new ProcessResult<SearchResponse<ICollection<RoleDTO>>>(string.Empty);
            try
            {
                using (var conn = UnitOfWork.CreateOpenConnection())
                {
                    var result = new SearchResponse<ICollection<RoleDTO>>();
                    var entities = await Task.FromResult(_rolesRepository.GetAll(false));
                    List<RoleDTO> dtos = null;
                    if (entities.Any())
                    {
                        dtos = entities
                                .Skip(request.PageSize * (request.PageIndex - 1))
                                .Take(request.PageSize)
                                .Select(entity => new RoleDTO
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
                    processResult = new ProcessResult<SearchResponse<ICollection<RoleDTO>>>(result);
                }
            }
            catch (Exception ex)
            {
                processResult = new ProcessResult<SearchResponse<ICollection<RoleDTO>>>(ex.Message);
            }
            return processResult;
        }

        public async Task<ProcessResult<RoleDTO>> GetRoleByIdAsync(SearchByIdRequest request)
        {
            var processResult = new ProcessResult<RoleDTO>(string.Empty);
            try
            {
                using (var conn = UnitOfWork.CreateOpenConnection())
                {
                    var entity = await Task.FromResult(_rolesRepository.GetById(request.Id));
                    RoleDTO dto = null;
                    if (entity != null)
                    {
                        dto = new RoleDTO
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
                    processResult = new ProcessResult<RoleDTO>(dto);
                }
            }
            catch (Exception ex)
            {
                processResult = new ProcessResult<RoleDTO>(ex.Message);
            }
            return processResult;
        }

        public async Task<ProcessResult<Guid>> UpdateRoleAsync(UpdateRoleRequest request)
        {
            var processResult = new ProcessResult<Guid>(string.Empty);
            try
            {
                var result = await GetRoleByNameAsync(request.Name);
                using (var conn = UnitOfWork.CreateOpenConnection())
                {
                    var entity = await Task.FromResult(_rolesRepository.GetById(request.Id));
                    if (entity != null)
                    {
                        entity.Description = request.Description;
                        entity.IsDeleted = request.IsDeleted;
                        entity.ModifiedBy = request.ModifiedBy;
                        entity.ModifiedDate = request.ModifiedDate;
                        entity.Name = request.Name;
                        var role = result.ResultObject;
                        if (role != null && role.Id == entity.Id)
                        {
                            using (var trans = UnitOfWork.BeginTransaction())
                            {
                                await Task.FromResult(_rolesRepository.Update(entity));
                                UnitOfWork.Commit();
                                processResult = new ProcessResult<Guid>(entity.Id);
                            }
                        }
                        else
                        {
                            processResult = new ProcessResult<Guid>("The role name has already been existed.");
                        }
                    }
                    else
                    {
                        var createRequest = new CreateRoleRequest
                        {
                            CreatedBy = request.ModifiedBy.Value,
                            CreatedDate = request.ModifiedDate.Value,
                            Description = request.Description,
                            Id = request.Id,
                            IsDeleted = request.IsDeleted,
                            Name = request.Name
                        };
                        return CreateRoleAsync(createRequest).Result;
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

        public async Task<ProcessResult<int>> DeleteRoleAsync(DeleteRequest request)
        {
            var processResult = new ProcessResult<int>(string.Empty);
            try
            {
                using (var conn = UnitOfWork.CreateOpenConnection())
                {
                    using (var trans = UnitOfWork.BeginTransaction())
                    {
                        var entity = await Task.FromResult(_rolesRepository.GetById(request.Id));
                        if (entity != null)
                        {
                            entity.IsDeleted = true;
                            await Task.FromResult(_rolesRepository.Update(entity));
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

        public async Task<ProcessResult<RoleDTO>> GetRoleByNameAsync(string name)
        {
            var processResult = new ProcessResult<RoleDTO>(string.Empty);
            try
            {
                using (var conn = UnitOfWork.CreateOpenConnection())
                {
                    var entities = await Task.FromResult(_rolesRepository.GetAll(false));
                    var entity = entities.FirstOrDefault(entity => entity.Name == name);
                    RoleDTO dto = null;
                    if (entity != null)
                    {
                        dto = new RoleDTO
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
                    processResult = new ProcessResult<RoleDTO>(dto);
                }
            }
            catch (Exception ex)
            {
                processResult = new ProcessResult<RoleDTO>(ex.Message);
            }
            return processResult;
        }
    }
}
