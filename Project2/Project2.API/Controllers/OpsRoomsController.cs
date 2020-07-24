using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        [Route("/AvailableRooms")]
        public async Task<IActionResult> GetAvailableRooms()
        {
            var opsRooms = await opsroomRepo.GetAvailableRoomsAsync();
            return Ok(opsRooms);
        }

        // GET api/OpsRooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OpsRoom>> Get(int id)
        {
            var room = await opsroomRepo.GetOpsRoomAsync(id);
            if (room is OpsRoom item)
            {
                return item;
            }

            return NotFound();
        }

        // POST api/OpsRooms
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/OpsRooms/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] OpsRoom value)
        {
            if(id != value.OpsRoomId)
            {
                return BadRequest();
            }

            await opsroomRepo.Update(value);

            try
            {
                await opsroomRepo.SaveAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(opsroomRepo.GetOpsRoomAsync(id).Result == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE api/OpsRooms/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
