using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MyCrm.Products;

public interface IProductAppService :
    ICrudAppService< //Defines CRUD methods
        ProductDto, //Used to show products
        int, //Primary key of the product entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateProductDto> //Used to create/update a product
{
    Task<ListResultDto<ProductCategoryLookupDto>> GetProductCategoryLookupAsync();
}
