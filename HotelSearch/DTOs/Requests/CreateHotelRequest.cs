using System.ComponentModel.DataAnnotations;

namespace HotelSearch.DTOs.Requests;

public class CreateHotelRequest
{
    [Required, MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }

    [Range(-90, 90)]
    public double Latitude { get; set; }

    [Range(-180, 180)]
    public double Longitude { get; set; }
}
