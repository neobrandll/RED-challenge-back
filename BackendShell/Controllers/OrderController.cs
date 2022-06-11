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
        private readonly UserManager<IdentityUser> _userManager;
        public OrderController(ILogger<OrderController> logger, IUnitOfWork unitOfWork, IAuthorizationService authorizationService, UserManager<IdentityUser> userManager)
        {
          _logger = logger;
          _unitOfWork = unitOfWork;
          _authorizationService = authorizationService;
          _userManager = userManager;
        }


        // Get all
        [HttpGet()]
        [AllowAnonymous]
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
        public async Task<JsonResult> Create(OrderModel order)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(BadRequest());
            }
            var username = _userManager.GetUserName(User);
            if (User == null || username == null ) return new JsonResult(Unauthorized());
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
                return new JsonResult(Ok(result));
            }

            return new JsonResult(Problem("Something went wrong"));
        }


        // Update
        [HttpPut]
        public async Task<JsonResult> Update(OrderModel order)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(BadRequest());
            }
            var username = _userManager.GetUserName(User);
            var isAuthorized = await _authorizationService.AuthorizeAsync(User, order, OrderOperations.Update);

            if(isAuthorized.Succeeded == false || User == null || username == null)
                return new JsonResult(Unauthorized());

            Order orderData = new Order();
            orderData.CustomerName = order.CustomerName;
            orderData.OrderType = order.OrderType;
            orderData.CustomerName = order.CustomerName;
            orderData.CreatedDate = order.CreatedDate;
            orderData.CreatedByUserName = username;

            var result = await _unitOfWork.Orders.Update(orderData);

            if (result != null) {
                await _unitOfWork.CompleteAsync();
                return new JsonResult(Ok(result));
            } 

            return new JsonResult(Problem("Something went wrong"));
        }


        // Search
        [HttpGet("/search")]
        [AllowAnonymous]
        public async Task<JsonResult> Search([FromQuery]OrderQueryModel orderQuery  )
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