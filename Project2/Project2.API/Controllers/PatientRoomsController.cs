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
    public class PatientRoomsController : ControllerBase
    {
        private readonly IPatientRoomRepository proomRepo;

        public PatientRoomsController(IPatientRoomRepository patientRoomRepository)
        {
            proomRepo = patientRoomRepository;
        }
        // GET: api/PatientRooms
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var patientRooms = await proomRepo.GetRoomsAsync();
            return Ok(patientRooms);
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
        public async Task<IActionResult> Put(int id, [FromBody] PatientRoom value)
        {
            if(id != value.PatientRoomId)
            {
                return BadRequest();
            }

            await proomRepo.Update(value);

            try
            {
                await proomRepo.SaveAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(proomRepo.GetRoomAsync(value.PatientRoomId).Result == null)
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

        // DELETE api/PatientRooms/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
