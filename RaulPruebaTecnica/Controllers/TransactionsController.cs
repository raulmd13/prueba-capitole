using Microsoft.AspNetCore.Mvc;
using RaulPruebaTecnica.Models.DTO;
using RaulPruebaTecnica.Service;

namespace RaulPruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ILogger<TransactionsController> _logger;
        private readonly ITransactionsService _transactionsService;

        public TransactionsController(ILogger<TransactionsController> logger, ITransactionsService transactionsService)
        {
            _logger = logger;
            _transactionsService = transactionsService;
        }


        //GET: api/Transactions/5
        [HttpGet("{sku}")]
        public IActionResult Get(string sku)
        {
            var result = _transactionsService.GetTransactions(sku);

            if (result.Transactions.Any())
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }

        }

        //GET: api/Conversions
        [HttpGet]
        [Route("api/GetConversiones")]
        public IActionResult GetConversiones()
        {
            var result = _transactionsService.GetConversions();
            if (result.Count > 0)
            {
                return Ok(result);
            }
            else
            {
                return NoContent();
            }
        }

        //GET api/Transactions
        [HttpGet]
        [Route("api/GetTransacciones")]
        public IActionResult GetTransacciones()
        {
            var result = _transactionsService.GetTransactions();

            if (result.Count > 0)
            {
                return Ok(result);
            }
            else
            {
                return NoContent();
            }
        }
    }
}
