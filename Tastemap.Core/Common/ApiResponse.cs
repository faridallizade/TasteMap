namespace Tastemap.Core.Common;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T? Data { get; set; }
    public int StatusCode { get; set; }


    public static ApiResponse<T> Ok(T data, string message = "Successful")
        => new() { Success = true, Data = data, Message = message, StatusCode = 200 };

    public static ApiResponse<T> Created(T data, string message = "Created")
        => new() { Success = true, Data = data, Message = message, StatusCode = 201 };

    public static ApiResponse<T> Fail(string message, int statusCode = 400)
        => new() { Success = false, Data = default, Message = message, StatusCode = statusCode };
}