using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.Persistence.EfCore.DataModels;

[Table("donation_intents")]
public record DonationIntentDataModel
{
    [Key]
    [Column("code")]
    public required string Code { get; init; }
    
    [Column("name")]
    public required string Name { get; init; }
    
    [Column("email")]
    public required string Email { get; init; }
    
    [Column("cpf")]
    public required string Cpf { get; init; }
    
    [Column("amount")]
    public required decimal Amount { get; init; }
    
    [Column("status")]
    public required DonationIntentStatus Status { get; set; }
    
    [Column("pending_at")]
    public required DateTime PendingAt { get; init; }
    
    [Column("completed_at")]
    public DateTime? CompletedAt { get; init; }
    
    [Column("donation_date")]
    public required DateOnly? DonationDate { get; init; }
    
    [Column("donation_amount")]
    public required decimal? DonationAmount { get; init; }
    
    [Column("donation_recorded_at")]
    public required DateTime? DonationRecordedAt { get; init; }
    
    [Column("created_at")]
    public required DateTime CreatedAt { get; init; }
    
    [Column("updated_at")]
    public required DateTime UpdatedAt { get; set; }
    
    public enum DonationIntentStatus
    {
        Pending,
        Completed
    }
}