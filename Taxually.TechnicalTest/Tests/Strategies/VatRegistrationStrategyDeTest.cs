//using Moq;
//using Taxually.TechnicalTest.Controllers;
//using Taxually.TechnicalTest.Interfaces.Services.Clients;
//using Taxually.TechnicalTest.Models.Vat;
//using Taxually.TechnicalTest.Strategies.VatRegistration;

//namespace Tests.Strategies;

//[TestFixture]
//internal class VatRegistrationStrategyDeTest
//{
//    private VatRegistrationRequest _request = new()
//    {
//        CompanyId = "1",
//        Country = "DE",
//        CompanyName = "Test Company",
//    };

//    private Mock<ITaxuallyQueueClient> _mockQueueClient;
//    private VatRegistrationStrategyDe _strategy;

//    [SetUp]
//    public void Setup()
//    {
//        _mockQueueClient = new Mock<ITaxuallyQueueClient>();
//        _strategy = new VatRegistrationStrategyDe(_mockQueueClient.Object);
//    }

//    [Test]
//    public void Process_ShouldSerializeRequestToXmlAndEnqueueIt()
//    {
//        // Arrange
//        var expectedXml = @"<?xml version=""1.0"" encoding=""utf-16""?>
//<VatRegistrationRequest xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
//  <CompanyName>Test Company</CompanyName>
//  <CompanyId>1</CompanyId>
//  <Country>DE</Country>
//</VatRegistrationRequest>";

//        // Act
//        _strategy.Process(new VatRegistrationController());

//        // Assert
//        _mockQueueClient.Verify(m => m.EnqueueAsync("vat-registration-xml", expectedXml), Times.Once);
//    }

//}
