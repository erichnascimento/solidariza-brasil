using Domain;
using Infra.Clock;
using Infra.Donation;
using Infra.Persistence.EfCore;
using Infra.Persistence.EfCore.DataModels;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IClock, SystemClock>();
builder.Services.AddScoped<IDonationIntentCodeGenerator, DonationIntentCodeGenerator>();

var pgConnectionString = builder.Configuration.GetConnectionString("Postgres");
var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString: pgConnectionString);
dataSourceBuilder.MapEnum<DonationIntentDataModel.DonationIntentStatus>("donation_intent_status");
dataSourceBuilder.MapEnum<FinancialTransactionDataModel.FinancialTransactionType>("financial_transaction_type");
var dataSource = dataSourceBuilder.Build();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(dataSource);
});
builder.Services.AddScoped<IQueryRepository, EfCoreRepository>();

builder.Services.AddScoped<IRepository, EfCoreRepository>();

builder.Services.AddScoped<CreateDonationUseCase>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();