using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MyCrm.Opportunities;

public interface IOpportunityAppService :
    ICrudAppService< //Defines CRUD methods
        OpportunityDto, //Used to show opportunities
        int, //Primary key of the opportunity entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateOpportunityDto> //Used to create/update a opportunity
{
}
