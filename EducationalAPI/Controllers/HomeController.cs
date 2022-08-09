using Microsoft.AspNetCore.Mvc;

namespace EducationalAPI.Controllers
{
    [EnableCors(origins: "http://mywebclient.azurewebsites.net", headers: "*", methods: "*")]
    [ApiController]
    [Route("api")]
    public class HomeController : ControllerBase
    {
        public HomeController()
        {
        }

    }
}