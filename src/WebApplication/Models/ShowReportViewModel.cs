namespace WebApplication.Models;

public record ShowReportViewModel(
    IEnumerable<ShowReportViewModel.Donation> Donations)
{
    public record Donation(
        string Name,
        string Email,
        string Cpf,
        string Amount,
        string Date,
        string Code,
        Donation.DonationStatus Status
    )
    {
        public string StatusText => Status switch
        {
            DonationStatus.Pending => "Pendente",
            DonationStatus.Confirmed => "Confirmada",
            _ => throw new ArgumentOutOfRangeException(nameof(Status), Status, null)
        };
        
        public enum DonationStatus
        {
            Pending,
            Confirmed
        }
    }
}