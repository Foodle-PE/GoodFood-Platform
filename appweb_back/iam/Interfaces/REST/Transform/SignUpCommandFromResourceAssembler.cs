using appweb_back.iam.Domain.Model.Commands;
using appweb_back.iam.Interfaces.REST.Resources;

namespace appweb_back.iam.Interfaces.REST.Transform;

public static class SignUpCommandFromResourceAssembler
{
    public static SignUpCommand ToCommandFromResource(SignUpResource resource)
    {
        return new SignUpCommand(
            resource.Username,
            resource.Password,
            resource.FirstName,
            resource.LastName,
            resource.Email,
            resource.Phone,
            resource.Role
        );
    } 
}