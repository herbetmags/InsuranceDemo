using Insurance.Common.Interfaces.Business;
using Insurance.Common.Interfaces.Repositories;
using Insurance.Common.Models.Bases;
using Insurance.Common.Models.DTOs.User;
using Insurance.Common.Models.Enums;
using Insurance.Data.Context;
using Insurance.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Insurance.Business.Managers
{
    public class UsersDataManager : DataManagerBase<InsuranceDbContext>, IUsersDataManager
    {
        private readonly IRepository<User> _usersRepository;
        private readonly IRepository<Role> _rolesRepository;
        private readonly IRepository<Status> _statusRepository;
        public UsersDataManager(IUnitOfWork<InsuranceDbContext> unitOfWork)
            : base (unitOfWork)
        {
            _usersRepository = UnitOfWork.GetRepository<User>();
            _rolesRepository = UnitOfWork.GetRepository<Role>();
            _statusRepository = UnitOfWork.GetRepository<Status>();
        }

        public async Task<ProcessResult<Guid>> CreateUserAsync(CreateUserRequest request)
        {
            var processResult = new ProcessResult<Guid>(string.Empty);
            try
            {
                using (var conn = UnitOfWork.CreateOpenConnection())
                {
                    using (var trans = UnitOfWork.BeginTransaction())
                    {
                        var role = await Task.FromResult(_rolesRepository.GetById(request.RoleId));
                        var roles = await Task.FromResult(_rolesRepository.GetAll(false));
                        var roleId = roles.First(entity => entity.Name == nameof(RolesEnum.CLIENT)).Id;
                        var statuses = await Task.FromResult(_statusRepository.GetAll(false));
                        var statusId = statuses.First(status => status.Code == nameof(StatusEnum.PENDING)).Id;
                        var entity = new User
                        {
                            CreatedBy = request.CreatedBy,
                            CreatedDate = request.CreatedDate,
                            Email = request.Email,
                            Firstname = request.Firstname,
                            Id = request.Id.Equals(Guid.Empty) ? Guid.NewGuid() : request.Id,
                            Lastname = request.Lastname,
                            Password = request.Password,
                            RoleId = role != null ? role.Id : roleId,
                            StatusId = request.StatusId.Equals(Guid.Empty) ? statusId : request.StatusId,
                            Username = request.Username
                        };
                        var result = await GetUserByUsernameAsync(request.Username);
                        var user = result.ResultObject;
                        if (user == null)
                        {
                            await Task.FromResult(_usersRepository.Insert(entity));
                            UnitOfWork.Commit();
                            processResult = new ProcessResult<Guid>(entity.Id);
                        }
                        else
                        {
                            processResult = new ProcessResult<Guid>("The username has already been taken.");
                        }
                    }
                    UnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                processResult = new ProcessResult<Guid>(ex.Message);
            }
            return processResult;
        }

        public async Task<ProcessResult<SearchResponse<ICollection<UserDTO>>>> GetAllUsersAsync(SearchRequest request)
        {
            var processResult = new ProcessResult<SearchResponse<ICollection<UserDTO>>>(string.Empty);
            try
            {
                using (var conn = UnitOfWork.CreateOpenConnection())
                {
                    var result = new SearchResponse<ICollection<UserDTO>>();
                    var entities = await Task
                        .FromResult(_usersRepository.GetAll(false)
                        .Include(u => u.Status)
                        .Include(u => u.Role)
                        .Include(u => u.UserPolicies));
                    List<UserDTO> dtos = null;
                    if (entities.Any())
                    {
                        dtos = entities
                                .Skip(request.PageSize * (request.PageIndex - 1))
                                .Take(request.PageSize)
                                .Select(entity => new UserDTO
                                {
                                    CreatedBy = entity.CreatedBy,
                                    CreatedDate = entity.CreatedDate,
                                    Email = entity.Email,
                                    Firstname = entity.Firstname,
                                    Id = entity.Id,
                                    Lastname = entity.Lastname,
                                    ModifiedBy = entity.ModifiedBy,
                                    ModifiedDate = entity.ModifiedDate,
                                    RoleId = entity.RoleId,
                                    RoleName = entity.Role.Name,
                                    StatusId = entity.StatusId,
                                    StatusCode = entity.Status.Code
                                })
                                .ToList();
                        result.TotalRecords = entities.Count();
                        result.Records = dtos;
                    }
                    processResult = new ProcessResult<SearchResponse<ICollection<UserDTO>>>(result);
                }
            }
            catch (Exception ex)
            {
                processResult = new ProcessResult<SearchResponse<ICollection<UserDTO>>>(ex.Message);
            }
            return processResult;
        }

        public async Task<ProcessResult<UserDTO>> GetUserByIdAsync(SearchByIdRequest request)
        {
            var processResult = new ProcessResult<UserDTO>(string.Empty);
            try
            {
                using (var conn = UnitOfWork.CreateOpenConnection())
                {
                    var entity = await Task.FromResult(_usersRepository.GetById(request.Id));
                    UserDTO dto = null;
                    if (entity != null)
                    {
                        var role = await Task.FromResult(_rolesRepository.GetById(entity.RoleId));
                        var status = await Task.FromResult(_statusRepository.GetById(entity.StatusId));
                        dto = new UserDTO
                        {
                            CreatedBy = entity.CreatedBy,
                            CreatedDate = entity.CreatedDate,
                            Email = entity.Email,
                            Firstname = entity.Firstname,
                            Id = entity.Id,
                            Lastname = entity.Lastname,
                            ModifiedBy = entity.ModifiedBy,
                            ModifiedDate = entity.ModifiedDate,
                            RoleId = role.Id,
                            RoleName = role.Name,
                            StatusId = status.Id,
                            StatusCode = status.Code,
                            Username = entity.Username
                        };
                    }
                    processResult = new ProcessResult<UserDTO>(dto);
                }
            }
            catch (Exception ex)
            {
                processResult = new ProcessResult<UserDTO>(ex.Message);
            }
            return processResult;
        }

        public async Task<ProcessResult<Guid>> UpdateUserAsync(UpdateUserRequest request)
        {
            var processResult = new ProcessResult<Guid>(string.Empty);
            try
            {
                using (var conn = UnitOfWork.CreateOpenConnection())
                {
                    using (var trans = UnitOfWork.BeginTransaction())
                    {
                        var entity = await Task.FromResult(_usersRepository.GetById(request.Id));
                        var role = await Task.FromResult(_rolesRepository.GetById(request.RoleId));
                        var roles = await Task.FromResult(_rolesRepository.GetAll(false));
                        var roleId = roles.First(entity => entity.Name == nameof(RolesEnum.CLIENT)).Id;
                        var status = await Task.FromResult(_statusRepository.GetById(request.StatusId));
                        var statuses = await Task.FromResult(_statusRepository.GetAll(false));
                        var statusId = statuses.First(status => status.Code == nameof(StatusEnum.PENDING)).Id;
                        if (entity != null)
                        {
                            entity.Email = request.Email;
                            entity.Firstname = request.Firstname;
                            entity.Lastname = request.Lastname;
                            entity.ModifiedBy = request.ModifiedBy;
                            entity.ModifiedDate = request.ModifiedDate;
                            entity.RoleId = role != null ? role.Id : roleId;
                            entity.StatusId = status != null ? status.Id : statusId;
                            entity.Password = request.Password;
                            await Task.FromResult(_usersRepository.Update(entity));
                            UnitOfWork.Commit();
                            processResult = new ProcessResult<Guid>(entity.Id);
                        }
                        else
                        {
                            throw new Exception("The user you're trying to update no longer exists.");
                        }
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

        public async Task<ProcessResult<int>> DeleteUserAsync(DeleteRequest request)
        {
            var processResult = new ProcessResult<int>(string.Empty);
            try
            {
                using (var conn = UnitOfWork.CreateOpenConnection())
                {
                    using (var trans = UnitOfWork.BeginTransaction())
                    {
                        var statuses = await Task.FromResult(_statusRepository.GetAll(false));
                        var status = statuses.First(entity => entity.Code == nameof(StatusEnum.TERMINATED));
                        var entity = await Task.FromResult(_usersRepository.GetById(request.Id));
                        if (entity != null)
                        {
                            entity.StatusId = status.Id;
                            await Task.FromResult(_usersRepository.Update(entity));
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

        public async Task<ProcessResult<UserDTO>> GetUserByUsernameAsync(string username)
        {
            var processResult = new ProcessResult<UserDTO>(string.Empty);
            try
            {
                using (var conn = UnitOfWork.CreateOpenConnection())
                {
                    var entities = await Task
                        .FromResult(_usersRepository
                            .GetAll(false)
                            .Include(u => u.Status)
                            .Include(u => u.Role)
                            .Include(u => u.UserPolicies));
                    var entity = entities.FirstOrDefault(entity => entity.Username == username);
                    UserDTO dto = null;
                    if (entity != null)
                    {
                        dto = new UserDTO
                        {
                            CreatedBy = entity.CreatedBy,
                            CreatedDate = entity.CreatedDate,
                            Email = entity.Email,
                            Firstname = entity.Firstname,
                            Id = entity.Id,
                            Lastname = entity.Lastname,
                            ModifiedBy = entity.ModifiedBy,
                            ModifiedDate = entity.ModifiedDate,
                            RoleId = entity.RoleId,
                            RoleName = entity.Role.Name,
                            StatusId = entity.StatusId,
                            StatusCode = entity.Status.Code,
                            Username = entity.Username
                        };
                        processResult = new ProcessResult<UserDTO>(dto);
                    }
                    else
                    {
                        processResult = new ProcessResult<UserDTO>(dto);
                    }
                }
            }
            catch (Exception ex)
            {
                processResult = new ProcessResult<UserDTO>(ex.Message);
            }
            return processResult;
        }

        public async Task<ProcessResult<UserDTO>> GetUserByUserCredentialsAsync(string username, string password)
        {
            var processResult = new ProcessResult<UserDTO>(string.Empty);
            try
            {
                using (var conn = UnitOfWork.CreateOpenConnection())
                {
                    var entities = await Task
                        .FromResult(_usersRepository
                            .GetAll(false)
                            .Include(u => u.Status)
                            .Include(u => u.Role)
                            .Include(u => u.UserPolicies));
                    var entity = entities
                        .FirstOrDefault(entity =>
                            entity.Username == username && entity.Password == password);
                    UserDTO dto = null;
                    if (entity != null)
                    {
                        dto = new UserDTO
                        {
                            CreatedBy = entity.CreatedBy,
                            CreatedDate = entity.CreatedDate,
                            Email = entity.Email,
                            Firstname = entity.Firstname,
                            Id = entity.Id,
                            Lastname = entity.Lastname,
                            ModifiedBy = entity.ModifiedBy,
                            ModifiedDate = entity.ModifiedDate,
                            RoleId = entity.RoleId,
                            RoleName = entity.Role.Name,
                            StatusId = entity.StatusId,
                            StatusCode = entity.Status.Code,
                            Username = entity.Username
                        };
                        processResult = new ProcessResult<UserDTO>(dto);
                    }
                    else
                    {
                        processResult = new ProcessResult<UserDTO>(dto);
                    }
                }
            }
            catch (Exception ex)
            {
                processResult = new ProcessResult<UserDTO>(ex.Message);
            }
            return processResult;
        }
    }
}
