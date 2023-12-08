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
    private readonly Dictionary<string, IProcessingStrategy<VatRegistrationRequest>> _requestStrategies;
    private readonly Dictionary<string, IProcessingStrategy<VatRegistrationController>> _controllerStrategies;

    public VatRegistrationService()
    {
        _requestStrategies = new Dictionary<string, IProcessingStrategy<VatRegistrationRequest>>
        {
            { "GB", new VatRegistrationStrategyGb(new TaxuallyHttpClient())  },
            { "FR", new VatRegistrationStrategyFr(new TaxuallyQueueClient(), new CsvService()) },
        };

        _controllerStrategies = new Dictionary<string, IProcessingStrategy<VatRegistrationController>>
        {
            { "DE", new VatRegistrationStrategyDe(new TaxuallyQueueClient())  },
        };
    }

    //THIS VERSION WAS MADE FOR CORRENT TYPE FOR XML SERIALIZATION
    public void ProcessRegistration(VatRegistrationRequest request, VatRegistrationController controller)
    {
        if (_requestStrategies.TryGetValue(request.Country, out var strategy))
        {
            strategy.Process(request);
        }
        else if (_controllerStrategies.TryGetValue(request.Country, out var strategy2))
        {
            strategy2.Process(controller);
        }
        else
        {
            throw new Exception("Country not supported");
        }
    }
}
