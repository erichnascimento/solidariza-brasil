using Domain;
using Infra.Persistence.EfCore.DataModels;
using DonationIntent = Domain.DonationIntent;

namespace Infra.Persistence.EfCore;

public class EfCoreRepository : IRepository, IQueryRepository
{
    private readonly ApplicationDbContext _context;
    
    public EfCoreRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public Task Add(DonationIntent donationIntent)
    {
        var donationIntentDataModel = new DonationIntentDataModel
        {
            Code = donationIntent.Code,
            Name = donationIntent.Name,
            Cpf = donationIntent.Cpf,
            Amount = donationIntent.Amount,
            Email = donationIntent.Email,
            Status = Map(donationIntent.Status),
            CreatedAt = donationIntent.CreatedAt.ToUniversalTime(),
            DonationAmount = donationIntent.Donation?.Amount,
            DonationDate = donationIntent.Donation?.Date,
            PendingAt = donationIntent.PendingAt.ToUniversalTime(),
            UpdatedAt = donationIntent.UpdatedAt.ToUniversalTime(),
            DonationRecordedAt = donationIntent.Donation?.RecordedAt,
            CompletedAt = donationIntent.CompletedAt?.ToUniversalTime()
        };
        _context.DonationIntents.Add(donationIntentDataModel);
        
        return _context.SaveChangesAsync();
    }

    private static DonationIntentDataModel.DonationIntentStatus Map(DonationIntent.DonationIntentStatus status)
    {
        return status switch
        {
            DonationIntent.DonationIntentStatus.Pending => DonationIntentDataModel.DonationIntentStatus.Pending,
            DonationIntent.DonationIntentStatus.Completed => DonationIntentDataModel.DonationIntentStatus.Completed,
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
    }

    public Task<IEnumerable<IQueryRepository.GetFinancialTransactionsResult>> GetFinancialTransactions(IQueryRepository.GetFinancialTransactionsQuery query)
    {
        var result = _context.FinancialTransactions
            .OrderByDescending(f => f.CreatedAt)
            .Select(f => new IQueryRepository.GetFinancialTransactionsResult(
                f.Code,
                f.Type.ToString(),
                f.Amount,
                f.CreatedAt.ToString("yyyy-MM-dd"),
                f.Receipt
            ))
            .AsEnumerable();
        
        return Task.FromResult(result);
    }

    public Task<IEnumerable<IQueryRepository.GetDonationIntentsResult>> GetDonationIntents(IQueryRepository.GetDonationIntentsQuery query)
    {
        var result = _context.DonationIntents
            .OrderByDescending(d => d.CreatedAt)
            .Select(d => new IQueryRepository.GetDonationIntentsResult(
                d.Code,
                d.Name,
                d.Email,
                d.Cpf,
                d.Amount,
                d.DonationDate,
                d.Status.ToString()
            ))
            .AsEnumerable();
        
        return Task.FromResult(result);
    }
}