using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Store.Models;

namespace Store.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class HomeController : ControllerBase
    {
        private static string ACCOUNT_SERVICE_API_BASE = Environment.GetEnvironmentVariable("ACCOUNT_SERVICE_API_BASE");
        private static string INVENTORY_SERVICE_API_BASE = Environment.GetEnvironmentVariable("INVENTORY_SERVICE_API_BASE");
        private static string SHOPPING_SERVICE_API_BASE = Environment.GetEnvironmentVariable("SHOPPING_SERVICE_API_BASE");

        public HomeController()
        {
            if (ACCOUNT_SERVICE_API_BASE == null)
            {
                ACCOUNT_SERVICE_API_BASE = "http://localhost:5001/api";
            }

            if (INVENTORY_SERVICE_API_BASE == null)
            {
                INVENTORY_SERVICE_API_BASE = "http://localhost:5002/api";
            }

            if (SHOPPING_SERVICE_API_BASE == null)
            {
                SHOPPING_SERVICE_API_BASE = "http://localhost:5003/api";
            }
        }

        [HttpGet]
        public async Task<ActionResult<Commerce>> Index()
        {
            var client = new HttpClient();

            //represents the current consumer
            var user = new Consumer();
            HttpResponseMessage res1 = await client.GetAsync($"{ACCOUNT_SERVICE_API_BASE}/consumers/5");
            if (res1.IsSuccessStatusCode)
            {
                var result = res1.Content.ReadAsStringAsync().Result;
                user = JsonConvert.DeserializeObject<Consumer>(result);
            }

            //represents the possible list of products that can be purchased
            var products = new List<Product>();
            HttpResponseMessage res2 = await client.GetAsync($"{INVENTORY_SERVICE_API_BASE}/products");
            if (res2.IsSuccessStatusCode)
            {
                var result = res2.Content.ReadAsStringAsync().Result;
                products = JsonConvert.DeserializeObject<List<Product>>(result);
            }

            //represents the current consumers selected shopping cart items
            var cart = new Cart();
            HttpResponseMessage res3 = await client.GetAsync($"{SHOPPING_SERVICE_API_BASE}/cart/30");
            if (res3.IsSuccessStatusCode)
            {
                var result = res3.Content.ReadAsStringAsync().Result;
                cart = JsonConvert.DeserializeObject<Cart>(result);
            }

            var commerce = new Commerce()
            {
                User = user,
                Products = products,
                Cart = cart
            };

            return commerce;
        }
    }
}