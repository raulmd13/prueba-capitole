using RaulPruebaTecnica.Models.DTO;

namespace RaulPruebaTecnica.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly GNBDbContext _dbContext;

        public TransactionRepository(GNBDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<TransactionDTO> GetAll()
        {
            return _dbContext.Transactions.ToList();
        }


        public void UpdateAll(List<TransactionDTO> transactionDTO)
        {
            var allEntities = _dbContext.Transactions.ToList();
            _dbContext.Transactions.RemoveRange(transactionDTO);

            _dbContext.Transactions.AddRange(transactionDTO);
            _dbContext.SaveChanges();
        }
    }
}