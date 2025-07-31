using Bookie.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookie.Application.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(RefreshToken token);
        Task<RefreshToken> GetByTokenAsync(string token);
        Task UpdateAsync(RefreshToken refreshToken);
        Task SaveChangesAsync();
    }
}
