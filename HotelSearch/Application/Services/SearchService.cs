using HotelSearch.Application.Interfaces;
using HotelSearch.Common.Helpers;
using HotelSearch.Common.Responses;
using HotelSearch.Domain.Models;
using HotelSearch.DTOs.Requests;
using HotelSearch.DTOs.Responses;

namespace HotelSearch.Application.Services;

public class SearchService : ISearchService
{
    private readonly IHotelRepository _repository;
    private readonly IHotelRankingService _ranking;

    public SearchService(
        IHotelRepository repository,
        IHotelRankingService ranking)
    {
        _repository = repository;
        _ranking = ranking;
    }

    public async Task<ApiResponse<IEnumerable<HotelResponse>>> SearchAsync(
        SearchHotelsRequest request)
    {
        var hotels = await _repository.GetAllAsync();
        var userLocation = new GeoLocation(request.Latitude, request.Longitude);

        var mapped = hotels.Select(h => new HotelResponse
        {
            Id = h.Id,
            Name = h.Name,
            Price = h.Price,
            DistanceKm = GeoDistanceHelper.CalculateDistanceKm(
                userLocation, h.Location)
        });

        var ranked = _ranking.Rank(mapped);

        var paged = ranked
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize);

        return ResponseHelper.Success(paged);
    }
}
