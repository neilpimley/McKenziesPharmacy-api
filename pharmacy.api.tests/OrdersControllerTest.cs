using System;
using Moq;
using Pharmacy.Controllers;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Services.Interfaces;
using Xunit;

namespace Pharmacy.ControllerTests
{
    public class OrdersControllerTest
    {
        private readonly Mock<IOrdersService> _mockOrdersService;
        private readonly Mock<ICustomersService> _mockCustomersService;

        public void SetupServices()
        {

        }

        [Fact]
        public void TestGetOrder()
        {
            var id = Guid.NewGuid();

            var controller = new OrdersController(_mockOrdersService.Object, _mockCustomersService.Object);

            IActionResult actionResult = controller.GetOrder();
            var contentResult = actionResult as OkResult;

            Assert.NotNull(contentResult);
        }

        [Fact]
        public void TestGetOrderById()
        {
            var id = Guid.NewGuid();

            var controller = new OrdersController(_mockOrdersService.Object, _mockCustomersService.Object);

            var order = controller.GetOrder(id);

            Assert.NotNull(order);
            Assert.Equal(id, order.OrderId);
        }

        [Fact]
        public void TestGetOrders()
        {
        }

        [Fact]
        public void TestGetOrderLines()
        {
        }

        [Fact]
        public void TestSubmitOrder()
        {
        }

        [Fact]
        public void TestAddOrderLineOrder()
        {
        }

        [Fact]
        public void TestDeleteOrderLine()
        {
        }
    }
}
