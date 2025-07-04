using appweb_back.Profiles.Domain.Model.Aggregates;
using appweb_back.Profiles.Domain.Repositories;
using appweb_back.Shared.Infrastructure.Persistence.EFC.Configuration;
using appweb_back.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace appweb_back.Profiles.Infrastructure.Persistence.EFC.Repositories;

public class ProfileRepository(AppDbContext context): BaseRepository<Profile>(context),IProfileRepository
{
    public async Task<Profile?> FindByUserIdAsync(int userId)
    {
        return await context.Set<Profile>().FirstOrDefaultAsync(p => p.UserId == userId);
    }
    public async Task SaveAsync(Profile profile)
    {
        context.Update(profile);
        await context.SaveChangesAsync();
    }
}