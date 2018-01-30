using System;
using System.Threading.Tasks;
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
        public async Task TestGetOrder()
        {
            var id = Guid.NewGuid();

            var controller = new OrdersController(_mockOrdersService.Object, _mockCustomersService.Object);

            IActionResult actionResult = await controller.GetOrder();
            var contentResult = actionResult as OkResult;

            Assert.NotNull(contentResult);
        }

        [Fact]
        public async Task TestGetOrderById()
        {
            var id = Guid.NewGuid();

            var controller = new OrdersController(_mockOrdersService.Object, _mockCustomersService.Object);

            var order = await controller.GetOrder(id);

            Assert.NotNull(order);
            Assert.Equal(id, order.OrderId);
        }

        [Fact]
        public async Task TestGetOrders()
        {
        }

        [Fact]
        public async Task TestGetOrderLines()
        {
        }

        [Fact]
        public async Task TestSubmitOrder()
        {
        }

        [Fact]
        public async Task TestAddOrderLineOrder()
        {
        }

        [Fact]
        public async Task TestDeleteOrderLine()
        {
        }
    }
}
