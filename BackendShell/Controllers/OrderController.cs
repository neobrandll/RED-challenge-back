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
using Microsoft.AspNetCore.Authorization;
using API.Authorization;
using API.Models.Orders;
using Microsoft.AspNetCore.Identity;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
      public class OrderController : ControllerBase
    {

        private readonly ILogger<OrderController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthorizationService _authorizationService;
        public OrderController(ILogger<OrderController> logger, IUnitOfWork unitOfWork, IAuthorizationService authorizationService)
        {
          _logger = logger;
          _unitOfWork = unitOfWork;
          _authorizationService = authorizationService;
        }


        // Get all
        [HttpGet()]
        [AllowAnonymous]
        public async Task<ActionResult> GetAll()
        {

           var result = await _unitOfWork.Orders.GetAll();
            return Ok(result);
        }

        // Delete
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _unitOfWork.Orders.Delete(id);
            if (result == false)
                return NotFound();

            await _unitOfWork.CompleteAsync();
            return NoContent();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetById(int id)
        {
            var result = await _unitOfWork.Orders.GetById(id);
            if (result == null)
                return NotFound();
            SingleOrderProjection orderFormatted = new SingleOrderProjection(result);
                return Ok(orderFormatted);
                   }

        // Create
        [HttpPost]
        public async Task<ActionResult> Create(OrderModel order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var username = _unitOfWork.UserManager.GetUserName(User);
            if (User == null || username == null ) return Unauthorized();
            Order orderData = new Order();
            orderData.CustomerName = order.CustomerName;
            orderData.OrderType = order.OrderType;
            orderData.CustomerName = order.CustomerName;
            orderData.CreatedDate = order.CreatedDate;
            orderData.CreatedByUserName = username;

            var result = await _unitOfWork.Orders.Create(orderData);
            if (result != null)
            {
                await _unitOfWork.CompleteAsync();
                return Ok(result);
            }

            return Problem("Something went wrong");
        }


        // Update
        [HttpPut]
        public async Task<ActionResult> Update(OrderModel order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var username = _unitOfWork.UserManager.GetUserName(User);

            if (User == null || username == null)
                return Unauthorized();

            Order orderData = new Order();
            orderData.OrderId = order.OrderId;
            orderData.CustomerName = order.CustomerName;
            orderData.OrderType = order.OrderType;
            orderData.CreatedDate = order.CreatedDate;
            orderData.CreatedByUserName = username;

            var result = await _unitOfWork.Orders.Update(orderData);

            if (result != null) {
                await _unitOfWork.CompleteAsync();
                return Ok(result);
            }

            return Problem("Something went wrong");
        }


        // Search
        [HttpGet("/search")]
        [AllowAnonymous]
        public async Task<ActionResult> Search([FromQuery]OrderQueryModel orderQuery  )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _unitOfWork.Orders.GetAll(orderQuery);
            return Ok(result);
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