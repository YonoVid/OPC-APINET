using Ejercicio9.DOT.v2;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Ejercicio9.Controllers.v2
{
    [ApiVersion("2.0")]
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

        [MapToApiVersion("2.0")]
        [HttpGet(Name = "GetProductsData")]
        public async Task<IActionResult> GetProductsAsync()
        {
            var response = await _httpClient.GetStreamAsync(_externalApiURL);

            var products = await JsonSerializer.DeserializeAsync<Product[]>(response);

            foreach (var product in products) product.InternalId = Guid.NewGuid();

            return Ok(products);
        }
    }
}
