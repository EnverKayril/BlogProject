using Microsoft.AspNetCore.Mvc;

namespace BlogProject_UI.Controllers
{
    [Route("Error")]
    public class ErrorController : Controller
    {
        public IActionResult HandleStatusCode(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    return View("404");
                case 500:
                    return View("500");
                case 403:
                    return View("403");
                case 400:
                    return View("400");
                default:
                    return View("Error");
            }
        }
    }
}