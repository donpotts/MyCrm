using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using MyCrm.Addresses;
using MyCrm.Contacts;
using MyCrm.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.ChangeTracking;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace MyCrm.Customers;

[Authorize(MyCrmPermissions.Customers.Default)]
public class CustomerAppService :
    CrudAppService<
        Customer, //The Customer entity
        CustomerDto, //Used to show customers
        int, //Primary key of the customer entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateCustomerDto>, //Used to create/update a customer
    ICustomerAppService //implement the ICustomerAppService
{
    private readonly IRepository<Address, int> _addressRepository;
    private readonly IRepository<Contact, int> _contactRepository;

    public CustomerAppService(
        IRepository<Customer, int> repository,
        IRepository<Address, int> addressRepository,
        IRepository<Contact, int> contactRepository)
        : base(repository)
    {
        _addressRepository = addressRepository;
        _contactRepository = contactRepository;
        GetPolicyName = MyCrmPermissions.Customers.Default;
        GetListPolicyName = MyCrmPermissions.Customers.Default;
        CreatePolicyName = MyCrmPermissions.Customers.Create;
        UpdatePolicyName = MyCrmPermissions.Customers.Edit;
        DeletePolicyName = MyCrmPermissions.Customers.Delete;
    }

    [DisableEntityChangeTracking]
    public override async Task<CustomerDto> GetAsync(int id)
    {
        var customer = await Repository.GetAsync(id) ?? throw new EntityNotFoundException(typeof(Customer), id);

        var customerDto = ObjectMapper.Map<Customer, CustomerDto>(customer);
        customerDto.Address = customer.Address == null ? null : ObjectMapper.Map<Address, AddressDto>(customer.Address);
        customerDto.Contact = customer.Contact == null ? null : ObjectMapper.Map<Contact, ContactDto>(customer.Contact);
        return customerDto;
    }

    [DisableEntityChangeTracking]
    public override async Task<PagedResultDto<CustomerDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        var customers = await Repository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, input.Sorting ?? nameof(Customer.Id), true);

        var customerDtos = customers.Select(x =>
        {
            var customerDto = ObjectMapper.Map<Customer, CustomerDto>(x);
            customerDto.Address = x.Address == null ? null : ObjectMapper.Map<Address, AddressDto>(x.Address);
            customerDto.Contact = x.Contact == null ? null : ObjectMapper.Map<Contact, ContactDto>(x.Contact);
            return customerDto;
        }).ToList();

        var totalCount = await Repository.GetCountAsync();

        return new PagedResultDto<CustomerDto>(
            totalCount,
            customerDtos
        );
    }

    public override async Task<CustomerDto> CreateAsync(CreateUpdateCustomerDto input)
    {
        var customer = ObjectMapper.Map<CreateUpdateCustomerDto, Customer>(input);

        await Repository.InsertAsync(customer, true);

        using (Repository.DisableTracking())
        {
            customer = await Repository.GetAsync(customer.Id);
            var customerDto = ObjectMapper.Map<Customer, CustomerDto>(customer);
            customerDto.Address = customer.Address == null ? null : ObjectMapper.Map<Address, AddressDto>(customer.Address);
            customerDto.Contact = customer.Contact == null ? null : ObjectMapper.Map<Contact, ContactDto>(customer.Contact);
            return customerDto;
        }
    }

    public override async Task<CustomerDto> UpdateAsync(int id, CreateUpdateCustomerDto input)
    {
        var customer = await Repository.GetAsync(id) ?? throw new EntityNotFoundException(typeof(Customer), id);

        ObjectMapper.Map(input, customer);

        await Repository.UpdateAsync(customer, true);

        using (Repository.DisableTracking())
        {
            customer = await Repository.GetAsync(customer.Id);
            var customerDto = ObjectMapper.Map<Customer, CustomerDto>(customer);
            customerDto.Address = customer.Address == null ? null : ObjectMapper.Map<Address, AddressDto>(customer.Address);
            customerDto.Contact = customer.Contact == null ? null : ObjectMapper.Map<Contact, ContactDto>(customer.Contact);
            return customerDto;
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
}
