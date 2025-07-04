using appweb_back.iam.Domain.Model.Aggregates;
using appweb_back.iam.Domain.Repositories;
using appweb_back.Shared.Infrastructure.Persistence.EFC.Configuration;
using appweb_back.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace appweb_back.iam.Infrastructure.Persistence.EFC.Repositories;

public class UserRepository(AppDbContext context) : BaseRepository<User>(context), IUserRepository
{
    /**
     * The FindByUsernameAsync method
     * <summary>
     * This method is responsible for finding a user by username.
     * </summary>
     * <param name="username">The username of the user to find.</param>
     * <returns>The user with the specified username.</returns>
     */
    public async Task<User?> FindByUsernameAsync(string username)
    {
        return await Context.Set<User>().FirstOrDefaultAsync(user => user.Username.Equals(username));
    }
    
    public async Task<User?> FindByRoleAsync(string role)
    {
        return await context.Set<User>().FirstOrDefaultAsync(user => user.Role.Equals(role) );
    }
    /**
     * The ExistsByUsername method
     * <summary>
     * This method is responsible for checking if a user with the specified username exists.
     * </summary>
     * <param name="username">The username of the user to check.</param>
     * <returns>True if a user with the specified username exists, otherwise false.</returns>
     */
    public bool ExistsByUsername(string username)
    {
        return Context.Set<User>().Any(user => user.Username.Equals(username));
    }
}