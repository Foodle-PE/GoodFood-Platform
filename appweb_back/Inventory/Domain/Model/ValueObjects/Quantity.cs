namespace appweb_back.Inventory.Domain.Model.ValueObjects;

public record Quantity
{
    public int Value { get; private set; }

    // Requerido por EF Core
    private Quantity() { }

    private Quantity(int value)
    {
        Value = value;
    }

    public static Quantity Create(int value)
    {
        if (value < 0)
            throw new ArgumentException("Quantity must be non-negative.");
        return new Quantity(value);
    }

    public Quantity Add(Quantity other) => new Quantity(this.Value + other.Value);
}

