using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MyCrm.Contacts;

public interface IContactAppService :
    ICrudAppService< //Defines CRUD methods
        ContactDto, //Used to show contacts
        int, //Primary key of the contact entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateContactDto> //Used to create/update a contact
{
    Task<ListResultDto<AddressLookupDto>> GetAddressLookupAsync();

    Task<ListResultDto<RewardLookupDto>> GetRewardLookupAsync();
}
