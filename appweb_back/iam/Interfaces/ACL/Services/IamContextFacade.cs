using appweb_back.iam.Domain.Model.Commands;
using appweb_back.iam.Domain.Model.Queries;
using appweb_back.iam.Domain.Services;
using appweb_back.iam.Interfaces.ACL;
using appweb_back.Profiles.Interfaces.ACL;
using appweb_back.Profiles.Interfaces.ACL.Services;

namespace appweb_back.iam.Interfaces.ACL.Services;

public class IamContextFacade(IUserCommandService userCommandService, IUserQueryService userQueryService, IProfilesContextFacade profilesContextFacade) : IIamContextFacade
{
    public async Task<int> CreateUser(string username, string password, string firstName, string lastName, string email, string phone, string role)
    {
        var signUpCommand = new SignUpCommand(username, password, firstName, lastName, email, phone, role);
        await userCommandService.Handle(signUpCommand);

        // 2. Obtener el ID del nuevo usuario
        var user = await userQueryService.Handle(new GetUserByUsernameQuery(username));
        if (user == null)
            throw new Exception("User creation failed unexpectedly.");

        // 3. Crear el perfil (Profiles)
        await profilesContextFacade.CreateProfile(user.Id,firstName, lastName, email, phone, role);

        return user.Id;
    }

    public async Task<int> FetchUserIdByUsername(string username)
    {
        var getUserByUsernameQuery = new GetUserByUsernameQuery(username);
        var result = await userQueryService.Handle(getUserByUsernameQuery);
        return result?.Id ?? 0;
    }

    public async Task<string> FetchUsernameByUserId(int userId)
    {
        var getUserByIdQuery = new GetUserByIdQuery(userId);
        var result = await userQueryService.Handle(getUserByIdQuery);
        return result?.Username ?? string.Empty;
    }
}