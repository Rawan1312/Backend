using System; 
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
public interface ICategoryService{
    Task<PaginatedResult<CategoryDto>> GetAllCategoryService(QueryParameters queryParameters);
    Task<CategoryDto?> GetCategoryByIdService(Guid categoryId);
    Task<bool> DeleteCategoryByIdService(Guid id);
    Task<Category> CreateCategoryService(CreateCategoryDto newcategory);
    Task<Category> UpdateCategoryService(Guid id, UpdateCategoryDto UpdateCategoryDto);
    }

public class CategoryService:ICategoryService
  {
    private readonly AppDBContext _appDbContext;
    private readonly IMapper _mapper;
public CategoryService(AppDBContext appDbContext,IMapper mapper){
    _mapper = mapper;
  _appDbContext=appDbContext;}
      
    public async Task<PaginatedResult<CategoryDto>> GetAllCategoryService(QueryParameters queryParameters)
{
    try
    {
        var query = _appDbContext.Category.AsQueryable();

        // 1. البحث (Search)
        if (!string.IsNullOrEmpty(queryParameters.SearchTerm))
        {
            query = query.Where(c => c.CategoryName.Contains(queryParameters.SearchTerm));
        }

        // 2. الترتيب (Sorting)
        if (!string.IsNullOrEmpty(queryParameters.SortBy))
        {
            switch (queryParameters.SortBy.ToLower())
            {
                case "name":
                    query = queryParameters.SortOrder.ToLower() == "asc"
                        ? query.OrderBy(c => c.CategoryName)
                        : query.OrderByDescending(c => c.CategoryName);
                    break;
                // يمكنك إضافة المزيد من معايير الترتيب إذا لزم الأمر
                default:
                    query = query.OrderBy(c => c.CategoryName); // الترتيب الافتراضي بالاسم
                    break;
            }
        }

        // 3. إجمالي عدد النتائج قبل تطبيق التقسيم إلى صفحات
        var totalCount = await query.CountAsync();

        // 4. التقسيم إلى صفحات (Pagination)
        var items = await query
            .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)  // تجاوز النتائج السابقة حسب الصفحة
            .Take(queryParameters.PageSize)  // جلب عدد النتائج المطلوبة
            .ToListAsync();

        // تحويل النتائج إلى Dto
        var categoryDtos = _mapper.Map<List<CategoryDto>>(items);

        // 5. إرجاع النتائج بتنسيق PaginatedResult
        var result = new PaginatedResult<CategoryDto>
        {
            Items = categoryDtos,
            TotalCount = totalCount,
            PageNumber = queryParameters.PageNumber,
            PageSize = queryParameters.PageSize,
            SearchBy = queryParameters.SearchTerm
        };

        return result;
    }
    catch (System.Exception)
    {
        throw new ApplicationException("An error occurred while fetching categories.");
    }
}
    public async Task<CategoryDto?> GetCategoryByIdService(Guid categoryId)
{
    try
    {
        var category = await _appDbContext.Category
            .FirstOrDefaultAsync(u => u.CategoryId == categoryId);
        if (category == null)
        {
            return null; 
        }
 

        return _mapper.Map<CategoryDto>(category);
    }
    catch (Exception)
    {
        throw new ApplicationException("Error occurred while retrieving the category.");
    }
}
     public async Task<bool> DeleteCategoryByIdService(Guid id)
{
    try
    {
        var categoryToRemove = await _appDbContext.Category.FirstOrDefaultAsync(u => u.CategoryId == id);

        if (categoryToRemove != null)
        {
            _appDbContext.Category.Remove(categoryToRemove);
            await _appDbContext.SaveChangesAsync();
            return true; 
        }
        return false; 
    }
    catch (Exception)
    {
        throw new ApplicationException("Error occurred while deleting the category.");
    }
}
public async Task<Category> CreateCategoryService(CreateCategoryDto newcategory)
    {
      try
      {
        var category = _mapper.Map<Category>(newcategory);
         
         await _appDbContext.Category.AddAsync(category);
         await _appDbContext.SaveChangesAsync();
         return category;
      }
      catch (System.Exception)
      {
        
        throw new ApplicationException("erorr ocurred when creat the  category ");
      }
    }
    public async Task<Category> UpdateCategoryService(Guid id, UpdateCategoryDto UpdateCategoryDto)
{
    try
    {
        var existingcategory = await _appDbContext.Category.FindAsync(id);

        if (existingcategory == null)
        {
            throw new ApplicationException("category not found.");
        }
            // BEFOR
        // existingcategory.CategoryName = UpdateCategoryDto.CategoryName ?? existingcategory.CategoryName;
        // existingcategory.Description = UpdateCategoryDto.Description ?? existingcategory.Description;
        // AFTER
        _mapper.Map(UpdateCategoryDto, existingcategory);

        _appDbContext.Category.Update(existingcategory);
        await _appDbContext.SaveChangesAsync();
        return existingcategory;}
    catch (System.Exception)
    {
        throw new ApplicationException("Error occurred when updating the category.");
    }
}}