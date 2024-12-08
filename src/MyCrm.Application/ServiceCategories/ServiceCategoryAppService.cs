using System;
using MyCrm.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace MyCrm.ServiceCategories;

public class ServiceCategoryAppService :
    CrudAppService<
        ServiceCategory, //The ServiceCategory entity
        ServiceCategoryDto, //Used to show service categories
        int, //Primary key of the service category entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateServiceCategoryDto>, //Used to create/update a service category
    IServiceCategoryAppService //implement the IServiceCategoryAppService
{
    public ServiceCategoryAppService(IRepository<ServiceCategory, int> repository)
        : base(repository)
    {
        GetPolicyName = MyCrmPermissions.ServiceCategories.Default;
        GetListPolicyName = MyCrmPermissions.ServiceCategories.Default;
        CreatePolicyName = MyCrmPermissions.ServiceCategories.Create;
        UpdatePolicyName = MyCrmPermissions.ServiceCategories.Edit;
        DeletePolicyName = MyCrmPermissions.ServiceCategories.Delete;
    }
}
