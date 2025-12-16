using HotelSearch.Application.Interfaces;
using HotelSearch.Common.Helpers;
using HotelSearch.Common.Responses;
using HotelSearch.Domain.Models;
using HotelSearch.DTOs.Requests;
using HotelSearch.DTOs.Responses;

namespace HotelSearch.Application.Services;

public class HotelService : IHotelService
{
    private readonly IHotelRepository _repository;

    public HotelService(IHotelRepository repository)
    {
        _repository = repository;
    }

    public async Task<ApiResponse<IEnumerable<HotelResponse>>> GetAllAsync()
    {
        var hotels = await _repository.GetAllAsync();

        var response = hotels.Select(h => new HotelResponse
        {
            Id = h.Id,
            Name = h.Name,
            Price = h.Price,
            DistanceKm = 0
        });

        return ResponseHelper.Success(response);
    }

    public async Task<ApiResponse<HotelResponse>> GetByIdAsync(Guid id)
    {
        var hotel = await _repository.GetByIdAsync(id);
        if (hotel is null)
            return ResponseHelper.Failure<HotelResponse>("Hotel not found");

        return ResponseHelper.Success(new HotelResponse
        {
            Id = hotel.Id,
            Name = hotel.Name,
            Price = hotel.Price,
            DistanceKm = 0
        });
    }

    public async Task<ApiResponse<Guid>> CreateAsync(CreateHotelRequest request)
    {
        var hotel = new Hotel(
            Guid.NewGuid(),
            request.Name,
            request.Price,
            new GeoLocation(request.Latitude, request.Longitude)
        );

        await _repository.AddAsync(hotel);
        return ResponseHelper.Success(hotel.Id);
    }

    public async Task<ApiResponse<bool>> UpdateAsync(Guid id, UpdateHotelRequest request)
    {
        var hotel = await _repository.GetByIdAsync(id);
        if (hotel is null)
            return ResponseHelper.Failure<bool>("Hotel not found");

        hotel.Update(
            request.Name,
            request.Price,
            new GeoLocation(request.Latitude, request.Longitude)
        );

        await _repository.UpdateAsync(hotel);
        return ResponseHelper.Success(true);
    }

    public async Task<ApiResponse<bool>> DeleteAsync(Guid id)
    {
        var hotel = await _repository.GetByIdAsync(id);
        if (hotel is null)
            return ResponseHelper.Failure<bool>("Hotel not found");

        await _repository.DeleteAsync(id);
        return ResponseHelper.Success(true);
    }
}
