
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace MyCrm.Shared.Models;

[DataContract]
public class Opportunity
{
    [Key]
    public long Id { get; set; }
    public DateTime EstimatedCloseDate { get; set; }
    public string? Stage { get; set; }
    public string? Icon { get; set; }
    public string? Notes { get; set; }

}
