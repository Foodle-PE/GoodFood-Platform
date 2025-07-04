using appweb_back.iam.Domain.Model.Aggregates;
using appweb_back.iam.Domain.Model.Queries;

namespace appweb_back.iam.Domain.Services;

public interface IUserQueryService
{
    Task<IEnumerable<User>> Handle(GetAllUsersQuery query);
    Task<User?> Handle(GetUserByIdQuery query);
    Task<User?> Handle(GetUserByUsernameQuery query);
    Task<User?> Handle(GetUsersByRoleQuery query);
}