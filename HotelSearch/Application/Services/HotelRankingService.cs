using HotelSearch.DTOs.Responses;

public class HotelRankingService : IHotelRankingService
{
    public IEnumerable<HotelResponse> Rank(IEnumerable<HotelResponse> hotels)
        => hotels
            .OrderBy(h => h.Price)
            .ThenBy(h => h.DistanceKm);
}
