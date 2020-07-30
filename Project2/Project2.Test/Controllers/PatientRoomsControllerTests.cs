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
    public class PatientRoomsControllerTests
    {
        //initial setup
        private readonly Mock<IPatientRoomRepository> _mockRepo;
        private readonly PatientRoomsController _controller;


        //initial setup
        public PatientRoomsControllerTests()
        {
            _mockRepo = new Mock<IPatientRoomRepository>();
            _controller = new PatientRoomsController(_mockRepo.Object);

            List<PatientRoom> rooms = new List<PatientRoom>()
            {
                new PatientRoom(1, true)
            };

            // get all patient rooms
            _mockRepo.Setup(repo => repo.GetRoomsAsync())
                .Returns(async () => await Task.Run(() => rooms));

            // get patient room by id
            _mockRepo.Setup(repo => repo.GetRoomAsync(It.IsAny<int>()))
                .Returns(async (int id) => await Task.Run(() =>
                    rooms.Where<PatientRoom>(r => r.PatientRoomId == id).FirstOrDefault()));

            // create patient room
            _mockRepo.Setup(repo => repo.CreateAsync(It.IsAny<PatientRoom>()))
                .Returns(async (PatientRoom room) => await Task.Run(() => rooms.Add(room)));

            // updates patient room
            _mockRepo.Setup(repo => repo.Update(It.IsAny<PatientRoom>()))
                .Returns(async (PatientRoom room) => await Task.Run(() => rooms));

            // deletes a patient room
            _mockRepo.Setup(repo => repo.DeleteAsync(It.IsAny<PatientRoom>()))
                .Returns(async (PatientRoom room) => await Task.Run(() => rooms.Remove(room)));
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
        public async void GetRoomById_Action_ReturnsOK_IfFound()
        {
            var result = await _controller.GetRoomById(1);

            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async void GetRoomById_Action_ReturnsNotFound()
        {
            var result = await _controller.GetRoomById(23);

            var notFoundResult = result as NotFoundResult;

            Assert.NotNull(notFoundResult);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async void Post_Action_ReturnsCreatedAtAction()
        {
            PatientRoom room = new PatientRoom(3, true);
            var result = await _controller.Post(room);

            var createdResult = result as CreatedAtActionResult;

            Assert.NotNull(createdResult);
            Assert.Equal(201, createdResult.StatusCode);
        }

        [Fact]
        public async void Post_Action_ReturnsConflict_OnDuplicate()
        {
            PatientRoom room = new PatientRoom(1, true);
            var result = await _controller.Post(room);

            var createdResult = result as ConflictResult;

            Assert.NotNull(createdResult);
            Assert.Equal(409, createdResult.StatusCode);
        }

        [Fact]
        public async void Put_Action_ReturnsOk()
        {
            PatientRoom room = new PatientRoom(1, false);
            var result = await _controller.Put(1, room);

            var okResult = result as NoContentResult;

            Assert.NotNull(okResult);
            Assert.Equal(204, okResult.StatusCode);
        }

        [Fact]
        public async void Put_Action_ReturnsBadRequest()
        {
            PatientRoom room = new PatientRoom(1, false);
            var result = await _controller.Put(2, room);

            var badRequestResult = result as BadRequestResult;

            Assert.NotNull(badRequestResult);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public async void Put_Action_ReturnsNotFound()
        {
            PatientRoom room = new PatientRoom(23, false);
            var result = await _controller.Put(23, room);

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
