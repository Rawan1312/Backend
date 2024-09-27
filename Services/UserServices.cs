using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



  public class UserService
  {

    private static List<UserDto> _users = new List<UserDto>();
      

    public List<UserDto> GetAllUsersService()
    {
      return _users;
    }
    public UserDto? GetUserByIdService(Guid id)
    {
      var foundUser = _users.Find(user => user.UserId == id);
      return foundUser;
    }

    public UserDto? CreateUserService(CreateUserDto newUser)
    {
      // name, email, password
      // user = id, name, email, password, createdAt
      var user = new UserDto {
        UserId = Guid.NewGuid(),
        Name = newUser.Name,
        Email = newUser.Email,
        Password = newUser.Password
      };
      if(user == null){
        return null;
      }
      _users.Add(user);
      return user;
    }

    public bool DeleteUserByIdService(Guid id)
    {
      var userToRemove = _users.FirstOrDefault(u => u.UserId == id);
      if (userToRemove != null)
      {
        _users.Remove(userToRemove);
        return true;
      }
      return false;
    }
    public async Task<UserDto?> UpdateUserService(Guid Id, UserDto UpdateUser)
  {
    var existingUser = _users.FirstOrDefault(u => u.UserId == Id);
    if (existingUser != null)
    {
  
      existingUser.Name = UpdateUser.Name ?? existingUser.Name;
     existingUser.Email = UpdateUser.Email ?? existingUser.Email;
      existingUser.Password= UpdateUser.Password ?? existingUser.Password;
      
    }
     return await Task.FromResult(existingUser);
  }
    }