namespace appweb_back.Profiles.Domain.Model.Commands;

public record UpdateProfileCommand(
    int UserId, 
    string FirstName,
    string LastName, 
    string Email, 
    string Phone);