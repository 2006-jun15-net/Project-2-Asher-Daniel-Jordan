using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Project2.Domain.Interface;
using Project2.API.Controllers;
using Project2.Domain.Model;
using System.Threading.Tasks;
using System.Linq;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace Project2.Test.Controllers
{
    public class IllnessControllerTests
    {
        private readonly Mock<IIllnessRepository> _mockRepo;
        private readonly IllnessesController _controller;

        public IllnessControllerTests()
        {
            _mockRepo = new Mock<IIllnessRepository>();
            _controller = new IllnessesController(_mockRepo.Object);

            List<Illness> illnesses = new List<Illness>()
            {
                new Illness(1, "TestIllness")
            };

            // get all illnesses
            _mockRepo.Setup(repo => repo.GetAllAsync())
                .Returns(async () => await Task.Run(() => illnesses));

            // get illness by id
            _mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .Returns(async (int id) => await Task.Run(() =>
                    illnesses.Where<Illness>(i => i.IllnessId == id).FirstOrDefault()));

            // create illness
            _mockRepo.Setup(repo => repo.CreateAsync(It.IsAny<Illness>()))
                .Returns(async (Illness illness) => await Task.Run(() => illnesses.Add(illness)));

            // updates illness
            _mockRepo.Setup(repo => repo.UpdateIllnessAsync(It.IsAny<Illness>()))
                .Returns(async (Illness illness) => await Task.Run(() => illnesses));

            // deletes illness
            _mockRepo.Setup(repo => repo.DeleteIllnessAsync(It.IsAny<Illness>()))
                .Returns(async (Illness illness) => await Task.Run(() => illnesses.Remove(illness)));
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
        public async void GetByIllnessId_Action_ReturnsOK_IfFound()
        {
            var result = await _controller.GetByIllnessId(1);

            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async void GetByIllnessId_Action_ReturnsNotFound()
        {
            var result = await _controller.GetByIllnessId(23);

            var notFoundResult = result as NotFoundResult;

            Assert.NotNull(notFoundResult);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async void PostIllness_Action_ReturnsCreatedAtAction()
        {
            Illness illness = new Illness(3, "Test");
            var result = await _controller.PostIllness(illness);

            var createdResult = result as CreatedAtActionResult;

            Assert.NotNull(createdResult);
            Assert.Equal(201, createdResult.StatusCode);
        }

        [Fact]
        public async void PostIllness_Action_ReturnsConflict_OnDuplicate()
        {
            Illness illness = new Illness(1, "Test");
            var result = await _controller.PostIllness(illness);

            var createdResult = result as ConflictResult;

            Assert.NotNull(createdResult);
            Assert.Equal(409, createdResult.StatusCode);
        }

        [Fact]
        public async void Put_Action_ReturnsOk()
        {
            Illness illness = new Illness(1, "Test");
            var result = await _controller.Put(1, illness);

            var okResult = result as OkResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async void Put_Action_ReturnsBadRequest()
        {
            Illness illness = new Illness(1, "Test");
            var result = await _controller.Put(2, illness);

            var badRequestResult = result as BadRequestResult;

            Assert.NotNull(badRequestResult);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public async void Put_Action_ReturnsNotFound()
        {
            Illness illness = new Illness(23, "Test");
            var result = await _controller.Put(23, illness);

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
