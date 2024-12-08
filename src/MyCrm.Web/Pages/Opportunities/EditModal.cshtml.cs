using System;
using System.Threading.Tasks;
using MyCrm.Opportunities;
using Microsoft.AspNetCore.Mvc;

namespace MyCrm.Web.Pages.Opportunities;

public class EditModalModel : MyCrmPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    [BindProperty]
    public CreateUpdateOpportunityDto Opportunity { get; set; } = null!;

    private readonly IOpportunityAppService _opportunityAppService;

    public EditModalModel(IOpportunityAppService opportunityAppService)
    {
        _opportunityAppService = opportunityAppService;
    }

    public async Task OnGetAsync()
    {
        var opportunityDto = await _opportunityAppService.GetAsync(Id);
        Opportunity = ObjectMapper.Map<OpportunityDto, CreateUpdateOpportunityDto>(opportunityDto);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _opportunityAppService.UpdateAsync(Id, Opportunity);
        return NoContent();
    }
}
