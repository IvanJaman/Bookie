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

        public async Task<UserDto> RegisterUserAsync(RegisterUserDto registerDto)
        {
            if (await _userRepo.ExistsByEmailAsync(registerDto.Email))
                throw new Exception("Email is already in use.");

            if (await _userRepo.ExistsByUsernameAsync(registerDto.Username))
                throw new Exception("Username is already taken.");

            var role = await _roleRepo.GetByNameAsync("User");
            if (role == null) throw new Exception("User role not found.");

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = registerDto.Username,
                Email = registerDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                Bio = "",
                RoleId = role.Id
            };

            await _userRepo.AddAsync(user);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> RegisterPublisherAsync(RegisterPublisherDto registerDto)
        {
            if (await _userRepo.ExistsByEmailAsync(registerDto.Email))
                throw new Exception("Email is already in use.");

            if (await _userRepo.ExistsByUsernameAsync(registerDto.Username))
                throw new Exception("Username is already taken.");

            var role = await _roleRepo.GetByNameAsync("Publisher");
            if (role == null) throw new Exception("Publisher role not found.");

            var publisher = new User
            {
                Id = Guid.NewGuid(),
                Username = registerDto.Username,
                Email = registerDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                Bio = "",
                WebsiteUrl = registerDto.WebsiteUrl,
                RoleId = role.Id
            };

            await _userRepo.AddAsync(publisher);
            return _mapper.Map<UserDto>(publisher);
        }

        public async Task<UserDto> AuthenticateAsync(LoginDto loginDto)
        {
            var user = await _userRepo.GetByEmailAsync(loginDto.Email);
            if (user == null) throw new Exception("Invalid credentials.");

            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
                throw new Exception("Invalid credentials.");

            return _mapper.Map<UserDto>(user);
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
