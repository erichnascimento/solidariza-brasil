using Infra.Persistence.EfCore.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Infra.Persistence.EfCore;

public class ApplicationDbContext : DbContext
{
    public DbSet<DonationIntentDataModel> DonationIntents { get; set; } = null!;
}