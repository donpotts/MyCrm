
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace MyCrm.Shared.Models;

[DataContract]
public class Reward
{
    [Key]
    public long Id { get; set; }
    
    public int Rewardpoints { get; set; }
    
    public double CreditsDollars { get; set; }
    
    public string? ConversionRate { get; set; }
    
    public DateTime ExpirationDate { get; set; }
    
    public string? Notes { get; set; }

}
