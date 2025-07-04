namespace appweb_back.Profiles.Interfaces.REST.Resources;

public record CreateProfileResource(int UserId,string FirstName, string LastName, string Email, string Phone, string Role);