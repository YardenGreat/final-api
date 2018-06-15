using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProductApp.Models;

namespace ProductApp.Controllers
{
    [RoutePrefix("api/customers")]
    public class CustomersController : ApiController
    {
        Customer[] customers = new Customer[]
        {
            new Customer{Id = 1, FirstName = "Larry", LastName= "Jenkins"},
            new Customer{Id = 2, FirstName = "Jenny", LastName= "Miller"},
            new Customer{Id = 3, FirstName = "Mike", LastName= "Tyson"}
        };

        [Route("")]
        public IEnumerable<Customer> GetAllCustomers()
        {
            return customers;
        }

        // The parameter from the url and the parameter received by the action must have the same name
        [Route("getSpecificCustomer/{id}")]
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = customers.FirstOrDefault((p) => p.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }
    }
}
