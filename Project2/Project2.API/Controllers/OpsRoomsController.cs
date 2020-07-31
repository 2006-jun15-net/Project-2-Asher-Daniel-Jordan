using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project2.Domain.Interface;
using Project2.Domain.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpsRoomsController : ControllerBase
    {
        private readonly IOpsRoomRepository opsroomRepo;

        public OpsRoomsController(IOpsRoomRepository orr)
        {
            opsroomRepo = orr;

        }

        // GET: api/OpsRooms
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var opsRooms = await opsroomRepo.GetAllRoomsAsync();
            return Ok(opsRooms);
        }

        // GET: api/OpsRooms/AvailableRooms
        [HttpGet]
        [Route("AvailableRooms")]
        public async Task<IActionResult> GetAvailableRooms()
        {
            var opsRooms = await opsroomRepo.GetAvailableRoomsAsync();
            return Ok(opsRooms);
        }

        // GET api/OpsRooms/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var room = await opsroomRepo.GetOpsRoomAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            return Ok(room);
        }

        // POST api/OpsRooms
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] OpsRoom opsRoom)
        {
            if (opsroomRepo.GetAllRoomsAsync().Result.Any(or => or.OpsRoomId == opsRoom.OpsRoomId))
            {
                return Conflict();
            }

            await opsroomRepo.CreateOpsRoomAsync(opsRoom);

            return CreatedAtAction(
                actionName: nameof(Get),
                routeValues: new { id = opsRoom.OpsRoomId},
                value: opsRoom);
        }

        // PUT api/OpsRooms/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Put(int id, [FromBody] OpsRoom value)
        {
            if(id != value.OpsRoomId)
            {
                return BadRequest();
            }

            
            if (opsroomRepo.GetOpsRoomAsync(id).Result == null)
            {
                return NotFound();
            }
            else
            {
                await opsroomRepo.Update(value);
            }

            return NoContent();
        }

        // DELETE api/OpsRooms/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int id)
        {
            var existingOpsRoom = await opsroomRepo.GetOpsRoomAsync(id);

            if (existingOpsRoom != null)
            {
                await opsroomRepo.DeleteAsync(existingOpsRoom);
            }
            else
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
