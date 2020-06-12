using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotesApi.Database.Infrastructer;
using NotesApi.Database.Interfaces;
using NotesApi.Models;
using NotesApi.Response;
using NotesApi.Services.Interfaces;

namespace NotesApi.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IRepository<Role> roleRepository;
        private readonly IRepository<User> userRepository;
        private readonly IUnitOfWork unitOfWork;
        public UserRoleService(IRepository<Role>  roleRepository,
                               IRepository<User>  userRepository,
                               IUnitOfWork unitOfWork)
        {
            this.roleRepository = roleRepository;
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }
        public async Task<UserResponse> DeleteRoleAsync(int userId, int roleId)
        {
            try
            {
                User user = await userRepository.FindByIdAsync(userId);
                user.UserRoles.Remove(user.UserRoles.SingleOrDefault(x => x.RoleId == roleId));
                await unitOfWork.CompleteAsync();
                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"An error occured with deleting role: {ex.Message}");
            }

        }

        public async Task<IEnumerable<User>> ListUsersByRoleAsync(int roleId)
        {
            var users = await userRepository.ListAsync();
            var usersInRole = users.Where(x => x.UserRoles.Contains(x.UserRoles.SingleOrDefault(y => y.RoleId == roleId)));
            return usersInRole;
        }

        public async Task<UserResponse> SetUserRoleAsync(int userId, int roleId)
        {
            try
            {
                User user = await userRepository.FindByIdAsync(userId);
                user.UserRoles.Add(new UserRole { UserId = userId, RoleId = roleId });
                await unitOfWork.CompleteAsync();
                user = await userRepository.FindByIdAsync(userId);
                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"An error occuren when setting the role: {ex.Message}");
            }

        }


    }
}