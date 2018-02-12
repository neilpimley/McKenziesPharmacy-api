using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Pharmacy.Models;
using Pharmacy.Models.Pocos;
using Xunit;

namespace Pharmacy.IntegrationTests.Scenarios
{
    [Collection("RegisterCollection")]
    public class RegisterTests
    {
        public readonly TestContext Context;
        public readonly Practice practice;
        public readonly Shop shop;
        public readonly Doctor doctor;
        public readonly Address address;
        public readonly Title title;

        public RegisterTests(TestContext context)
        {
            // TODO: get an access token somehow  or add middleware so we can test the API authorize endpoints
        }

        [Fact]
        public async Task CheckServerReturnsOkResponse()
        {
            var response = await Context.Client.GetAsync("/api/CheckServer");

            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }


        [Fact]
        public async Task GetShopsOkResponse()
        {
            var response = await Context.Client.GetAsync("api/Shops");

            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetPracticesOkResponse()
        {
            var response = await Context.Client.GetAsync("api/Titles");

            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetTitlesOkResponse()
        {
            var response = await Context.Client.GetAsync("api/Practices");

            response.EnsureSuccessStatusCode();
            
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetDoctorsOkResponse()
        {
            var response = await Context.Client.GetAsync("api/Doctors");

            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetDoctorsByPractriceIdOkResponse()
        {
            var response = await Context.Client.GetAsync("api/Practices/{practiceId}/Doctors");

            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetAddressesOkResponse()
        {
            var response = await Context.Client.GetAsync("api/Addresses/{postCode}");

            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task RegisterOkResponse()
        {
            var customerJson = JsonConvert.SerializeObject(new CustomerPoco
            {
                CreatedOn = new DateTime(),
                ShopId = shop.ShopId,
                DoctorId = doctor.DoctorId,
                AddressId = address.AddressId,
                Dob = Convert.ToDateTime("01/01/1970"),
                Home = "02890909090",
                ModifiedOn = new DateTime(),
                Mobile = "07909090909",
                Lastname = "Bloggs",
                Firstname = "Joe",
                TitleId = title.TitleId,
                Email = "user@neilpimley.com",
                UserId = null,
                CustomerId = Guid.NewGuid(),
                Sex = "M",
                Active = true
            });


            var response = await Context.Client.PostAsync("api/Customers", 
                new StringContent(customerJson, Encoding.UTF8, "application/json"));

            
            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

    }
}
