using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;
using NLayer.Web.Filters;
using NLayer.Web.Services;

namespace NLayer.Web.Controllers
{
    public class ProductsController : Controller
    {
        //private readonly IProductService _service;
        //private readonly ICategoryService _categoryService;
        //private readonly IMapper _mapper;
        //public ProductsController(IProductService service, ICategoryService categoryService, IMapper mapper)
        //{
        //    _service = service;
        //    _categoryService = categoryService;
        //    _mapper = mapper;
        //}
        private readonly ProductApiService _productApiService;
        private readonly CategoryApiService _categoryApiService;
        public ProductsController(ProductApiService productApiService, CategoryApiService categoryApiService)
        {
            _productApiService = productApiService;
            _categoryApiService = categoryApiService;
        }

        public async Task<IActionResult> Index()
        {
            //var CustomResponse = await _service.GetProductsWithCategory();
            ////return View(CustomResponse);
            //return View(CustomResponse.Data); // service apiye göre ayarlanınca buradan .Data dönmek zorundayız yoksa hata veriyor
            return View(await _productApiService.GetProductsWithCategoryAsync());
        }

        public async Task<IActionResult> Save()
        {
            // var categories = await _categoryService.GetAllAsync();

            //var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());

            var categoriesDto = await _categoryApiService.GetAllAsync();

            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {


            if (ModelState.IsValid)
            {
                await _productApiService.SaveAsync(productDto);

               // await _service.AddAsync(_mapper.Map<Product>(productDto));

                return RedirectToAction(nameof(Index));
            }

            //var categories = await _categoryService.GetAllAsync();

            //var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());

            var categoriesDto = await _categoryApiService.GetAllAsync();

            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name");

            return View();
        }
        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        public async Task<IActionResult> Update(int id)
        {
            //var product = await _service.GetByIdAsync(id);

            //var categories = await _categoryService.GetAllAsync();

            //var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());
            var product = await _productApiService.GetByIdAsync(id);
            var categoriesDto = await _categoryApiService.GetAllAsync();

            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name", product.CategoryId);

            return View(product);
            //return View(_mapper.Map<ProductDto>(product));

        }
        [HttpPost]
        public async Task<IActionResult> Update(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                //await _service.UpdateAsync(_mapper.Map<Product>(productDto));
                await _productApiService.UpdateAsync(productDto);
                return RedirectToAction(nameof(Index));
            }

            //var categories = await _categoryService.GetAllAsync();

            //var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());
            var categoriesDto = await _categoryApiService.GetAllAsync();

            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name", productDto.CategoryId);

            return View(productDto);
        }
       
        public async Task<IActionResult> Remove(int id)
        {
            //var product = await _service.GetByIdAsync(id);

            //await _service.RemoveAsync(product);

            await _productApiService.RemoveAsync(id);

            return RedirectToAction(nameof(Index));

        }
    }
}
