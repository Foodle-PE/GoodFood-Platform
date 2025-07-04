using appweb_back.iam.Domain.Model.Aggregates;
using appweb_back.iam.Interfaces.REST.Resources;

namespace appweb_back.iam.Interfaces.REST.Transform;

public static class AuthenticatedUserResourceFromEntityAssembler
{
    public static AuthenticatedUserResource ToResourceFromEntity(User entity, string token)
    {
        return new AuthenticatedUserResource(entity.Id, entity.Username, token, entity.Role);
    }
}