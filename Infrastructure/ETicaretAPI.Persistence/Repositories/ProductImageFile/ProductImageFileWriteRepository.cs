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
    internal class ProductImageFileWriteRepository : WriteRepository<ProductImageFile>, IProductImageWriteRepository
    {
        public ProductImageFileWriteRepository(ETicaretAPIDBContext context) : base(context)
        {
        }
    }
}
