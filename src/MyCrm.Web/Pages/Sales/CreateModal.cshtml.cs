using System.Threading.Tasks;
using MyCrm.Sales;
using Microsoft.AspNetCore.Mvc;

namespace MyCrm.Web.Pages.Sales;

public class CreateModalModel : MyCrmPageModel
{
    [BindProperty]
    public CreateUpdateSaleDto Sale { get; set; } = null!;

    private readonly ISaleAppService _saleAppService;

    public CreateModalModel(ISaleAppService saleAppService)
    {
        _saleAppService = saleAppService;
    }

    public void OnGet()
    {
        Sale = new CreateUpdateSaleDto();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _saleAppService.CreateAsync(Sale);
        return NoContent();
    }
}
