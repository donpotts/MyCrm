using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCrm.Shared.Blazor.Models
{
    public class PermissionsDto
    {
        public bool AllSelected { get; set; }
        public string EntityDisplayName { get; set; }
        public List<Group> Groups { get; set; }

        public class GrantedProvider
        {
            public string ProviderName { get; set; }
            public string ProviderKey { get; set; }
        }

        public class Permission
        {
            public string Name { get; set; }
            public string DisplayName { get; set; }
            public string ParentName { get; set; }
            public bool IsGranted { get; set; }
            public List<string> AllowedProviders { get; set; }
            public List<GrantedProvider> GrantedProviders { get; set; }
        }

        public class Group
        {
            public string Name { get; set; }
            public string DisplayName { get; set; }
            public string DisplayNameKey { get; set; }
            public string DisplayNameResource { get; set; }
            public List<Permission> Permissions { get; set; }
        }
    }
}
