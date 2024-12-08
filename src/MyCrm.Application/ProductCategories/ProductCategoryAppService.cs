using System;
using MyCrm.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace MyCrm.ProductCategories;

public class ProductCategoryAppService :
    CrudAppService<
        ProductCategory, //The ProductCategory entity
        ProductCategoryDto, //Used to show product categories
        int, //Primary key of the product category entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateProductCategoryDto>, //Used to create/update a product category
    IProductCategoryAppService //implement the IProductCategoryAppService
{
    public ProductCategoryAppService(IRepository<ProductCategory, int> repository)
        : base(repository)
    {
        GetPolicyName = MyCrmPermissions.ProductCategories.Default;
        GetListPolicyName = MyCrmPermissions.ProductCategories.Default;
        CreatePolicyName = MyCrmPermissions.ProductCategories.Create;
        UpdatePolicyName = MyCrmPermissions.ProductCategories.Edit;
        DeletePolicyName = MyCrmPermissions.ProductCategories.Delete;
    }
}
