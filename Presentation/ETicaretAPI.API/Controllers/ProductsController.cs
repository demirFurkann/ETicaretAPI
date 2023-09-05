using ETicaret.Application.Abstractions.Storage;
using ETicaret.Application.Repositories;
using ETicaret.Application.Repositories.ProductImageFile;
using ETicaret.Application.RequestParameters;

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
        private readonly IWebHostEnvironment _webHostEnvironment;

        readonly IFileWriteRepository _fileWriteRep;
        readonly IFileReadRepository _fileReadRep;
        readonly IProductImageReadRepository _productImageReadRep;
        readonly IProductImageWriteRepository _productImageWriteRep;
        readonly IInvoiceFileReadRepository _invoiceFileReadRep;
        readonly IInvoiceFileWriteRepository _invoiceFileWriteRep;
        readonly IStorageService _storageService;



        public ProductsController(IProductWriteRepository productWriteRep, IProductReadRepository productReadRep, IWebHostEnvironment webHostEnvironment, IProductImageWriteRepository productImageWriteRep, IInvoiceFileReadRepository invoiceFileReadRep, IInvoiceFileWriteRepository invoiceFileWriteRep, IFileWriteRepository fileWriteRepository, IStorageService storageService)
        {
            _productWriteRep = productWriteRep;
            _productReadRep = productReadRep;
            _webHostEnvironment = webHostEnvironment;

            _productImageWriteRep = productImageWriteRep;
            _invoiceFileReadRep = invoiceFileReadRep;
            _invoiceFileWriteRep = invoiceFileWriteRep;
            _fileWriteRep = fileWriteRepository;
            _storageService = storageService;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Pagination pagination)
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
            var totalCount = _productReadRep.GetAll(false).Count();

            var products = _productReadRep.GetAll(false).Skip(pagination.Page * pagination.Size).Take(pagination.Size).Select(x => new
            {

                x.ID,
                x.Name,
                x.Stock,
                x.Price,
                x.CreatedDate,
                x.UpdateDate
            });

            return Ok(new
            {
                totalCount,
                products
            });

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _productReadRep.GetByIdAsync(id, false));
        }

        [HttpPost]
        public async Task<IActionResult> Post(VM_Create_Product model)
        {

            await _productWriteRep.AddAsync(new()
            {
                Name = model.Name,
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
            product.Name = model.Name;
            product.Price = model.Price;

            await _productWriteRep.SaveAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productWriteRep.RemoveAsync(id);
            await _productWriteRep.SaveAsync();
            return Ok(new
            {
                message = "Silme Başarili!"
            });
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload()
        {

            var datas = await _storageService.UploadAsync("resource/files", Request.Form.Files);

            //var datas = await _fileService.UploadAsync("resource/files", Request.Form.Files);
            await _productImageWriteRep.AddRangeAsync(datas.Select(d => new ProductImageFile()
            {
                FileName = d.fileName,
                Path = d.pathOrContainer,
                Storage=_storageService.StorageName
            }).ToList());

            //await _productImageWriteRep.SaveAsync();

            //await _invoiceFileWriteRep.AddRangeAsync(datas.Select(d => new InvoiceFile()
            //{
            //    FileName = d.fileName,
            //    Path = d.path,
            //    Price = new Random().Next(0, 166)

            //}).ToList()); ;

            //await _invoiceFileWriteRep.SaveAsync();


            //await _fileWriteRep.AddRangeAsync(datas.Select(d => new ETicaretAPI.Domain.Entities.File()
            //{
            //    FileName = d.fileName,
            //    Path = d.path,

            //}).ToList()); 

            //await _fileWriteRep.SaveAsync();


            return Ok();


        }



    }
}
