using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ecommerce_db_api.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("/api/v1/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly AuthService _authService;
    public UserController(IUserService userService,AuthService authService)
    {
         _userService = userService;
        _authService = authService;
    }
        [Authorize(Roles = "Admin")]
         [HttpGet("profile")]
         public IActionResult GetUserProfile()
        {
            return Ok("user data is returned");
        }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            var users = await _userService.GetAllUsersService();
            var response=new{Message="return all the users",Users=users};
        return Ok(response);
        }
        catch (ApplicationException ex)
        {
            
             return ApiResponse.ServerError("server error:"+ ex.Message);
        }
        catch (System.Exception ex){
            
             return ApiResponse.ServerError("server error:"+ ex.Message);
        }}

[HttpGet("{userId}")]
public async Task<IActionResult> GetUserById(Guid userId)
{
    try
    {
        var user = await _userService.GetUserByIdService(userId);
        if (user == null)
        { 
            // cheng her
            return ApiResponse.NotFound( "User not found" );
        }
        return ApiResponse.Success(user,"user is retuned succcessfuly");
    }
    catch (ApplicationException ex)
    {
        return ApiResponse.ServerError("server error:"+ ex.Message);
    }
    catch (System.Exception ex)
    {
        return ApiResponse.ServerError("server error:"+ ex.Message);
    }
}
// اتاكدي منها 

[HttpDelete("{id}")]
public async Task<IActionResult> DeleteUser(Guid id)
{
    try
    {
        var result = await _userService.DeleteUserByIdService(id);
        if (result==false)
        {
            return ApiResponse.NotFound($"User with this id {id}dose not exist" );
        }
        return ApiResponse.Success("user is deleted successfuly");
        
        }
    catch (ApplicationException ex)
    {
        return ApiResponse.ServerError("server error:"+ ex.Message);
    }
    catch (System.Exception ex)
    {
        return ApiResponse.ServerError("server error:"+ ex.Message);
    }}

[HttpPost]
    public async Task<IActionResult> CreatUsers([FromBody]CreateUserDto newuser)
    {
        try
        {
            var user = await _userService.CreateUserService(newuser);
            var response=new{Message="creat the users",Users=user};
        return ApiResponse.Created("user is created successfuly");
        }
        catch (ApplicationException ex)
        {
            
             return ApiResponse.ServerError("server error:"+ ex.Message);
        }
        catch (System.Exception ex)
        {
             return ApiResponse.ServerError("server error:"+ ex.Message);
        }}
[HttpPut("{id}")]
public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUser updateUser)
{
    try
    {
        if (updateUser == null)
        {
            return ApiResponse.BadRequest("Invalid user data.");}

        var updatedUser = await _userService.UpdateUserService(id, updateUser);
        
        if (updatedUser == null)
        {
            return ApiResponse.NotFound("User not found.");
        }
        return ApiResponse.Success("user is updated successfuly");
    }
    catch (ApplicationException ex)
    {
         return ApiResponse.ServerError("server error:"+ ex.Message);
    }
    catch (System.Exception ex)
    {
         return ApiResponse.ServerError("server error:"+ ex.Message);
    }
}  }