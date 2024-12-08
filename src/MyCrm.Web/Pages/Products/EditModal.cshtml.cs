using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MyCrm.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace MyCrm.Web.Pages.Products;

public class EditModalModel : MyCrmPageModel
{

    [BindProperty]
    public EditProductViewModel Product { get; set; } = null!;

    public List<SelectListItem> ProductCategories { get; set; } = null!;

    private readonly IProductAppService _productAppService;

    public EditModalModel(IProductAppService productAppService)
    {
        _productAppService = productAppService;
    }

    public async Task OnGetAsync(int id)
    {
        var productDto = await _productAppService.GetAsync(id);
        Product = ObjectMapper.Map<ProductDto, EditProductViewModel>(productDto);
        Product.ProductCategoryId = productDto.ProductCategory?.Id;

        var productCategoryLookup = await _productAppService.GetProductCategoryLookupAsync();
        ProductCategories = productCategoryLookup.Items
            .OrderBy(x => x.Name)
            .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
            .ToList();
        ProductCategories.Insert(0, new SelectListItem(string.Empty, null));
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _productAppService.UpdateAsync(
            Product.Id,
            ObjectMapper.Map<EditProductViewModel, CreateUpdateProductDto>(Product)
        );

        return NoContent();
    }

    public class EditProductViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public double Price { get; set; }

        public int StockQuantity { get; set; }

        public string? Photo { get; set; }

        public string? Notes { get; set; }

        [SelectItems(nameof(ProductCategories))]
        [DisplayName("ProductCategory")]
        public int? ProductCategoryId { get; set; }
    }
}
