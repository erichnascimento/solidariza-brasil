using Domain;
using Infra.Persistence.EfCore.DataModels;
using DonationIntent = Domain.DonationIntent;

namespace Infra.Persistence.EfCore;

public class EfCoreRepository : IRepository
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
}