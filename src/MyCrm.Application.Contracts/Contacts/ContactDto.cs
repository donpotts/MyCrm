using System;
using Volo.Abp.Application.Dtos;
using MyCrm.Addresses;
using MyCrm.Rewards;

namespace MyCrm.Contacts;

public class ContactDto : AuditedEntityDto<int>
{
    public string? Name { get; set; }

    public string? Email { get; set; }

    public int Phone { get; set; }

    public string? Role { get; set; }

    public string? Photo { get; set; }

    public string? Notes { get; set; }

    public AddressDto? Address { get; set; }

    public RewardDto? Reward { get; set; }
}
