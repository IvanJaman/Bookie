using Bookie.Application.DTOs;

namespace Bookie.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserDto> GetByIdAsync(Guid id);
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> RegisterUserAsync(RegisterUserDto registerDto);
        Task<UserDto> RegisterPublisherAsync(RegisterPublisherDto registerDto);
        Task<UserDto> AuthenticateAsync(LoginDto loginDto);
        Task<bool> UpdateProfileAsync(Guid userId, UpdateUserDto updateDto);
        Task<bool> ChangePasswordAsync(Guid userId, ChangePasswordDto changeDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
