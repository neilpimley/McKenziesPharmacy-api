using Xunit;

namespace Pharmacy.IntegrationTests
{
    [CollectionDefinition("SystemCollection")]
    public class Collection : ICollectionFixture<TestContext>
    {

    }
}
