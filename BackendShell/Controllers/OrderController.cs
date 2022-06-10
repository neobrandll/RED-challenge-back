using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using API.DataModels;
using API.Enums;
using API.Projections;
using API.Models;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;

        }


        // Get all
        [HttpGet()]
        public JsonResult GetAll()
        {
        var result = from order in _context.Orders
        select new OrderProjection(order);
           
            return new JsonResult(Ok(result));
        }

        // Delete
        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var result = _context.Orders.Find(id);

            if (result == null)
                return new JsonResult(NotFound());

            _context.Orders.Remove(result);
            _context.SaveChanges();

            return new JsonResult(NoContent());
        }

        // Create
        [HttpPost]
        public JsonResult Create(Order order)
        {
         _context.Orders.Add(order);
         _context.SaveChanges();

         return new JsonResult(Ok(new OrderProjection(order)));
        }


        // Update
        [HttpPut]
        public JsonResult Update(Order order)
        {
            var result = _context.Orders.Find(order.OrderId);
            if (result == null)
                return new JsonResult(NotFound());
            result = order;

                 _context.SaveChanges();

            return new JsonResult(Ok(order));
        }


        // Search
        [HttpGet("/search")]
        public JsonResult Search([FromQuery]OrderQuery orderQuery  )
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(BadRequest());
            }
            var result = from order in _context.Orders
                         where order.OrderType == orderQuery.orderType
                         where order.CustomerName.Contains(orderQuery.customerName)
                         select new OrderProjection(order);
                         return new JsonResult(Ok(result));
        }



        //[HttpGet]
        //public IEnumerable<Projections.Order> Get()
        //{
        //    // rewrite to use IDataProvider using dependency injection
        //    //var data = Program.Data.Values.Select(o => new Projections.Order(o));
        //    //return data;
        //}

        // Add a Search endpoint


    }
}