using Moq;
using RaulPruebaTecnica.Models;
using RaulPruebaTecnica.Models.DTO;
using RaulPruebaTecnica.Repository;
using RaulPruebaTecnica.Service;
using System.Text.Json;

namespace RaulPruebaTecnicaTests
{
    [TestFixture]
    public class TransactionsServiceTests
    {
        private readonly string Sku = "T2006";

        private Mock<TransactionsService> MockService;
        private ExternalServices externalServices;

        private readonly string jsonTransaction = @"[ { ""Sku"": ""T2006"", ""Amount"": 10.00, ""Currency"": ""USD"" }, { ""Sku"": ""M2007"", ""Amount"": 34.57, ""Currency"": ""CAD"" }, { ""Sku"": ""R2008"", ""Amount"": 17.95, ""Currency"": ""USD"" }, { ""Sku"": ""T2006"", ""Amount"": 7.63, ""Currency"": ""EUR"" }, { ""Sku"": ""B2009"", ""Amount"": 21.23, ""Currency"": ""USD"" } ]";
        private readonly string jsonConversion = @"[ { ""From"": ""EUR"", ""To"": ""USD"", ""Rate"": 1.359 }, { ""From"": ""CAD"", ""To"": ""EUR"", ""Rate"": 0.732 }, { ""From"": ""USD"", ""To"": ""EUR"", ""Rate"": 0.736 }, { ""From"": ""EUR"", ""To"": ""CAD"", ""Rate"": 1.366 } ]";

        private List<TransactionDTO> transactions;
        private List<ConversionRateDTO> conversionRates;

        [SetUp]
        public void SetUp()
        {
            externalServices = new ExternalServices();
            MockService = new Mock<TransactionsService>(externalServices, new Mock<ITransactionRepository>().Object, new Mock<IConversionRepository>().Object);
            MockService.CallBase = true;

            transactions = JsonSerializer.Deserialize<List<TransactionDTO>>(jsonTransaction.ToString());
            conversionRates = JsonSerializer.Deserialize<List<ConversionRateDTO>>(jsonConversion.ToString());
        }

        [Test]
        public void Get_WithValidSku_Returns_ValidResult()
        {
            MockService.Setup(s => s.GetTransactions()).Returns(transactions);
            MockService.Setup(s => s.GetConversions()).Returns(conversionRates);

            // Act
            var transactionsSum = MockService.Object.GetTransactions(Sku);

            // Assert
            Assert.AreEqual(transactionsSum.Total, 14.99);
            Assert.AreEqual(transactionsSum.Transactions.Count, 2);
        }

        [Test]
        public void Get_WithNonValidSku_Returns_EmptyResult()
        {
            MockService.Setup(s => s.GetTransactions()).Returns(transactions);
            MockService.Setup(s => s.GetConversions()).Returns(conversionRates);

            // Act
            var transactionsSum = MockService.Object.GetTransactions("BadSku");

            // Assert
            Assert.AreEqual(transactionsSum.Total, 0);
            Assert.AreEqual(transactionsSum.Transactions.Count, 0);
        }
    }
}