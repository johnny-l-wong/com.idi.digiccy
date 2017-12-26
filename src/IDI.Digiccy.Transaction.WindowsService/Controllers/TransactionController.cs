using IDI.Digiccy.Domain.Transaction;
using IDI.Digiccy.Models.Transaction;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Digiccy.Transaction.WindowsService.Controllers
{
    [Route("api/trans")]
    public class TransactionController : Controller
    {
        [HttpGet("queue")]
        public OrderQueue GetQueue()
        {
            return TransactionQueue.Instance.Current();
        }
    }
}
