using System.Xml.Serialization;
using Taxually.TechnicalTest.Interfaces.Services.Clients;
using Taxually.TechnicalTest.Interfaces.Strategies;
using Taxually.TechnicalTest.Models.Vat;

namespace Taxually.TechnicalTest.Strategies.VatRegistration;

public class VatRegistrationStrategyDe : IProcessingStrategy<VatRegistrationRequest>
{
    private readonly ITaxuallyQueueClient _queueClient;

    public VatRegistrationStrategyDe(ITaxuallyQueueClient queueClient)
    {
        _queueClient = queueClient;
    }
    public void Process(VatRegistrationRequest request)
    {
        using (var stringwriter = new StringWriter())
        {
            // Germany requires an XML document to be uploaded to register for a VAT number
            var serializer = new XmlSerializer(typeof(VatRegistrationRequest));
            serializer.Serialize(stringwriter, request);
            var xml = stringwriter.ToString();
            // Queue xml doc to be processed
            _queueClient.EnqueueAsync("vat-registration-xml", xml).Wait();
        }
    }
}
