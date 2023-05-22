using Microsoft.EntityFrameworkCore;
using RaulPruebaTecnica.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace RaulPruebaTecnica.Repository
{
    public class GNBDbContext : DbContext
    {
        public GNBDbContext(DbContextOptions<GNBDbContext> options) : base(options)
        {
        }

        public DbSet<ConversionRateDTO> ConversionRates { get; set; }
        public DbSet<TransactionDTO> Transactions { get; set; }
    }

}
