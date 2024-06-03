namespace Domain;

public interface IRepository
{
    public Task Add(DonationIntent donationIntent);
}