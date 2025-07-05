namespace appweb_back.iam.Interfaces.REST.Resources;
public record AuthenticatedUserResource(int Id, string Username, string Token, string Role);