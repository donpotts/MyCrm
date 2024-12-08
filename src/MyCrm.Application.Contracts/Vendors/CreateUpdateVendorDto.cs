using System;

namespace MyCrm.Vendors;

public class CreateUpdateVendorDto
{
    public string? Name { get; set; }

    public string? ContactName { get; set; }

    public int Phone { get; set; }

    public string? Email { get; set; }

    public string? Logo { get; set; }

    public string? Notes { get; set; }

    public int? AddressId { get; set; }

    public int? ServiceId { get; set; }

    public int? ProductId { get; set; }
}
