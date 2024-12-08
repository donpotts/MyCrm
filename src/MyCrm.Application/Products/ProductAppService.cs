using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using MyCrm.ProductCategories;
using MyCrm.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.ChangeTracking;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace MyCrm.Products;

[Authorize(MyCrmPermissions.Products.Default)]
public class ProductAppService :
    CrudAppService<
        Product, //The Product entity
        ProductDto, //Used to show products
        int, //Primary key of the product entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateProductDto>, //Used to create/update a product
    IProductAppService //implement the IProductAppService
{
    private readonly IRepository<ProductCategory, int> _productCategoryRepository;

    public ProductAppService(
        IRepository<Product, int> repository,
        IRepository<ProductCategory, int> productCategoryRepository)
        : base(repository)
    {
        _productCategoryRepository = productCategoryRepository;
        GetPolicyName = MyCrmPermissions.Products.Default;
        GetListPolicyName = MyCrmPermissions.Products.Default;
        CreatePolicyName = MyCrmPermissions.Products.Create;
        UpdatePolicyName = MyCrmPermissions.Products.Edit;
        DeletePolicyName = MyCrmPermissions.Products.Delete;
    }

    [DisableEntityChangeTracking]
    public override async Task<ProductDto> GetAsync(int id)
    {
        var product = await Repository.GetAsync(id) ?? throw new EntityNotFoundException(typeof(Product), id);

        var productDto = ObjectMapper.Map<Product, ProductDto>(product);
        productDto.ProductCategory = product.ProductCategory == null ? null : ObjectMapper.Map<ProductCategory, ProductCategoryDto>(product.ProductCategory);
        return productDto;
    }

    [DisableEntityChangeTracking]
    public override async Task<PagedResultDto<ProductDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        var products = await Repository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, input.Sorting ?? nameof(Product.Id), true);

        var productDtos = products.Select(x =>
        {
            var productDto = ObjectMapper.Map<Product, ProductDto>(x);
            productDto.ProductCategory = x.ProductCategory == null ? null : ObjectMapper.Map<ProductCategory, ProductCategoryDto>(x.ProductCategory);
            return productDto;
        }).ToList();

        var totalCount = await Repository.GetCountAsync();

        return new PagedResultDto<ProductDto>(
            totalCount,
            productDtos
        );
    }

    public override async Task<ProductDto> CreateAsync(CreateUpdateProductDto input)
    {
        var product = ObjectMapper.Map<CreateUpdateProductDto, Product>(input);

        await Repository.InsertAsync(product, true);

        using (Repository.DisableTracking())
        {
            product = await Repository.GetAsync(product.Id);
            var productDto = ObjectMapper.Map<Product, ProductDto>(product);
            productDto.ProductCategory = product.ProductCategory == null ? null : ObjectMapper.Map<ProductCategory, ProductCategoryDto>(product.ProductCategory);
            return productDto;
        }
    }

    public override async Task<ProductDto> UpdateAsync(int id, CreateUpdateProductDto input)
    {
        var product = await Repository.GetAsync(id) ?? throw new EntityNotFoundException(typeof(Product), id);

        ObjectMapper.Map(input, product);

        await Repository.UpdateAsync(product, true);

        using (Repository.DisableTracking())
        {
            product = await Repository.GetAsync(product.Id);
            var productDto = ObjectMapper.Map<Product, ProductDto>(product);
            productDto.ProductCategory = product.ProductCategory == null ? null : ObjectMapper.Map<ProductCategory, ProductCategoryDto>(product.ProductCategory);
            return productDto;
        }
    }

    [DisableEntityChangeTracking]
    public async Task<ListResultDto<ProductCategoryLookupDto>> GetProductCategoryLookupAsync()
    {
        var productCategories = await _productCategoryRepository.GetListAsync();

        return new ListResultDto<ProductCategoryLookupDto>(
            ObjectMapper.Map<List<ProductCategory>, List<ProductCategoryLookupDto>>(productCategories)
        );
    }
}
