using System.Xml.Serialization;
using Taxually.TechnicalTest.Controllers;
using Taxually.TechnicalTest.Interfaces.Services.Clients;
using Taxually.TechnicalTest.Interfaces.Strategies;
using Taxually.TechnicalTest.Models.Vat;

namespace Taxually.TechnicalTest.Strategies.VatRegistration;

public class VatRegistrationStrategyDe : IProcessingStrategy<VatRegistrationController>
{
    private readonly ITaxuallyQueueClient _queueClient;

    public VatRegistrationStrategyDe(ITaxuallyQueueClient queueClient)
    {
        _queueClient = queueClient;
    }
    public void Process(VatRegistrationController controller)
    {
        using (var stringwriter = new StringWriter())
        {
            // Germany requires an XML document to be uploaded to register for a VAT number
            var serializer = new XmlSerializer(typeof(VatRegistrationRequest));
            serializer.Serialize(stringwriter, controller);
            var xml = stringwriter.ToString();
            // Queue xml doc to be processed
            _queueClient.EnqueueAsync("vat-registration-xml", xml).Wait();
        }
    }
}
