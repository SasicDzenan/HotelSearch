using HotelSearch.Application.Interfaces;
using HotelSearch.Common.Helpers;
using HotelSearch.Common.Responses;
using HotelSearch.DTOs.Requests;
using HotelSearch.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;

namespace HotelSearch.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HotelsController : ControllerBase
{
    private readonly IHotelService _service;

    public HotelsController(IHotelService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<HotelResponse>>>> GetAll()
    {
        var response = await _service.GetAllAsync();
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ApiResponse<HotelResponse>>> GetById(Guid id)
    {
        var response = await _service.GetByIdAsync(id);
        return response.Success ? Ok(response) : NotFound(response);
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<Guid>>> Create(
        [FromBody] CreateHotelRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(
                ResponseHelper.Failure<Guid>("Invalid request"));

        var response = await _service.CreateAsync(request);

        return response.Success
            ? CreatedAtAction(nameof(GetById), new { id = response.Data }, response)
            : BadRequest(response);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ApiResponse<bool>>> Update(
    Guid id,
    [FromBody] UpdateHotelRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(
                ResponseHelper.Failure<bool>("Invalid request"));

        var response = await _service.UpdateAsync(id, request);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<ApiResponse<bool>>> Delete(Guid id)
    {
        var response = await _service.DeleteAsync(id);
        return response.Success ? Ok(response) : NotFound(response);
    }
}
