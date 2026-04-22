namespace Tastemap.Core.Common;

public class Response<T>
{
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }
    public string? ErrorMessage { get; set; }
    public int StatusCode { get; set; }


    private Response(bool isSuccess, T? data, string? errorMessage, int statusCode)
    {
        IsSuccess = isSuccess;
        Data = data;
        ErrorMessage = errorMessage;
        StatusCode = statusCode;
    }

    public static Response<T> Success(T data, int statusCode = 200)
        => new(true, data, null, statusCode);

    public static Response<T> Fail(string errorMessage, int statusCode = 400)
        => new(false, default, errorMessage, statusCode);
}