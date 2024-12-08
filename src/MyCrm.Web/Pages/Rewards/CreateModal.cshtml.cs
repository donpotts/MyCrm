using System.Threading.Tasks;
using MyCrm.Rewards;
using Microsoft.AspNetCore.Mvc;

namespace MyCrm.Web.Pages.Rewards;

public class CreateModalModel : MyCrmPageModel
{
    [BindProperty]
    public CreateUpdateRewardDto Reward { get; set; } = null!;

    private readonly IRewardAppService _rewardAppService;

    public CreateModalModel(IRewardAppService rewardAppService)
    {
        _rewardAppService = rewardAppService;
    }

    public void OnGet()
    {
        Reward = new CreateUpdateRewardDto();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _rewardAppService.CreateAsync(Reward);
        return NoContent();
    }
}
