using System.Collections.Generic;
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
    public class OpsRoomControllerTests
    {
        //initial setup
        private readonly Mock<IOpsRoomRepository> _mockRepo;
        private readonly OpsRoomsController _controller;

        //initial setup
        public OpsRoomControllerTests()
        {
            _mockRepo = new Mock<IOpsRoomRepository>();
            _controller = new OpsRoomsController(_mockRepo.Object);

            List<OpsRoom> opsRooms = new List<OpsRoom>()
            {
                new OpsRoom(1, true),
                new OpsRoom(2, false)
            };

            // get all opsRooms
            _mockRepo.Setup(repo => repo.GetAllRoomsAsync())
                .Returns(async () => await Task.Run(() => opsRooms));

            // get all available opsRooms
            _mockRepo.Setup(repo => repo.GetAvailableRoomsAsync())
                .Returns(async () => await Task.Run(() => opsRooms.Where<OpsRoom>(room => room.Available == true)));

            // get opsRoom by id
            _mockRepo.Setup(repo => repo.GetOpsRoomAsync(It.IsAny<int>()))
                .Returns(async (int id) => await Task.Run(() =>
                    opsRooms.Where<OpsRoom>(o => o.OpsRoomId == id).FirstOrDefault()));

            /*// create opsRoom
            _mockRepo.Setup(repo => repo.CreateOpsRoomAsync(It.IsAny<OpsRoom>()))
                .Returns(async (OpsRoom room) => await Task.Run(() => opsRooms.Add(room)));*/

            // updates opsRoom
            _mockRepo.Setup(repo => repo.Update(It.IsAny<OpsRoom>()))
                .Returns(async (OpsRoom room) => await Task.Run(() => opsRooms));

            /*// deletes a opsRoom
            _mockRepo.Setup(repo => repo.DeleteAsync(It.IsAny<OpsRoom>()))
                .Returns(async (OpsRoom room) => await Task.Run(() => opsRooms.Remove(room)));*/
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
        public async void GetAvailableRooms_ActionExecutes_ReturnsOKStatus()
        {
            var result = await _controller.GetAvailableRooms();

            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        /*[Fact]
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
        }*/

        /*[Fact]
        public async void Post_Action_ReturnsCreatedAtAction()
        {
            OpsRoom room = new OpsRoom(3, true);
            var result = await _controller.Post(room);

            var createdResult = result as CreatedAtActionResult;

            Assert.NotNull(createdResult);
            Assert.Equal(201, createdResult.StatusCode);
        }

        [Fact]
        public async void Post_Action_ReturnsConflict_OnDuplicate()
        {
            OpsRoom room = new OpsRoom(1, true);
            var result = await _controller.Post(room);

            var createdResult = result as ConflictResult;

            Assert.NotNull(createdResult);
            Assert.Equal(409, createdResult.StatusCode);
        }*/

        [Fact]
        public async void Put_Action_ReturnsOk()
        {
            OpsRoom room = new OpsRoom(1, false);
            var result = await _controller.Put(1, room);

            var okResult = result as NoContentResult;

            Assert.NotNull(okResult);
            Assert.Equal(204, okResult.StatusCode);
        }

        /*[Fact]
        public async void Put_Action_ReturnsBadRequest()
        {
            OpsRoom room = new OpsRoom(1, false);
            var result = await _controller.Put(2, room);

            var badRequestResult = result as BadRequestResult;

            Assert.NotNull(badRequestResult);
            Assert.Equal(400, badRequestResult.StatusCode);
        }*/

        [Fact]
        public async void Put_Action_ReturnsNotFound()
        {
            OpsRoom room = new OpsRoom(23, false);
            var result = await _controller.Put(23, room);

            var notFoundResult = result as NotFoundResult;

            Assert.NotNull(notFoundResult);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        /*[Fact]
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
