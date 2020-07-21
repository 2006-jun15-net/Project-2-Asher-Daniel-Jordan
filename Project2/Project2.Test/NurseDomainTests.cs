using Project2.Domain.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xunit;

namespace Project2.Test
{
    public class NurseDomainTests
    {
        private Nurse nurse = new Nurse();

        [Fact]

        public void NurseFirstNameShouldBeSettable()
        {
            string testName = "test";
            nurse.FirstName = testName;

            Assert.Equal(testName, nurse.FirstName);

        }

        [Fact]

        public void NurseLastNameShouldBeSettable()
        {
            string testName = "test";
            nurse.LastName = testName;

            Assert.Equal(testName, nurse.LastName);

        }

        [Fact]

        public void NurseFirstNameShouldNotAllowNull()
        {
            Assert.ThrowsAny<ArgumentNullException>(() => nurse.FirstName = string.Empty);
        }

        [Fact]

        public void NurseLastNameShouldNotAllowNull()
        {
            Assert.ThrowsAny<ArgumentNullException>(() => nurse.LastName = string.Empty);

        }
    }
}
