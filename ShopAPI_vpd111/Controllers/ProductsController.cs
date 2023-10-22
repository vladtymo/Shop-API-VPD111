using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShopAPI_vpd111.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // DO NOT use EF classes, use Business services instead
        //private readonly Shop111DbContext ctx;
        private readonly IProductsService service;

        public ProductsController(IProductsService service)
        {
            this.service = service;
        }

        [HttpGet("all")]
        public IActionResult Get()
        {
            // DO NOT write business logic here, use services
            return Ok(service.Get()); // status: 200
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(service.Get(id));
        }

        [HttpPost]
        public IActionResult Create(Product model)
        {
            if (!ModelState.IsValid) return BadRequest();

            service.Create(model);

            return Ok();
        }

        [HttpPut]
        public IActionResult Edit(Product model)
        {
            if (!ModelState.IsValid) return BadRequest();

            service.Edit(model);

            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            service.Delete(id);

            return Ok();
        }
    }
}
