using ETicaret.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly private IProductWriteRepository _productWriteRep;
        readonly private IProductReadRepository _productReadRep;

        readonly private IOrderWriteRepository _orderWriteRep;
        readonly private IOrderReadRepository _orderReadRep;


        readonly private ICustomerWriteRepository _customerWriteRep;
        public ProductsController(IProductWriteRepository productWriteRep, IProductReadRepository productReadRep, IOrderWriteRepository orderWriteRep, ICustomerWriteRepository customerWriteRep, IOrderReadRepository orderReadRep)
        {
            _productWriteRep = productWriteRep;
            _productReadRep = productReadRep;
            _orderWriteRep = orderWriteRep;
            _customerWriteRep = customerWriteRep;
            _orderReadRep = orderReadRep;
        }
        [HttpGet]
        public async Task Get()
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
            #endregion

            //_productWriteRep.AddAsync(new() { ProductName = "C PRoduct", Price = 1.500F, Stock = 250, CreatedDate = DateTime.UtcNow });
            //_productWriteRep.SaveAsync();
            //var customerId = 1;
            //await _customerWriteRep.AddAsync(new() { ID = customerId,FirstName="Muuiddin"});

            //await _orderWriteRep.AddAsync(new() { Description = "oldu la", Address = "Kocaeli,Gebze" ,CustomerId= customerId});
            //await _orderWriteRep.AddAsync(new() { Description = "kesin oldu la", Address = "Kocaeli,Dilovası", CustomerId = customerId });
            //await _orderWriteRep.SaveAsync();

           Order order= await _orderReadRep.GetByIdAsync(1);
            order.Address = "İst";
            await _orderWriteRep.SaveAsync();


        }



        //[HttpGet("id")]
        //public async Task<IActionResult> Get(int id)
        //{
        //    Product product = await _productReadRep.GetByIdAsync(id);
        //    return Ok(product);
        //}

    }
}
