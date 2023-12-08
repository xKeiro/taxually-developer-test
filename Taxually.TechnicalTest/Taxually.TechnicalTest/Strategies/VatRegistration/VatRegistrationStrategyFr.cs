using System.Text;
using Taxually.TechnicalTest.Interfaces.Services.Clients;
using Taxually.TechnicalTest.Interfaces.Services.Utilities;
using Taxually.TechnicalTest.Interfaces.Strategies;
using Taxually.TechnicalTest.Models.Vat;

namespace Taxually.TechnicalTest.Strategies.VatRegistration;

public class VatRegistrationStrategyFr : IProcessingStrategy<VatRegistrationRequest>
{
    private readonly ITaxuallyQueueClient _queueClient;
    private readonly ICsvService _csvService;

    public VatRegistrationStrategyFr(ITaxuallyQueueClient queueClient, ICsvService csvService)
    {
        _queueClient = queueClient;
        _csvService = csvService;
    }

    public void Process(VatRegistrationRequest request)
    {
        // France requires an excel spreadsheet to be uploaded to register for a VAT number
        var csv = _csvService.makeCompanyCsvString(request.CompanyName, request.CompanyId);
        var encodedCsv = Encoding.UTF8.GetBytes(csv);
        // Queue file to be processed
        _queueClient.EnqueueAsync("vat-registration-csv", encodedCsv).Wait();
    }
}