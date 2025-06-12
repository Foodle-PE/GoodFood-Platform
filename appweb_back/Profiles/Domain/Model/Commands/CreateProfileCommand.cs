namespace appweb_back.Profiles.Domain.Model.Commands;

public record CreateProfileCommand(string FirstName, string LastName, string Email, string Role);