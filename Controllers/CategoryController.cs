using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_db_api.Utilities;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/v1/Category")]
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
            
             return ApiResponse.ServerError("server error:"+ ex.Message);
        }
        catch (System.Exception ex){
            
             return ApiResponse.ServerError("server error:"+ ex.Message);
        }}

    [HttpGet("{categoryId}")]
public async Task<IActionResult> GetCategoryById(Guid categoryId)
{
    try
    {
        var category = await _categoryservice.GetCategoryByIdService(categoryId);
        
        if (category == null)
        {
            return ApiResponse.NotFound( "category not found" );
        }
        
        return ApiResponse.Success(category,"user is retuned succcessfuly");
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

    [HttpDelete("{id}")]
public async Task<IActionResult> DeleteCategory(Guid id)
{
    try
    {
        var result = await _categoryservice.DeleteCategoryByIdService(id);
        if (result==true)
        {
        return ApiResponse.Success("category is deleted successfuly");
        }
        else
        {
            return ApiResponse.NotFound($"categoty with this id {id}dose not exist" );
        }}
    catch (ApplicationException ex)
    {
             return ApiResponse.ServerError("server error:"+ ex.Message);
    }
    catch (System.Exception ex)
    {
             return ApiResponse.ServerError("server error:"+ ex.Message);
    }}


     [HttpPost]
    public async Task<IActionResult> CreatCategoryDto([FromBody]CreateCategoryDto newcategory)
    {
        try
        {
            var category = await _categoryservice.CreateCategoryService(newcategory);
            var response=new{Message="creat the Category",Category=category};
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
public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] CategoryDto updatecategory)
{
    try
    {
        if (updatecategory == null)
        {
            return ApiResponse.BadRequest("Invalid user data.");}

        var updatedcategory = await _categoryservice.UpdateCategoryService(id, updatecategory);
        
        if (updatedcategory == null)
        {
            return ApiResponse.NotFound("category not found.");
        }
                return ApiResponse.Success(updatedcategory,"category is updated successfuly");

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
}