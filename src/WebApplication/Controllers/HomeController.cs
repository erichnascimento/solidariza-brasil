using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Domain;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly CreateDonationUseCase _createDonationUseCase;

    public HomeController(ILogger<HomeController> logger, CreateDonationUseCase createDonationUseCase)
    {
        _logger = logger;
        _createDonationUseCase = createDonationUseCase;
    }

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
        var output = await _createDonationUseCase.Execute(input);


        var viewModel = new ShowPixDonationViewModel(
            Code: output.Code,
            Name: model.Name,
            Email: model.Email,
            Cpf: model.Cpf,
            Amount: model.Amount
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

    public IActionResult ShowReport()
    {
        var donations = new List<ShowReportViewModel.Donation>
        {
            new(
                Name: "Eri** *******nto",
                Email: "eri**@gmail.com",
                Cpf: "123.***.***-00",
                Amount: "R$ 100,00",
                Date: "01/10/2021",
                Code: "123456",
                Status: ShowReportViewModel.Donation.DonationStatus.Confirmed
            ),
            new(
                Name: "Ana ***** *ais",
                Email: "ana*****@hotmail.com",
                Cpf: "033.***.***-06",
                Amount: "R$ 50,00",
                Date: "23/11/2021",
                Code: "654321",
                Status: ShowReportViewModel.Donation.DonationStatus.Pending
            ),
        };
        var viewModel = new ShowReportViewModel(
            Donations: donations);

        return View(model: viewModel);
    }

    public IActionResult ShowFinancialReport()
    {
        var transactions = new List<ShowFinancialReportViewModel.Transaction>
        {
            new(
                Type: ShowFinancialReportViewModel.Transaction.TransactionType.Donation,
                Amount: "100,00",
                Date: "01/10/2021",
                Code: "123456"
            ),
            new(
                Type: ShowFinancialReportViewModel.Transaction.TransactionType.Withdraw,
                Amount: "50,00",
                Date: "23/11/2021",
                Code: "654321",
                Url: "/images/874102984610928.jpeg"
            ),
        };
        var viewModel = new ShowFinancialReportViewModel(
            Transactions: transactions);
        
        return View(model: viewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}