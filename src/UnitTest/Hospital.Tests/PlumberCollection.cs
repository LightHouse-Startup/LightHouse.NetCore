using Xunit;
using Xunit.Abstractions;

namespace Hospital.Tests
{
    [CollectionDefinition("PlumberCollection")]
    public class PlumberCollection : ICollectionFixture<PlumberFixture>
    {
    }

    [Trait("Category", "Collection")]
    [Collection("PlumberCollection")]
    public class PlumberTest1 : TestBase
    {
        private readonly PlumberFixture _gameStateFixture;

        public PlumberTest1(PlumberFixture plumberFixture, ITestOutputHelper output) : base(output)
        {
            _gameStateFixture = plumberFixture;
        }

        [Fact]
        public void Test1()
        {
            _output.WriteLine($"Plumber ID={_gameStateFixture.Instance.Id}");
        }

        [Fact]
        public void Test2()
        {
            _output.WriteLine($"Plumber ID={_gameStateFixture.Instance.Id}");
        }
    }

    [Trait("Category", "Collection")]
    [Collection("PlumberCollection")]
    public class PlumberTest2 : TestBase
    {
        private readonly PlumberFixture _gameStateFixture;

        public PlumberTest2(PlumberFixture plumberFixture, ITestOutputHelper output) : base(output)
        {
            _gameStateFixture = plumberFixture;
        }

        [Fact]
        public void Test3()
        {
            _output.WriteLine($"Plumber ID={_gameStateFixture.Instance.Id}");
        }

        [Fact]
        public void Test4()
        {
            _output.WriteLine($"Plumber ID={_gameStateFixture.Instance.Id}");
        }
    }
}
