using appweb_back.Profiles.Domain.Model.Commands;
using appweb_back.Profiles.Interfaces.REST.Resources;

namespace appweb_back.Profiles.Interfaces.REST.Transform;

public static class UpdateProfileCommandFromResourceAssembler
{
    public static UpdateProfileCommand ToCommandFromResource(UpdateProfileResource resource, int userId)
    {
        return new UpdateProfileCommand(
            userId,
            resource.FirstName,
            resource.LastName,
            resource.Email,
            resource.Phone
        );
    }
}