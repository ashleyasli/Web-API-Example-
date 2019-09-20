using Ashley.WebApi.Entities;
using Ashley.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ashley.WebApi.DataAccess
{
    internal interface IProductDal : IEntityRepository<Product>
    {
        List<ProductModel> GetProductWithDetails();
    }
}
