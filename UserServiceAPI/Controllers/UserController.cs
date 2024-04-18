using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace TaxaManager.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserRepository _userRepository;

    public UserController(ILogger<UserController> logger, IUserRepository userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        if (user == null)
        {
            return BadRequest("User data is invalid.");
        }

        var createdUser = await _userRepository.CreateUserAsync(user);
        return CreatedAtAction("GetUser", new { id = createdUser.Id }, createdUser);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(Guid id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userRepository.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpGet("department/{department}")]
    public async Task<IActionResult> GetUsersByDepartment(string department)
    {
        var users = await _userRepository.GetUsersByDepartmentAsync(department);
        return Ok(users);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(string id, [FromBody] User user)
    {
        if (user == null)
        {
            return BadRequest("User data is invalid.");
        }

        var updatedUser = await _userRepository.UpdateUserAsync(id, user);
        if (updatedUser == null)
        {
            return NotFound();
        }

        return Ok(updatedUser);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var result = await _userRepository.DeleteUserAsync(id);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
