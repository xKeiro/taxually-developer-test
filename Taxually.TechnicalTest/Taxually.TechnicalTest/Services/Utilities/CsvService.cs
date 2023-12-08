using System.Text;
using Taxually.TechnicalTest.Interfaces.Services.Utilities;

namespace Taxually.TechnicalTest.Services.Utilities;

public class CsvService : ICsvService
{
    public string makeCompanyCsvString(string companyName, string companyId)
    {
        var csvBuilder = new StringBuilder();
        csvBuilder.AppendLine("CompanyName,CompanyId");
        csvBuilder.AppendLine($"{companyName}{companyId}");
        return csvBuilder.ToString();
    }
}
