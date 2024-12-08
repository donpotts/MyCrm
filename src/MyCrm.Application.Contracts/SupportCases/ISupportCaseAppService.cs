using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MyCrm.SupportCases;

public interface ISupportCaseAppService :
    ICrudAppService< //Defines CRUD methods
        SupportCaseDto, //Used to show support cases
        int, //Primary key of the support case entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateSupportCaseDto> //Used to create/update a support case
{
}
