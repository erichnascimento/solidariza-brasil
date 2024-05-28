using Infra.Persistence.EFCore.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Infra.Persistence.EFCore;

public class ApplicationDbContext : DbContext
{
    public DbSet<DonationIntent> DonationIntents { get; set; } = null!;

    public string DbPath { get; }

    public ApplicationDbContext()
    {
        const Environment.SpecialFolder folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "solidariza-brasil.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}