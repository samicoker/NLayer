using Microsoft.AspNetCore.Mvc;
using NLayer.Core.Services;

namespace NLayer.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _service;
        public ProductsController(IProductService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var CustomResponse = await _service.GetProductWithCategory();
            return View(CustomResponse);
        }
    }
}
