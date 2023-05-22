using RaulPruebaTecnica.Models.DTO;

namespace RaulPruebaTecnica.Repository
{
    public class ConversionRepository : IConversionRepository
    {
        private readonly GNBDbContext _dbContext;

        public ConversionRepository(GNBDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<ConversionRateDTO> GetAll()
        {
            return _dbContext.ConversionRates.ToList();
        }


        public void UpdateAll(List<ConversionRateDTO> conversionRateDTO)
        {
            var allEntities = _dbContext.ConversionRates.ToList();
            _dbContext.ConversionRates.RemoveRange(conversionRateDTO);

            _dbContext.ConversionRates.AddRange(conversionRateDTO);
            _dbContext.SaveChanges();
        }
    }
}
