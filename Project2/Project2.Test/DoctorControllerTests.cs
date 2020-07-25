using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Project2.API.Controllers;
using Project2.Domain.Interface;
using Project2.Domain.Model;
using Xunit;

namespace Project2.Test
{
    public class DoctorControllerTests
    {
        //initial setup
        private readonly Mock<IDoctorRepository> _mockRepo;
        private readonly DoctorsController _controller;


        //initial setup
        public DoctorControllerTests()
        {
            _mockRepo = new Mock<IDoctorRepository>();
            _controller = new DoctorsController(_mockRepo.Object);
        }

        /*
        [Fact]
        public void Get_ActionExecutes_ReturnsDoctors()
        {
            var result = _controller.Get();
            Assert.Collection(result, );
        }

        */
    }
}
