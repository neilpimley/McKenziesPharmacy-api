using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Pharmacy.IntegrationTests.Scenarios
{
    [Collection("CustomerCollection")]
    public class LoginAndOrderTest
    {
        public readonly TestContext Context;

        public LoginAndOrderTest(TestContext context)
        {
        }

        [Fact]
        public async Task PingReturnsOkResponse()
        {
            var response = await Context.Client.GetAsync("/api/CheckServer");

            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
