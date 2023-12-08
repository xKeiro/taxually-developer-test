namespace Taxually.TechnicalTest.Interfaces.Services.Utilities;

public interface ICsvService
{
    string makeCompanyCsvString(string companyName, string companyId);
}