using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/v1/users")]
public class CategoryController : ControllerBase
{
  private readonly CategoryService _categoryservice;
    public CategoryController(CategoryService categoryservice)
    {
        _categoryservice = categoryservice;
    }
[HttpGet]
    public async Task<IActionResult> GetAllCategory()
    {
        try
        {
            var category = await _categoryservice.GetAllCategoryService();
            var response=new{Message="return all the gategory",Category=category};
        return Ok(response);
        }
        catch (ApplicationException ex)
        {
            
            return StatusCode(500, ex.Message);
        }
        catch (Exception ex){
            
            return StatusCode(500, ex.Message);
        }}

    [HttpGet("{categoryId}")]
public async Task<IActionResult> GetCategoryById(Guid categoryId)
{
    try
    {
        var category = await _categoryservice.GetCategoryByIdService(categoryId);
        
        if (category == null)
        {
            return NotFound(new { Message = "cayegory not found" });
        }
        
        return Ok(category);
    }
    catch (ApplicationException ex)
    {
        return StatusCode(500, ex.Message);
    }
    catch (Exception ex)
    {
        return StatusCode(500, ex.Message);
    }
}

    [HttpDelete("{id}")]
public async Task<IActionResult> DeleteCategory(Guid id)
{
    try
    {
        var result = await _categoryservice.DeleteCategoryByIdService(id);
        if (result)
        {
            return Ok(new { Message = "category deleted successfully" });
        }
        else
        {
            return NotFound(new { Message = "category not found" });
        }}
    catch (ApplicationException ex)
    {
        return StatusCode(500, ex.Message);
    }
    catch (Exception ex)
    {
        return StatusCode(500, ex.Message);
    }}


     [HttpPost]
    public async Task<IActionResult> CreatCategoryDto([FromBody]CreateCategoryDto newcategory)
    {
        try
        {
            var category = await _categoryservice.CreateCategoryService(newcategory);
            var response=new{Message="creat the Category",Category=category};
        return Created($"/api/category/{category.CategoryId}",response);
        }
        catch (ApplicationException ex)
        {
            
            return StatusCode(500, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }}
[HttpPut("{id}")]
public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] CategoryDto updatecategory)
{
    try
    {
        if (updatecategory == null)
        {
            return BadRequest("Invalid user data.");}

        var updatedcategory = await _categoryservice.UpdateCategoryService(id, updatecategory);
        
        if (updatedcategory == null)
        {
            return NotFound("category not found.");
        }

        var response = new { Message = "category updated successfully", Category= updatedcategory };
        return Ok(response);
    }
    catch (ApplicationException ex)
    {
        return StatusCode(500, ex.Message);
    }
    catch (Exception ex)
    {
        return StatusCode(500, ex.Message);
    }
}  
}