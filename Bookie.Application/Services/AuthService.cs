using AutoMapper;
using Bookie.Application.DTOs;
using Bookie.Application.Interfaces;
using Bookie.Application.Interfaces.Services;
using Bookie.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens; 
using System.IdentityModel.Tokens.Jwt; 
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Bookie.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly IRoleRepository _roleRepo;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IRefreshTokenRepository _refreshTokenRepo;
        private readonly IShelfRepository _shelfRepo;

        public AuthService(
            IUserRepository userRepo, 
            IRoleRepository roleRepo, 
            IMapper mapper, 
            IConfiguration config, 
            IRefreshTokenRepository refreshTokenRepo, 
            IShelfRepository shelfRepo)
        {
            _userRepo = userRepo;
            _roleRepo = roleRepo;
            _mapper = mapper;
            _config = config;
            _refreshTokenRepo = refreshTokenRepo;
            _shelfRepo = shelfRepo;
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

            await CreateDefaultShelvesForUserAsync(user.Id);

            var token = GenerateToken(user, role.Name);

            var refreshToken = GenerateRefreshToken(user.Id);
            await _refreshTokenRepo.AddAsync(refreshToken);
            await _refreshTokenRepo.SaveChangesAsync();

            return new AuthResponseDto
            {
                UserId = user.Id,
                Username = user.Username,
                RoleName = role.Name,
                Token = token,
                RefreshToken = refreshToken.Token
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

            await CreateDefaultShelvesForUserAsync(publisher.Id);

            var token = GenerateToken(publisher, role.Name);

            var refreshToken = GenerateRefreshToken(publisher.Id);
            await _refreshTokenRepo.AddAsync(refreshToken);
            await _refreshTokenRepo.SaveChangesAsync();

            return new AuthResponseDto
            {
                UserId = publisher.Id,
                Username = publisher.Username,
                RoleName = role.Name,
                Token = token,
                RefreshToken = refreshToken.Token
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

            var token = GenerateToken(user, role?.Name ?? "Unknown");

            var refreshToken = GenerateRefreshToken(user.Id);
            await _refreshTokenRepo.AddAsync(refreshToken);
            await _refreshTokenRepo.SaveChangesAsync();

            return new AuthResponseDto
            {
                UserId = user.Id,
                Username = user.Username,
                RoleName = role?.Name ?? "Unknown",
                Token = token,
                RefreshToken = refreshToken.Token
            };

        }

        public async Task<AuthResponseDto> RefreshTokenAsync(string refreshToken)
        {
            var storedToken = await _refreshTokenRepo.GetByTokenAsync(refreshToken);

            if (storedToken == null || storedToken.IsRevoked || storedToken.Expires < DateTime.UtcNow)
                throw new Exception("Invalid or expired refresh token.");

            var user = await _userRepo.GetByIdAsync(storedToken.UserId);
            if (user == null)
                throw new Exception("User not found.");

            var role = await _roleRepo.GetByIdAsync(user.RoleId);
            var newAccessToken = GenerateToken(user, role?.Name ?? "Unknown");

            var newRefreshToken = GenerateRefreshToken(user.Id);

            storedToken.IsRevoked = true;
            await _refreshTokenRepo.UpdateAsync(storedToken);

            await _refreshTokenRepo.AddAsync(newRefreshToken);
            await _refreshTokenRepo.SaveChangesAsync();

            return new AuthResponseDto
            {
                UserId = user.Id,
                Username = user.Username,
                RoleName = role?.Name ?? "Unknown",
                Token = newAccessToken,
                RefreshToken = newRefreshToken.Token
            };
        }

        private string GenerateToken(User user, string roleName)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim(ClaimTypes.Role, roleName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private RefreshToken GenerateRefreshToken(Guid userId)
        {
            return new RefreshToken
            {
                Id = Guid.NewGuid(),
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                UserId = userId,
                Expires = DateTime.UtcNow.AddDays(7), 
                CreatedAt = DateTime.UtcNow,
                IsRevoked = false
            };
        }

        private async Task CreateDefaultShelvesForUserAsync(Guid userId)
        {
            var wantToReadShelf = new Shelf
            {
                Id = Guid.NewGuid(),
                Name = "Want to Read",
                UserId = userId
            };

            var readShelf = new Shelf
            {
                Id = Guid.NewGuid(),
                Name = "Read",
                UserId = userId
            };

            await _shelfRepo.AddAsync(wantToReadShelf);
            await _shelfRepo.AddAsync(readShelf);
        }
    }
}
