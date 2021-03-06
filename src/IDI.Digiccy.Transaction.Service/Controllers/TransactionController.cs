﻿using IDI.Core.Common;
using IDI.Digiccy.Common.Enums;
using IDI.Digiccy.Domain.Transaction.Services;
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

        [HttpGet("quotation")]
        public Result<Quotation> GetQuotation()
        {
            var quotation = new Quotation { Symbol = "BTC/USDH", Success = false };
            var result = service.GetKLine(KLineRange.M1);

            if (result.Status == ResultStatus.Success)
            {
                quotation.KLine = result.Data;
                quotation.Success = true;
            }

            return Result.Success(quotation);
        }

        [HttpPost("trade")]
        public Result Trade([FromBody]TradeInput input)
        {
            switch (input.Type)
            {
                case TranType.Bid:
                    return service.Bid(input.UID, input.Price, input.Size);
                case TranType.Ask:
                    return service.Ask(input.UID, input.Price, input.Size);
                default:
                    return Result.Fail("transaction failed.");
            }
        }

        [HttpPost("start")]
        public Result Start()
        {
            service.Start();

            return service.Running ? Result.Success("transaction service startup.") : Result.Fail("transaction service startup fail.");
        }

        [HttpPost("stop")]
        public Result Stop()
        {
            service.Stop();

            return service.Running ? Result.Success("transaction service stopped.") : Result.Fail("transaction service close failed.");
        }
    }
}
