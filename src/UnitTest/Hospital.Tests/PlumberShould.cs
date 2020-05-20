using Xunit;
using Xunit.Abstractions;

namespace Hospital.Tests
{
    [Trait("Category", "Plumber")]
    public class PlumberShould : TestBase, IClassFixture<PlumberFixture>
    {
        //private readonly Plumber plumber;
        private readonly PlumberFixture _plumberFixture;

        public PlumberShould(PlumberFixture plumberStateFixture, ITestOutputHelper output) : base(output)
        {
            //plumber = new Plumber();
            //_output.WriteLine($"正在创建新的...{plumber.Id}");

            _plumberFixture = plumberStateFixture;
            _output.WriteLine($"正在创建新的...{_plumberFixture.Instance.Id}");
        }

        [Fact]
        public void HaveCorrectSalary()
        {
            //var plumber = new Plumber();
            //Assert.Equal(66.667, plumber.Salary, 3);
            Assert.Equal(66.667, _plumberFixture.Instance.Salary, 3);
        }

        [Fact]
        public void NotHaveNameByDefault()
        {
            //var plumber = new Plumber();
            //Assert.Null(plumber.Name);
            Assert.Null(_plumberFixture.Instance.Name);
        }

        [Fact(Skip = "不需要跑这个测试")]
        public void HaveNameValue()
        {
            var plumber = new Plumber
            {
                Name = "Brian"
            };
            Assert.NotNull(plumber.Name);
        }

        [Fact]
        public void HaveScrewdriver()
        {
            //var plumber = new Plumber();
            //Assert.Contains("螺丝刀", plumber.Tools);
            Assert.Contains("螺丝刀", _plumberFixture.Instance.Tools);
        }

        [Fact]
        public void NotHaveKeyboard()
        {
            //var plumber = new Plumber();
            //Assert.DoesNotContain("键盘", plumber.Tools);
            Assert.DoesNotContain("键盘", _plumberFixture.Instance.Tools);
        }

        [Fact]
        public void HaveAtLeastOneScrewdriver()
        {
            //var plumber = new Plumber();
            //Assert.Contains(plumber.Tools, t => t.Contains("螺丝"));
            Assert.Contains(_plumberFixture.Instance.Tools, t => t.Contains("螺丝"));
        }

        [Fact]
        public void HaveAllTools()
        {
            //var plumber = new Plumber();
            var expectedTools = new[]
            {
                "螺丝刀",
                "扳子",
                "钳子"
            };
            //Assert.Equal(expectedTools, plumber.Tools);
            Assert.Equal(expectedTools, _plumberFixture.Instance.Tools);
        }

        [Fact]
        public void HaveNoEmptyDefaultTools()
        {
            //var plumber = new Plumber();
            //Assert.All(plumber.Tools, t => Assert.False(string.IsNullOrEmpty(t)));
            Assert.All(_plumberFixture.Instance.Tools, t => Assert.False(string.IsNullOrEmpty(t)));
        }

        [Theory]
        [InlineData(200, 4, 50)]
        [InlineData(200, 3, 66.67)]
        public void CalculateSalary(double money, double hour, double expectSalary)
        {
            Assert.Equal(_plumberFixture.Instance.CalculateSalary(money, hour), expectSalary);
        }

        [Theory]
        [MemberData(nameof(PlumberInternalTestData.TestData), MemberType = typeof(PlumberInternalTestData))]
        public void CalculateSalaryInternalTest(double money, double hour, double expectSalary)
        {
            Assert.Equal(_plumberFixture.Instance.CalculateSalary(money, hour), expectSalary);
        }

        [Theory]
        [MemberData(nameof(PlumberExternalTestData.TestData), MemberType = typeof(PlumberExternalTestData))]
        public void CalculateSalaryExternalTest(double money, double hour, double expectSalary)
        {
            Assert.Equal(_plumberFixture.Instance.CalculateSalary(money, hour), expectSalary);
        }

        [Theory]
        [PlumberData]
        public void CalculateSalaryDataAttribute(double money, double hour, double expectSalary)
        {
            Assert.Equal(_plumberFixture.Instance.CalculateSalary(money, hour), expectSalary);
        }
    }
}
