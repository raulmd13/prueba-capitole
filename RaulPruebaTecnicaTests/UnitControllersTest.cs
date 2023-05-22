using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RaulPruebaTecnica.Controllers;
using RaulPruebaTecnica.Models.DTO;
using RaulPruebaTecnica.Service;

namespace RaulPruebaTecnicaTests
{
    [TestFixture]
    public class TransactionsControllerTests
    {
        private TransactionsController _controller;
        private Mock<ITransactionsService> _serviceMock;
        private Mock<ILogger<TransactionsController>> _loggerMock;

        [SetUp]
        public void SetUp()
        {
            _serviceMock = new Mock<ITransactionsService>();
            _loggerMock = new Mock<ILogger<TransactionsController>>();
            _controller = new TransactionsController(_loggerMock.Object, _serviceMock.Object);
        }

        [Test]
        public void Get_WithValidSku_ReturnsOkResult()
        {
            // Arrange
            var sku = "OkSku";
            var transactions = new List<TransactionDTO> { new TransactionDTO { Sku = sku, Amount = 100, Currency = "EUR" } };
            var result = new TransactionsSumDTO { Total = 100, Transactions = transactions };
            _serviceMock.Setup(s => s.GetTransactions(sku)).Returns(result);

            // Act
            var actionResult = _controller.Get(sku);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(actionResult);
            var okResult = (OkObjectResult)actionResult;
            Assert.AreEqual(result, okResult.Value);
        }

        [Test]
        public void Get_WithInvalidSku_ReturnsNotFoundResult()
        {
            // Arrange
            var sku = "BadSku";
            var result = new TransactionsSumDTO { Total = 0, Transactions = new List<TransactionDTO>() };
            _serviceMock.Setup(s => s.GetTransactions(sku)).Returns(result);

            // Act
            var actionResult = _controller.Get(sku);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(actionResult);
        }

        [Test]
        public void Get_Transactions_ReturnsOkResult()
        {
            // Arrange
            var transactions = new List<TransactionDTO> { new TransactionDTO { Sku = "B2006", Amount = 100, Currency = "EUR" } };
            _serviceMock.Setup(s => s.GetTransactions()).Returns(transactions);

            // Act
            var actionResult = _controller.GetTransacciones();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(actionResult);
            var okResult = (OkObjectResult)actionResult;
            Assert.AreEqual(transactions, okResult.Value);
        }

        [Test]
        public void Get_Transactions_ReturnsNoContentResult()
        {
            // Arrange
            var transactions = new List<TransactionDTO>();
            _serviceMock.Setup(s => s.GetTransactions()).Returns(transactions);

            // Act
            var actionResult = _controller.GetTransacciones();

            // Assert
            Assert.IsInstanceOf<NoContentResult>(actionResult);
        }

        [Test]
        public void Get_Conversions_ReturnsOkResult()
        {
            // Arrange
            var conversionRates = new List<ConversionRateDTO> { new ConversionRateDTO { From = "EUR", To = "USD", Rate = 1.45 } };
            _serviceMock.Setup(s => s.GetConversions()).Returns(conversionRates);

            // Act
            var actionResult = _controller.GetConversiones();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(actionResult);
            var okResult = (OkObjectResult)actionResult;
            Assert.AreEqual(conversionRates, okResult.Value);
        }

        [Test]
        public void Get_Conversions_ReturnsNoContentResult()
        {
            // Arrange
            var conversionRateDTO = new List<ConversionRateDTO>();
            _serviceMock.Setup(s => s.GetConversions()).Returns(conversionRateDTO);

            // Act
            var actionResult = _controller.GetConversiones();

            // Assert
            Assert.IsInstanceOf<NoContentResult>(actionResult);
        }
    }

}