using NLayer.Core.DTOs;

namespace NLayer.Web.Services
{
    public class ProductApiService
    {
        /*program.cs e 'builder.Services.AddHttpClient<ProductApiService>(opt =>
        {
            opt.BaseAddress = new Uri(builder.Configuration["BaseUrl"]);
        });' ekledik*/
        private readonly HttpClient _httpClient;
        public ProductApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProductWithCategoryDto>> GetProductsWithCategoryAsync()
        {
            //var resp = await _httpClient.GetAsync("products/GetProductsWithCategory");
            //if (resp.IsSuccessStatusCode)
            //{
            //    resp.Content.ReadAsStringAsync
            //}  eskiden böyle okunurdu, resp.Content.ReadAsStringAsync, jsona cast edilirdi ama .Net5 ten sonra yeni sürümle aşağıdaki gibi okuyoruz. Avantajı ise direkt json olarak vermesi

            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<ProductWithCategoryDto>>>("products/GetProductsWithCategory");

            return response.Data;
        }
        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<ProductDto>>($"products/{id}");

            //if (response.Errors.Any())
            //{

            //}

            return response.Data;
        }
        public async Task<ProductDto> SaveAsync(ProductDto newProduct)
        {
            var response = await _httpClient.PostAsJsonAsync("products", newProduct);

            if (!response.IsSuccessStatusCode == false) return null;

            var responseBody = await response.Content.ReadFromJsonAsync<CustomResponseDto<ProductDto>>();

            return responseBody.Data;
        }

        public async Task<bool> UpdateAsync(ProductDto newProduct)
        {
            var response = await _httpClient.PutAsJsonAsync("products", newProduct);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> RemoveAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"products/{id}");
            return response.IsSuccessStatusCode;
        }
        
    }
}
