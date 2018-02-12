using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Pharmacy.IntegrationTests
{
    [Collection("SystemCollection")]
    public class HealthCheckTests
    {
        public readonly TestContext Context;

        public HealthCheckTests(TestContext context)
        {
            Context = context;
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