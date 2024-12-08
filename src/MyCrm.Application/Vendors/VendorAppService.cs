using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using MyCrm.Addresses;
using MyCrm.Services;
using MyCrm.Products;
using MyCrm.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.ChangeTracking;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace MyCrm.Vendors;

[Authorize(MyCrmPermissions.Vendors.Default)]
public class VendorAppService :
    CrudAppService<
        Vendor, //The Vendor entity
        VendorDto, //Used to show vendors
        int, //Primary key of the vendor entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateVendorDto>, //Used to create/update a vendor
    IVendorAppService //implement the IVendorAppService
{
    private readonly IRepository<Address, int> _addressRepository;
    private readonly IRepository<Service, int> _serviceRepository;
    private readonly IRepository<Product, int> _productRepository;

    public VendorAppService(
        IRepository<Vendor, int> repository,
        IRepository<Address, int> addressRepository,
        IRepository<Service, int> serviceRepository,
        IRepository<Product, int> productRepository)
        : base(repository)
    {
        _addressRepository = addressRepository;
        _serviceRepository = serviceRepository;
        _productRepository = productRepository;
        GetPolicyName = MyCrmPermissions.Vendors.Default;
        GetListPolicyName = MyCrmPermissions.Vendors.Default;
        CreatePolicyName = MyCrmPermissions.Vendors.Create;
        UpdatePolicyName = MyCrmPermissions.Vendors.Edit;
        DeletePolicyName = MyCrmPermissions.Vendors.Delete;
    }

    [DisableEntityChangeTracking]
    public override async Task<VendorDto> GetAsync(int id)
    {
        var vendor = await Repository.GetAsync(id) ?? throw new EntityNotFoundException(typeof(Vendor), id);

        var vendorDto = ObjectMapper.Map<Vendor, VendorDto>(vendor);
        vendorDto.Address = vendor.Address == null ? null : ObjectMapper.Map<Address, AddressDto>(vendor.Address);
        vendorDto.Service = vendor.Service == null ? null : ObjectMapper.Map<Service, ServiceDto>(vendor.Service);
        vendorDto.Product = vendor.Product == null ? null : ObjectMapper.Map<Product, ProductDto>(vendor.Product);
        return vendorDto;
    }

    [DisableEntityChangeTracking]
    public override async Task<PagedResultDto<VendorDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        var vendors = await Repository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, input.Sorting ?? nameof(Vendor.Id), true);

        var vendorDtos = vendors.Select(x =>
        {
            var vendorDto = ObjectMapper.Map<Vendor, VendorDto>(x);
            vendorDto.Address = x.Address == null ? null : ObjectMapper.Map<Address, AddressDto>(x.Address);
            vendorDto.Service = x.Service == null ? null : ObjectMapper.Map<Service, ServiceDto>(x.Service);
            vendorDto.Product = x.Product == null ? null : ObjectMapper.Map<Product, ProductDto>(x.Product);
            return vendorDto;
        }).ToList();

        var totalCount = await Repository.GetCountAsync();

        return new PagedResultDto<VendorDto>(
            totalCount,
            vendorDtos
        );
    }

    public override async Task<VendorDto> CreateAsync(CreateUpdateVendorDto input)
    {
        var vendor = ObjectMapper.Map<CreateUpdateVendorDto, Vendor>(input);

        await Repository.InsertAsync(vendor, true);

        using (Repository.DisableTracking())
        {
            vendor = await Repository.GetAsync(vendor.Id);
            var vendorDto = ObjectMapper.Map<Vendor, VendorDto>(vendor);
            vendorDto.Address = vendor.Address == null ? null : ObjectMapper.Map<Address, AddressDto>(vendor.Address);
            vendorDto.Service = vendor.Service == null ? null : ObjectMapper.Map<Service, ServiceDto>(vendor.Service);
            vendorDto.Product = vendor.Product == null ? null : ObjectMapper.Map<Product, ProductDto>(vendor.Product);
            return vendorDto;
        }
    }

    public override async Task<VendorDto> UpdateAsync(int id, CreateUpdateVendorDto input)
    {
        var vendor = await Repository.GetAsync(id) ?? throw new EntityNotFoundException(typeof(Vendor), id);

        ObjectMapper.Map(input, vendor);

        await Repository.UpdateAsync(vendor, true);

        using (Repository.DisableTracking())
        {
            vendor = await Repository.GetAsync(vendor.Id);
            var vendorDto = ObjectMapper.Map<Vendor, VendorDto>(vendor);
            vendorDto.Address = vendor.Address == null ? null : ObjectMapper.Map<Address, AddressDto>(vendor.Address);
            vendorDto.Service = vendor.Service == null ? null : ObjectMapper.Map<Service, ServiceDto>(vendor.Service);
            vendorDto.Product = vendor.Product == null ? null : ObjectMapper.Map<Product, ProductDto>(vendor.Product);
            return vendorDto;
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
    public async Task<ListResultDto<ServiceLookupDto>> GetServiceLookupAsync()
    {
        var services = await _serviceRepository.GetListAsync();

        return new ListResultDto<ServiceLookupDto>(
            ObjectMapper.Map<List<Service>, List<ServiceLookupDto>>(services)
        );
    }

    [DisableEntityChangeTracking]
    public async Task<ListResultDto<ProductLookupDto>> GetProductLookupAsync()
    {
        var products = await _productRepository.GetListAsync();

        return new ListResultDto<ProductLookupDto>(
            ObjectMapper.Map<List<Product>, List<ProductLookupDto>>(products)
        );
    }
}
