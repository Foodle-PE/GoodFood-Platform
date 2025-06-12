using appweb_back.Profiles.Domain.Model.Aggregates;
using appweb_back.Profiles.Domain.Model.Commands;

namespace appweb_back.Profiles.Domain.Services;

public interface IProfileCommandService
{
    Task<Profile?> Handle(CreateProfileCommand command);
}