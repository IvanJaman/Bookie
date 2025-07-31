using Bookie.Application.DTOs;

namespace Bookie.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterUserAsync(RegisterUserDto dto);
        Task<AuthResponseDto> RegisterPublisherAsync(RegisterPublisherDto dto);
        Task<AuthResponseDto> LoginAsync(LoginDto dto);
        Task<AuthResponseDto> RefreshTokenAsync(string refreshToken);
    }
}
