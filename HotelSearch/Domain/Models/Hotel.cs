namespace HotelSearch.Domain.Models;

public class Hotel
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public GeoLocation Location { get; private set; }

    public Hotel(Guid id, string name, decimal price, GeoLocation location)
    {
        Validate(name, price);

        Id = id;
        Name = name;
        Price = price;
        Location = location;
    }

    public void Update(string name, decimal price, GeoLocation location)
    {
        Validate(name, price);

        Name = name;
        Price = price;
        Location = location;
    }

    private static void Validate(string name, decimal price)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Hotel name is required");

        if (price <= 0)
            throw new ArgumentException("Price must be greater than zero");
    }
}
