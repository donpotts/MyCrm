using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCrm.Shared.Models
{
    public class RoleItems
    {
        public string Name { get; set; }

        public bool IsDefault { get; set; }
        
        public bool IsStatic { get; set; }
        
        public bool IsPublic { get; set; }
        
        public string ConcurrencyStamp { get; set; }
        
        public string Id { get; set; }
        
        public Dictionary<string, object> ExtraProperties { get; set; }
    }
}
