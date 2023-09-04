using ETicaret.Application.Repositories.ProductImageFile;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories
{
    public class ProductImageFileReadRepository : ReadRepository<ProductImageFile>, IProductImageReadRepository
    {
        public ProductImageFileReadRepository(ETicaretAPIDBContext context) : base(context)
        {
        }
    }
}
