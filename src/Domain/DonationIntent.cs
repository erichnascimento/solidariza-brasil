namespace Domain;

public class DonationIntent
{
    public string Code { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }
    public string Cpf { get; init; }
    public decimal Amount { get; init; }
    public DonationIntentStatus Status { get; private set; }
    public DateTime PendingAt { get; init; }
    public DateTime? CompletedAt { get; private set; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; private set; }
    public DonationInfo? Donation { get; private set; }

    public static DonationIntent Create(
        string code,
        string name,
        string email,
        string cpf,
        decimal amount,
        DateTime now
    )
    {
        return new DonationIntent(
            code: code,
            name: name,
            email: email,
            cpf: cpf,
            amount: amount,
            status: DonationIntentStatus.Pending,
            pendingAt: now,
            completedAt: null,
            createdAt: now,
            updatedAt: now
        );
    }

    public void CompleteDonation(DateOnly receiveDate, decimal receivedAmount, DateTime now)
    {
        if (Status != DonationIntentStatus.Pending)
        {
            throw new InvalidOperationException("Donation intent is not pending");
        }

        var donationInfo = new DonationInfo(
            Date: receiveDate,
            Amount: receivedAmount,
            RecordedAt: now
        );

        Donation = donationInfo;
        Status = DonationIntentStatus.Completed;
        UpdatedAt = now;
    }

    private DonationIntent(
        string code,
        string name,
        string email,
        string cpf,
        decimal amount,
        DonationIntentStatus status,
        DateTime pendingAt,
        DateTime? completedAt,
        DateTime createdAt,
        DateTime updatedAt
    )
    {
        Code = code;
        Name = name;
        Email = email;
        Cpf = cpf;
        Amount = amount;
        Status = status;
        PendingAt = pendingAt;
        CompletedAt = completedAt;
        
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public enum DonationIntentStatus
    {
        Pending,
        Completed
    }

    public record DonationInfo(
        DateOnly Date,
        decimal Amount,
        DateTime RecordedAt
    );
}