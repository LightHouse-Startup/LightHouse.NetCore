using System;
using Xunit;
using Xunit.Abstractions;

namespace Hospital.Tests
{
    [Trait("Category", "Patient")]
    public class PatientShould : TestBase
    {
        public PatientShould(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void HaveHeartBeatWhenNew()
        {
            var patient = new Patient();

            Assert.True(patient.IsNew);
        }

        [Fact]
        public void CalculateFullName()
        {
            var p = new Patient
            {
                FirstName = "Nick",
                LastName = "Carter"
            };
            _output.WriteLine(p.FullName);
            Assert.Equal("Nick Carter", p.FullName, true);
        }

        [Fact]
        public void CalculateFullNameStartsWithFirstName()
        {
            var p = new Patient
            {
                FirstName = "Nick",
                LastName = "Carter"
            };
            Assert.StartsWith("Nick", p.FullName);
        }

        [Fact]
        public void CalculateFullNameEndsWithFirstName()
        {
            var p = new Patient
            {
                FirstName = "Nick",
                LastName = "Carter"
            };
            Assert.EndsWith("Carter", p.FullName);
        }

        [Fact]
        public void CalculateFullNameSubstring()
        {
            var p = new Patient
            {
                FirstName = "Nick",
                LastName = "Carter"
            };
            Assert.Contains("ck Ca", p.FullName);
        }

        [Fact]
        public void CalculcateFullNameWithTitleCase()
        {
            var p = new Patient
            {
                FirstName = "Nick",
                LastName = "Carter"
            };
            Assert.Matches("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+", p.FullName);
        }

        [Fact]
        public void BloodSugarStartWithDefaultValue()
        {
            var p = new Patient();
            Assert.Equal(5.0, p.BloodSugar);
        }

        [Fact]
        public void BloodSugarIncreaseAfterDinner()
        {
            var p = new Patient();
            p.HaveDinner();
            Assert.InRange(p.BloodSugar, 5, 6);
        }

        [Fact]
        public void RaiseSleptEvent()
        {
            var p = new Patient();
            Assert.Raises<EventArgs>(
                handler => p.PatientSlept += handler,
                handler => p.PatientSlept -= handler,
                () => p.Sleep());
        }

        [Fact]
        public void RaisePropertyChangedEvent()
        {
            var p = new Patient();
            Assert.PropertyChanged(p, "BloodSugar", () => p.HaveDinner());
        }
    }
}
