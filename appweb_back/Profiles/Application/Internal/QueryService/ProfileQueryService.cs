using appweb_back.Profiles.Domain.Model.Aggregates;
using appweb_back.Profiles.Domain.Model.Queries;
using appweb_back.Profiles.Domain.Services;
using appweb_back.Profiles.Domain.Repositories;

namespace appweb_back.Profiles.Application.Internal.QueryService;

public class ProfileQueryService(IProfileRepository profileRepository): IProfileQueryService
{
    public async Task<IEnumerable<Profile>> Handle(GetAllProfilesQuery query)
    {
        return await profileRepository.ListAsync();
    }

    public async Task<Profile?> Handle(GetProfileByIdQuery query)
    {
        return await profileRepository.FindByIdAsync(query.ProfileId);
    }
}