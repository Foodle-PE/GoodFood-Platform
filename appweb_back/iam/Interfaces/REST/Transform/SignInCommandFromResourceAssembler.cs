using appweb_back.iam.Domain.Model.Commands;
using appweb_back.iam.Interfaces.REST.Resources;

namespace appweb_back.iam.Interfaces.REST.Transform;

public static class SignInCommandFromResourceAssembler
{
    public static SignInCommand ToCommandFromResource(SignInResource resource)
    {
        return new SignInCommand(resource.Username, resource.Password);
    } 
}