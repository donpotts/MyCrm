using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MyCrm.Services;

public interface IServiceAppService :
    ICrudAppService< //Defines CRUD methods
        ServiceDto, //Used to show services
        int, //Primary key of the service entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateServiceDto> //Used to create/update a service
{
    Task<ListResultDto<ServiceCategoryLookupDto>> GetServiceCategoryLookupAsync();
}
