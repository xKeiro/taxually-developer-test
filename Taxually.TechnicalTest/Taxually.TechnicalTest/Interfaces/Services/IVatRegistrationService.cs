using Taxually.TechnicalTest.Controllers;
using Taxually.TechnicalTest.Models.Vat;

namespace Taxually.TechnicalTest.Interfaces.Services;

public interface IVatRegistrationService
{
    void ProcessRegistration(VatRegistrationRequest request);
}
