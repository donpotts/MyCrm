using System;
using System.Threading.Tasks;
using MyCrm.ServiceCategories;
using Microsoft.AspNetCore.Mvc;

namespace MyCrm.Web.Pages.ServiceCategories;

public class EditModalModel : MyCrmPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    [BindProperty]
    public CreateUpdateServiceCategoryDto ServiceCategory { get; set; } = null!;

    private readonly IServiceCategoryAppService _serviceCategoryAppService;

    public EditModalModel(IServiceCategoryAppService serviceCategoryAppService)
    {
        _serviceCategoryAppService = serviceCategoryAppService;
    }

    public async Task OnGetAsync()
    {
        var serviceCategoryDto = await _serviceCategoryAppService.GetAsync(Id);
        ServiceCategory = ObjectMapper.Map<ServiceCategoryDto, CreateUpdateServiceCategoryDto>(serviceCategoryDto);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _serviceCategoryAppService.UpdateAsync(Id, ServiceCategory);
        return NoContent();
    }
}
