using Bookie.Application.DTOs;

namespace Bookie.Application.Interfaces.Services
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreDto>> GetAllAsync();
    }
}
