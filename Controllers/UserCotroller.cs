// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;

// [ApiController]
// [Route("/api/users")]
// public class UserController : ControllerBase
// {
//     private readonly UserService _userService;
//     public UserController(UserService userService)
//     {
//         _userService = userService;
//     }
//     //  private readonly AuthService _authService;
//     //     public UserController(AuthService authService)
//     //     {
//     //         _authService = authService;
//     // //     }
//     //      [Authorize(Roles = "Admin")]
//     //     [HttpGet("profile")]
//     //     public IActionResult GetUserProfile()
//     //     {
//     //         return Ok("user data is returned");
//     //     }


//     [HttpGet]
//     public async Task<IActionResult> GetAllUsers()
//     {
//         try
//         {
//             var users = await _userService.GetAllUsersService();
//             var response=new{Message="return all the users",Users=users};
//         return Ok(response);
//         }
//         catch (ApplicationException ex)
//         {
            
//             return StatusCode(500, ex.Message);
//         }
//         catch (Exception ex){
            
//             return StatusCode(500, ex.Message);
//         }}

// [HttpGet("{userId}")]
// public async Task<IActionResult> GetUserById(Guid userId)
// {
//     try
//     {
//         var user = await _userService.GetUserByIdService(userId);
        
//         if (user == null)
//         {
//             return NotFound(new { Message = "User not found" });
//         }
        
//         return Ok(user);
//     }
//     catch (ApplicationException ex)
//     {
//         return StatusCode(500, ex.Message);
//     }
//     catch (Exception ex)
//     {
//         return StatusCode(500, ex.Message);
//     }
// }
// [HttpDelete("{id}")]
// public async Task<IActionResult> DeleteUser(Guid id)
// {
//     try
//     {
//         var result = await _userService.DeleteUserByIdService(id);
//         if (result)
//         {
//             return Ok(new { Message = "User deleted successfully" });
//         }
//         else
//         {
//             return NotFound(new { Message = "User not found" });
//         }}
//     catch (ApplicationException ex)
//     {
//         return StatusCode(500, ex.Message);
//     }
//     catch (Exception ex)
//     {
//         return StatusCode(500, ex.Message);
//     }}

// [HttpPost]
//     public async Task<IActionResult> CreatUsers([FromBody]CreateUserDto newuser)
//     {
//         try
//         {
//             var user = await _userService.CreateUserService(newuser);
//             var response=new{Message="creat the users",Users=user};
//         return Created($"/api/users/{user.UserId}",response);
//         }
//         catch (ApplicationException ex)
//         {
            
//             return StatusCode(500, ex.Message);
//         }
//         catch (Exception ex)
//         {
//             return StatusCode(500, ex.Message);
//         }}
// [HttpPut("{id}")]
// public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUser updateUser)
// {
//     try
//     {
//         if (updateUser == null)
//         {
//             return BadRequest("Invalid user data.");}

//         var updatedUser = await _userService.UpdateUserService(id, updateUser);
        
//         if (updatedUser == null)
//         {
//             return NotFound("User not found.");
//         }

//         var response = new { Message = "User updated successfully", User = updatedUser };
//         return Ok(response);
//     }
//     catch (ApplicationException ex)
//     {
//         return StatusCode(500, ex.Message);
//     }
//     catch (Exception ex)
//     {
//         return StatusCode(500, ex.Message);
//     }
// }  

// }
