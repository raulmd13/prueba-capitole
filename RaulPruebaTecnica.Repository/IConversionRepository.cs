using Microsoft.EntityFrameworkCore;
using RaulPruebaTecnica.Models.DTO;

namespace RaulPruebaTecnica.Repository
{
    public interface IConversionRepository
    {
        public IEnumerable<ConversionRateDTO> GetAll();
        public void UpdateAll(List<ConversionRateDTO> conversionRateDTO);
    }
}