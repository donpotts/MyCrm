using System;
using MyCrm.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace MyCrm.Sales;

public class SaleAppService :
    CrudAppService<
        Sale, //The Sale entity
        SaleDto, //Used to show sales
        int, //Primary key of the sale entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateSaleDto>, //Used to create/update a sale
    ISaleAppService //implement the ISaleAppService
{
    public SaleAppService(IRepository<Sale, int> repository)
        : base(repository)
    {
        GetPolicyName = MyCrmPermissions.Sales.Default;
        GetListPolicyName = MyCrmPermissions.Sales.Default;
        CreatePolicyName = MyCrmPermissions.Sales.Create;
        UpdatePolicyName = MyCrmPermissions.Sales.Edit;
        DeletePolicyName = MyCrmPermissions.Sales.Delete;
    }
}
