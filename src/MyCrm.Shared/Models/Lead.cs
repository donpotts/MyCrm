
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace MyCrm.Shared.Models;

[DataContract]
public class Lead
{
    [Key]
    public long Id { get; set; }
    
    public string? Source { get; set; }
    
    public string? Status { get; set; }
    
    public double PotentialValue { get; set; }
    
    public string? Photo { get; set; }
    
    public string? Notes { get; set; }
    
    [DataMember]
    public long? AddressId { get; set; }
    
    [DataMember]
    public Address? Address { get; set; }
    [DataMember]
    public long? ContactId { get; set; }
    
    [DataMember]
    public Contact? Contact { get; set; }
    [DataMember]
    public long? OpportunityId { get; set; }
    
    [DataMember]
    public Opportunity? Opportunity { get; set; }

}
