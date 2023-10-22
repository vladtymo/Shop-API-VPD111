using BusinessLogic.ApiModels;
using DataAccess.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IProductsService
    {
        List<Product> Get();
        Product? Get(int id);
        void Create(CreateProductModel model);
        void Edit(EditProductModel model);
        void Delete(int id);
    }
}
