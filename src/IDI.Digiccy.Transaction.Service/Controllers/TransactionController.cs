using IDI.Digiccy.Domain.Transaction;
using IDI.Digiccy.Models.Transaction;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Digiccy.Transaction.Service.Controllers
{
    [Route("api/trans")]
    public class TransactionController : Controller
    {
        private readonly ITransactionService service;

        public TransactionController(ITransactionService service)
        {
            this.service = service;
        }

        [HttpGet("queue")]
        public TranQueue GetQueue()
        {
            return service.Queue();
        }
    }
}
