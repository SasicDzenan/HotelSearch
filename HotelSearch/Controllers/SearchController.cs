using HotelSearch.Application.Interfaces;
using HotelSearch.Common.Helpers;
using HotelSearch.Common.Responses;
using HotelSearch.DTOs.Requests;
using HotelSearch.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;

namespace HotelSearch.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SearchController : ControllerBase
{
    private readonly ISearchService _searchService;

    public SearchController(ISearchService searchService)
    {
        _searchService = searchService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<HotelResponse>>>> Search(
        [FromQuery] SearchHotelsRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(
                ResponseHelper.Failure<IEnumerable<HotelResponse>>(
                    "Invalid search parameters"));
        }

        var response = await _searchService.SearchAsync(request);
        return response.Success ? Ok(response) : BadRequest(response);
    }
}
