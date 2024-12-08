using System;
using System.Threading.Tasks;
using MyCrm.ProductCategories;
using Microsoft.AspNetCore.Mvc;

namespace MyCrm.Web.Pages.ProductCategories;

public class EditModalModel : MyCrmPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    [BindProperty]
    public CreateUpdateProductCategoryDto ProductCategory { get; set; } = null!;

    private readonly IProductCategoryAppService _productCategoryAppService;

    public EditModalModel(IProductCategoryAppService productCategoryAppService)
    {
        _productCategoryAppService = productCategoryAppService;
    }

    public async Task OnGetAsync()
    {
        var productCategoryDto = await _productCategoryAppService.GetAsync(Id);
        ProductCategory = ObjectMapper.Map<ProductCategoryDto, CreateUpdateProductCategoryDto>(productCategoryDto);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _productCategoryAppService.UpdateAsync(Id, ProductCategory);
        return NoContent();
    }
}
