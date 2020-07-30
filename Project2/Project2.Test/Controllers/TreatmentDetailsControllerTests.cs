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

namespace Project2.Test.Controllers
{
    public class TreatmentDetailsControllerTests
    {
        //initial setup
        private readonly Mock<ITreatmentDetailsRepository> _mockRepo;
        private readonly TreatmentDetailsController _controller;


        //initial setup
        public TreatmentDetailsControllerTests()
        {
            _mockRepo = new Mock<ITreatmentDetailsRepository>();
            _controller = new TreatmentDetailsController(_mockRepo.Object);

            List<TreatmentDetails> details = new List<TreatmentDetails>()
            {
                new TreatmentDetails(1, 1, 1, 1, "Dummy")
            };

            // get all doctors
            _mockRepo.Setup(repo => repo.GetAllAsync())
                .Returns(async () => await Task.Run(() => details));

            // get doctor by id
            _mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .Returns(async (int id) => await Task.Run(() =>
                    details.Where<TreatmentDetails>(d => d.TreatmentDetailsId == id).FirstOrDefault()));

            // create doctor
            _mockRepo.Setup(repo => repo.CreateAsync(It.IsAny<TreatmentDetails>()))
                .Returns(async (TreatmentDetails doctor) => await Task.Run(() => details.Add(doctor)));

            // updates doctor
            _mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<TreatmentDetails>()))
                .Returns(async (TreatmentDetails doctor) => await Task.Run(() => details));

            // deletes a doctor
            _mockRepo.Setup(repo => repo.DeleteAsync(It.IsAny<TreatmentDetails>()))
                .Returns(async (TreatmentDetails doctor) => await Task.Run(() => details.Remove(doctor)));
        }


        [Fact]
        public async void GetDoctors_ActionExecutes_ReturnsOKStatus()
        {
            var result = await _controller.Get();

            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async void GetDoctorById_Action_ReturnsOK_IfFound()
        {
            var result = await _controller.GetTreatmentDetail(1);

            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async void GetDoctorById_Action_ReturnsNotFound()
        {
            var result = await _controller.GetTreatmentDetail(23);

            var notFoundResult = result as NotFoundResult;

            Assert.NotNull(notFoundResult);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async void PostDoctor_Action_ReturnsCreatedAtAction()
        {
            TreatmentDetails detail = new TreatmentDetails(3, 1, 1, 1, "Dummy");
            var result = await _controller.Post(detail);

            var createdResult = result as CreatedAtActionResult;

            Assert.NotNull(createdResult);
            Assert.Equal(201, createdResult.StatusCode);
        }

        [Fact]
        public async void PostDoctor_Action_ReturnsConflict_OnDuplicate()
        {
            TreatmentDetails detail = new TreatmentDetails(1, 1, 1, 1, "Dummy");
            var result = await _controller.Post(detail);

            var createdResult = result as ConflictResult;

            Assert.NotNull(createdResult);
            Assert.Equal(409, createdResult.StatusCode);
        }

        [Fact]
        public async void PutDoctor_Action_ReturnsNoContent()
        {
            TreatmentDetails detail = new TreatmentDetails(1, 1, 2, 1, "Dummy");
            var result = await _controller.Put(1, detail);

            var noContentResult = result as NoContentResult;

            Assert.NotNull(noContentResult);
            Assert.Equal(204, noContentResult.StatusCode);
        }

        [Fact]
        public async void PutDoctor_Action_ReturnsBadRequest()
        {
            TreatmentDetails detail = new TreatmentDetails(1, 1, 2, 1, "Dummy");
            var result = await _controller.Put(2, detail);

            var badRequestResult = result as BadRequestResult;

            Assert.NotNull(badRequestResult);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public async void PutDoctor_Action_ReturnsNotFound()
        {
            TreatmentDetails detail = new TreatmentDetails(23, 1, 2, 1, "Dummy");
            var result = await _controller.Put(23, detail);

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
