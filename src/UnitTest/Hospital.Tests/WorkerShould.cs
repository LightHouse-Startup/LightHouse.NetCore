using System;
using Xunit;
using Xunit.Abstractions;

namespace Hospital.Tests
{
    [Trait("Category", "Worker")]
    public class WorkerShould : TestBase
    {
        private readonly WorkerFactory factory;

        public WorkerShould(ITestOutputHelper output) : base(output)
        {
            factory = new WorkerFactory();
            _output.WriteLine($"正在创建新的...{factory.Id}");
        }

        [Fact]
        public void CreatePlumberByDefault()
        {
            //var factory = new WorkerFactory();
            Worker worker = factory.Create("Nick");
            Assert.IsType<Plumber>(worker);
        }

        [Fact]
        public void CreateProgrammerAndCastReturnedType()
        {
            //var factory = new WorkerFactory();
            Worker worker = factory.Create("Nick", isProgrammer: true);
            Programmer programmer = Assert.IsType<Programmer>(worker);
            Assert.Equal("Nick", programmer.Name);
        }

        [Fact]
        public void CreateProgrammer_AssertAssignableTypes()
        {
            //var factory = new WorkerFactory();
            Worker worker = factory.Create("Nick", isProgrammer: true);
            Assert.IsAssignableFrom<Worker>(worker);
        }

        [Fact]
        public void CreateSeperateInstances()
        {
            //var factory = new WorkerFactory();
            var p1 = factory.Create("Nick");
            var p2 = factory.Create("Nick");
            Assert.NotSame(p1, p2);
        }

        [Fact]
        public void NotAllowNullName()
        {
            //var factory = new WorkerFactory();
            // var p = factory.Create(null); // 这个会失败
            Assert.Throws<ArgumentNullException>(() => factory.Create(null));
            Assert.Throws<ArgumentNullException>("name", () => factory.Create(null));
        }

        [Fact]
        public void NotAllowNullNameAndUseReturnedException()
        {
            //var factory = new WorkerFactory();
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => factory.Create(null));
            Assert.Equal("name", ex.ParamName);
        }

        public override void Dispose()
        {
            _output.WriteLine($"正在清理对象{this.GetType().FullName}------");
            //base.Dispose();
        }
    }
}
