using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project2.Domain.Interface;

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
        public IActionResult Get()
        {
            return Ok();
        }

        // GET api/OpsRooms/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/OpsRooms
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/OpsRooms/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/OpsRooms/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
