using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.DTO;
using Vidly.Models;

namespace Vidly.Controllers.API
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        //GET /api/customers
        public IHttpActionResult GetCustomers()
        {
            var customerDTOs = _context.Customers.ToList().Select(Mapper.Map<Customers, CustomerDTO>);
            return Ok(customerDTOs);
        }

        //GET /api/customers/1
        public IHttpActionResult GetCustomers(int id)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.ID == id);

            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Ok(Mapper.Map<Customers, CustomerDTO> (customer));
        }

        //POST /api/customers
        [HttpPost]
        public IHttpActionResult CreateCustomers(CustomerDTO customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customer = Mapper.Map<CustomerDTO, Customers>(customerDto);

            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.ID = customer.ID;

            return Created(new Uri(Request.RequestUri +"/" + customer.ID), customerDto);
        }

        //PUT /api/customers/1
        [HttpPut]
        public void UpdateCustomers(int id, CustomerDTO customerDto)
        {
            if (ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customerInDb = _context.Customers.FirstOrDefault(c => c.ID == id);

            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(customerDto, customerInDb);
           
            _context.SaveChanges();
        }

        //DELETE /api/customers/1
        [HttpDelete]
        public void DeleteCustomers(int id)
        {
            var customerInDb = _context.Customers.FirstOrDefault(c => c.ID == id);

            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();
        }
    }
}
