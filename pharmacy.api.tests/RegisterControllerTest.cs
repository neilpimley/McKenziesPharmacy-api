using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pharmacy.Controllers;
using Moq;
using Pharmacy.Services.Interfaces;
using Xunit;

namespace Pharmacy.ControllerTests
{
    [TestClass]
    public class RegisterControllerTest
    {
        private readonly IRegisterService _registerService;

        [Fact]
        public async Task GetAddresses()
        {
            string postcode = "BT10 )JH";

            // Arrange
            /*    var mockService = new Mock<IRegisterService>();
                mockService.Setup(x => x.GetAddressesByPostcode(postcode))
                    .Returns(new Address { AddressLine1 = "1 Long Road" });

                // Arrange
                var controller = new RegisterController(mockService.Object);
                controller.Request = new HttpRequestMessage
                {
                    RequestUri = new Uri("http://localhost:60001/api/Customers")
                };

                // Act
                IHttpActionResult actionResult = controller.GetCustomer(userid);
                var contentResult = actionResult as OkNegotiatedContentResult<CustomerPoco>;

                // Assert
                Assert.IsNotNull(contentResult);
                Assert.IsNotNull(contentResult.Content);
                Assert.AreEqual(userid, contentResult.Content.UserID);         }
                */
        }

    }
}