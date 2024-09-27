using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public interface ICategoryService{
    List<CategoryDto> GetAllCategoryService();
    CategoryDto? GetCategoryByIdService(Guid id);
    CategoryDto? CreateCategoryService(CreateCategoryDto newCategory);
    bool DeleteCategoryByIdService(Guid id);
    Task<CategoryDto?> UpdateUserService(Guid Id, CategoryDto UpdateCategoryDto);
    }
public class CategoryService: ICategoryService
  {

    private static List<CategoryDto> _category = new List<CategoryDto>();
      
public class PaginatedResult<T>
{
    // = new List<Item>();تاكدي بعدين هل يوضع لها ؟ او 
    public List<T>? Items { get; set; }
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
        public string? SearchBy { get; set; }

        public PaginatedResult<CategoryDto> GetCategoryService(int pageNumber, int pageSize, string? searchBy = null)
    {
        // products => Id, Name, Price, Description, CreatedAt 
        // ProductDto => Id, Name, Price


        var filterCategory = _category.Where(p => 
        string.IsNullOrEmpty(searchBy) || p.Name.Contains(searchBy, StringComparison.OrdinalIgnoreCase));


        var totalFilteredCategory = filterCategory.Count();

   
    var Category = filterCategory.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(Category => new CategoryDto
    {
        CategoryId = Category.CategoryId,
        Name = Category.Name,
        Description = Category.Description
    }).ToList();

   
    var paginatedResult = new PaginatedResult<CategoryDto>
    {
        PageSize = pageSize,
        PageNumber = pageNumber,
        SearchBy = searchBy,
        TotalCount = totalFilteredCategory,  
        Items = Category
    };

    return paginatedResult;}}
    public List<CategoryDto> GetAllCategoryService()
    {
      return _category;
    }
    public CategoryDto? GetCategoryByIdService(Guid id)
    {
      var foundCategory = _category.Find(user => user.CategoryId == id);
      return foundCategory;
    }
    public CategoryDto? CreateCategoryService(CreateCategoryDto newCategory)
    {
      var category = new CategoryDto {
        CategoryId = Guid.NewGuid(),
        Name = newCategory.Name,};
      if(category == null){
        return null;
      }
      _category.Add(category);
      return category;
    }
    public bool DeleteCategoryByIdService(Guid id)
    {
      var categoryToRemove = _category.FirstOrDefault(u => u.CategoryId == id);
      if (categoryToRemove != null)
      {
        _category.Remove(categoryToRemove);
        return true;
      }
      return false;
    
    }
    public async Task<CategoryDto?> UpdateUserService(Guid Id, CategoryDto UpdateCategoryDto)
  {
    var existingCategory = _category.FirstOrDefault(u => u.CategoryId == Id);
    if (existingCategory != null)
    {
  
      existingCategory.Name = UpdateCategoryDto.Name ?? existingCategory.Name;
     existingCategory.Description = UpdateCategoryDto.Description ?? existingCategory.Description;
     
      
    }
     return await Task.FromResult(existingCategory);
  }
    }
  