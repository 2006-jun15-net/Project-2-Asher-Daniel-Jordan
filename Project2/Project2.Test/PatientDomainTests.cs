using Project2.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Project2.Test
{
    public class PatientDomainTests
    {
        private readonly Patient patient = new Patient();

        [Fact]
        public void FirstName_Valid()
        {
            string testString = "testName";
            patient.FirstName = testString;

            Assert.Equal(testString, patient.FirstName);
        }

        [Fact]
        public void LastName_Valid()
        {
            string testString = "testName";
            patient.LastName = testString;

            Assert.Equal(testString, patient.LastName);
        }

        [Fact]
        public void Empty_FirstName_throws_Error()
        {
            Assert.ThrowsAny<ArgumentException>(() => patient.FirstName = String.Empty);
        }

        [Fact]
        public void Empty_LastName_throws_Error()
        {
            Assert.ThrowsAny<ArgumentException>(() => patient.LastName = String.Empty);
        }
    }
}
