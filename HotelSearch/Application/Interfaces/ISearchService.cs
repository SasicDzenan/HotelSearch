using HotelSearch.Common.Responses;
using HotelSearch.DTOs.Requests;
using HotelSearch.DTOs.Responses;

namespace HotelSearch.Application.Interfaces;

public interface ISearchService
{
    Task<ApiResponse<IEnumerable<HotelResponse>>> SearchAsync(SearchHotelsRequest request);
}
