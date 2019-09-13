using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ashley.WebApi.Controllers
{
    public class ProductsController : Controller
    {
        [Route("api/products")]
        [HttpGet("")]
        public string Get()
        {
            return "Product1";
        }
    }
}
