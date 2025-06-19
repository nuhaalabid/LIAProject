using Application.Users.Commands.AddUser;
using Application.Users.Commands.UpdateUser;
using Application.Users.Queries.GetAll;
using Application.Users.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PresentationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IMediator mediator, ILogger<UsersController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var users = await _mediator.Send(new GetAllUsersQuery());
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " Error fetching users");
                return StatusCode(500, "Error: {ex.Message}");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserCommand command)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid registration request.");
                return BadRequest(ModelState);
            }

            var createdUser = await _mediator.Send(command);

            _logger.LogInformation("New user registered: {Email}", createdUser.Email);

            return Ok(createdUser); // 200 OK med UserDto
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserCommand command)
        {
            if (id != command.Id)
            {
                _logger.LogWarning("User update failed: route ID {RouteId} ≠ command ID {CommandId}", id, command.Id);
                return BadRequest("ID mismatch.");
            }

            try
            {
                var updatedUser = await _mediator.Send(command);
                _logger.LogInformation("User updated successfully: {Email}", updatedUser.Email);
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user with ID: {Id}", id);
                return NotFound($"User with ID {id} not found.");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserQuery query)
        {
            var result = await _mediator.Send(query);

            if (result == null)
            {
                _logger.LogWarning("Login attempt failed for email: {Email}", query.Email);
                return Unauthorized("Invalid email or password.");
            }

            _logger.LogInformation("User logged in: {Email}", result.User.Email);
            return Ok(result); // AuthResultDto (user + token)
        }

    }
}
    

