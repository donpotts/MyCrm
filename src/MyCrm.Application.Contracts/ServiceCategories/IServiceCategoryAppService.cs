using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MyCrm.ServiceCategories;

public interface IServiceCategoryAppService :
    ICrudAppService< //Defines CRUD methods
        ServiceCategoryDto, //Used to show service categories
        int, //Primary key of the service category entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateServiceCategoryDto> //Used to create/update a service category
{
}
