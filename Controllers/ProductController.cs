using System.Security.Cryptography.X509Certificates;
using ecommerce_db_api.Utilities;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("/api/v1/products")]

public class ProductController : ControllerBase
{

  private readonly ProductService _productService;

  public ProductController(ProductService productService)
  {
    _productService = productService;
  }

  //? GET => /api/products => Get all the products
  [HttpGet]
  public async Task<IActionResult> GetAllProducts()
  {
    try
        {
            var product =await _productService.GetProductsService();
            var response=new{Message="return all the product",Product=product};
        return Ok(response);
        }
        catch (ApplicationException ex)
        {
            
             return ApiResponse.ServerError("server error:"+ ex.Message);

        }
        catch (System.Exception ex){
            
             return ApiResponse.ServerError("server error:"+ ex.Message);
  }}

  //? GET => /api/products/{id} => Get a single product by Id
  [HttpGet("{ProductId}")]
public async Task<IActionResult> GetProductById(Guid ProId)
{
    try
    {
        var pro =await _productService.GetProductByIdService(ProId); 

        if (pro == null)
        {
            return ApiResponse.NotFound("product not found" );
        }
        return ApiResponse.Success(pro,"user is retuned succcessfuly");
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

  // //? Delete => /api/products/{id} => delete a single product by Id
  [HttpDelete("{id}")]
public async Task<IActionResult> DeleteProduct(Guid id)
{
    try
    {
        var result = await _productService.DeleteProductByIdService(id);
        if (result==true)
        {
            return ApiResponse.Success("product deleted successfully" );
        }
        else
        {
            return ApiResponse.NotFound("product not found");
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
    public async Task<IActionResult> CreatUsers([FromBody]CreateProductDto newproduct)
    {
        try
        {
            var product = await _productService.CreateProductService(newproduct);
            var response=new{Message="creat the users",Product=product};
        return ApiResponse.Created(product,"product is created successfully");
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
public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductDto updateProduct)
{
    try
    {
        if (updateProduct == null)
        {
            return ApiResponse.BadRequest("Invalid product data.");}

        var updatedProduct = await _productService.UpdateProductService(id, updateProduct);
        
        if (updatedProduct == null)
        {
            return ApiResponse.NotFound("product not found.");
        }

                return ApiResponse.Success(updateProduct,"product is updated successfuly");

    }
    catch (ApplicationException ex)
    {
             return ApiResponse.ServerError("server error:"+ ex.Message);
    }
    catch (System.Exception ex)
    {
             return ApiResponse.ServerError("server error:"+ ex.Message);
    }}
}