using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;


public interface IProductService{
    Task<List<ProductDto>> GetProductsService();
    Task<ProductDto?> GetProductByIdService(Guid Id);
    Task<bool> DeleteProductByIdService(Guid id);
    Task<Product> CreateProductService(CreateProductDto newproduct);
     Task<Product> UpdateProductService(Guid id, UpdateProductDto updateProduct);
    
    
} 
public class ProductService:IProductService
{
    private readonly AppDBContext _appDbContext;
private readonly IMapper _mapper;
public ProductService(AppDBContext appDbContext,IMapper mapper){
    _mapper = mapper;
  _appDbContext=appDbContext;
}     
    
//     public class PaginatedResult<T>
// {
//     // = new List<Item>();تاكدي بعدين هل يوضع لها ؟ او 
//     public List<T>? Items { get; set; }
//     public int TotalCount { get; set; }
//     public int PageNumber { get; set; }
//     public int PageSize { get; set; }
//         public string? SearchBy { get; set; }

//         public async Task<PaginatedResult<ProductDto>> GetProductsService(int pageNumber, int pageSize, string? searchBy = null)
//     {
//         var products = await _appDbContext.Products.ToListAsync();

//         var filteredProducts = products.Where(p =>
//             string.IsNullOrEmpty(searchBy) || p.Name.Contains(searchBy, StringComparison.OrdinalIgnoreCase));

//         var totalFilteredProducts = filteredProducts.Count();

   
//     var paginatedProducts = filteredProducts.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(product => new ProductDto
//         {
//             Id = product.Id,
//             Name = product.Name,
//             Price = product.Price
//         }).ToList();

   
//     var paginatedResult = new PaginatedResult<ProductDto>
//         {
//             PageSize = pageSize,
//             PageNumber = pageNumber,
//             SearchBy = searchBy,
//             TotalCount = totalFilteredProducts,
//             Items = paginatedProducts
//         };

//     return paginatedResult;}}

    public async Task<List<ProductDto>> GetProductsService()
{
    try
    {
        var products = await _appDbContext.Product.ToListAsync();
        
        var productDtos = products.Select(p => new ProductDto
        {
            Id = p.Id, 
            Name = p.Name,
            Price = p.Price,
        }).ToList();

        return productDtos;
    }
    catch (System.Exception)
    {
        throw new ApplicationException("Error occurred when getting data from the product table");
    }
}
public async Task<ProductDto?> GetProductByIdService(Guid Id)
{
    try
    {
        var product = await _appDbContext.Product
            .FirstOrDefaultAsync(u => u.Id == Id);

        if (product == null)
        {
            return null; // Return null if product not found
        }

        // Convert product to productDto if needed
        return _mapper.Map<ProductDto>(product);

        
    }
    catch (Exception)
    {
        throw new ApplicationException("Error occurred while retrieving the product.");
    }
}

public async Task<bool> DeleteProductByIdService(Guid id)
{
    try
    {
        var productToRemove = await _appDbContext.Product.FirstOrDefaultAsync(u => u.Id == id);

        if (productToRemove != null)
        {
            _appDbContext.Product.Remove(productToRemove);
            await _appDbContext.SaveChangesAsync();
            return true; 
        }
        return false; 
    }
    catch (Exception)
    {
        throw new ApplicationException("Error occurred while deleting the product.");
    }
}

public async Task<Product> CreateProductService(CreateProductDto newproduct)
    {
      try
      {
        var product = _mapper.Map<Product>(newproduct);
         await _appDbContext.Product.AddAsync(product);
         await _appDbContext.SaveChangesAsync();
         return product;
      }
      catch (System.Exception)
      {
        
        throw new ApplicationException("erorr ocurred when creat the  product ");
      }
    }
   public async Task<Product> UpdateProductService(Guid id, UpdateProductDto updateProduct)
{
    try
    {
        var existingProduct = await _appDbContext.Product.FindAsync(id);

        if (existingProduct == null)
        {
            throw new ApplicationException("Product not found.");
        }

        // existingProduct.Name = updateProduct.Name ?? existingProduct.Name;
        // existingProduct.Price = updateProduct.Price ?? existingProduct.Price;
        _mapper.Map(updateProduct,existingProduct);
        

        _appDbContext.Product.Update(existingProduct);
        await _appDbContext.SaveChangesAsync();

        return existingProduct;
    }
    catch (System.Exception)
    {
        throw new ApplicationException("Error occurred when updating the user.");
    }
}


}

// need to check which properites that user wanna update 
// in case user only update FirstName, then we will take the update value of FirstName from user 
// and the others will be the same