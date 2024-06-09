using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.Persistence.EfCore.DataModels;

[Table("financial_transactions")]
public record FinancialTransactionDataModel
{
    [Key]
    [Column("code")]
    public required string Code { get; init; }
    
    [Column("amount")]
    public required decimal Amount { get; init; }
    
    [Column("transaction_date")]
    public required DateOnly TransactionDate { get; init; }
    
    [Column("type")]
    public required FinancialTransactionType Type { get; init; }
    
    [Column("receipt")]
    public required string? Receipt { get; init; }
    
    [Column("created_at")]
    public required DateTime CreatedAt { get; init; }
    
    public enum FinancialTransactionType
    {
        Donation,
        Withdrawal
    }
}