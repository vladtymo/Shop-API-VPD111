using AutoMapper;
using BusinessLogic.ApiModels;
using BusinessLogic.Dtos;
using BusinessLogic.Exceptions;
using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class ProductsService : IProductsService
    {
        private readonly Shop111DbContext ctx;
        private readonly IMapper mapper;

        public ProductsService(Shop111DbContext ctx, IMapper mapper)
        {
            this.ctx = ctx;
            this.mapper = mapper;
        }

        public void Create(CreateProductModel model)
        {
            //var entity = new Product()
            //{
            //    Name = model.Name,
            //    CategoryId = model.CategoryId,
            //    Description = model.Description,
            //    Discount = model.Discount,
            //    ImageUrl = model.ImageUrl,
            //    Price = model.Price,
            //    InStock = model.InStock
            //};

            ctx.Products.Add(mapper.Map<Product>(model));
            ctx.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = ctx.Products.Find(id);

            if (item == null) throw new HttpException("Invalid product ID.", HttpStatusCode.NotFound); // 404

            ctx.Products.Remove(item);
            ctx.SaveChanges();
        }

        public void Edit(EditProductModel model)
        {
            //var entity = new Product()
            //{
            //    Id = model.Id,
            //    Name = model.Name,
            //    CategoryId = model.CategoryId,
            //    Description = model.Description,
            //    Discount = model.Discount,
            //    ImageUrl = model.ImageUrl,
            //    Price = model.Price,
            //    InStock = model.InStock
            //};

            ctx.Products.Update(mapper.Map<Product>(model));
            ctx.SaveChanges();
        }

        public List<ProductDto> Get()
        {
            var items = ctx.Products.Include(x => x.Category).ToList();
            return mapper.Map<List<ProductDto>>(items);
        }

        public ProductDto? Get(int id)
        {
            var item = ctx.Products.Find(id);

            if (item == null) throw new HttpException("Invalid product ID.", HttpStatusCode.NotFound);

            return mapper.Map<ProductDto>(item);
        }
    }
}
