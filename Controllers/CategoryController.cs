using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/v1/users")]
public class CategoryController : ControllerBase
{
  private readonly CategoryService _categoryService;
    public CategoryController()
    {
        _categoryService = new CategoryService();
    }
[HttpGet]
    public IActionResult GetAllCategory()
    {
        var category = _categoryService.GetAllCategoryService();
        return Ok(category);
    }
    [HttpGet("{categoryId}")]
    public IActionResult GetSingleCategoryById(string categoryId)
    {
        if (!Guid.TryParse(categoryId, out Guid categoryIdGuid))
        {
            return BadRequest("Invalid user ID Format");
        }

        var category = _categoryService.GetCategoryByIdService(categoryIdGuid);

        if (category == null)
        {
            return NotFound($"User with {categoryId} does not exist");
        }
        return Ok(category);
    }


    [HttpDelete("{categoryId}")]
    public IActionResult DeleteCategoryByIdService(string categoryId)
    {
        if (!Guid.TryParse(categoryId, out Guid categoryIdGuid))
        {
            return BadRequest("Invalid category ID Format");
        }

        bool result = _categoryService.DeleteCategoryByIdService(categoryIdGuid);

        if (!result)
        {
            return NotFound($"User with {categoryId} does not exist");
        }
        return NoContent();
    }


     [HttpPost]
    public IActionResult CreateCategory(CreateCategoryDto newcatogery)
    {
          if (!CategoryValidation.isValidName(newcatogery.Name)) // "test"
    {
      return BadRequest("Name can not be empty");
    }
           var category = _categoryService.CreateCategoryService(newcatogery);


        if (category == null)
        {
            return NotFound($"Category could not be created");
        }
        return Created("created category", category);
    }
[HttpPut("{id}")]
  public IActionResult UpdateCategory(string id, CategoryDto updateCategory)
{
    if (!Guid.TryParse( id, out Guid categoryId))
    {
        return BadRequest("Invalid product ID Format");
    }

    var category = _categoryService.GetCategoryByIdService(categoryId);

    if (category == null)
    {
        return NotFound($"category with {id} not found");
    }

    if (updateCategory.Name != null && !CategoryValidation.isValidName(updateCategory.Name))
    {
        return BadRequest("Name cannot be empty");
    }

    // if (updateProduct.Price && !ProductValidation.isValidPrice(updateProduct.Price))
    // {
    //     return BadRequest("Price cannot be negative");
    // }

  
    _categoryService.UpdateUserService( categoryId, updateCategory);

    return NoContent(); 
}
}