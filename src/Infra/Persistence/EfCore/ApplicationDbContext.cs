using Infra.Persistence.EfCore.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Infra.Persistence.EfCore;

public class ApplicationDbContext : DbContext
{
    public DbSet<DonationIntentDataModel> DonationIntents { get; set; } = null!;

    /// <summary>
    /// Constructor for ApplicationDbContext.
    /// This is used by the DI container to create an instance of the DbContext.
    /// It requires an instance of DbContextOptions to be passed in.
    /// The DI requires this constructor to be present to be able to create an instance of the DbContext.
    /// </summary>
    /// <param name="options"></param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Console.WriteLine("ApplicationDbContext::ctor -> options: " + options);
    }
}