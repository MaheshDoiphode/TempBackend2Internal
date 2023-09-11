using Microsoft.AspNetCore.Mvc;
using InfraProject.Repositories;
using DomainProject.DomainModels;

namespace ApplicationProject.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly RoleRepository _roleRepository;

        public RoleController(RoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        [HttpGet]
        public IActionResult GetAllRoles()
        {
            var roles = _roleRepository.GetAllRoles();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public IActionResult GetRoleById(string id)
        {
            var role = _roleRepository.GetRoleById(id);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        [HttpPost]
        public IActionResult AddRole([FromBody] Role role)
        {
            _roleRepository.AddRole(role);
            return CreatedAtAction(nameof(GetRoleById), new { id = role.RoleID }, role);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRole(string id, [FromBody] Role role)
        {
            if (id != role.RoleID)
            {
                return BadRequest();
            }

            _roleRepository.UpdateRole(role);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRole(string id)
        {
            _roleRepository.DeleteRole(id);
            return NoContent();
        }

        [HttpPost("ConfigureRole")]
        public IActionResult ConfigureRole([FromBody] Role role)
        {
            _roleRepository.AddOrUpdateRole(role);
            return CreatedAtAction(nameof(GetRoleById), new { id = role.RoleID }, role);
        }

    }
}
