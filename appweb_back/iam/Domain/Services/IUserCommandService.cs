using appweb_back.iam.Domain.Model.Aggregates;
using appweb_back.iam.Domain.Model.Commands;

namespace appweb_back.iam.Domain.Services;

public interface IUserCommandService
{
    Task Handle(SignUpCommand command);
    Task<(User user, string token)> Handle(SignInCommand command);
    
}