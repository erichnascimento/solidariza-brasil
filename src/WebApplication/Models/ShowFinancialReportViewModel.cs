namespace WebApplication.Models;

public record ShowFinancialReportViewModel(
    IEnumerable<ShowFinancialReportViewModel.Transaction> Transactions)
{
    public string TotalDonations => Transactions
        .Where(t => t.Type == Transaction.TransactionType.Donation)
        .Sum(t => decimal.Parse(t.Amount))
        .ToString("C");
    
    public string TotalWithdraws => Transactions
        .Where(t => t.Type == Transaction.TransactionType.Withdraw)
        .Sum(t => decimal.Parse(t.Amount))
        .ToString("C");
    
    public string TotalBalance => Transactions
        .Sum(t => t.Type == Transaction.TransactionType.Donation
            ? decimal.Parse(t.Amount)
            : -decimal.Parse(t.Amount))
        .ToString("C");
    
    public record Transaction(
        Transaction.TransactionType Type,
        string Amount,
        string Date,
        string Code,
        string? Url = null
    )
    {
        public string CreditDebit => Type switch
        {
            TransactionType.Donation => "C",
            TransactionType.Withdraw => "D",
            _ => throw new ArgumentOutOfRangeException(nameof(Type), Type, null)
        };
        
        public string Description => Type switch
        {
            TransactionType.Donation => $"Doação {Code}",
            TransactionType.Withdraw => $"Ação solidária {Code}",
            _ => throw new ArgumentOutOfRangeException(nameof(Type), Type, null)
        };
        
        public string TypeText => Type switch
        {
            TransactionType.Donation => "Doação",
            TransactionType.Withdraw => "Saque",
            _ => throw new ArgumentOutOfRangeException(nameof(Type), Type, null)
        };

        public enum TransactionType
        {
            Donation,
            Withdraw
        }
    }
}