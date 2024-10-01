using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


public interface IUserService{
    Task<List<User>> GetAllUsersService();
    Task<User> CreateUserService(CreateUserDto newuser);
    Task<UserDto?> GetUserByIdService(Guid userId);
    Task<bool> DeleteUserByIdService(Guid id);
    Task<User> UpdateUserService(Guid id, UpdateUser updateUser);
}
public class UserService:IUserService
  {
private readonly AppDBContext _appDbContext;
public UserService(AppDBContext appDbContext){
  _appDbContext=appDbContext;
}      

    public async Task<List<User>> GetAllUsersService()
    {
      try
      {
        var user= await _appDbContext.Users.ToListAsync();
      return user;
      }
      catch (System.Exception)
      {
        
        throw new ApplicationException("erorr ocurred when get the data from the user table");
      }
    }
    public async Task<User> CreateUserService(CreateUserDto newuser)
    {
      try
      {
        var user = new User {
          Name=newuser.Name,
          Email = newuser.Email,
         Password = newuser.Password};
         await _appDbContext.Users.AddAsync(user);
         await _appDbContext.SaveChangesAsync();
         return user;
      }
      catch (System.Exception)
      {
        
        throw new ApplicationException("erorr ocurred when creat the  user ");
      }
    }

    public async Task<UserDto?> GetUserByIdService(Guid userId)
{
    try
    {
        var user = await _appDbContext.Users
            .FirstOrDefaultAsync(u => u.UserId == userId);

        if (user == null)
        {
            return null; // Return null if user not found
        }

        // Convert User to UserDto if needed
        var userDto = new UserDto
        {
            UserId = user.UserId,
            Name = user.Name,
            Email = user.Email,
            // Map other properties as needed
        };

        return userDto;
    }
    catch (Exception)
    {
        throw new ApplicationException("Error occurred while retrieving the user.");
    }
}
   public async Task<bool> DeleteUserByIdService(Guid id)
{
    try
    {
        var userToRemove = await _appDbContext.Users.FirstOrDefaultAsync(u => u.UserId == id);

        if (userToRemove != null)
        {
            _appDbContext.Users.Remove(userToRemove);
            await _appDbContext.SaveChangesAsync();
            return true; 
        }
        return false; 
    }
    catch (Exception)
    {
        throw new ApplicationException("Error occurred while deleting the user.");
    }
}

    public async Task<User> UpdateUserService(Guid id, UpdateUser updateUser)
{
    try
    {
        var existingUser = await _appDbContext.Users.FindAsync(id);

        if (existingUser == null)
        {
            return null;
        }

        existingUser.Name = updateUser.Name ?? existingUser.Name;
        existingUser.Email = updateUser.Email ?? existingUser.Email;
        existingUser.Password = updateUser.Password??existingUser.Password;

        _appDbContext.Users.Update(existingUser);
        await _appDbContext.SaveChangesAsync();

        return existingUser;
    }
    catch (System.Exception)
    {
        throw new ApplicationException("Error occurred when updating the user.");
    }
}
    }