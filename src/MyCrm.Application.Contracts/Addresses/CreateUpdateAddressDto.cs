using System;

namespace MyCrm.Addresses;

public class CreateUpdateAddressDto
{
    public string? Street { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public int ZipCode { get; set; }

    public string? Country { get; set; }

    public string? Photo { get; set; }

    public string? Notes { get; set; }
}
