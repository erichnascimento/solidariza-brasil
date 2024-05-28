using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models;

public record CreateDonationViewModel(
    [Required]
    string Name,
    [Required]
    string Email,
    [Required]
    string Cpf,
    [Required]
    string Address,
    [Required]
    string City,
    [Required]
    string State,
    [Required]
    string Amount
);