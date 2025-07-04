using appweb_back.iam.Domain.Model.Aggregates;
using appweb_back.iam.Domain.Model.Queries;
using appweb_back.iam.Domain.Repositories;
using appweb_back.iam.Domain.Services;

namespace appweb_back.iam.Application.Internal.QueryServices;

public class UserQueryService(IUserRepository userRepository):IUserQueryService
{
    public async Task<User?> Handle(GetUserByIdQuery query)
    {
        return await userRepository.FindByIdAsync(query.UserId);
    }
    public async Task<User?> Handle(GetUserByUsernameQuery query)
    {
        return await userRepository.FindByUsernameAsync(query.Username);
    }
    public async Task<IEnumerable<User>> Handle(GetAllUsersQuery query)
    {
        return await userRepository.ListAsync();
    }
    public async Task<User?> Handle(GetUsersByRoleQuery query)
    {
        return await userRepository.FindByRoleAsync(query.Role);
    }
}