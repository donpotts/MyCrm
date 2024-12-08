using System;
using MyCrm.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace MyCrm.Addresses;

public class AddressAppService :
    CrudAppService<
        Address, //The Address entity
        AddressDto, //Used to show addresses
        int, //Primary key of the address entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateAddressDto>, //Used to create/update a address
    IAddressAppService //implement the IAddressAppService
{
    public AddressAppService(IRepository<Address, int> repository)
        : base(repository)
    {
        GetPolicyName = MyCrmPermissions.Addresses.Default;
        GetListPolicyName = MyCrmPermissions.Addresses.Default;
        CreatePolicyName = MyCrmPermissions.Addresses.Create;
        UpdatePolicyName = MyCrmPermissions.Addresses.Edit;
        DeletePolicyName = MyCrmPermissions.Addresses.Delete;
    }
}
