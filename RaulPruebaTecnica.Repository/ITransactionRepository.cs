using RaulPruebaTecnica.Models.DTO;

namespace RaulPruebaTecnica.Repository
{
    public interface ITransactionRepository
    {
        IEnumerable<TransactionDTO> GetAll();
        void UpdateAll(List<TransactionDTO> user);
    }
}