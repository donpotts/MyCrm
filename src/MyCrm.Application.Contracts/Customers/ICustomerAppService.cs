using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MyCrm.Customers;

public interface ICustomerAppService :
    ICrudAppService< //Defines CRUD methods
        CustomerDto, //Used to show customers
        int, //Primary key of the customer entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateCustomerDto> //Used to create/update a customer
{
    Task<ListResultDto<AddressLookupDto>> GetAddressLookupAsync();

    Task<ListResultDto<ContactLookupDto>> GetContactLookupAsync();
}
