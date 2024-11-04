using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;


public interface IProductService{
    Task<PaginatedResult<ProductDto>> GetProductsService(QueryParameters queryParameters);
    Task<ProductDto> GetProductByIdService(Guid Id);
    Task<bool> DeleteProductByIdService(Guid id);
    Task<Product> CreateProductService(CreateProductDto newProductDto);
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
public async Task<PaginatedResult<ProductDto>> GetProductsService(QueryParameters queryParameters)
{
    try
    {
        var query = _appDbContext.Product.Include(p => p.Category).AsQueryable();

        if (!string.IsNullOrEmpty(queryParameters.SearchTerm))
        {
            query = query.Where(p => p.Name.Contains(queryParameters.SearchTerm));
        }

        if (!string.IsNullOrEmpty(queryParameters.SortBy))
        {
            switch (queryParameters.SortBy.ToLower())
            {
                case "name":
                    query = queryParameters.SortOrder.ToLower() == "asc"
                        ? query.OrderBy(p => p.Name)
                        : query.OrderByDescending(p => p.Name);
                    break;
                case "price":
                    query = queryParameters.SortOrder.ToLower() == "asc"
                        ? query.OrderBy(p => p.Price)
                        : query.OrderByDescending(p => p.Price);
                    break;
                default:
                    query = query.OrderBy(p => p.Name);
                    break;
            }
        }

        var totalCount = await query.CountAsync();
        var items = await query
            .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)
            .Take(queryParameters.PageSize)
            .ToListAsync();

        var productDtos = items.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            CategoryName = p.Category.CategoryName,
            Description = p.Description,
            Author = p.Author,
            Genre = p.Genre,
            PublicationYear = p.PublicationYear,
            ImageUrl = p.ImageUrl
        }).ToList();

        return new PaginatedResult<ProductDto>
        {
            Items = productDtos,
            TotalCount = totalCount,
            PageNumber = queryParameters.PageNumber,
            PageSize = queryParameters.PageSize,
            SearchBy = queryParameters.SearchTerm,  
            SortBy = queryParameters.SortBy 

            
        };
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error occurred when getting data from the product table:{ex.Message}"); // تسجيل الخطأ
        throw new ApplicationException("Error occurred when getting data from the product table", ex);
    }
}


//     public async Task<PaginatedResult<ProductDto>> GetProductsService(QueryParameters queryParameters)
// {
//     try
//     {
//         // 1. الاستعلام الأساسي على المنتجات وربطها مع الفئات
//         var query = _appDbContext.Product.Include(p => p.Category).AsQueryable();

//         // 2. البحث (Search)
//         if (!string.IsNullOrEmpty(queryParameters.SearchTerm))
//         {
//             query = query.Where(p => p.Name.Contains(queryParameters.SearchTerm));
//         }

//         // 3. الترتيب (Sorting)
//         if (!string.IsNullOrEmpty(queryParameters.SortBy))
//         {
//             switch (queryParameters.SortBy.ToLower())
//             {
//                 case "name":
//                     query = queryParameters.SortOrder.ToLower() == "asc"
//                         ? query.OrderBy(p => p.Name)
//                         : query.OrderByDescending(p => p.Name);
//                     break;
//                 case "price":
//                     query = queryParameters.SortOrder.ToLower() == "asc"
//                         ? query.OrderBy(p => p.Price)
//                         : query.OrderByDescending(p => p.Price);
//                     break;
//                 default:
//                     query = query.OrderBy(p => p.Name); // الترتيب الافتراضي بالاسم
//                     break;
//             }
//         }

//         // 4. إجمالي عدد المنتجات قبل البيجنيشن
//         var totalCount = await query.CountAsync();

//         // 5. تطبيق البيجنيشن: تخطي النتائج السابقة وجلب النتائج المطلوبة فقط
//         var items = await query
//             .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)  // تخطي النتائج السابقة حسب رقم الصفحة
//             .Take(queryParameters.PageSize)  // جلب عدد النتائج المحددة في الصفحة
//             .ToListAsync();

//         // 6. تحويل المنتجات إلى ProductDto
//         var productDtos = items.Select(p => new ProductDto
//         {
//             Id = p.Id,
//             Name = p.Name,
//             Price = p.Price,
//             CategoryName = p.Category.CategoryName,
//              Description = p.Description,
//              Author = p.Author,
//              Genre = p.Genre,
//              PublicationYear = p.PublicationYear

//         }).ToList();

//         // 7. إرجاع النتيجة مع معلومات البيجنيشن
//         return new PaginatedResult<ProductDto>
//         {
//             Items = productDtos,
//             TotalCount = totalCount,
//             PageNumber = queryParameters.PageNumber,
//             PageSize = queryParameters.PageSize
//         };
//     }
//     catch (System.Exception)
//     {
//         throw new ApplicationException("Error occurred when getting data from the product table");
//     }
// }
public async Task<ProductDto> GetProductByIdService(Guid Id)
{
    Console.WriteLine($"befor add {Id}");
    
    try
    {
        var product = await _appDbContext.Product.FindAsync(Id);

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

public async Task<Product> CreateProductService(CreateProductDto newProductDto)
{
    try
    {
        var existingCategory = await _appDbContext.Category
            .FirstOrDefaultAsync(c => c.CategoryId == newProductDto.CategoryId);

        if (existingCategory == null)
        {
            throw new ApplicationException("Category not found.");
        }

        var product = _mapper.Map<Product>(newProductDto);
        
        product.CategoryId = existingCategory.CategoryId;

        await _appDbContext.Product.AddAsync(product);
        await _appDbContext.SaveChangesAsync();

        return product;
    }
    catch (Exception ex)
    {
        throw new ApplicationException($"An error occurred while creating the product: {ex.Message}");
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