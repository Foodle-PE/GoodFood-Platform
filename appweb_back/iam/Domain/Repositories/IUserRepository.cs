using appweb_back.iam.Domain.Model.Aggregates;
using appweb_back.Shared.Domain.Repositories;

namespace appweb_back.iam.Domain.Repositories;

public interface IUserRepository: IBaseRepository<User>
{
    Task<User?> FindByUsernameAsync(string username);
    Task<User?> FindByRoleAsync(string role);
    bool ExistsByUsername(string username);
    
}