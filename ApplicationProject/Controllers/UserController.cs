using Microsoft.AspNetCore.Mvc;
using InfraProject.Repositories;
using DomainProject.DomainModels;

namespace ApplicationProject.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userRepository.GetAllUsers();
            return Ok(users);
        }

        [HttpPost("CreateAdmin")]
        public IActionResult CreateAdminUser([FromBody] User adminUser)
        {
            _userRepository.CreateAdminUser(adminUser);
            return CreatedAtAction(nameof(GetUserById), new { id = adminUser.UserID }, adminUser);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(string id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }


        [HttpPut("UpdateUserRole/{userId}/{newRoleId}")]
        public IActionResult UpdateUserRole(string userId, string newRoleId)
        {
            _userRepository.UpdateUserRole(userId, newRoleId);
            return NoContent();
        }

    }
}
