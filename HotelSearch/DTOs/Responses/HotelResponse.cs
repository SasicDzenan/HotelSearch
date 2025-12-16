namespace HotelSearch.DTOs.Responses;

public class HotelResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public double DistanceKm { get; set; }
}
