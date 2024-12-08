using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MyCrm.ProductCategories;

public interface IProductCategoryAppService :
    ICrudAppService< //Defines CRUD methods
        ProductCategoryDto, //Used to show product categories
        int, //Primary key of the product category entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateProductCategoryDto> //Used to create/update a product category
{
}
