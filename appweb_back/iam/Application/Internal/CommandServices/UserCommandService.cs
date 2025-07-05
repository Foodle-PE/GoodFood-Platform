using appweb_back.iam.Application.Internal.OutboundServices;
using appweb_back.iam.Domain.Model.Aggregates;
using appweb_back.iam.Domain.Model.Commands;
using appweb_back.iam.Domain.Repositories;
using appweb_back.iam.Domain.Services;
using appweb_back.Profiles.Interfaces.ACL;
using appweb_back.Shared.Domain.Repositories;

namespace appweb_back.iam.Application.Internal.CommandServices;

public class UserCommandService(IUserRepository userRepository, ITokenService tokenService, IHashingService hashingService, IUnitOfWork unitOfWork,  IProfilesContextFacade profilesContextFacade): IUserCommandService
{
    public async Task Handle(SignUpCommand command)
    {
        if (userRepository.ExistsByUsername(command.Username))
            throw new Exception($"Username {command.Username} already exists.");
        var hashedPassword = hashingService.HashPassword(command.Password);
        var user = new User(command.Username, hashedPassword, command.Role);
        try
        {
            await userRepository.AddAsync(user);
            await unitOfWork.CompleteAsync();
        } catch (Exception e)
        {
            throw new Exception("An error occurred while trying to sign up the user.", e);
        }
    }
    public async Task<(User user, string token)> Handle(SignInCommand command)
    {
        var user = await userRepository.FindByUsernameAsync(command.Username);
        if (user is null || !hashingService.VerifyPassword(command.Password, user.PasswordHash))
            throw new Exception("Invalid username or password.");
        var token = tokenService.GenerateToken(user);
        if (user == null)
            Console.WriteLine("[DEBUG] Usuario no encontrado");
        else if (!hashingService.VerifyPassword(command.Password, user.PasswordHash))
            Console.WriteLine($"[DEBUG] Contraseña inválida para usuario: {user.Username}");
        return (user, token);
        
        
    }
    
    
}