using Microsoft.AspNetCore.Mvc;

namespace EducationalAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [ApiController]
    [Route("api")]
    public class HomeController : ControllerBase
    {
        public HomeController()
        {
        }

    }
}