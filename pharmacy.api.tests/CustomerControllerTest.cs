using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Pharmacy.Controllers;
using Pharmacy.Models.Pocos;
using Pharmacy.Services.Interfaces;
using Xunit;

namespace Pharmacy.ControllerTests
{
    public class CustomerControllerTest
    {
        [Fact]
        public async Task GetCustomer_Test()
        {
            string userid = "google-1";

            // Arrange
            var mockService = new Mock<ICustomersService>();
            mockService.Setup(x => x.GetCustomerByUsername(userid))
                .Returns(Task.FromResult(new CustomerPoco { UserId = "google-1" }));

            // Arrange
            var controller = new CustomersController(mockService.Object);

            // Act
            IActionResult actionResult = await controller.GetCustomer(userid);
            var contentResult = actionResult as OkObjectResult;

            // Assert
            Assert.NotNull(contentResult);
            var customer = contentResult.Value as CustomerPoco;
            Assert.Equal(userid, customer.UserId);
        }

        [Fact]
        public async Task PutCustomer_Test()
        {
            var customer = new CustomerPoco()
            {
                CustomerId = Guid.NewGuid()
            };
            // Arrange
            var mockService = new Mock<ICustomersService>();
            mockService.Setup(x => x.UpdateCustomerDetails(customer));

            // Arrange
            var controller = new CustomersController(mockService.Object);

            // Act
            IActionResult actionResult = await controller.PutCustomer(customer.CustomerId, customer);
            var contentResult = actionResult as OkResult;

            // Assert
            Assert.NotNull(contentResult);
        }

        [Fact]
        public async Task PostCustomer_Test()
        {
            var customer = new CustomerPoco()
            {
                CustomerId = Guid.NewGuid()
            };
            // Arrange
            var mockService = new Mock<ICustomersService>();
            mockService.Setup(x => x.RegisterCustomer(customer));

            // Arrange
            var controller = new CustomersController(mockService.Object);

            // Act
            IActionResult actionResult = await controller.PostCustomer(customer);
            var contentResult = actionResult as OkResult;

            // Assert
            Assert.NotNull(contentResult);
        }
    }
}
