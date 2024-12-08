using System;
using Volo.Abp.Application.Dtos;

namespace MyCrm.Rewards;

public class RewardDto : AuditedEntityDto<int>
{
    public int Rewardpoints { get; set; }

    public double CreditsDollars { get; set; }

    public string? ConversionRate { get; set; }

    public DateTime ExpirationDate { get; set; }

    public string? Notes { get; set; }
}
