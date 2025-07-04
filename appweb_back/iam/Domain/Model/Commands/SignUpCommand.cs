namespace appweb_back.iam.Domain.Model.Commands;

public record SignUpCommand(
    string Username,
    string Password,
    string FirstName,
    string LastName,
    string Email,
    string Phone,
    string Role);