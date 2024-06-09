using System.Diagnostics;
using Domain;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers;

public class HomeController(
    ILogger<HomeController> logger,
    CreateDonationUseCase createDonationUseCase,
    IQueryRepository queryRepository)
    : Controller
{
    private readonly ILogger<HomeController> _logger = logger;

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateDonation([FromForm] CreateDonationViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View("Index");
        }

        var input = new CreateDonationUseCase.Input(
            model.Name,
            model.Email,
            model.Cpf,
            model.Amount
        );
        var output = await createDonationUseCase.Execute(input);


        var viewModel = new ShowPixDonationViewModel(
            Code: output.Code,
            Name: model.Name,
            Email: model.Email,
            Cpf: model.Cpf,
            Amount: model.Amount.ToString("C")
        );

        return View("ShowPixDonation", model: viewModel);
    }

    public IActionResult ShowPixDonation()
    {
        return View();
    }

    public IActionResult ShowManifesto()
    {
        return View("Manifesto");
    }

    public async Task<IActionResult> ShowReport()
    {
        var query = new IQueryRepository.GetDonationIntentsQuery();
        var donations = await queryRepository.GetDonationIntents(query: query);

        var viewModel = new ShowReportViewModel(
            Donations: donations.Select(d => new ShowReportViewModel.Donation(
                Name: d.Name,
                Email: d.Email,
                Cpf: d.Cpf,
                Amount: d.Amount.ToString("C"),
                Date: d.DonationDate?.ToString("dd/MM/yyyy"),
                Code: d.Code,
                Status: ShowReportViewModel.Donation.Map(d.Status)
            ))
        );

        return View(model: viewModel);
    }

    public async Task<IActionResult> ShowFinancialReport()
    {
        var query = new IQueryRepository.GetFinancialTransactionsQuery();
        var transactions = await queryRepository.GetFinancialTransactions(query: query);

        var viewModel = new ShowFinancialReportViewModel(
            Transactions: transactions.Select(t => new ShowFinancialReportViewModel.Transaction(
                Type: ShowFinancialReportViewModel.Transaction.Map(t.Type),
                Amount: t.Amount.ToString("C"),
                AmountValue: t.Amount,
                Date: t.Date,
                Code: t.Code,
                Url: t.Url
            ))
        );

        return View(model: viewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}