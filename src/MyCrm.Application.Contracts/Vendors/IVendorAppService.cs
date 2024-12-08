using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MyCrm.Vendors;

public interface IVendorAppService :
    ICrudAppService< //Defines CRUD methods
        VendorDto, //Used to show vendors
        int, //Primary key of the vendor entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateVendorDto> //Used to create/update a vendor
{
    Task<ListResultDto<AddressLookupDto>> GetAddressLookupAsync();

    Task<ListResultDto<ServiceLookupDto>> GetServiceLookupAsync();

    Task<ListResultDto<ProductLookupDto>> GetProductLookupAsync();
}
