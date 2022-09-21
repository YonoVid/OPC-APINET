using Ejercicio9.DOT.v1;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Ejercicio9.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly String _externalApiURL = "https://fakestoreapi.com/products";
        private readonly HttpClient _httpClient;

        public ProductsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [MapToApiVersion("1.0")]
        [HttpGet(Name = "GetProductsData")]
        public async Task<IActionResult> GetProductsAsync()
        {
            var response = await _httpClient.GetStreamAsync(_externalApiURL);

            var products = await JsonSerializer.DeserializeAsync<Product[]>(response);

            return Ok(products);
        }
    }
}
