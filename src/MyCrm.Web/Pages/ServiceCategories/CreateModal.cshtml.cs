using System.Threading.Tasks;
using MyCrm.ServiceCategories;
using Microsoft.AspNetCore.Mvc;

namespace MyCrm.Web.Pages.ServiceCategories;

public class CreateModalModel : MyCrmPageModel
{
    [BindProperty]
    public CreateUpdateServiceCategoryDto ServiceCategory { get; set; } = null!;

    private readonly IServiceCategoryAppService _serviceCategoryAppService;

    public CreateModalModel(IServiceCategoryAppService serviceCategoryAppService)
    {
        _serviceCategoryAppService = serviceCategoryAppService;
    }

    public void OnGet()
    {
        ServiceCategory = new CreateUpdateServiceCategoryDto();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _serviceCategoryAppService.CreateAsync(ServiceCategory);
        return NoContent();
    }
}
