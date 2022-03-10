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

        public TransferController(ITransferDao trDao)
        {
            transferDao = trDao;
        }

        [HttpGet("{id}")]
        public ActionResult<Transfer> GetAccount(int transferId)
        {
            Transfer transfer = transferDao.GetTransfer(transferId);

            if (transfer != null)
            {
                return Ok(transfer);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet()]
        public ActionResult<Transfer> GetOwnTransfers(int userId)
        {
            List<Transfer> transfers = transferDao.GetOwnTransfers(userId);

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
            Transfer transfer = transferDao.AddTransfer(transferToAdd);

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
