using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public interface IProductService{
    List<ProductDto> GetProductsService();
    Product CreateProductService(CreateProductDto createProduct);
    bool DeleteProductByIdService(Guid id);
    ProductDto? GetProductByIdService(Guid id);
    Task<Product?> UpdateProductService(Guid Id, UpdateProductDto UpdateProductDto);
    
    
} 
public class ProductService: IProductService
{
    private static readonly List<Product> _products = new List<Product>();
    
    public class PaginatedResult<T>
{
    // = new List<Item>();تاكدي بعدين هل يوضع لها ؟ او 
    public List<T>? Items { get; set; }
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
        public string? SearchBy { get; set; }

        public PaginatedResult<ProductDto> GetProductsService(int pageNumber, int pageSize, string? searchBy = null)
    {
        // products => Id, Name, Price, Description, CreatedAt 
        // ProductDto => Id, Name, Price


        var filterProducts = _products.Where(p => 
        string.IsNullOrEmpty(searchBy) || p.Name.Contains(searchBy, StringComparison.OrdinalIgnoreCase));


        var totalFilteredProducts = filterProducts.Count();

   
    var products = filterProducts.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(product => new ProductDto
    {
        Id = product.Id,
        Name = product.Name,
        Price = product.Price
    }).ToList();

   
    var paginatedResult = new PaginatedResult<ProductDto>
    {
        PageSize = pageSize,
        PageNumber = pageNumber,
        SearchBy = searchBy,
        TotalCount = totalFilteredProducts,  
        Items = products
    };

    return paginatedResult;}}

    public List<ProductDto> GetProductsService()
    {
        // products => Id, Name, Price, Description, CreatedAt 
        // ProductDto => Id, Name, Price
        var products = _products.Select(product => new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price
        }).ToList();

        return products;
    }

    public Product CreateProductService(CreateProductDto createProduct)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = createProduct.Name,
            Price = createProduct.Price,
            CreatedAt = DateTime.Now,
        };
        _products.Add(product);
        return product;
    }

    public bool DeleteProductByIdService(Guid id)
    {
        // Find the product 
        var product = _products.FirstOrDefault(product => product.Id == id);

        if (product == null)
        {
            return false;
        }

        _products.Remove(product);
        return true;
    }


    public ProductDto? GetProductByIdService(Guid id)
    {
        var product = _products.FirstOrDefault(product => product.Id == id);

        if (product == null)
        {
            return null;
        }

        // create the return dto 
        var productData = new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price
        };

        return productData;

    }

    public async Task<Product?> UpdateProductService(Guid Id, UpdateProductDto UpdateProductDto)
  {
    var existingProduct = _products.FirstOrDefault(u => u.Id == Id);
    if (existingProduct != null)
    {
        // check if update product has name value or not, if yes, then get the value of update productdto
        // if not, get existing product nam
      existingProduct.Name = UpdateProductDto.Name ?? existingProduct.Name;
     existingProduct.Price = UpdateProductDto.Price ?? existingProduct.Price;
      
      
    }
     return await Task.FromResult(existingProduct);
  }
}
// need to check which properites that user wanna update 
// in case user only update FirstName, then we will take the update value of FirstName from user 
// and the others will be the same