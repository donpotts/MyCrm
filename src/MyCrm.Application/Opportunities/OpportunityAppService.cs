using System;
using MyCrm.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace MyCrm.Opportunities;

public class OpportunityAppService :
    CrudAppService<
        Opportunity, //The Opportunity entity
        OpportunityDto, //Used to show opportunities
        int, //Primary key of the opportunity entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateOpportunityDto>, //Used to create/update a opportunity
    IOpportunityAppService //implement the IOpportunityAppService
{
    public OpportunityAppService(IRepository<Opportunity, int> repository)
        : base(repository)
    {
        GetPolicyName = MyCrmPermissions.Opportunities.Default;
        GetListPolicyName = MyCrmPermissions.Opportunities.Default;
        CreatePolicyName = MyCrmPermissions.Opportunities.Create;
        UpdatePolicyName = MyCrmPermissions.Opportunities.Edit;
        DeletePolicyName = MyCrmPermissions.Opportunities.Delete;
    }
}
