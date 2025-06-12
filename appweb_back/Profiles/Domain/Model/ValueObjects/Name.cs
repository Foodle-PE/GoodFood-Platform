namespace appweb_back.Profiles.Domain.Model.ValueObjects;

public record Name(string FirstName, string LastName)
{
    public Name() : this(string.Empty, string.Empty)
    {
    }
    
    public Name(string FirstName) : this(FirstName, string.Empty){}
    
    public string FullName => $"{FirstName} {LastName}";
}