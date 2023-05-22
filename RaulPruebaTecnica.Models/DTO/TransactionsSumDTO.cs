using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaulPruebaTecnica.Models.DTO
{
    public class TransactionsSumDTO
    {
        public decimal Total { get; set; }
        public List<TransactionDTO> Transactions { get; set; }

        public TransactionsSumDTO()
        {
            Total = 0;
            Transactions = new List<TransactionDTO>();
        }
    }
}
