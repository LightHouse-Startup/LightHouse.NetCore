using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Hospital.Tests
{
    public class PlumberExternalTestData
    {
        public static IEnumerable<object[]> TestData
        {
            get
            {
                string[] csvLines = File.ReadAllLines("TestData.csv");
                var testCases = new List<object[]>();
                foreach (var csvLine in csvLines)
                {
                    IEnumerable<double> values = csvLine.Split(',').Select(double.Parse);
                    object[] testCase = values.Cast<object>().ToArray();
                    testCases.Add(testCase);
                }
                return testCases;
            }
        }
    }
}
