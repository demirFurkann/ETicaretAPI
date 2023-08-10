using ETicaret.Application.Repositories;
using ETicaret.Application.ViewModels.Products;
using ETicaretAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly private IProductWriteRepository _productWriteRep;
        readonly private IProductReadRepository _productReadRep;

      
        public ProductsController(IProductWriteRepository productWriteRep, IProductReadRepository productReadRep)
        {
            _productWriteRep = productWriteRep;
            _productReadRep = productReadRep;
           
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            #region Denemeler
            //await _productWriteRep.AddRangeAsync(new()
            //{
            //    new() { ProductName="Product 1",Price=100,CreatedDate=DateTime.UtcNow,Stock=50},
            //    new() { ProductName="Product2",Price=200,CreatedDate=DateTime.UtcNow,Stock=60},
            //    new() { ProductName="Product 3",Price=300,CreatedDate=DateTime.UtcNow,Stock=30},
            //});

            //var count = await _productWriteRep.SaveAsync();
            //Product p = await _productReadRep.GetByIdAsync(16,false);
            //p.ProductName = "Ahmet";
            //await _productWriteRep.SaveAsync();

            //_productWriteRep.AddAsync(new() { ProductName = "C PRoduct", Price = 1.500F, Stock = 250, CreatedDate = DateTime.UtcNow });
            //_productWriteRep.SaveAsync();
            //var customerId = 1;
            //await _customerWriteRep.AddAsync(new() { ID = customerId,FirstName="Muuiddin"});

            //await _orderWriteRep.AddAsync(new() { Description = "oldu la", Address = "Kocaeli,Gebze" ,CustomerId= customerId});
            //await _orderWriteRep.AddAsync(new() { Description = "kesin oldu la", Address = "Kocaeli,Dilovası", CustomerId = customerId });
            //await _orderWriteRep.SaveAsync();

            #endregion


            return Ok(_productReadRep.GetAll(false));

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
          return Ok(await _productReadRep.GetByIdAsync(id,false));
        }

        [HttpPost]
        public async Task<IActionResult> Post(VM_Create_Product model)
        {
            await _productWriteRep.AddAsync(new()
            {
                ProductName = model.Name,
                Price = model.Price,
                Stock = model.Stock,
            });
            await _productWriteRep.SaveAsync();
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Put(VM_Update_Product model)
        {
            Product product = await _productReadRep.GetByIdAsync(model.Id);

            product.Stock = model.Stock;
            product.ProductName=model.Name;
            product.Price = model.Price;

            await _productWriteRep.SaveAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productWriteRep.RemoveAsync(id);
            await _productWriteRep.SaveAsync();
            return Ok();
        }







    }
}
