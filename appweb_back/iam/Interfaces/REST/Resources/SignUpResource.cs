namespace appweb_back.iam.Interfaces.REST.Resources;

public record SignUpResource(
    string Username,
    string Password,
    string FirstName,
    string LastName,
    string Email,
    string Phone,
    string Role);