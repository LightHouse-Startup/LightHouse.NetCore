using System;

namespace Hospital.Tests
{
    /// <summary>
    /// 用于创建共享的Plumber实例
    /// </summary>
    public class PlumberFixture : IDisposable
    {
        public Plumber Instance { get; private set; }

        public PlumberFixture()
        {
            Instance = new Plumber();
        }

        public void Dispose()
        {
            // Cleanup
        }
    }
}
