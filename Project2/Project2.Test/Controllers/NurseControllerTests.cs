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
    public class NurseControllerTests
    {
        /*//initial setup
        private readonly Mock<INurseRepository> _mockRepo;
        private readonly NursesController _controller;

        public NurseControllerTests()
        {
            *//*_mockRepo = new Mock<INurseRepository>();
            _controller = new NursesController(_mockRepo.Object);

            List<Nurse> nurses = new List<Nurse>()
            {
                new Nurse(1, "Test", "Dummy")
            };

            // get all nurses
            _mockRepo.Setup(repo => repo.GetNursesAsync())
                .Returns(async () => await Task.Run(() => nurses));

            // get nurse by id
            _mockRepo.Setup(repo => repo.GetByNurseIdAsync(It.IsAny<int>()))
                .Returns(async (int id) => await Task.Run(() =>
                    nurses.Where<Nurse>(n => n.NurseId == id).FirstOrDefault()));

            // create nurse
            _mockRepo.Setup(repo => repo.CreateNurseAsync(It.IsAny<Nurse>()))
                .Returns(async (Nurse nurse) => await Task.Run(() => nurses.Add(nurse)));

            // updates nurse
            _mockRepo.Setup(repo => repo.UpdateNurseAsync(It.IsAny<Nurse>()))
                .Returns(async (Nurse nurse) => await Task.Run(() => nurses));

            // deletes a nurse
            _mockRepo.Setup(repo => repo.DeleteNurseAsync(It.IsAny<Nurse>()))
                .Returns(async (Nurse nurse) => await Task.Run(() => nurses.Remove(nurse)));
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
        public async void GetNurseById_Action_ReturnsOK_IfFound()
        {
            var result = await _controller.GetNurseById(1);

            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async void GetNurseById_Action_ReturnsNotFound()
        {
            var result = await _controller.GetNurseById(23);

            var notFoundResult = result as NotFoundResult;

            Assert.NotNull(notFoundResult);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async void Post_Action_ReturnsCreatedAtAction()
        {
            Nurse nurse = new Nurse(3, "Test", "Dummy");
            var result = await _controller.Post(nurse);

            var createdResult = result as CreatedAtActionResult;

            Assert.NotNull(createdResult);
            Assert.Equal(201, createdResult.StatusCode);
        }

        [Fact]
        public async void Post_Action_ReturnsConflict_OnDuplicate()
        {
            Nurse nurse = new Nurse(1, "Test", "Dummy");
            var result = await _controller.Post(nurse);

            var createdResult = result as ConflictResult;

            Assert.NotNull(createdResult);
            Assert.Equal(409, createdResult.StatusCode);
        }

        [Fact]
        public async void Put_Action_ReturnsOk()
        {
            Nurse nurse = new Nurse(1, "Test", "Dummy");
            var result = await _controller.Put(1, nurse);

            var okResult = result as OkResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async void Put_Action_ReturnsBadRequest()
        {
            Nurse nurse = new Nurse(1, "Test", "Dummy");
            var result = await _controller.Put(2, nurse);

            var badRequestResult = result as BadRequestResult;

            Assert.NotNull(badRequestResult);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public async void Put_Action_ReturnsNotFound()
        {
            Nurse nurse = new Nurse(23, "Test", "Dummy");
            var result = await _controller.Put(23, nurse);

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
