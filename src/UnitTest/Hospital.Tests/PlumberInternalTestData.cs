using System.Collections.Generic;

namespace Hospital.Tests
{
    public class PlumberInternalTestData
    {
        private static readonly List<object[]> Data = new List<object[]>
        {
            new object[] {100, 3,33.33},
            new object[] {100, 4,25},
            new object[] {100, 2,50},
            new object[] {100, 6,16.67}
        };

        public static IEnumerable<object[]> TestData => Data;
    }
}
