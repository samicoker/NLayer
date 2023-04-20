using NLayer.Core.DTOs;
using NLayer.Core.Models;

namespace NLayer.Core.Services
{
    public interface IProductService : IService<Product>
    {
        //Task<List<ProductWithCategoryDto>> GetProductWithCategory(); // web için
        Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory(); // API için
    }
}
