using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;

using Microsoft.EntityFrameworkCore;


public interface IUserService{
    Task<PaginatedResult<User>> GetAllUsersService(QueryParameters queryParameters);
    Task<User> CreateUserService(CreateUserDto newuser);
    Task<UserDto?> GetUserByIdService(Guid userId);
    Task<bool> DeleteUserByIdService(Guid id);
    Task<User> UpdateUserService(Guid id, UpdateUser updateUser);
}
public class UserService:IUserService
  {
private readonly AppDBContext _appDbContext;
private readonly IMapper _mapper;
public UserService(AppDBContext appDbContext,IMapper mapper){
    _mapper = mapper;
  _appDbContext=appDbContext;
}     

    public async Task<PaginatedResult<User>> GetAllUsersService(QueryParameters queryParameters)
{
    try
    {
        // 1. إنشاء استعلام قابل للتصفية
        var query = _appDbContext.Users.AsQueryable();

        // 2. البحث (Search) - إذا كنت ترغب في إضافة ميزة البحث
        if (!string.IsNullOrEmpty(queryParameters.SearchTerm))
        {
            query = query.Where(u => u.Name.Contains(queryParameters.SearchTerm) || u.Email.Contains(queryParameters.SearchTerm));
        }

        // 3. الترتيب (Sorting) - إذا كنت ترغب في إضافة ميزة الترتيب
        if (!string.IsNullOrEmpty(queryParameters.SortBy))
        {
            switch (queryParameters.SortBy.ToLower())
            {
                case "Name":
                    query = queryParameters.SortOrder.ToLower() == "asc"
                        ? query.OrderBy(u => u.Name)
                        : query.OrderByDescending(u => u.Name);
                    break;
                case "Email":
                    query = queryParameters.SortOrder.ToLower() == "asc"
                        ? query.OrderBy(u => u.Email)
                        : query.OrderByDescending(u => u.Email);
                    break;
                default:
                    query = query.OrderBy(u => u.Name); // الترتيب الافتراضي بالاسم
                    break;
            }
        }

        // 4. إجمالي عدد النتائج قبل تطبيق البيجينيشن
        var totalCount = await query.CountAsync();

        // 5. البيجينيشن (Pagination)
        var users = await query
            .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize) // تجاوز النتائج السابقة حسب الصفحة
            .Take(queryParameters.PageSize) // جلب عدد النتائج المطلوبة
            .ToListAsync();

        // 6. إرجاع النتائج مع معلومات البيجينيشن
        return new PaginatedResult<User>
        {
            Items = users,
            TotalCount = totalCount,
            PageNumber = queryParameters.PageNumber,
            PageSize = queryParameters.PageSize
        };
    }
    catch (System.Exception)
    {
        throw new ApplicationException("Error occurred when getting data from the user table");
    }
}

    public async Task<User> CreateUserService(CreateUserDto newuser)
    {
      try
      {
        var user = _mapper.Map<User>(newuser);
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

         return _mapper.Map<UserDto>(user);  
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

        // existingUser.Name = updateUser.Name ?? existingUser.Name;
        // existingUser.Email = updateUser.Email ?? existingUser.Email;
        // existingUser.Password = updateUser.Password??existingUser.Password;
        _mapper.Map(updateUser, existingUser);

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