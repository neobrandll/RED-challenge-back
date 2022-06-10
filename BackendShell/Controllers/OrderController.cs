using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using API.DataModels;
using API.Enums;
using API.Projections;
using API.Models;
using Microsoft.Extensions.Logging;
using API.Core.IConfiguration;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {

        private readonly ILogger<OrderController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public OrderController(ILogger<OrderController> logger, IUnitOfWork unitOfWork)
        {
          _logger = logger;
          _unitOfWork = unitOfWork;
        }


        // Get all
        [HttpGet()]
        public async Task<JsonResult> GetAll()
        {

            var result = await _unitOfWork.Orders.GetAll();         
            return new JsonResult(Ok(result));
        }

        // Delete
        [HttpDelete]
        public async Task<JsonResult> Delete(int id)
        {
            var result = await _unitOfWork.Orders.Delete(id);
            if (result == false)
                return new JsonResult(NotFound());

            await _unitOfWork.CompleteAsync();
            return new JsonResult(NoContent());
        }

        // Create
        [HttpPost]
        public async Task<JsonResult> Create(Order order)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(BadRequest());
            }
            var result = await _unitOfWork.Orders.Create(order);
            if (result != null)
            {
                await _unitOfWork.CompleteAsync();
                return new JsonResult(Ok(result));
            }

            return new JsonResult(Problem("Something went wrong"));
        }


        // Update
        [HttpPut]
        public async Task<JsonResult> Update(Order order)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(BadRequest());
            }
            var result = await _unitOfWork.Orders.Update(order);

            if (result != null) {
                await _unitOfWork.CompleteAsync();
                return new JsonResult(Ok(result));
            } 

            return new JsonResult(Problem("Something went wrong"));
        }


        // Search
        [HttpGet("/search")]
        public async Task<JsonResult> Search([FromQuery]OrderQuery orderQuery  )
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(BadRequest());
            }
            var result = await _unitOfWork.Orders.GetAll(orderQuery);
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