using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("/api/products")]

public class ProductController : ControllerBase
{

  private readonly IProductService _productService;

  public ProductController(IProductService productService)
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
            
            return StatusCode(500, ex.Message);
        }
        catch (Exception ex){
            
            return StatusCode(500, ex.Message);
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
            return NotFound(new { Message = "product not found" });
        }

        return Ok(pro);
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

  // //? Delete => /api/products/{id} => delete a single product by Id
  [HttpDelete("{id}")]
public async Task<IActionResult> DeleteProduct(Guid id)
{
    try
    {
        var result = await _productService.DeleteProductByIdService(id);
        if (result)
        {
            return Ok(new { Message = "product deleted successfully" });
        }
        else
        {
            return NotFound(new { Message = "product not found" });
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
    public async Task<IActionResult> CreatUsers([FromBody]CreateProductDto newproduct)
    {
        try
        {
            var product = await _productService.CreateProductService(newproduct);
            var response=new{Message="creat the users",Product=product};
        return Created($"/api/product/{product.Id}",response);
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
public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductDto updateProduct)
{
    try
    {
        if (updateProduct == null)
        {
            return BadRequest("Invalid product data.");}

        var updatedProduct = await _productService.UpdateProductService(id, updateProduct);
        
        if (updatedProduct == null)
        {
            return NotFound("product not found.");
        }

        var response = new { Message = "Product updated successfully", Product = updatedProduct };
        return Ok(response);
    }
    catch (ApplicationException ex)
    {
        return StatusCode(500, ex.Message);
    }
    catch (Exception ex)
    {
        return StatusCode(500, ex.Message);
    }}
}