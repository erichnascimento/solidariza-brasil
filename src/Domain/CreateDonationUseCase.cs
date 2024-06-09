namespace Domain;

public class CreateDonationUseCase
{
    private readonly IDonationIntentCodeGenerator _donationIntentCodeGenerator;
    private readonly IClock _clock;
    private readonly IRepository _repository;

    public CreateDonationUseCase(
        IDonationIntentCodeGenerator donationIntentCodeGenerator,
        IClock clock,
        IRepository repository
    )
    {
        _clock = clock;
        _donationIntentCodeGenerator = donationIntentCodeGenerator;
        _repository = repository;
    }

    public async Task<Output> Execute(Input input)
    {
        var now = _clock.Now();
        var code = _donationIntentCodeGenerator.Generate();
        var donationIntent = DonationIntent.Create(
            code: code,
            name: input.Name,
            email: input.Email,
            cpf: input.Cpf,
            amount: input.Amount,
            now: now
        );

        await _repository.Add(donationIntent);

        var output =  new Output(
            Code: donationIntent.Code,
            Name: donationIntent.Name,
            Email: donationIntent.Email,
            Cpf: donationIntent.Cpf,
            Amount: donationIntent.Amount.ToString(format: "F")
        );

        return output;
    }

    public record Input(
        string Name,
        string Email,
        string Cpf,
        decimal Amount
    );

    public record Output(
        string Code,
        string Name,
        string Email,
        string Cpf,
        string Amount
    );
}