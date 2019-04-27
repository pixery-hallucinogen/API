using Hallucinogen_API.Contract;
using Microsoft.AspNetCore.Mvc;

namespace Hallucinogen_API.Common
{
    public class ApiControllerBase : ControllerBase
    {
        [NonAction]
        public IActionResult GenerateResponse(ResponseBase response)
        {
            return StatusCode(response.StatusCode, response);
        }
    }
}