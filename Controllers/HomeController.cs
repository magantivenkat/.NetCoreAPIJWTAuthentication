using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace JwtTokenDemo.Controllers
{
    [Route("/")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Get() {
            //Redirection to Swagger documentation 
            return Redirect("~/swagger");
        }
    }
}