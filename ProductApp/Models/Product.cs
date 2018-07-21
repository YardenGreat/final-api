using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// Had to manually add the reference to this namespace to the project's referenced assemblies.
// was not added by default
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using Newtonsoft.Json;

namespace ProductApp.Models
{
    [DataContract]
    public class Product
    {
        const string PRODUCTS_FILE = @"C:\Users\Yarden\Source\Repos\ProductApp\ProductApp\Products.json";

        // Must also have an empty Ctor for form post requests with "product" type data to work properly
        public Product()
        {

        }

        public Product(int id, string name, string category, double price)
        {
            this.Id = id;
            this.Name = name;
            this.Category = category;
            this.Price = price;           
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Category { get; set; }

        [DataMember]
        public double Price { get; set; }

        public static void WriteArrayToFile(Product[] products)
        {
            JsonSerializer serializer = new JsonSerializer();

            using (var writer = new StreamWriter(PRODUCTS_FILE))
            using (JsonWriter jwriter = new JsonTextWriter(writer))
            {
                serializer.Serialize(jwriter, products);
            }
        }
    }
}