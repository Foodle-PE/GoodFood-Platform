using appweb_back.Profiles.Domain.Model.Aggregates;
using appweb_back.Profiles.Interfaces.REST.Resources;


namespace appweb_back.Profiles.Interfaces.REST.Transform;

public interface ProfileResourceFromEntityAssembler
{
    public static ProfileResource ToResourceFromEntity(Profile entity)
    {
        return new ProfileResource(entity.Id, entity.FullName, entity.EmailAddress,entity.PhoneNumber,entity.RoleType);
    }
}