using Project2.Domain.Model;
using System;
using Xunit;


namespace Project2.Test
{
    public class DoctorDomainTests
    {
        private readonly Doctor doctor = new Doctor();

        [Fact]
        public void FirstName_Valid()
        {
            string testString = "testName";
            doctor.FirstName = testString;

            Assert.Equal(testString, doctor.FirstName);
        }

        [Fact]
        public void LastName_Valid()
        {
            string testString = "testName";
            doctor.LastName = testString;

            Assert.Equal(testString, doctor.LastName);
        }

        [Fact]
        public void Empty_FirstName_throws_Error()
        {
            Assert.ThrowsAny<ArgumentException>(() => doctor.FirstName = String.Empty);
        }

        [Fact]
        public void Empty_LastName_throws_Error()
        {
            Assert.ThrowsAny<ArgumentException>(() => doctor.LastName = String.Empty);
        }
    }
}
