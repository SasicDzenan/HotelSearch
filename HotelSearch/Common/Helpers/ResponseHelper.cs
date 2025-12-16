using HotelSearch.Common.Responses;

namespace HotelSearch.Common.Helpers;

public static class ResponseHelper
{
    public static ApiResponse<T> Success<T>(T data, string message = "")
        => new() { Success = true, Message = message, Data = data };

    public static ApiResponse<T> Failure<T>(string message)
        => new() { Success = false, Message = message };
}
