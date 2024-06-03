using Domain;

namespace Infra.Donation;

public class DonationIntentCodeGenerator : IDonationIntentCodeGenerator
{
    /// <summary>
    /// Generates a unique donation intent code. It should be unique across all donation intents.
    /// Uses the current date and time to generate the code.
    /// </summary>
    /// <returns></returns>
    public string Generate()
    {
        // TODO: Implement a better way to generate the code
        var now = DateTime.Now;
        
        return now.ToString("yyyyMMddHHmmss");
    }
}