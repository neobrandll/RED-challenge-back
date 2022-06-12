using API.Controllers;
using API.Core.IConfiguration;
using API.DataModels;
using API.Models.Orders;
using API.Projections;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class OrderControllerTests
    {

        private readonly OrderController _orderController;
        private readonly Mock<ILogger<OrderController>> _loggerMock = new Mock<ILogger<OrderController>>();
        private readonly Mock<IUnitOfWork> _unitOfWork = new Mock<IUnitOfWork>();
        private readonly Mock<IAuthorizationService> _authorizationService = new Mock<IAuthorizationService>();

        public OrderControllerTests()
        {
            _orderController = new OrderController(_loggerMock.Object, _unitOfWork.Object, _authorizationService.Object);

        }



        [Fact]
        public async Task GetAll_ShouldReturnAllOrders()
        {
            // Arrange
            var ordersMock = new List<OrderProjection>();
            Order o = new Order
            {
                OrderId = 0,
                OrderType = 0,
                CustomerName = "string",
                CreatedDate = new System.DateTime()
            };
            ordersMock.Add(new OrderProjection(o));

            _unitOfWork.Setup(x => x.Orders.GetAll()).ReturnsAsync(ordersMock);

            // Act
            var orders = await _orderController.GetAll();
            var result = orders as OkObjectResult;


            // Assert

            Assert.Equal(ordersMock, result.Value);


        }
        [Fact]
        public async Task Delete_ShouldReturnNotFound_WhenOrderDoesNotExists()
        {

            List<int> mockList = new List<int>();
            mockList.Add(1);
            // Arrange
             _unitOfWork.Setup(x => x.Orders.Delete(It.IsAny<List<int>>())).ReturnsAsync(false);
            // Act
            var operationResult = await _orderController.Delete(mockList);
            var result = operationResult as NotFoundResult;
            

            // Assert


            Assert.Equal(new NotFoundResult().StatusCode, result.StatusCode);
        }

        //[Fact]
        //public async Task Create_ShouldInsertUserName()
        //{

        //    var user = new Mock<ClaimsPrincipal>();

        //    _unitOfWork.Setup(x => x.UserManager.GetUserName(user.Object)).Returns("testUser");
            
        //    Order o = new Order
        //    {
        //        OrderId = 0,
        //        OrderType = 0,
        //        CustomerName = "string",
        //        CreatedDate = new System.DateTime(),
        //        CreatedByUserName = "testUser",
        //    };
        //    OrderProjection orderFormatted = new OrderProjection(o);
            

        //    _unitOfWork.Setup(x => x.Orders.Create(o)).ReturnsAsync(orderFormatted);


        //    OrderModel orderBody = new OrderModel
        //    {
        //        OrderId = 0,
        //        OrderType = 0,
        //        CustomerName = "string",
        //        CreatedDate = new System.DateTime(),
        //    };

        //    // Act
        //    var operationResult = await _orderController.Create(orderBody);
        //    var value = operationResult as OkObjectResult;

        //    // Assert

        //    Assert.Equal(orderFormatted, value.Value);
        //}
    }
}


