using RaulPruebaTecnica.Models.DTO;

namespace RaulPruebaTecnica.Service
{
    public interface ITransactionsService
    {
        List<ConversionRateDTO> GetConversions();
        TransactionsSumDTO GetTransactions(string sku);
        List<TransactionDTO> GetTransactions();
    }
}