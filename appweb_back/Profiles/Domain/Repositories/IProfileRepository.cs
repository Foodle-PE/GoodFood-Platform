using appweb_back.Profiles.Domain.Model.Aggregates;
using appweb_back.Shared.Domain.Repositories;

namespace appweb_back.Profiles.Domain.Repositories;

public interface IProfileRepository : IBaseRepository<Profile>
{
    Task AddAsync(Profile profile);
    Task<Profile?> FindByUserIdAsync(int userId);
    Task SaveAsync(Profile profile); // ⬅️ Necesario para el update
}