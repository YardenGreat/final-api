using ProductApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Web;
using System.Web.SessionState;
using System.Runtime.Caching;

namespace ProductsApp.Controllers
{
    // The RoutePrefix attribute represents how to access the specific controller
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        const string PRODUCTS_FILE = @"C:\Users\Yarden\Source\Repos\ProductApp\ProductApp\Products.json";
        /*Product[] products = new Product[]
        {
            new Product (1, "Tomato Soup", "Groceries", 1 ),
            new Product (2, "Yo-yo", "Toys", 3.75 ),
            new Product (3, "Hammer", "Hardware", 16.99 )
        };*/

        // Initialized here to guarantee that 'products' will be cached and not reset after each http request
        Product[] products = ReadProductsFromFile();

        // Web api would know to address all incoming http 'get' requests
        // (that refer to the default controller URL '/api/products') to this action
        // even without the Route attribute because its name starts with the word 'get'.
        [Route("")]
        public IEnumerable<Product> GetAllProducts()
        {
            /*Product.WriteArrayToFile(products);

            return null;*/
            /*if (HttpContext.Current.Session?["products"] == null)
            {
                
                this.ReadProductsFromFile();
            }

            products = HttpContext.Current.Session["products"] as Product[];*/

            return products;
        }

        // The parameter from the url and the parameter received by the action must have the same name
        [Route("getSpecificProduct/{id}")]
        public IHttpActionResult GetProduct(int id)
        {
            var product = products.FirstOrDefault((p) => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        [Route("addProduct")]
        public IHttpActionResult AddProduct([FromBody]Product product)
        { 
            if (!this.isNewProductValid(product))
            {
                return BadRequest();
            }

            products = products.Concat(new[] { product }).ToArray();
            Product.WriteArrayToFile(products);
            return Ok();
        }

        private static Product[] ReadProductsFromFile()
        {
            using (StreamReader reader = new StreamReader(PRODUCTS_FILE))
            {
                return JsonConvert.DeserializeObject<Product[]>(reader.ReadToEnd());
            }
        }

        private bool isNewProductValid(Product product)
        {
            int id;

            if (product?.Id == null || product.Name == null || product.Category == null || !int.TryParse(product.Id.ToString(), out id))
            {
                return false;
            }

            product.Id = id;
           
            return true;
        }
    }
}
