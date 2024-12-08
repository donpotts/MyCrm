using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MyCrm.Rewards;

public interface IRewardAppService :
    ICrudAppService< //Defines CRUD methods
        RewardDto, //Used to show rewards
        int, //Primary key of the reward entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateRewardDto> //Used to create/update a reward
{
}
