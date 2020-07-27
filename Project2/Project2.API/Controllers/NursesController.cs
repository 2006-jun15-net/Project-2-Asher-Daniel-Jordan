using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project2.Data.Repository;
using Project2.Domain.Interface;
using Project2.Domain.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NursesController : ControllerBase
    {
        private readonly INurseRepository nRepo;

        //Test

        public NursesController(INurseRepository nurseRepository)
        {
            nRepo = nurseRepository;

        }
        // GET: api/Nurses
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var nurses = await nRepo.GetNursesAsync();
            return Ok(nurses);
        }

        // GET api/Nurses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNurseById(int id)
        {
            return Ok(await nRepo.GetByNurseIdAsync(id));
        }

        // POST api/Nurses
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> PostDoctor([FromBody] Nurse nurse)
        {
            if (nRepo.GetNursesAsync().Result.Any(n => n.NurseId == nurse.NurseId))
            {
                return Conflict();
            }

            await nRepo.CreateNurseAsync(nurse);

            return CreatedAtAction(
                actionName: nameof(GetNurseById),
                routeValues: new { id = nurse.NurseId },
                value: nurse);
        }

        // PUT api/Nurses/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<IActionResult> Put(int id, [FromBody] Nurse nurse)
        {
            var existingNurse = await nRepo.GetByNurseIdAsync(id);

            if (existingNurse != null)
            {
                await nRepo.UpdateNurseAsync(nurse);
            }
            else
            {
                return NotFound();
            }

            return Ok();

        }

        // DELETE api/Nurses/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int id)
        {
            var existingNurse = await nRepo.GetByNurseIdAsync(id);

            if (existingNurse != null)
            {
                await nRepo.DeleteNurseAsync(existingNurse);
            }
            else
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
