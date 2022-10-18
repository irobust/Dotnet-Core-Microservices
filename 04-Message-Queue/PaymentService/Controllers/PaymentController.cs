using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PaymentService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private OrderService _storage;
        public PaymentController(OrderService storage){
            this._storage = storage;
        }

        [HttpGet]
        public IEnumerable<Order> Get()
        {
           return this._storage.Get();
        }
    }
}
