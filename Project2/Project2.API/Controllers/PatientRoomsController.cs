using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientRoomsController : ControllerBase
    {
        // GET: api/PatientRooms
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/PatientRooms/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/PatientRooms
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/PatientRooms/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/PatientRooms/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
