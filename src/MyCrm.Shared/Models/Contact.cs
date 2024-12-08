
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace MyCrm.Shared.Models;

[DataContract]
public class Contact
{
    [Key]
    public long Id { get; set; }
    
    public string? Name { get; set; }
    
    public string? Email { get; set; }
    
    public int Phone { get; set; }
    
    public string? Role { get; set; }
    
    public string? Photo { get; set; }
    
    public string? Notes { get; set; }
    
    [DataMember]
    public long? AddressId { get; set; }
    
    [DataMember]
    public Address? Address { get; set; }
    [DataMember]
    public long? RewardId { get; set; }
    
    [DataMember]
    public Reward? Reward { get; set; }

}
