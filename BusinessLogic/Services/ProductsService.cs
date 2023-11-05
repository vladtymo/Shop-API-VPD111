using AutoMapper;
using BusinessLogic.ApiModels;
using BusinessLogic.Dtos;
using BusinessLogic.Exceptions;
using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Data.Entities;
using DataAccess.Repositories;
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
        //private readonly Shop111DbContext ctx;
        private readonly IRepository<Product> productsRepo;

        private readonly IMapper mapper;

        public ProductsService(IRepository<Product> productsRepo, IMapper mapper)
        {
            this.productsRepo = productsRepo;
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

            productsRepo.Insert(mapper.Map<Product>(model));
            productsRepo.Save();
        }

        public void Delete(int id)
        {
            var item = productsRepo.GetByID(id);

            if (item == null) throw new HttpException("Invalid product ID.", HttpStatusCode.NotFound); // 404

            productsRepo.Delete(item);
            productsRepo.Save();
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

            productsRepo.Update(mapper.Map<Product>(model));
            productsRepo.Save();
        }

        public List<ProductDto> Get()
        {
            var items = productsRepo.Get(includeProperties: "Category");
            return mapper.Map<List<ProductDto>>(items);
        }

        public ProductDto? Get(int id)
        {
            var item = productsRepo.GetByID(id);

            if (item == null) throw new HttpException("Invalid product ID.", HttpStatusCode.NotFound);

            return mapper.Map<ProductDto>(item);
        }
    }
}
