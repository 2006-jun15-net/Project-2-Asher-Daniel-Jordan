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
using Project2.Domain.Service;

namespace Project2.Test.Controllers
{
    public class PatientControllerTests
    {
        //initial setup
        private readonly Mock<IPatientRepository> _mockRepo;
        private readonly Mock<IPatientRoomRepository> _roomMockRepo;
        private readonly PatientsController _controller;
        private readonly Mock<PatientService> _service;


        //initial setup
        public PatientControllerTests()
        {
            _mockRepo = new Mock<IPatientRepository>();
            _roomMockRepo = new Mock<IPatientRoomRepository>();
            _service = new Mock<PatientService>(_mockRepo.Object, _roomMockRepo.Object);
            _controller = new PatientsController(_mockRepo.Object, _service.Object);

            List<Patient> patients = new List<Patient>()
            {
                new Patient(1, 1, "Test", "Dummy")
            };

            // get all patients
            _mockRepo.Setup(repo => repo.GetPatientsAsync())
                .Returns(async () => await Task.Run(() => patients));

            // get patient by id
            _mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .Returns(async (int id) => await Task.Run(() =>
                    patients.Where<Patient>(p => p.PatientId == id).FirstOrDefault()));

            // create patient
            _mockRepo.Setup(repo => repo.CreateAsync(It.IsAny<Patient>()))
                .Returns(async (Patient patient) => await Task.Run(() => patients.Add(patient)));

            // updates patient
            _mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Patient>()))
                .Returns(async (Patient patient) => await Task.Run(() => patients));

            // deletes a patient
            _mockRepo.Setup(repo => repo.DeletePatientAsync(It.IsAny<Patient>()))
                .Returns(async (Patient patient) => await Task.Run(() => patients.Remove(patient)));
        }


        [Fact]
        public async void Get_ActionExecutes_ReturnsOKStatus()
        {
            var result = await _controller.Get();

            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async void Get_Action_ReturnsOK_IfFound()
        {
            var result = await _controller.Get(1);

            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async void Get_Action_ReturnsNotFound()
        {
            var result = await _controller.Get(23);

            var notFoundResult = result as NotFoundResult;

            Assert.NotNull(notFoundResult);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async void Post_Action_ReturnsCreatedAtAction()
        {
            Patient patient = new Patient(3, 1, "Test", "Dummy");
            var result = await _controller.Post(patient);

            var createdResult = result as CreatedAtActionResult;

            Assert.NotNull(createdResult);
            Assert.Equal(201, createdResult.StatusCode);
        }

        [Fact]
        public async void Post_Action_ReturnsConflict_OnDuplicate()
        {
            Patient patient = new Patient(1, 1, "Test", "Dummy");
            var result = await _controller.Post(patient);

            var createdResult = result as ConflictResult;

            Assert.NotNull(createdResult);
            Assert.Equal(409, createdResult.StatusCode);
        }

        [Fact]
        public async void Put_Action_ReturnsOk()
        {
            Patient patient = new Patient(1, 2, "Test", "Dummy");
            var result = await _controller.Put(1, patient);

            var okResult = result as OkResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async void Put_Action_ReturnsBadRequest()
        {
            Patient patient = new Patient(1, 2, "Test", "Dummy");
            var result = await _controller.Put(2, patient);

            var badRequestResult = result as BadRequestResult;

            Assert.NotNull(badRequestResult);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public async void Put_Action_ReturnsNotFound()
        {
            Patient patient = new Patient(23, 2, "Test", "Dummy");
            var result = await _controller.Put(23, patient);

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
