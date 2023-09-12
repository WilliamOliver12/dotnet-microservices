using Microsoft.AspNetCore.Mvc;

namespace WeatherService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        [HttpGet("{city}")]
        public ActionResult<string> Get(string city)
        {
            // For the sake of simplicity, we'll return a hardcoded response.
            // In a real-world application, you might fetch this from a database or an external API.
            return $"Weather in {city}: Sunny, 25°C";
        }
    }
}