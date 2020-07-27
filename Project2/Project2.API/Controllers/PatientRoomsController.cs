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
        public async Task<IActionResult> GetRoomById(int id)
        {
            return Ok(await proomRepo.GetRoomAsync(id));
            
        }

        // POST api/PatientRooms
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] PatientRoom pRoom)
        {
            if (proomRepo.GetRoomsAsync().Result.Any(pr => pr.PatientRoomId == pRoom.PatientRoomId))
            {
                return Conflict();
            }

            await proomRepo.CreateAsync(pRoom);

            return CreatedAtAction(
                actionName: nameof(GetRoomById),
                routeValues: new { id = pRoom.PatientRoomId },
                value: pRoom);
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int id)
        {
            var existingPRoom = await proomRepo.GetRoomAsync(id);

            if (existingPRoom != null)
            {
                await proomRepo.DeleteAsync(existingPRoom);
            }
            else
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
