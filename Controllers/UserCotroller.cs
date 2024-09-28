using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/v1/users")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    public UserController()
    {
        _userService = new UserService();
    }

    [HttpGet]
    public IActionResult GetAllUsers()
    {
        var users = _userService.GetAllUsersService();
        return Ok(users);
    }

    [HttpGet("{userId}")]
    public IActionResult GetSingleUserById(string userId)
    {
        if (!Guid.TryParse(userId, out Guid userIdGuid))
        {
            return BadRequest("Invalid user ID Format");
        }

        var user = _userService.GetUserByIdService(userIdGuid);

        if (user == null)
        {
            return NotFound($"User with {userId} does not exist");
        }
        return Ok(user);
    }

    [HttpDelete("{userId}")]
    public IActionResult DeleteUserById(string userId)
    {
        if (!Guid.TryParse(userId, out Guid userIdGuid))
        {
            return BadRequest("Invalid user ID Format");
        }

        bool result = _userService.DeleteUserByIdService(userIdGuid);

        if (!result)
        {
            return NotFound($"User with {userId} does not exist");
        }
        return NoContent();
    }


     [HttpPost]
    public IActionResult CreateUser(CreateUserDto newUser)
    {if (!UserValidation.isValidName(newUser.Name)) // "test"
    {
      return BadRequest("Name can not be empty");
    }
    if (!UserValidation.IsValidEmail(newUser.Email)) // "test"
    {
      return BadRequest("you have erro in email 'empty or not Contain @'");
    }
           var user = _userService.CreateUserService(newUser);

        // bool result = _userService.DeleteUserByIdService(userIdGuid);

        if (user == null)
        {
            return NotFound($"User could not be created");
        }
        return Created("created user", user);
    }
[HttpPut("{id}")]
  public IActionResult UpdateUser(string id, UserDto updateUser)
{
    if (!Guid.TryParse( id, out Guid UserId))
    {
        return BadRequest("Invalid product ID Format");
    }

    var user = _userService.GetUserByIdService(UserId);

    if (user == null)
    {
        return NotFound($"user with {id} not found");
    }

    if (updateUser.Name != null && !UserValidation.isValidName(updateUser.Name))
    {
        return BadRequest("Name cannot be empty");
    }


  
    _userService.UpdateUserService( UserId, updateUser);

    return NoContent(); 
}
}
