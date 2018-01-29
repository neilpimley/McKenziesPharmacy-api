using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Pharmacy.Controllers;
using Pharmacy.Models.Pocos;
using Pharmacy.Services.Interfaces;
using Xunit;

namespace Pharmacy.ControllerTests
{
    
    public class DrugsControllerTest
    {
        [Fact]
        public async Task GetDrugs_Test()
        {
            var drugName = "viagra";
            var customerID = Guid.NewGuid();
            var customer = new CustomerPoco()
            {
                CustomerId = customerID
            };
            // Arrange
            var mockDrugService = new Mock<IDrugsService>();
            mockDrugService.Setup(x => x.GetDrugs(customerID,drugName))
                .Returns(Task.FromResult<IEnumerable<DrugPoco>>(new List<DrugPoco>() { new DrugPoco() { DrugName = "viagra" } }));

            var mockCustomersService = new Mock<ICustomersService>();
            mockCustomersService.Setup(x => x.GetCustomer(customerID))
                .Returns(Task.FromResult(customer));


            // Arrange
            var controller = new DrugsController(mockDrugService.Object, mockCustomersService.Object);

            // Act
            var drugs = await controller.GetDrugs(drugName);


            // Assert
            Assert.NotNull(drugs);
            Assert.Single(drugs);
        }
    }
    
}
