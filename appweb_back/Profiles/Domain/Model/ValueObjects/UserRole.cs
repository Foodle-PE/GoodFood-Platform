namespace appweb_back.Profiles.Domain.Model.ValueObjects;

public record UserRole(string Role)
{
    public UserRole() : this(string.Empty)
    {
        
    }
};