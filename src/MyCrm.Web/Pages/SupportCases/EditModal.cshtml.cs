using System;
using System.Threading.Tasks;
using MyCrm.SupportCases;
using Microsoft.AspNetCore.Mvc;

namespace MyCrm.Web.Pages.SupportCases;

public class EditModalModel : MyCrmPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    [BindProperty]
    public CreateUpdateSupportCaseDto SupportCase { get; set; } = null!;

    private readonly ISupportCaseAppService _supportCaseAppService;

    public EditModalModel(ISupportCaseAppService supportCaseAppService)
    {
        _supportCaseAppService = supportCaseAppService;
    }

    public async Task OnGetAsync()
    {
        var supportCaseDto = await _supportCaseAppService.GetAsync(Id);
        SupportCase = ObjectMapper.Map<SupportCaseDto, CreateUpdateSupportCaseDto>(supportCaseDto);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _supportCaseAppService.UpdateAsync(Id, SupportCase);
        return NoContent();
    }
}
