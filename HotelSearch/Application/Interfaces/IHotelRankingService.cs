using HotelSearch.DTOs.Responses;

public interface IHotelRankingService
{
    IEnumerable<HotelResponse> Rank(IEnumerable<HotelResponse> hotels);
}
