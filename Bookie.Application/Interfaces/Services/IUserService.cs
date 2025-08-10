using Bookie.Application.DTOs;

namespace Bookie.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserDto> GetByIdAsync(Guid id);
        Task<UserProfileDto> GetCurrentUserProfileAsync(Guid id);
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<bool> UpdateProfileAsync(Guid userId, UpdateUserDto updateDto);
        Task<bool> ChangePasswordAsync(Guid userId, ChangePasswordDto changeDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
