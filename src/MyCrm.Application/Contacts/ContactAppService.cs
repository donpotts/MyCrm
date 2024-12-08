using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using MyCrm.Addresses;
using MyCrm.Rewards;
using MyCrm.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.ChangeTracking;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace MyCrm.Contacts;

[Authorize(MyCrmPermissions.Contacts.Default)]
public class ContactAppService :
    CrudAppService<
        Contact, //The Contact entity
        ContactDto, //Used to show contacts
        int, //Primary key of the contact entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateContactDto>, //Used to create/update a contact
    IContactAppService //implement the IContactAppService
{
    private readonly IRepository<Address, int> _addressRepository;
    private readonly IRepository<Reward, int> _rewardRepository;

    public ContactAppService(
        IRepository<Contact, int> repository,
        IRepository<Address, int> addressRepository,
        IRepository<Reward, int> rewardRepository)
        : base(repository)
    {
        _addressRepository = addressRepository;
        _rewardRepository = rewardRepository;
        GetPolicyName = MyCrmPermissions.Contacts.Default;
        GetListPolicyName = MyCrmPermissions.Contacts.Default;
        CreatePolicyName = MyCrmPermissions.Contacts.Create;
        UpdatePolicyName = MyCrmPermissions.Contacts.Edit;
        DeletePolicyName = MyCrmPermissions.Contacts.Delete;
    }

    [DisableEntityChangeTracking]
    public override async Task<ContactDto> GetAsync(int id)
    {
        var contact = await Repository.GetAsync(id) ?? throw new EntityNotFoundException(typeof(Contact), id);

        var contactDto = ObjectMapper.Map<Contact, ContactDto>(contact);
        contactDto.Address = contact.Address == null ? null : ObjectMapper.Map<Address, AddressDto>(contact.Address);
        contactDto.Reward = contact.Reward == null ? null : ObjectMapper.Map<Reward, RewardDto>(contact.Reward);
        return contactDto;
    }

    [DisableEntityChangeTracking]
    public override async Task<PagedResultDto<ContactDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        var contacts = await Repository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, input.Sorting ?? nameof(Contact.Id), true);

        var contactDtos = contacts.Select(x =>
        {
            var contactDto = ObjectMapper.Map<Contact, ContactDto>(x);
            contactDto.Address = x.Address == null ? null : ObjectMapper.Map<Address, AddressDto>(x.Address);
            contactDto.Reward = x.Reward == null ? null : ObjectMapper.Map<Reward, RewardDto>(x.Reward);
            return contactDto;
        }).ToList();

        var totalCount = await Repository.GetCountAsync();

        return new PagedResultDto<ContactDto>(
            totalCount,
            contactDtos
        );
    }

    public override async Task<ContactDto> CreateAsync(CreateUpdateContactDto input)
    {
        var contact = ObjectMapper.Map<CreateUpdateContactDto, Contact>(input);

        await Repository.InsertAsync(contact, true);

        using (Repository.DisableTracking())
        {
            contact = await Repository.GetAsync(contact.Id);
            var contactDto = ObjectMapper.Map<Contact, ContactDto>(contact);
            contactDto.Address = contact.Address == null ? null : ObjectMapper.Map<Address, AddressDto>(contact.Address);
            contactDto.Reward = contact.Reward == null ? null : ObjectMapper.Map<Reward, RewardDto>(contact.Reward);
            return contactDto;
        }
    }

    public override async Task<ContactDto> UpdateAsync(int id, CreateUpdateContactDto input)
    {
        var contact = await Repository.GetAsync(id) ?? throw new EntityNotFoundException(typeof(Contact), id);

        ObjectMapper.Map(input, contact);

        await Repository.UpdateAsync(contact, true);

        using (Repository.DisableTracking())
        {
            contact = await Repository.GetAsync(contact.Id);
            var contactDto = ObjectMapper.Map<Contact, ContactDto>(contact);
            contactDto.Address = contact.Address == null ? null : ObjectMapper.Map<Address, AddressDto>(contact.Address);
            contactDto.Reward = contact.Reward == null ? null : ObjectMapper.Map<Reward, RewardDto>(contact.Reward);
            return contactDto;
        }
    }

    [DisableEntityChangeTracking]
    public async Task<ListResultDto<AddressLookupDto>> GetAddressLookupAsync()
    {
        var addresses = await _addressRepository.GetListAsync();

        return new ListResultDto<AddressLookupDto>(
            ObjectMapper.Map<List<Address>, List<AddressLookupDto>>(addresses)
        );
    }

    [DisableEntityChangeTracking]
    public async Task<ListResultDto<RewardLookupDto>> GetRewardLookupAsync()
    {
        var rewards = await _rewardRepository.GetListAsync();

        return new ListResultDto<RewardLookupDto>(
            ObjectMapper.Map<List<Reward>, List<RewardLookupDto>>(rewards)
        );
    }
}
