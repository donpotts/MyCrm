using System.ComponentModel.DataAnnotations;

namespace MyCrm.Shared.Blazor.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; }
        public string TenantId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool IsActive { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public DateTime? LockoutEnd { get; set; }
        public string ConcurrencyStamp { get; set; }
        public int EntityVersion { get; set; }
        public DateTime LastPasswordChangeTime { get; set; }
        public bool IsDeleted { get; set; }
        public string DeleterId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public string LastModifierId { get; set; }
        public DateTime CreationTime { get; set; }
        public string CreatorId { get; set; }
        public List<string> RoleNames { get; set; }
        public Dictionary<string, object> ExtraProperties { get; set; }
    }
}
