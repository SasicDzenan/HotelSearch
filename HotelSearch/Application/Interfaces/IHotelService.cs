using HotelSearch.Common.Responses;
using HotelSearch.DTOs.Requests;
using HotelSearch.DTOs.Responses;

namespace HotelSearch.Application.Interfaces;

public interface IHotelService
{
    Task<ApiResponse<IEnumerable<HotelResponse>>> GetAllAsync();
    Task<ApiResponse<HotelResponse>> GetByIdAsync(Guid id);
    Task<ApiResponse<Guid>> CreateAsync(CreateHotelRequest request);
    Task<ApiResponse<bool>> UpdateAsync(Guid id, UpdateHotelRequest request);
    Task<ApiResponse<bool>> DeleteAsync(Guid id);
}
