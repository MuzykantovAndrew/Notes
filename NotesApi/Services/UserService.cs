using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NotesApi.Database.Infrastructer;
using NotesApi.Database.Interfaces;
using NotesApi.Models;
using NotesApi.Resourse;
using NotesApi.Response;
using NotesApi.Response.Result;
using NotesApi.Services.Interfaces;

namespace NotesApi.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> userRepository;
        private readonly IUnitOfWork unitOfWork;
        public UserService(IRepository<User> categoryRepository, IUnitOfWork unitOfWork)
        {
            this.userRepository = categoryRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<UserResponse> DeleteAsync(int id)
        {
            var existingUser = await userRepository.FindByIdAsync(id);
            if (existingUser == null)
                return new UserResponse("User not found");

            try
            {
                userRepository.Remove(existingUser);
                await unitOfWork.CompleteAsync();

                return new UserResponse(existingUser);
            }
            catch (Exception ex)
            {
                return new UserResponse($"Error when deleting user: {ex.Message}");
            }

        }

        public async Task<IEnumerable<User>> ListAsync()
        {
            return await userRepository.ListAsync();
        }

        public async Task<UserResponse> SaveAsync(User user)
        {
            try 
            {
                await userRepository.AddAsync(user);
                await unitOfWork.CompleteAsync();

                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"Error occured when saving user: {ex.Message}");
            }
        }

        public async Task<UserResponse> UpdateAsync(int id, User user)
        {
            var existingUser  = await userRepository.FindByIdAsync(id);

            if (existingUser == null)
                return new UserResponse("User not found");

            existingUser.Name = user.Name;
            existingUser.Lastname = user.Lastname;
            existingUser.Password = user.Password;

            try
            {
                userRepository.Update(existingUser);
                await unitOfWork.CompleteAsync();
                return new UserResponse(existingUser);
            }
            catch (Exception ex)
            {
                return new UserResponse($"User update error: {ex.Message}");
            }
        }
    }
}