using System;

namespace MyCrm.Contacts;

public class CreateUpdateContactDto
{
    public string? Name { get; set; }

    public string? Email { get; set; }

    public int Phone { get; set; }

    public string? Role { get; set; }

    public string? Photo { get; set; }

    public string? Notes { get; set; }

    public int? AddressId { get; set; }

    public int? RewardId { get; set; }
}
