using System;
using System.Threading.Tasks;
using MyCrm.Rewards;
using Microsoft.AspNetCore.Mvc;

namespace MyCrm.Web.Pages.Rewards;

public class EditModalModel : MyCrmPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    [BindProperty]
    public CreateUpdateRewardDto Reward { get; set; } = null!;

    private readonly IRewardAppService _rewardAppService;

    public EditModalModel(IRewardAppService rewardAppService)
    {
        _rewardAppService = rewardAppService;
    }

    public async Task OnGetAsync()
    {
        var rewardDto = await _rewardAppService.GetAsync(Id);
        Reward = ObjectMapper.Map<RewardDto, CreateUpdateRewardDto>(rewardDto);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _rewardAppService.UpdateAsync(Id, Reward);
        return NoContent();
    }
}
