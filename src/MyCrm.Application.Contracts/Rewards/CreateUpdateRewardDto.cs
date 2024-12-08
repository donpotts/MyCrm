using System;

namespace MyCrm.Rewards;

public class CreateUpdateRewardDto
{
    public int Rewardpoints { get; set; }

    public double CreditsDollars { get; set; }

    public string? ConversionRate { get; set; }

    public DateTime ExpirationDate { get; set; }

    public string? Notes { get; set; }
}
