using Microsoft.AspNetCore.Mvc;
using Ski_ServiceNoSQL.Models;
using Ski_ServiceNoSQL.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ski_ServiceNoSQL.Controllers
{
    [Route("api")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;
        public OrdersController(IOrdersService ordersService)
        {
            this._ordersService = ordersService;
        }
        // GET: api/<OrdersController>
        [HttpGet]
        public ActionResult<List<Orders>> Get()
        {
            return _ordersService.Get();
        }

        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public ActionResult<Orders> Get(string id)
        {
            return _ordersService.Get(id);
        }

        // POST api/<OrdersController>
        [HttpPost]
        public void Post([FromBody] Orders value)
        {
            _ordersService.Create(value);
        }

        // PUT api/<OrdersController>/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] Orders value)
        {
            _ordersService.Update(id, value);
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _ordersService.Remove(id);
        }
    }
}
