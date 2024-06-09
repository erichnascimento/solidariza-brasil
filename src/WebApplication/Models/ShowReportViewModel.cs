namespace WebApplication.Models;

public record ShowReportViewModel(
    IEnumerable<ShowReportViewModel.Donation> Donations)
{
    public record Donation(
        string Name,
        string Email,
        string Cpf,
        string Amount,
        string? Date,
        string Code,
        Donation.DonationStatus Status
    )
    {
        public string NameMasked => Mask(Name, 3, 3);
        public string CpfMasked => Mask(Cpf, 3, 3);
        public string EmailMasked => Mask(Email, 3, Email.Length - Email.IndexOf('@'));
        public string StatusText => Status switch
        {
            DonationStatus.Pending => "Pendente",
            DonationStatus.Confirmed => "Confirmada",
            _ => throw new ArgumentOutOfRangeException(nameof(Status), Status, null)
        };
        
        private static string Mask(string value, int preserveLeft, int preserveRight)
        {
            var masked = value.ToCharArray();
            for (var i = preserveLeft; i < masked.Length - preserveRight; i++)
            {
                masked[i] = '*';
            }

            return new string(masked);
        }
        
        public enum DonationStatus
        {
            Pending,
            Confirmed
        }

        public static DonationStatus Map(string status)
        {
            return status switch
            {
                "pending" => DonationStatus.Pending,
                "completed" => DonationStatus.Confirmed,
                _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
            };
        }
    }
}