using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using MyCrm.Addresses;
using MyCrm.Contacts;
using MyCrm.Opportunities;
using MyCrm.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.ChangeTracking;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace MyCrm.Leads;

[Authorize(MyCrmPermissions.Leads.Default)]
public class LeadAppService :
    CrudAppService<
        Lead, //The Lead entity
        LeadDto, //Used to show leads
        int, //Primary key of the lead entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateLeadDto>, //Used to create/update a lead
    ILeadAppService //implement the ILeadAppService
{
    private readonly IRepository<Address, int> _addressRepository;
    private readonly IRepository<Contact, int> _contactRepository;
    private readonly IRepository<Opportunity, int> _opportunityRepository;

    public LeadAppService(
        IRepository<Lead, int> repository,
        IRepository<Address, int> addressRepository,
        IRepository<Contact, int> contactRepository,
        IRepository<Opportunity, int> opportunityRepository)
        : base(repository)
    {
        _addressRepository = addressRepository;
        _contactRepository = contactRepository;
        _opportunityRepository = opportunityRepository;
        GetPolicyName = MyCrmPermissions.Leads.Default;
        GetListPolicyName = MyCrmPermissions.Leads.Default;
        CreatePolicyName = MyCrmPermissions.Leads.Create;
        UpdatePolicyName = MyCrmPermissions.Leads.Edit;
        DeletePolicyName = MyCrmPermissions.Leads.Delete;
    }

    [DisableEntityChangeTracking]
    public override async Task<LeadDto> GetAsync(int id)
    {
        var lead = await Repository.GetAsync(id) ?? throw new EntityNotFoundException(typeof(Lead), id);

        var leadDto = ObjectMapper.Map<Lead, LeadDto>(lead);
        leadDto.Address = lead.Address == null ? null : ObjectMapper.Map<Address, AddressDto>(lead.Address);
        leadDto.Contact = lead.Contact == null ? null : ObjectMapper.Map<Contact, ContactDto>(lead.Contact);
        leadDto.Opportunity = lead.Opportunity == null ? null : ObjectMapper.Map<Opportunity, OpportunityDto>(lead.Opportunity);
        return leadDto;
    }

    [DisableEntityChangeTracking]
    public override async Task<PagedResultDto<LeadDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        var leads = await Repository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, input.Sorting ?? nameof(Lead.Id), true);

        var leadDtos = leads.Select(x =>
        {
            var leadDto = ObjectMapper.Map<Lead, LeadDto>(x);
            leadDto.Address = x.Address == null ? null : ObjectMapper.Map<Address, AddressDto>(x.Address);
            leadDto.Contact = x.Contact == null ? null : ObjectMapper.Map<Contact, ContactDto>(x.Contact);
            leadDto.Opportunity = x.Opportunity == null ? null : ObjectMapper.Map<Opportunity, OpportunityDto>(x.Opportunity);
            return leadDto;
        }).ToList();

        var totalCount = await Repository.GetCountAsync();

        return new PagedResultDto<LeadDto>(
            totalCount,
            leadDtos
        );
    }

    public override async Task<LeadDto> CreateAsync(CreateUpdateLeadDto input)
    {
        var lead = ObjectMapper.Map<CreateUpdateLeadDto, Lead>(input);

        await Repository.InsertAsync(lead, true);

        using (Repository.DisableTracking())
        {
            lead = await Repository.GetAsync(lead.Id);
            var leadDto = ObjectMapper.Map<Lead, LeadDto>(lead);
            leadDto.Address = lead.Address == null ? null : ObjectMapper.Map<Address, AddressDto>(lead.Address);
            leadDto.Contact = lead.Contact == null ? null : ObjectMapper.Map<Contact, ContactDto>(lead.Contact);
            leadDto.Opportunity = lead.Opportunity == null ? null : ObjectMapper.Map<Opportunity, OpportunityDto>(lead.Opportunity);
            return leadDto;
        }
    }

    public override async Task<LeadDto> UpdateAsync(int id, CreateUpdateLeadDto input)
    {
        var lead = await Repository.GetAsync(id) ?? throw new EntityNotFoundException(typeof(Lead), id);

        ObjectMapper.Map(input, lead);

        await Repository.UpdateAsync(lead, true);

        using (Repository.DisableTracking())
        {
            lead = await Repository.GetAsync(lead.Id);
            var leadDto = ObjectMapper.Map<Lead, LeadDto>(lead);
            leadDto.Address = lead.Address == null ? null : ObjectMapper.Map<Address, AddressDto>(lead.Address);
            leadDto.Contact = lead.Contact == null ? null : ObjectMapper.Map<Contact, ContactDto>(lead.Contact);
            leadDto.Opportunity = lead.Opportunity == null ? null : ObjectMapper.Map<Opportunity, OpportunityDto>(lead.Opportunity);
            return leadDto;
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
    public async Task<ListResultDto<ContactLookupDto>> GetContactLookupAsync()
    {
        var contacts = await _contactRepository.GetListAsync();

        return new ListResultDto<ContactLookupDto>(
            ObjectMapper.Map<List<Contact>, List<ContactLookupDto>>(contacts)
        );
    }

    [DisableEntityChangeTracking]
    public async Task<ListResultDto<OpportunityLookupDto>> GetOpportunityLookupAsync()
    {
        var opportunities = await _opportunityRepository.GetListAsync();

        return new ListResultDto<OpportunityLookupDto>(
            ObjectMapper.Map<List<Opportunity>, List<OpportunityLookupDto>>(opportunities)
        );
    }
}
