using System.Collections.Generic;
using System.Reflection;
using Xunit.Sdk;

namespace Hospital.Tests
{
    public class PlumberDataAttribute : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            yield return new object[] { 300, 100, 3 };
            yield return new object[] { 300, 2, 150 };
            yield return new object[] { 300, 50, 6 };
        }
    }
}
