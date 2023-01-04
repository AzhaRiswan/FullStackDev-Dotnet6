using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FullStackDev.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class transactionController : ControllerBase
    {
        private readonly DataContext dataContext;

        public transactionController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<transaction>>> Get()
        {
            return Ok(await dataContext.Transactions.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<transaction>> Get(int id)
        {
            var transaction = await dataContext.Transactions.FindAsync(id);
            if (transaction == null)
                return BadRequest("Transaction Not found");
            return Ok(transaction);
        }

        [HttpPost]

        public async Task<ActionResult<List<transaction>>> AddTransaction(transaction transaction)
            {
                dataContext.Transactions.Add(transaction);
                await dataContext.SaveChangesAsync();

                return Ok(await dataContext.Transactions.ToListAsync());
            }

        [HttpPut]

        public async Task<ActionResult<List<transaction>>> UpdateTransaction(transaction request)
        {
            var dbtransaction = await dataContext.Transactions.FindAsync(request.id);
            if (dbtransaction == null)
                return BadRequest("Transaction Not found");

            dbtransaction.productName = request.productName;
            dbtransaction.productDescription = request.productDescription;
            dbtransaction.price = request.price;
            dbtransaction.customerName = request.customerName;
            dbtransaction.customerAddress = request.customerAddress;
            dbtransaction.actualPrice = request.actualPrice;

            await dataContext.SaveChangesAsync();

            return Ok(await dataContext.Transactions.ToListAsync());
        }

        [HttpDelete]

        public async Task<ActionResult<List<transaction>>> Delete(int id)
        {
            var dbtransaction = await dataContext.Transactions.FindAsync(id);
            if (dbtransaction == null)
                return BadRequest("Transaction Not found");

            dataContext.Transactions.Remove(dbtransaction);
            await dataContext.SaveChangesAsync();

            return Ok(await dataContext.Transactions.ToListAsync());
        }

    }
}
