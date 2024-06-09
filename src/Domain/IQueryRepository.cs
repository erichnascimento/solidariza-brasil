namespace Domain;

public interface IQueryRepository
{
    public record GetDonationIntentsQuery;
    
    public Task<IEnumerable<GetFinancialTransactionsResult>> GetFinancialTransactions(GetFinancialTransactionsQuery query);
    
    public record GetDonationIntentsResult(
        string Code,
        string Name,
        string Email,
        string Cpf,
        decimal Amount,
        DateOnly? DonationDate,
        string Status
    );
    
    public record GetFinancialTransactionsQuery;
    
    public Task<IEnumerable<GetDonationIntentsResult>> GetDonationIntents(GetDonationIntentsQuery query);
    
    public record GetFinancialTransactionsResult(
        string Code,
        string Type,
        decimal Amount,
        string Date,
        string? Url
    );
}