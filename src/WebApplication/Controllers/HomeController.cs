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
            model.Address,
            model.City,
            model.State,
            model.Amount
        );
        var output = await _createDonationUseCase.Execute(input);
        

        return RedirectToAction("ShowPixDonation");
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
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}