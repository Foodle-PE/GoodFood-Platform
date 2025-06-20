using appweb_back.Profiles.Domain.Model.Aggregates;
using appweb_back.Profiles.Domain.Model.Queries;


namespace appweb_back.Profiles.Domain.Services;

public interface IProfileQueryService
{
    Task<IEnumerable<Profile>> Handle(GetAllProfilesQuery query);
    Task<Profile?> Handle(GetProfileByIdQuery query);
}