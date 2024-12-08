using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCrm.Shared.Blazor.Models
{
    public class RegisterAccount
    {
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";
        public string EmailAddress { get; set; } = "";
        public string AppName { get; set; } = "";
    }
}
