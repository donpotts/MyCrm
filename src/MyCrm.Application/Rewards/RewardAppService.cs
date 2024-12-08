using System;
using MyCrm.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace MyCrm.Rewards;

public class RewardAppService :
    CrudAppService<
        Reward, //The Reward entity
        RewardDto, //Used to show rewards
        int, //Primary key of the reward entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateRewardDto>, //Used to create/update a reward
    IRewardAppService //implement the IRewardAppService
{
    public RewardAppService(IRepository<Reward, int> repository)
        : base(repository)
    {
        GetPolicyName = MyCrmPermissions.Rewards.Default;
        GetListPolicyName = MyCrmPermissions.Rewards.Default;
        CreatePolicyName = MyCrmPermissions.Rewards.Create;
        UpdatePolicyName = MyCrmPermissions.Rewards.Edit;
        DeletePolicyName = MyCrmPermissions.Rewards.Delete;
    }
}
