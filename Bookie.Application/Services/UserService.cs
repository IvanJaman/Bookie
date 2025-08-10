using AutoMapper;
using Bookie.Application.DTOs;
using Bookie.Application.Interfaces;
using Bookie.Application.Interfaces.Services;
using Bookie.Domain.Entities;

namespace Bookie.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IRoleRepository _roleRepo;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepo, IRoleRepository roleRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _roleRepo = roleRepo;
            _mapper = mapper;
        }

        public async Task<UserProfileDto> GetCurrentUserProfileAsync(Guid id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null) throw new Exception("User not found.");
            return _mapper.Map<UserProfileDto>(user);
        }

        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null) throw new Exception("User not found.");
            return _mapper.Map<UserDto>(user);
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _userRepo.GetAllAsync(); 
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<bool> UpdateProfileAsync(Guid userId, UpdateUserDto updateDto)
        {
            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null) return false;

            _mapper.Map(updateDto, user);
            await _userRepo.UpdateAsync(user);
            return true;
        }

        public async Task<bool> ChangePasswordAsync(Guid userId, ChangePasswordDto changeDto)
        {
            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null) return false;

            if (!BCrypt.Net.BCrypt.Verify(changeDto.CurrentPassword, user.PasswordHash))
                throw new Exception("Current password is incorrect.");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(changeDto.NewPassword);
            await _userRepo.UpdateAsync(user);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null) return false;

            await _userRepo.DeleteAsync(id);
            return true;
        }
    }
}
