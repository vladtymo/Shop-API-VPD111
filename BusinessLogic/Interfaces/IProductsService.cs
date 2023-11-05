using BusinessLogic.ApiModels;
using BusinessLogic.Dtos;

namespace BusinessLogic.Interfaces
{
    public interface IProductsService
    {
        List<ProductDto> Get();
        ProductDto? Get(int id);
        void Create(CreateProductModel model);
        void Edit(EditProductModel model);
        void Delete(int id);
    }
}
