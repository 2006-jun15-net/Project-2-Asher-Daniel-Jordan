using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

            List<Doctor> doctors = new List<Doctor>()
            {
                new Doctor(1, "Test", "Dummy")
            };

            // get all doctors
            _mockRepo.Setup(repo => repo.GetDoctorsAsync())
                .Returns(async () => await Task.Run(() => doctors));

            // get doctor by id
            _mockRepo.Setup(repo => repo.GetDoctorAsync(It.IsAny<int>()))
                .Returns(async (int id) => await Task.Run(() => 
                    doctors.Where<Doctor>(d => d.DoctorId == id).FirstOrDefault()));

            // create doctor
            _mockRepo.Setup(repo => repo.CreateDoctorAsync(It.IsAny<Doctor>()))
                .Returns(async (Doctor doctor) => await Task.Run(() => doctors.Add(doctor)));

            // updates doctor
            _mockRepo.Setup(repo => repo.UpdateDoctorAsync(It.IsAny<Doctor>()))
                .Returns(async (Doctor doctor) => await Task.Run(() => doctors));

            // deletes a doctor
            _mockRepo.Setup(repo => repo.DeleteDoctorAsync(It.IsAny<Doctor>()))
                .Returns(async (Doctor doctor) => await Task.Run(() => doctors.Remove(doctor)));
        }


        [Fact]
        public async void GetDoctors_ActionExecutes_ReturnsOKStatus()
        {
            var result = await _controller.GetDoctors();

            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async void GetDoctorById_Action_ReturnsOK_IfFound()
        {
            var result = await _controller.GetDoctorById(1);

            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async void GetDoctorById_Action_ReturnsNotFound()
        {
            var result = await _controller.GetDoctorById(23);

            var notFoundResult = result as NotFoundResult;

            Assert.NotNull(notFoundResult);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async void PostDoctor_Action_ReturnsCreatedAtAction()
        {
            Doctor doctor = new Doctor(3, "Test", "Dummy");
            var result = await _controller.PostDoctor(doctor);

            var createdResult = result as CreatedAtActionResult;

            Assert.NotNull(createdResult);
            Assert.Equal(201, createdResult.StatusCode);
        }

        [Fact]
        public async void PostDoctor_Action_ReturnsConflict_OnDuplicate()
        {
            Doctor doctor = new Doctor(1, "Test", "Dummy");
            var result = await _controller.PostDoctor(doctor);

            var createdResult = result as ConflictResult;

            Assert.NotNull(createdResult);
            Assert.Equal(409, createdResult.StatusCode);
        }

        [Fact]
        public async void PutDoctor_Action_ReturnsOk()
        {
            Doctor doctor = new Doctor(1, "Test", "Dummy");
            var result = await _controller.Put(1, doctor);

            var okResult = result as OkResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        /*[Fact]
        public async void PutDoctor_Action_ReturnsBadRequest()
        {
            Doctor doctor = new Doctor(1, "Test", "Dummy");
            var result = await _controller.Put(2, doctor);

            var badRequestResult = result as BadRequestResult;

            Assert.NotNull(badRequestResult);
            Assert.Equal(400, badRequestResult.StatusCode);
        }*/

        [Fact]
        public async void PutDoctor_Action_ReturnsNotFound()
        {
            Doctor doctor = new Doctor(23, "Test", "Dummy");
            var result = await _controller.Put(23, doctor);

            var notFoundResult = result as NotFoundResult;

            Assert.NotNull(notFoundResult);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async void Delete_Action_ReturnsOk()
        {
            var result = await _controller.Delete(1);

            var okResult = result as OkResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async void Delete_Action_ReturnsNotFound()
        {
            var result = await _controller.Delete(23);

            var notFoundResult = result as NotFoundResult;

            Assert.NotNull(notFoundResult);
            Assert.Equal(404, notFoundResult.StatusCode);
        }
    }
}
