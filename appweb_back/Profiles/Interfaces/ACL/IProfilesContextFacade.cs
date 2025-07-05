namespace appweb_back.Profiles.Interfaces.ACL;

/// <summary>
/// Facade interface for interacting with profile-related services.
/// </summary>
public interface IProfilesContextFacade
{
    /// <summary>
    /// Creates a new profile.
    /// </summary>
    /// <param name="firstName">The first name of the profile.</param>
    /// <param name="lastName">The last name of the profile.</param>
    /// <param name="email">The email address of the profile.</param>
    /// <param name="phone">The phone number of the profile.</param>
    /// <param name="role">The role of the profile.</param>
    /// <returns>The ID of the created profile, or 0 if creation failed.</returns>
    Task<int> CreateProfile(int userId,string firstName, string lastName, string email, string phone, string role);
}