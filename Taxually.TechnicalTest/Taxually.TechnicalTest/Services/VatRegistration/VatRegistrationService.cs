using System.Xml.Serialization;
using Taxually.TechnicalTest.Controllers;
using Taxually.TechnicalTest.Interfaces.Services;
using Taxually.TechnicalTest.Interfaces.Strategies;
using Taxually.TechnicalTest.Models.Vat;
using Taxually.TechnicalTest.Services.Clients;
using Taxually.TechnicalTest.Services.Utilities;
using Taxually.TechnicalTest.Strategies.VatRegistration;

namespace Taxually.TechnicalTest.Services.VatRegistration;

public class VatRegistrationService : IVatRegistrationService
{
    private readonly Dictionary<string, IProcessingStrategy<VatRegistrationRequest>> _strategies;

    public VatRegistrationService()
    {
        _strategies = new Dictionary<string, IProcessingStrategy<VatRegistrationRequest>>
        {
            { "GB", new VatRegistrationStrategyGb(new TaxuallyHttpClient())  },
            { "DE", new VatRegistrationStrategyDe(new TaxuallyQueueClient()) },
            { "FR", new VatRegistrationStrategyFr(new TaxuallyQueueClient(), new CsvService()) },
        };
    }

    //THIS VERSION WAS MADE FOR CORRENT TYPE FOR XML SERIALIZATION
    public void ProcessRegistration(VatRegistrationRequest request)
    {
        if (_strategies.TryGetValue(request.Country, out var strategy))
        {
            strategy.Process(request);
        }
        else
        {
            throw new Exception("Country not supported");
        }
    }
}
