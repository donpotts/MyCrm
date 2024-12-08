using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using MyCrm.Addresses;
using MyCrm.Rewards;

namespace MyCrm.Contacts;

public class Contact : AuditedAggregateRoot<int>
{
    public string? Name { get; set; }

    public string? Email { get; set; }

    public int Phone { get; set; }

    public string? Role { get; set; }

    public string? Photo { get; set; }

    public string? Notes { get; set; }

    public int? AddressId { get; set; }

    public Address? Address { get; set; }

    public int? RewardId { get; set; }

    public Reward? Reward { get; set; }
}
