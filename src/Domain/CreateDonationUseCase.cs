namespace Domain;

public class CreateDonationUseCase
{
    public Task<Output> Execute(Input input)
    {
        return Task.FromResult(new Output("123"));
    }

    public record Input(
        string Name,
        string Email,
        string Cpf,
        string Address,
        string City,
        string State,
        string Amount
    );

    public record Output(
        string DonationId
    );
}