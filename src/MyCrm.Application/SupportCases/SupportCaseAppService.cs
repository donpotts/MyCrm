using System;
using MyCrm.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace MyCrm.SupportCases;

public class SupportCaseAppService :
    CrudAppService<
        SupportCase, //The SupportCase entity
        SupportCaseDto, //Used to show support cases
        int, //Primary key of the support case entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateSupportCaseDto>, //Used to create/update a support case
    ISupportCaseAppService //implement the ISupportCaseAppService
{
    public SupportCaseAppService(IRepository<SupportCase, int> repository)
        : base(repository)
    {
        GetPolicyName = MyCrmPermissions.SupportCases.Default;
        GetListPolicyName = MyCrmPermissions.SupportCases.Default;
        CreatePolicyName = MyCrmPermissions.SupportCases.Create;
        UpdatePolicyName = MyCrmPermissions.SupportCases.Edit;
        DeletePolicyName = MyCrmPermissions.SupportCases.Delete;
    }
}
