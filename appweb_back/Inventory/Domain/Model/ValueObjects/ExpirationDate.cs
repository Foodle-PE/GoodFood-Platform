namespace appweb_back.Inventory.Domain.Model.ValueObjects;

public record ExpirationDate(DateTime Value)
{
    public static ExpirationDate Create(DateTime value)
    {
        if (value <= DateTime.UtcNow)
            throw new ArgumentException("Expiration date must be in the future.");
        return new ExpirationDate(value);
    }
}
