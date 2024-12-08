using System.ComponentModel.DataAnnotations;

namespace MyCrm.Shared.Models;

public class ApplicationUserWithRolesDto : ApplicationUserDto
{
    public List<string>? Roles { get; set; }
}

