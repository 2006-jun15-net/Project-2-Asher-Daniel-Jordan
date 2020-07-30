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
using Microsoft.Extensions.Logging;

namespace Project2.Test.Controllers
{
    public class TreatmentControllerTests
    {
        //initial setup
        private readonly Mock<ITreatmentRepository> _mockRepo;
        private readonly TreatmentsController _controller;
        private readonly ILogger<TreatmentsController> _logger;


        //initial setup
        public TreatmentControllerTests()
        {
            _mockRepo = new Mock<ITreatmentRepository>();
            //_logger = new ILogger<TreatmentsController>();
            _controller = new TreatmentsController(_mockRepo.Object);

            List<Treatment> treatments = new List<Treatment>()
            {
                new Treatment(1, 1, 1, "Test", 1)
            };

            // get all treatments
            _mockRepo.Setup(repo => repo.GetTreatmentsAsync())
                .Returns(async () => await Task.Run(() => treatments));

            // get treatment by id
            _mockRepo.Setup(repo => repo.GetTreatmentAsync(It.IsAny<int>()))
                .Returns(async (int id) => await Task.Run(() =>
                    treatments.Where<Treatment>(d => d.DoctorId == id).FirstOrDefault()));

            // create treatment
            _mockRepo.Setup(repo => repo.CreateTreatmentAsync(It.IsAny<Treatment>()))
                .Returns(async (Treatment treatment) => await Task.Run(() => treatments.Add(treatment)));

            // updates treatment
            _mockRepo.Setup(repo => repo.UpdateTreatmentAsync(It.IsAny<Treatment>()))
                .Returns(async (Treatment treatment) => await Task.Run(() => treatments));

            // deletes a treatment
            _mockRepo.Setup(repo => repo.DeleteTreatmentAsync(It.IsAny<int>()))
                .Returns(async (Treatment treatment) => await Task.Run(() => treatments.Remove(treatment)));
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
        public async void GetById_Action_ReturnsOK_IfFound()
        {
            var result = await _controller.GetById(1);

            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async void GetById_Action_ReturnsNotFound()
        {
            var result = await _controller.GetById(23);

            var notFoundResult = result as NotFoundResult;

            Assert.NotNull(notFoundResult);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async void PostTreatment_Action_ReturnsCreatedAtAction()
        {
            Treatment treatment = new Treatment(3, 1, 1, "Dummy", 1);
            var result = await _controller.PostTreatment(treatment);

            var createdResult = result as CreatedAtActionResult;

            Assert.NotNull(createdResult);
            Assert.Equal(201, createdResult.StatusCode);
        }

        [Fact]
        public async void PostTreatment_Action_ReturnsConflict_OnDuplicate()
        {
            Treatment treatment = new Treatment(1, 1, 1, "Dummy", 1);
            var result = await _controller.PostTreatment(treatment);

            var createdResult = result as ConflictResult;

            Assert.NotNull(createdResult);
            Assert.Equal(409, createdResult.StatusCode);
        }

        /*[Fact]
        public async void Put_Action_ReturnsOk()
        {
            Treatment treatment = new Treatment(1, 1, 1, "Dummy", 5);
            var result = await _controller.Put(1, treatment);

            var okResult = result as OkResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async void Put_Action_ReturnsBadRequest()
        {
            Treatment treatment = new Treatment(1, 1, 1, "Dummy", 5);
            var result = await _controller.Put(2, treatment);

            var badRequestResult = result as BadRequestResult;

            Assert.NotNull(badRequestResult);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public async void Put_Action_ReturnsNotFound()
        {
            Treatment treatment = new Treatment(23, 1, 1, "Dummy", 5);
            var result = await _controller.Put(23, treatment);

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
        }*/
    }
}
