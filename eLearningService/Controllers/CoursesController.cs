using Microsoft.AspNetCore.Mvc;

namespace eLearningService.Controllers;

[ApiController]
[Route("[controller]")]
public class CoursesController : ControllerBase
{

    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "Course 1", "Course 2", "Course 3" };
    }
}
