using Taxually.TechnicalTest.Interfaces.Services.Clients;
using Taxually.TechnicalTest.Interfaces.Strategies;
using Taxually.TechnicalTest.Models.Vat;

namespace Taxually.TechnicalTest.Strategies.VatRegistration;

public class VatRegistrationStrategyGb : IProcessingStrategy<VatRegistrationRequest>
{
    private readonly ITaxuallyHttpClient _httpClient;

    public VatRegistrationStrategyGb(ITaxuallyHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public void Process(VatRegistrationRequest request)
    {
        // UK has an API to register for a VAT number
        _httpClient.PostAsync("https://api.uktax.gov.uk", request).Wait();
    }
}
