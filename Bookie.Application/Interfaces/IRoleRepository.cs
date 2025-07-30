using Bookie.Domain.Entities;

public interface IRoleRepository
{
    Task<Role?> GetByIdAsync(Guid id);
    Task<Role?> GetByNameAsync(string roleName); 
}
