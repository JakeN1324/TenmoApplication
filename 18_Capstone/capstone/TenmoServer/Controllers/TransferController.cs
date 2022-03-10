using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.DAO;
using TenmoServer.Models;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private readonly ITransferDao transferDao;
        private readonly IAccountDAO accountDao;

        public TransferController(ITransferDao trDao, IAccountDAO accDao)
        {
            transferDao = trDao;
            accountDao = accDao;
        }

        [HttpGet("{id}")]
        public ActionResult<Transfer> GetTransfer(int id)
        {
            Transfer transfer = transferDao.GetTransfer(id);

            if (transfer != null)
            {
                return Ok(transfer);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("mytransfers/{id}")]
        public ActionResult<Transfer> GetOwnTransfers(int id)
        {
            List<Transfer> transfers = transferDao.GetOwnTransfers(id);

            if (transfers != null)
            {
                return Ok(transfers);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("add")]
        public ActionResult AddTransfer(Transfer transferToAdd)
        {
            Account accountFrom = accountDao.GetAccount(transferToAdd.AccountFrom);
            Account accountTo = accountDao.GetAccount(transferToAdd.AccountTo);
            decimal accountBalance = accountFrom.Balance;
            Transfer transfer = null;

            if(accountBalance >= transferToAdd.Amount)
            {
                accountDao.UpdateBalance(transferToAdd);
                transferToAdd.AccountFrom = accountFrom.AccountId;
                transferToAdd.AccountTo = accountTo.AccountId;
                transfer = transferDao.AddTransfer(transferToAdd);
            }

            if (transfer != null)
            {
                return Created($"{transfer.TransferId}", null);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
