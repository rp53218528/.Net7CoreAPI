using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Learning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet] //Attributes
        public IActionResult GetAllStudents()
        {
            string[] studentNames = new string[] { "Ronak","Harsh","Shyam"};
            return Ok(studentNames);
        }
    }
}
