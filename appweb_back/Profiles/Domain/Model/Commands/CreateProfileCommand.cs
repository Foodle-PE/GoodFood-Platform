namespace appweb_back.Profiles.Domain.Model.Commands;

public record CreateProfileCommand(
    int UserId,string FirstName, string LastName, string Email,string Phone, string Role);