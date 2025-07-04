using appweb_back.iam.Domain.Model.Aggregates;
using appweb_back.iam.Interfaces.REST.Resources;

namespace appweb_back.iam.Interfaces.REST.Transform;

public static class UserResourceFromEntityAssembler
{
    public static UserResource ToResourceFromEntity(User entity)
    {
        return new UserResource(entity.Id, entity.Username);
    }
}