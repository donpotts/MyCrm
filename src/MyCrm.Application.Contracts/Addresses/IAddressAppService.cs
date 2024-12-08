using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MyCrm.Addresses;

public interface IAddressAppService :
    ICrudAppService< //Defines CRUD methods
        AddressDto, //Used to show addresses
        int, //Primary key of the address entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateAddressDto> //Used to create/update a address
{
}
