using appweb_back.Profiles.Domain.Model.Commands;
using appweb_back.Profiles.Interfaces.REST.Resources;

namespace appweb_back.Profiles.Interfaces.REST.Transform;

public static class CreateProfileCommandFromResourceAssembler
{
    public static CreateProfileCommand ToCommandFromResource(CreateProfileResource resource, int userId)
    {
        return new CreateProfileCommand(userId, resource.FirstName, resource.LastName, resource.Email, resource.Phone, resource.Role);
    }
}