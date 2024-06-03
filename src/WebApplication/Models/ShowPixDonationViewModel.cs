namespace WebApplication.Models;

public record ShowPixDonationViewModel(
    string Code,
    string Name,
    string Email,
    string Cpf,
    string Amount,
    
    string PixKey = "99a83454-88b0-406a-ada4-e2fe57d7b395",
    string PixCpf = "•••.605.269-••",
    string PixName = "Erich Aparecido do Nascimento"
);