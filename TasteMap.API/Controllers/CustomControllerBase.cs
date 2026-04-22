using Microsoft.AspNetCore.Mvc;
using Tastemap.Core.Common;

namespace TasteMap.API.Controllers;

public class CustomControllerBase : ControllerBase
{
    [NonAction]
    public IActionResult CreateActionResult<T>(Response<T> response)
    {
        var apiResponse = new ApiResponse<T>()
        {
            Success = response.IsSuccess,
            Data = response.Data,
            Message = response.IsSuccess ? "Successful" : response.ErrorMessage ?? "An error occured",
            StatusCode = response.StatusCode
        };

        if (apiResponse.StatusCode == 204)
        {
            return new ObjectResult(null)
            {
                StatusCode = response.StatusCode
            };
        }

        return new ObjectResult(response)
        {
            StatusCode = response.StatusCode
        };
    }
}