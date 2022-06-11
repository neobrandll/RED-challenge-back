using API;
using API.Controllers;
using API.Core.IConfiguration;
using API.Core.IRepositories;
using API.Core.Repositories;
using API.DataModels;
using API.Projections;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest
{
    public class OrderControllerTests
    {
        private readonly OrderController _orderController;
        private readonly Mock<ILogger<OrderController>> _loggerMock = new Mock<ILogger<OrderController>>();
        private readonly Mock<IUnitOfWork> _unitOfWork = new Mock<IUnitOfWork>();
        private readonly Mock<UserManager<IdentityUser>> _userManager = new Mock<UserManager<IdentityUser>>();
        private readonly Mock<IAuthorizationService> _authorizationService = new Mock<IAuthorizationService>();

        public OrderControllerTests()
        {
            _orderController = new OrderController(_loggerMock.Object, _unitOfWork.Object, _authorizationService.Object, _userManager.Object);

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

            // Assert

            Assert.Equal(ordersMock, ordersMock);

          
        }
    }
}
