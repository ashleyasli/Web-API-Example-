using Ashley.WebApi.DataAccess;
using Ashley.WebApi.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ashley.WebApi.Controllers
{
    [Route("api/products")]
    public class ProductsController : Controller
    {
        IProductDal _productDal;

        internal ProductsController(IProductDal productDal)
        {
            _productDal = productDal;
        }
        [HttpGet("")]
        public IActionResult Get()
        {
            var products = _productDal.GetList();
            return Ok(products);
        }

        [HttpGet("{productId}")]
        public IActionResult Get(int productId)
        {
            try
            {
                var product = _productDal.Get(p => p.ProductId==productId);

                if (product == null)
                {
                    return NotFound($"There is no product with Id ={productId}");
                }
                return Ok(product);
            }
            catch (Exception)
            {

             
            }
            return BadRequest();
        }

        public IActionResult Post(Product product)
        {
            return Ok();
        }
    }
}
