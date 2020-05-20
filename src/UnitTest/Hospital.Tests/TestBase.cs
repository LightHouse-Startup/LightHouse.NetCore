using System;
using Xunit.Abstractions;

namespace Hospital.Tests
{
    public class TestBase : IDisposable
    {
        /// <summary>
        /// 用于打印输出
        /// </summary>
        public readonly ITestOutputHelper _output;

        public TestBase(ITestOutputHelper output)
        {
            _output = output;
        }

        /// <summary>
        /// 要主动标记为virtual才能被派生类重写
        /// </summary>
        public virtual void Dispose()
        {
            _output.WriteLine($"正在清理对象{this.GetType().FullName}");
        }
    }
}
