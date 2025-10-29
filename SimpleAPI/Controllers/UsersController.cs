using Microsoft.AspNetCore.Mvc;

namespace SimpleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = new[]
            {
                new { Id = 1, Name = "Ravinder" },
                new { Id = 2, Name = "John" }
            };
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            return Ok(new { Id = id, Name = $"User {id}" });
        }
    }
}
