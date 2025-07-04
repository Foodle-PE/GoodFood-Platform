using appweb_back.Profiles.Domain.Model.Aggregates;
using appweb_back.Shared.Domain.Repositories;

namespace appweb_back.Profiles.Domain.Repositories;

public interface IProfileRepository : IBaseRepository<Profile>
{
    Task<Profile?> FindByUserIdAsync(int userId);
}