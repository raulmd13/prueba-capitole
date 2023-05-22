using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaulPruebaTecnica.Models.DTO
{
    public class TransactionDTO
    {
        public string Sku { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }
    }
}
