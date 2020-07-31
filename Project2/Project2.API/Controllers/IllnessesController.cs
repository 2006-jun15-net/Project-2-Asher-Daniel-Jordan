/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project2.Domain.Interface;
using Project2.Domain.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IllnessesController : ControllerBase
    {
        private readonly IIllnessRepository iRepo;

        public IllnessesController(IIllnessRepository illnessRepository)
        {
            iRepo = illnessRepository;
        }

        // GET: api/Illnesses
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var illnesses = await iRepo.GetAllAsync();
            return Ok(illnesses);
        }

        // GET api/Ilnesses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIllnessId(int id)
        {
            var result = await iRepo.GetByIdAsync(id);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST api/Illnesses
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> PostIllness([FromBody] Illness illness)
        {
            if (iRepo.GetAllAsync().Result.Any(i => i.IllnessId == illness.IllnessId))
            {
                return Conflict();
            }

            await iRepo.CreateAsync(illness);

            return CreatedAtAction(
                actionName: nameof(GetByIllnessId),
                routeValues: new { id = illness.IllnessId },
                value: illness);
        }

        // PUT api/Illnesses/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Put(int id, [FromBody] Illness illness)
        {
            if(id != illness.IllnessId)
            {
                return BadRequest();
            }

            var existingIllness = await iRepo.GetByIdAsync(id);

            if (existingIllness != null)
            {
                await iRepo.UpdateIllnessAsync(illness);
            }
            else
            {
                return NotFound();
            }

            return Ok();

        }

        // DELETE api/Illnesses/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int id)
        {
            var existingIllness = await iRepo.GetByIdAsync(id);

            if (existingIllness != null)
            {
                await iRepo.DeleteIllnessAsync(existingIllness);
            }
            else
            {
                return NotFound();
            }

            return Ok();
        }
    }
}*/
