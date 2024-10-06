using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public interface IAddressService{
     Task<PaginatedResult<AddressDto>> GetAllAddressService(QueryParameters queryParameters);
    Task<AddressDto?> GetAddresssByIdService(Guid id);
    Task<Address> CreateAddressService(CreateAddressDto newAddress);
    Task<bool> DeleteAddressByIdService(Guid id);
    Task<Address> UpdateAddressService(Guid id, UpdateAddress updateAddress);
    }

public class AddressService:IAddressService
  {
  private readonly AppDBContext _appDbContext;

  private readonly IMapper _mapper;
public AddressService(AppDBContext appDbContext , IMapper mapper ){
  _appDbContext=appDbContext; 
  _mapper = mapper ; 
}
   public async Task<PaginatedResult<AddressDto>> GetAllAddressService(QueryParameters queryParameters)
{
    try
    {
        // البدء بإنشاء استعلام قابل للتصفية
        var query = _appDbContext.Address.Include(u => u.User).AsQueryable();

        // 1. البحث (Search)
        if (!string.IsNullOrEmpty(queryParameters.SearchTerm))
        {
            query = query.Where(a => a.City.Contains(queryParameters.SearchTerm) || a.State.Contains(queryParameters.SearchTerm));
        }

        // 2. الترتيب (Sorting)
        if (!string.IsNullOrEmpty(queryParameters.SortBy))
        {
            switch (queryParameters.SortBy.ToLower())
            {
                case "city":
                    query = queryParameters.SortOrder.ToLower() == "asc"
                        ? query.OrderBy(a => a.City)
                        : query.OrderByDescending(a => a.City);
                    break;
                case "state":
                    query = queryParameters.SortOrder.ToLower() == "asc"
                        ? query.OrderBy(a => a.State)
                        : query.OrderByDescending(a => a.State);
                    break;
                default:
                    query = query.OrderBy(a => a.City); // الترتيب الافتراضي بالمدينة
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

        // 5. تحويل النتائج إلى AddressDto
        var addressDtos = items.Select(a => new AddressDto
        {
            AddressId = a.AddressId,
            City = a.City,
            State = a.State,
            User = new UserDto
            {
                UserId = a.User.UserId,
                Name = a.User.Name,
                Email = a.User.Email
            }
        }).ToList();

        // 6. إرجاع النتائج مع معلومات البيجينيشن
        return new PaginatedResult<AddressDto>
        {
            Items = addressDtos,
            TotalCount = totalCount,
            PageNumber = queryParameters.PageNumber,
            PageSize = queryParameters.PageSize
        };
    }
    catch (System.Exception)
    {
        throw new ApplicationException("Error occurred when getting data from the address table");
    }
}

    public async Task<AddressDto?> GetAddresssByIdService(Guid id)
{
    try
    {
        var addressEntity = await _appDbContext.Address
            .FirstOrDefaultAsync(d => d.AddressId == id);

        if (addressEntity == null)
        {
            return null; 
        }

        return _mapper.Map<AddressDto>(addressEntity); // تحويل العنوان إلى DTO
    }
    catch (Exception)
    {
        throw new ApplicationException("Error occurred while retrieving the address.");
    }
}
    public async Task<Address> CreateAddressService(CreateAddressDto newAddress)
    {
      // Make sure AddressDto newAddress & Task<Address> I think CreateAddressDto newAddress
      try{
        var address = _mapper.Map<Address>(newAddress);

       // user must add city & state 
      
      //_Address.Add(address);
      await _appDbContext.Address.AddAsync(address);
         await _appDbContext.SaveChangesAsync();
      return address;
    }
     catch (System.Exception)
      {
        
        throw new ApplicationException("erorr ocurred when creat the  address ");
      } }
    public async Task<bool> DeleteAddressByIdService(Guid id)
    {
      try{
      var addressToRemove = await _appDbContext.Address.FirstOrDefaultAsync(adr => adr.AddressId == id);
      
      if (addressToRemove != null)
      {
        _appDbContext.Address.Remove(addressToRemove);
            await _appDbContext.SaveChangesAsync();
            return true;
      }
      return false;
    }
     catch (Exception)
    {
        throw new ApplicationException("Error occurred while deleting the address.");
    }
   } 
    public async Task<Address> UpdateAddressService(Guid id, UpdateAddress updateAddress)
    // Make sure change From (Task<Address>) AddressDto => Address
    // before editung was variable updateAddress from AddressDto
{
    try
    {
      //  var address = _mapper.Map<Address>(updateAddress ,existingaddress );
        var existingaddress = await _appDbContext.Address.FindAsync(id);

        if (existingaddress == null)
        {
            throw new ApplicationException("address not found.");
        }

        // existingaddress.City = updateAddress.City ?? existingaddress.City;
        // existingaddress.State = updateAddress.State ?? existingaddress.State;
       _mapper.Map(updateAddress ,existingaddress );
        _appDbContext.Address.Update(existingaddress);
        await _appDbContext.SaveChangesAsync();

        return existingaddress;
    }
    catch (System.Exception)
    {
        throw new ApplicationException("Error occurred when updating the address.");
    }
}}