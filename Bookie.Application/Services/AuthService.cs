using AutoMapper;
using Bookie.Application.DTOs;
using Bookie.Application.Interfaces;
using Bookie.Application.Interfaces.Services;
using Bookie.Domain.Entities;

namespace Bookie.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly IRoleRepository _roleRepo;
        private readonly IMapper _mapper;

        public AuthService(IUserRepository userRepo, IRoleRepository roleRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _roleRepo = roleRepo;
            _mapper = mapper;
        }

        public async Task<AuthResponseDto> RegisterUserAsync(RegisterUserDto dto)
        {
            if (await _userRepo.ExistsByEmailAsync(dto.Email))
                throw new Exception("Email is already in use.");

            if (await _userRepo.ExistsByUsernameAsync(dto.Username))
                throw new Exception("Username is already taken.");

            var role = await _roleRepo.GetByNameAsync("User");
            if (role == null)
                throw new Exception("User role not found in database.");

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Bio = "",
                RoleId = role.Id
            };

            await _userRepo.AddAsync(user);

            var token = GenerateToken(user);

            return new AuthResponseDto
            {
                UserId = user.Id,
                Username = user.Username,
                RoleName = role.Name,
                Token = token
            };
        }

        public async Task<AuthResponseDto> RegisterPublisherAsync(RegisterPublisherDto dto)
        {
            if (await _userRepo.ExistsByEmailAsync(dto.Email))
                throw new Exception("Email is already in use.");

            if (await _userRepo.ExistsByUsernameAsync(dto.Username))
                throw new Exception("Username is already taken.");

            var role = await _roleRepo.GetByNameAsync("Publisher");
            if (role == null)
                throw new Exception("Publisher role not found in database.");

            var publisher = new User
            {
                Id = Guid.NewGuid(),
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Bio = "",
                RoleId = role.Id
            };

            await _userRepo.AddAsync(publisher);

            var token = GenerateToken(publisher);

            return new AuthResponseDto
            {
                UserId = publisher.Id,
                Username = publisher.Username,
                RoleName = role.Name,
                Token = token
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _userRepo.GetByEmailAsync(dto.Email);
            if (user == null)
                throw new Exception("Invalid credentials.");

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                throw new Exception("Invalid credentials.");

            var role = await _roleRepo.GetByIdAsync(user.RoleId);

            var token = GenerateToken(user);

            return new AuthResponseDto
            {
                UserId = user.Id,
                Username = user.Username,
                RoleName = role?.Name ?? "Unknown",
                Token = token
            };
        }

        private string GenerateToken(User user)
        {
            // TODO: Implement JWT generation
            return "FAKE_JWT_TOKEN";
        }
    }
}
