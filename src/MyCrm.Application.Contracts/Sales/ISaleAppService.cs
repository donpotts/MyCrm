using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MyCrm.Sales;

public interface ISaleAppService :
    ICrudAppService< //Defines CRUD methods
        SaleDto, //Used to show sales
        int, //Primary key of the sale entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateSaleDto> //Used to create/update a sale
{
}
