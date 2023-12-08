using Taxually.TechnicalTest.Services.Utilities;

namespace Tests.Services;

[TestFixture]
internal class CsvServiceTest
{
    private CsvService _csvService;

    [SetUp]
    public void Setup()
    {
        _csvService = new CsvService();
    }

    [TestCase("Test Company", "1")]
    [TestCase("0", "Zero")]
    [TestCase("52", "53")]
    [TestCase("Ékezetes", "Karakterek")]
    public void getCompanyCsvString_ShouldReturnCsvString(string companyName, string companyId)
    {
        // Arrange
        var expectedCsvString = $"CompanyName,CompanyId\r\n{companyName},{companyId}\r\n";
        // Act
        var csvString = _csvService.makeCompanyCsvString(companyName, companyId);
        // Assert
        Assert.That(csvString, Is.EqualTo(expectedCsvString));
    }
}
