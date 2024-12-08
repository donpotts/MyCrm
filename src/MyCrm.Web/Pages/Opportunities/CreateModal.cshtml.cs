using System.Threading.Tasks;
using MyCrm.Opportunities;
using Microsoft.AspNetCore.Mvc;

namespace MyCrm.Web.Pages.Opportunities;

public class CreateModalModel : MyCrmPageModel
{
    [BindProperty]
    public CreateUpdateOpportunityDto Opportunity { get; set; } = null!;

    private readonly IOpportunityAppService _opportunityAppService;

    public CreateModalModel(IOpportunityAppService opportunityAppService)
    {
        _opportunityAppService = opportunityAppService;
    }

    public void OnGet()
    {
        Opportunity = new CreateUpdateOpportunityDto();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _opportunityAppService.CreateAsync(Opportunity);
        return NoContent();
    }
}
