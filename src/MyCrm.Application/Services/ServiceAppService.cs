using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using MyCrm.ServiceCategories;
using MyCrm.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.ChangeTracking;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace MyCrm.Services;

[Authorize(MyCrmPermissions.Services.Default)]
public class ServiceAppService :
    CrudAppService<
        Service, //The Service entity
        ServiceDto, //Used to show services
        int, //Primary key of the service entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateServiceDto>, //Used to create/update a service
    IServiceAppService //implement the IServiceAppService
{
    private readonly IRepository<ServiceCategory, int> _serviceCategoryRepository;

    public ServiceAppService(
        IRepository<Service, int> repository,
        IRepository<ServiceCategory, int> serviceCategoryRepository)
        : base(repository)
    {
        _serviceCategoryRepository = serviceCategoryRepository;
        GetPolicyName = MyCrmPermissions.Services.Default;
        GetListPolicyName = MyCrmPermissions.Services.Default;
        CreatePolicyName = MyCrmPermissions.Services.Create;
        UpdatePolicyName = MyCrmPermissions.Services.Edit;
        DeletePolicyName = MyCrmPermissions.Services.Delete;
    }

    [DisableEntityChangeTracking]
    public override async Task<ServiceDto> GetAsync(int id)
    {
        var service = await Repository.GetAsync(id) ?? throw new EntityNotFoundException(typeof(Service), id);

        var serviceDto = ObjectMapper.Map<Service, ServiceDto>(service);
        serviceDto.ServiceCategory = service.ServiceCategory == null ? null : ObjectMapper.Map<ServiceCategory, ServiceCategoryDto>(service.ServiceCategory);
        return serviceDto;
    }

    [DisableEntityChangeTracking]
    public override async Task<PagedResultDto<ServiceDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        var services = await Repository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, input.Sorting ?? nameof(Service.Id), true);

        var serviceDtos = services.Select(x =>
        {
            var serviceDto = ObjectMapper.Map<Service, ServiceDto>(x);
            serviceDto.ServiceCategory = x.ServiceCategory == null ? null : ObjectMapper.Map<ServiceCategory, ServiceCategoryDto>(x.ServiceCategory);
            return serviceDto;
        }).ToList();

        var totalCount = await Repository.GetCountAsync();

        return new PagedResultDto<ServiceDto>(
            totalCount,
            serviceDtos
        );
    }

    public override async Task<ServiceDto> CreateAsync(CreateUpdateServiceDto input)
    {
        var service = ObjectMapper.Map<CreateUpdateServiceDto, Service>(input);

        await Repository.InsertAsync(service, true);

        using (Repository.DisableTracking())
        {
            service = await Repository.GetAsync(service.Id);
            var serviceDto = ObjectMapper.Map<Service, ServiceDto>(service);
            serviceDto.ServiceCategory = service.ServiceCategory == null ? null : ObjectMapper.Map<ServiceCategory, ServiceCategoryDto>(service.ServiceCategory);
            return serviceDto;
        }
    }

    public override async Task<ServiceDto> UpdateAsync(int id, CreateUpdateServiceDto input)
    {
        var service = await Repository.GetAsync(id) ?? throw new EntityNotFoundException(typeof(Service), id);

        ObjectMapper.Map(input, service);

        await Repository.UpdateAsync(service, true);

        using (Repository.DisableTracking())
        {
            service = await Repository.GetAsync(service.Id);
            var serviceDto = ObjectMapper.Map<Service, ServiceDto>(service);
            serviceDto.ServiceCategory = service.ServiceCategory == null ? null : ObjectMapper.Map<ServiceCategory, ServiceCategoryDto>(service.ServiceCategory);
            return serviceDto;
        }
    }

    [DisableEntityChangeTracking]
    public async Task<ListResultDto<ServiceCategoryLookupDto>> GetServiceCategoryLookupAsync()
    {
        var serviceCategories = await _serviceCategoryRepository.GetListAsync();

        return new ListResultDto<ServiceCategoryLookupDto>(
            ObjectMapper.Map<List<ServiceCategory>, List<ServiceCategoryLookupDto>>(serviceCategories)
        );
    }
}
