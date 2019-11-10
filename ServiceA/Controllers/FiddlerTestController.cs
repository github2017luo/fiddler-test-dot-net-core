using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceA.Client;

namespace ServiceA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FiddlerTestController : ControllerBase
    {
        private readonly IRestHttpClient client;

        public FiddlerTestController(IRestHttpClient client)
        {
            this.client = client;
        }

        // GET api/values
        [HttpGet]   
        public async Task<string> Get()
        {
            var message = await client.GetString("http://localhost:5001/api/values");
            return message;
        }

      
    }
}
