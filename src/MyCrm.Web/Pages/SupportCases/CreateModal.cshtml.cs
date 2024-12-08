using System.Threading.Tasks;
using MyCrm.SupportCases;
using Microsoft.AspNetCore.Mvc;

namespace MyCrm.Web.Pages.SupportCases;

public class CreateModalModel : MyCrmPageModel
{
    [BindProperty]
    public CreateUpdateSupportCaseDto SupportCase { get; set; } = null!;

    private readonly ISupportCaseAppService _supportCaseAppService;

    public CreateModalModel(ISupportCaseAppService supportCaseAppService)
    {
        _supportCaseAppService = supportCaseAppService;
    }

    public void OnGet()
    {
        SupportCase = new CreateUpdateSupportCaseDto();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _supportCaseAppService.CreateAsync(SupportCase);
        return NoContent();
    }
}
